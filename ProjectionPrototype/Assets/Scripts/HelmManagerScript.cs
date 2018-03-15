using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmManagerScript : MonoBehaviour {
	public AudioHelm.HelmController Drone; //ref to helm controller to play synth
	public AudioHelm.HelmController Bass; //ref to helm controller to play synth
	public AudioHelm.HelmController Arp;
	public AudioHelm.HelmController AirDrone;
	public AudioHelm.HelmController Lead;

	public AudioHelm.Sequencer KickSeq;
	public AudioHelm.Sequencer SnareSeq;
	public AudioHelm.Sequencer HihatSeq;
	public AudioHelm.Sequencer ArpSeq;
	public AudioHelm.Sequencer AirDroneSeq;
	public AudioHelm.Sequencer LeadSeq;

	public int KickNote = 60;
	public int SnareNote = 50;
	public int HihatNote = 67;

	public int BassNote = 48;

	public int DroneNote1= 48; //Is the midi note 0 to 127
	public int DroneNote2= 55;
	public int DroneNote3= 57;
	public int DroneNote4= 62;

	public int ArpNote1= 76;
	public int ArpNote2= 81;
	public int ArpNote3= 83;
	public int ArpNote4= 86;

	public int AirDroneNote1 = 95;
	public int AirDroneNote2 = 98;
	public int AirDroneNote3 = 100;

	public int LeadNote1 = 93;
	public int LeadNote2 = 98;
	public int LeadNote3 = 91;
	public int LeadNote4 = 88;

	//Drone	
	public float DroneY = 0.5f; // variable example to change a synth parameter
	public float DroneX = 0.2f; // variable example to change a synth parameter
	public float DronefilterBlend = 1.0f; // variable example to change a synth parameter
	//AirDrone	
	public float AirDroneX = 1.0f; // variable example to change a synth parameter
	public float AirDroneY = 0.0f; // variable example to change a synth parameter
	public float AirDronefilterBlend = 1.0f;
	public float AirDroneDelayTempo = 0.5f;
	public float AirDroneNoise = 0.0f;
	//Arp
	public float ArpStutter = 0.0f; // variable example to change a synth parameter
	public float ArpStutterResample = 1.0f; // variable example to change a synth parameter
	public float ArpFeedback = 0.5f;
	//Bass
	public float BassSubShuffle = 0.0f;
	public float BassOSC2tune = 0;
	public float BassFeedbackTune = 0.5f;
	public float BassFeedbackAmount = 0.5f;

	public float time = 1.5f;

	// Use this for initialization
	void Start () {
		//string[] notes = { "C-2", "Db-2", "D-2", "Eb-2", "E-2", "F-2", "Gb-2", "G-2", "Ab-2","A-2","Bb-2","B-2","C-1", "Db-1", "D-1", "Eb-1", "E-1", "F-1", "Gb-1", "G-1", "Ab-1","A-1","Bb-1","B-1","C0", "Db0", "D0", "Eb0", "E0", "F0", "Gb0", "G0", "Ab0","A0","Bb0","B0","C1", "Db1", "D1", "Eb1", "E1", "F1", "Gb1", "G1", "Ab1","A1","Bb1","B1","C2", "Db2", "D2", "Eb2", "E2", "F2", "Gb2", "G2", "Ab2","A2","Bb2","B2","C3", "Db3", "D3", "Eb3", "E3", "F3", "Gb3", "G3", "Ab3","A3","Bb3","B3","C4", "Db4", "D4", "Eb4", "E4", "F4", "Gb4", "G4", "Ab4","A4","Bb4","B4","C5", "Db5", "D5", "Eb5", "E5", "F5", "Gb5", "G5", "Ab5","A5","Bb5","B5","C6", "Db6", "D6", "Eb6", "E6", "F6", "Gb6", "G6", "Ab6","A6","Bb6","B6"};
		Invoke("DroneEnable", time);
		Invoke("AirDroneEnable", time*4);
		Invoke("BassEnable", time*8);
		Invoke("DrumsEnable", time*12);
		Invoke("ArpEnable", time*16);
		Invoke ("LeadEnable", time * 20);
	}
	void DroneEnable()
	{
		Drone.NoteOn (DroneNote1,1.0f);
		Drone.NoteOn (DroneNote2,1.0f);
		Drone.NoteOn (DroneNote3,1.0f); 
		Drone.NoteOn (DroneNote4,1.0f);
	}
	void AirDroneEnable()
	{
		AirDroneSeq.Clear ();
		AirDroneSeq.AddNote (AirDroneNote1, 0, 17);
		AirDroneSeq.AddNote (AirDroneNote2, 0, 17);
		AirDroneSeq.AddNote (AirDroneNote3, 0, 17);
	}
	void DrumsEnable()
	{
		KickSeq.Clear ();
		SnareSeq.Clear ();
		HihatSeq.Clear ();

		KickSeq.AddNote (KickNote,0,1);
		KickSeq.AddNote (KickNote,8,9);

		SnareSeq.AddNote (SnareNote,4,5);
		SnareSeq.AddNote (SnareNote,10,11);
		SnareSeq.AddNote (SnareNote,14,15);
		SnareSeq.AddNote (SnareNote,15,16,0.5f);

		HihatSeq.AddNote (HihatNote, 1, 2, 0.5f);
		HihatSeq.AddNote (HihatNote, 2, 3);
		HihatSeq.AddNote (HihatNote, 6, 7, 0.7f);
		HihatSeq.AddNote (HihatNote, 8, 9);
		HihatSeq.AddNote (HihatNote, 12, 13,0.7f);
		HihatSeq.AddNote (HihatNote, 14, 15);

	}
	void BassEnable()
	{
		Bass.NoteOn (BassNote);
		Bass.NoteOn (BassNote+12);
	}
	void ArpEnable()
	{
		ArpSeq.Clear ();
		ArpSeq.AddNote (ArpNote1, 0, 17);
		ArpSeq.AddNote (ArpNote2, 0, 17);
		ArpSeq.AddNote (ArpNote3, 0, 17);
		ArpSeq.AddNote (ArpNote4, 0, 17);
	}
	void LeadEnable()
	{
		LeadSeq.Clear ();
		LeadSeq.AddNote (LeadNote1, 0, 1);
		LeadSeq.AddNote (LeadNote2, 5, 6);
		LeadSeq.AddNote (LeadNote3, 8, 9);
		LeadSeq.AddNote (LeadNote4, 13, 14);
	}
	// Update is called once per frame
	void Update () {
		//Drone
		Drone.SetParameterPercent(AudioHelm.Param.kFormantY, DroneY);
		Drone.SetParameterPercent(AudioHelm.Param.kFormantX, DroneX);
		Drone.SetParameterPercent(AudioHelm.Param.kFilterBlend, DronefilterBlend);
		//Arp
		Arp.SetParameterPercent(AudioHelm.Param.kStutterTempo, ArpStutter);
		Arp.SetParameterPercent(AudioHelm.Param.kStutterResampleTempo, ArpStutterResample);
		Arp.SetParameterPercent (AudioHelm.Param.kOscFeedbackAmount, ArpFeedback);
		//AirDrone
		AirDrone.SetParameterPercent(AudioHelm.Param.kFormantX, AirDroneX);
		AirDrone.SetParameterPercent(AudioHelm.Param.kFormantY, AirDroneY);
		AirDrone.SetParameterPercent(AudioHelm.Param.kFilterBlend, AirDronefilterBlend);
		AirDrone.SetParameterPercent (AudioHelm.Param.kDelayTempo, AirDroneDelayTempo);
		AirDrone.SetParameterPercent (AudioHelm.Param.kDelaySync, AirDroneDelayTempo-0.2f);
		AirDrone.SetParameterPercent (AudioHelm.Param.kNoiseVolume, AirDroneNoise);
		//Bass
		Bass.SetParameterPercent(AudioHelm.Param.kSubShuffle, BassSubShuffle);
		Bass.SetParameterValue(AudioHelm.Param.kOsc2Tune, BassOSC2tune);
		Bass.SetParameterPercent(AudioHelm.Param.kOscFeedbackAmount, BassFeedbackTune);
		Bass.SetParameterPercent(AudioHelm.Param.kOscFeedbackTune, BassFeedbackAmount);


}
}
