using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSelector : MonoBehaviour {
	public AudioHelm.HelmController Drone; //ref to helm controller to play synth
	public AudioHelm.HelmController Bass; //ref to helm controller to play synth
	public AudioHelm.HelmController Arp;

	public int DroneNote1= 48; //Is the midi note 0 to 127
	public int DroneNote2= 55;
	public int DroneNote3= 57;
	public int DroneNote4= 62;
	public int BassNote = 48;
	//Drone	
	public float formantY = 0.0f; // variable example to change a synth parameter
	public float formantX = 0.0f; // variable example to change a synth parameter
	public float filterBlend = 0.0f; // variable example to change a synth parameter
	//Arp
	public float stutterTempo = 0.0f; // variable example to change a synth parameter
	public float stutterResampleTempo = 1.0f; // variable example to change a synth parameter

	// Use this for initialization
	void Start () {
		Drone.NoteOn(DroneNote1, 1.0f);
		Drone.NoteOn(DroneNote2, 1.0f);
		Drone.NoteOn(DroneNote3, 1.0f); 
		Drone.NoteOn(DroneNote4, 1.0f);

		Bass.NoteOn(BassNote, 1.0f);
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
}
