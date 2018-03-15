using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmManagerScript : MonoBehaviour {
	public AudioHelm.HelmController Drone; //ref to helm controller to play synth
	public AudioHelm.HelmController Bass; //ref to helm controller to play synth
	public AudioHelm.HelmController Arp;
	public AudioHelm.HelmController AirDrone;
	public AudioHelm.HelmController Lead;
	public AudioHelm.HelmController Snare;
	public AudioHelm.HelmController Hihat;
	public AudioHelm.HelmController CymbalHit;

	public AudioHelm.Sequencer KickSeq;
	public AudioHelm.Sequencer SnareSeq;
	public AudioHelm.Sequencer HihatSeq;
	public AudioHelm.Sequencer ArpSeq;
	public AudioHelm.Sequencer AirDroneSeq;
	public AudioHelm.Sequencer LeadSeq;

	public int Chordi = 1;
	public int Chordii = 0;
	public int Chordiv = 0;
	public int Chordv = 0;
	public int Chordvi = 0;
	public int Chordbvii = 0;
	public int Chordvii = 0;

	//Is the midi note 0 to 127 = to music note
	private int C2= 48;
	private int D2 = 50;
	private int F2 = 53;
	private int G2= 55;
	private int A2= 57;

	private int C3 = 60;
	private int D3= 62;
	private int E3 = 64;
	private int G3 = 67;

	private int C4 = 72;
	private int E4= 76;
	private int F4= 77;
	private int G4= 79;
	private int A4= 81;
	private int B4= 83;

	private int D5= 86;
	private int E5 = 88;
	private int F5 = 89;
	private int G5 = 91;
	private int A5= 93;
	private int B5= 95;
	private int C5= 96;

	private int D6= 98;
	private int E6 = 100;
	private int F6= 101;

	//Drone	Parameters
	public float DroneY = 0.5f; // variable example to change a synth parameter
	public float DroneX = 0.2f; // variable example to change a synth parameter
	public float DronefilterBlend = 1.0f; // variable example to change a synth parameter
	//AirDrone Parameters	
	public float AirDroneX = 1.0f; // variable example to change a synth parameter
	public float AirDroneY = 0.0f; // variable example to change a synth parameter
	public float AirDronefilterBlend = 1.0f;
	public float AirDroneDelayTempo = 0.5f;
	public float AirDroneNoise = 0.0f;
	//Arp Parameters
	public float ArpStutter = 0.0f; // variable example to change a synth parameter
	public float ArpStutterResample = 1.0f; // variable example to change a synth parameter
	public float ArpFeedback = 0.5f;
	//Bass Parameters
	public float BassSubShuffle = 0.0f;
	public float BassOSC2tune = 0;
	public float BassFeedbackTune = 0.5f;
	public float BassFeedbackAmount = 0.5f;
	//Lead Parameters
	public float LeadDelayMix = 0.0f;
	public float LeadDelayFeedback = 0.5f;
	public float LeadDelaySync = 0.5f;
	public float LeadSustain = 0.2f;
	//Snare Parameters
	public float SnareDelayMix = 0.0f;
	public float SnareDelayFeedback = 0.5f;
	public float SnareDelaySync = 0.4f;
	//Hihat Parameters
	public float HihatDelayMix = 0.0f;
	public float HihatDelayFeedback = 0.5f;
	public float HihatDelaySync = 0.4f;
	//Timer to invoke sequencers/synths on startup
	public float time = 1.5f;

	// Use this for initialization
	void Start () {
		//string[] notes = { "C-2", "Db-2", "D-2", "Eb-2", "E-2", "F-2", "Gb-2", "G-2", "Ab-2","A-2","Bb-2","B-2","C-1", "Db-1", "D-1", "Eb-1", "E-1", "F-1", "Gb-1", "G-1", "Ab-1","A-1","Bb-1","B-1","C0", "Db0", "D0", "Eb0", "E0", "F0", "Gb0", "G0", "Ab0","A0","Bb0","B0","C1", "Db1", "D1", "Eb1", "E1", "F1", "Gb1", "G1", "Ab1","A1","Bb1","B1","C2", "Db2", "D2", "Eb2", "E2", "F2", "Gb2", "G2", "Ab2","A2","Bb2","B2","C3", "Db3", "D3", "Eb3", "E3", "F3", "Gb3", "G3", "Ab3","A3","Bb3","B3","C4", "Db4", "D4", "Eb4", "E4", "F4", "Gb4", "G4", "Ab4","A4","Bb4","B4","C5", "Db5", "D5", "Eb5", "E5", "F5", "Gb5", "G5", "Ab5","A5","Bb5","B5","C6", "Db6", "D6", "Eb6", "E6", "F6", "Gb6", "G6", "Ab6","A6","Bb6","B6"};
		Invoke("DroneEnable", time);
		Invoke("AirDroneEnable", time*4);
		Invoke("BassEnable", time*8);
		Invoke("DrumsEnable", time*12);
		Invoke("ArpEnable", time*16);
		Invoke ("LeadEnable", time*20);
		Invoke ("CymbalHitEnable", time*12);
	}
	void DroneEnable()
	{
		if (Chordi == 1) {
			//Drone Chordi
			Drone.AllNotesOff();
			Drone.NoteOn (C2, 1.0f);
			Drone.NoteOn (G2, 1.0f);
			Drone.NoteOn (A2, 1.0f); 
			Drone.NoteOn (D3, 1.0f);
		}
		if (Chordii == 1) {
			//Drone Chordii
			Drone.AllNotesOff();
			Drone.NoteOn (D2, 1.0f);
			Drone.NoteOn (F2, 1.0f);
			Drone.NoteOn (C3, 1.0f); 
			Drone.NoteOn (E3, 1.0f);
		}
	}
	void AirDroneEnable()
	{
		if (Chordi == 1) {
		//AirDrone Chordi
		AirDroneSeq.Clear ();
		AirDroneSeq.AddNote (B5, 0, 17);
		AirDroneSeq.AddNote (D6, 0, 17);
		AirDroneSeq.AddNote (E6, 0, 17);
		}
		if (Chordii == 1) {
			//AirDrone Chordii
			AirDroneSeq.Clear ();
			AirDroneSeq.AddNote (C5, 0, 17);
			AirDroneSeq.AddNote (E6, 0, 17);
			AirDroneSeq.AddNote (F6, 0, 17);
		}
	}
	void DrumsEnable()
	{
		KickSeq.Clear ();
		SnareSeq.Clear ();
		HihatSeq.Clear ();

		KickSeq.AddNote (C3,0,1);
		KickSeq.AddNote (C3,8,9);

		SnareSeq.AddNote (D2,4,5);
		SnareSeq.AddNote (D2,10,11);
		SnareSeq.AddNote (D2,14,15);
		SnareSeq.AddNote (D2,15,16,0.5f);

		HihatSeq.AddNote (G3, 1, 2, 0.5f);
		HihatSeq.AddNote (G3, 2, 3);
		HihatSeq.AddNote (G3, 6, 7, 0.7f);
		HihatSeq.AddNote (G3, 8, 9);
		HihatSeq.AddNote (G3, 12, 13,0.7f);
		HihatSeq.AddNote (G3, 14, 15);

	}
	void BassEnable()
	{
		if (Chordi == 1) {
			//Bass Chordi
			Bass.AllNotesOff();
			Bass.NoteOn (C2);
			Bass.NoteOn (C3);
		}
		if (Chordii == 1) {
			//Bass Chordii
			Bass.AllNotesOff();
			Bass.NoteOn (D2);
			Bass.NoteOn (D3);			
		}
	}
	void ArpEnable()
	{
		if (Chordi == 1) {
			//Arp Chordi
			ArpSeq.Clear ();
			ArpSeq.AddNote (E4, 0, 17);
			ArpSeq.AddNote (A4, 0, 17);
			ArpSeq.AddNote (B4, 0, 17);
			ArpSeq.AddNote (D5, 0, 17);
		}
		if (Chordii == 1) {
			//Arp Chordii
			ArpSeq.Clear ();
			ArpSeq.AddNote (F4, 0, 17);
			ArpSeq.AddNote (G4, 0, 17);
			ArpSeq.AddNote (C4, 0, 17);
			ArpSeq.AddNote (E5, 0, 17);
		}
	}
	void LeadEnable()
	{
		if (Chordi == 1) {
		//Lead Chordi
		LeadSeq.Clear ();
		LeadSeq.AddNote (A5, 0, 1);
		LeadSeq.AddNote (D6, 5, 6);
		LeadSeq.AddNote (G5, 8, 9);
		LeadSeq.AddNote (E5, 13, 14);
		}
		if (Chordii == 1) {
			//Lead Chordii
			LeadSeq.Clear ();
			LeadSeq.AddNote (G5, 0, 1);
			LeadSeq.AddNote (E6, 5, 6);
			LeadSeq.AddNote (A5, 8, 9);
			LeadSeq.AddNote (F5, 13, 14);
		}
	}
	void CymbalHitEnable()
	{
		CymbalHit.NoteOn (C4, 1.0f, 2.0f);
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
		//Lead
		Lead.SetParameterPercent(AudioHelm.Param.kDelayDryWet, LeadDelayMix);
		Lead.SetParameterPercent(AudioHelm.Param.kDelayFeedback, LeadDelayFeedback);
		Lead.SetParameterPercent(AudioHelm.Param.kDelaySync, LeadDelaySync);
		Lead.SetParameterPercent(AudioHelm.Param.kAmplitudeSustain, LeadSustain);
		//Snare
		Snare.SetParameterPercent(AudioHelm.Param.kDelayDryWet, SnareDelayMix);
		Snare.SetParameterPercent(AudioHelm.Param.kDelayFeedback, SnareDelayFeedback);
		Snare.SetParameterPercent(AudioHelm.Param.kDelaySync, SnareDelaySync);
		//Hihat
		Hihat.SetParameterPercent(AudioHelm.Param.kDelayDryWet, HihatDelayMix);
		Hihat.SetParameterPercent(AudioHelm.Param.kDelayFeedback, HihatDelayFeedback);
		Hihat.SetParameterPercent(AudioHelm.Param.kDelaySync, HihatDelaySync);
}
}
