using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmManagerScript : MonoBehaviour {
	public AudioHelm.HelmController Drone; //ref to helm controller to play synth
	public AudioHelm.HelmController Bass; //ref to helm controller to play synth
	public AudioHelm.HelmController Arp;
	public AudioHelm.HelmController AirDrone;

	public AudioHelm.Sequencer KickSeq;
	public AudioHelm.Sequencer SnareSeq;
	public AudioHelm.Sequencer HihatSeq;
	public AudioHelm.Sequencer ArpSeq;
	public AudioHelm.Sequencer AirDroneSeq;

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

	//Drone	
	public float formantY = 0.0f; // variable example to change a synth parameter
	public float formantX = 0.0f; // variable example to change a synth parameter
	public float filterBlend = 0.0f; // variable example to change a synth parameter
	//Arp
	public float stutterTempo = 0.0f; // variable example to change a synth parameter
	public float stutterResampleTempo = 1.0f; // variable example to change a synth parameter

	// Use this for initialization
	void Start () {
		DrumsTriggerOnEnter ();
		BassTriggerOnEnter ();
		DroneTriggerOnEnter ();
		ArpTriggerOnEnter ();
		AirDroneTriggerOnEnter ();
	}
	// Update is called once per frame
	void Update () {
		//Drone
		Drone.SetParameterPercent(AudioHelm.Param.kFormantY, formantY);
		Drone.SetParameterPercent(AudioHelm.Param.kFormantX, formantX);
		Drone.SetParameterPercent(AudioHelm.Param.kFilterBlend, filterBlend);
		//Arp
		Arp.SetParameterValue(AudioHelm.Param.kStutterTempo, stutterTempo);
		Arp.SetParameterValue(AudioHelm.Param.kStutterResampleTempo, stutterResampleTempo);
	}

	void DrumsTriggerOnEnter()
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
	void BassTriggerOnEnter()
	{
		Bass.AllNotesOff ();
		Bass.NoteOn (BassNote);
	}

	void DroneTriggerOnEnter()
	{
		Drone.AllNotesOff ();
		Drone.NoteOn (DroneNote1);
		Drone.NoteOn (DroneNote2);
		Drone.NoteOn (DroneNote3); 
		Drone.NoteOn (DroneNote4);
	}

	void ArpTriggerOnEnter()
	{
		ArpSeq.Clear ();
		ArpSeq.AddNote (ArpNote1, 0, 17);
		ArpSeq.AddNote (ArpNote2, 0, 17);
		ArpSeq.AddNote (ArpNote3, 0, 17);
		ArpSeq.AddNote (ArpNote4, 0, 17);
	}

	void AirDroneTriggerOnEnter()
	{
		AirDroneSeq.Clear ();
		AirDroneSeq.AddNote (AirDroneNote1, 0, 17);
		AirDroneSeq.AddNote (AirDroneNote2, 0, 17);
		AirDroneSeq.AddNote (AirDroneNote3, 0, 17);
	}
}
