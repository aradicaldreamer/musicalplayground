using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthNoteOnTrigger : MonoBehaviour {

	public AudioHelm.HelmController helmController; //ref to helm controller to play synth
	public int note = 60; //Is the midi note 0 to 127
	public float subVolume = 0.0f; // variable example to change a synth parameter

	void OnCollisionEnter(Collision collision)
	{
		// Example of how to control a parameter from the standalone synth
		helmController.SetParameterPercent(AudioHelm.Param.kSubVolume, subVolume);

		// int note, float velocity (how hard note is hit), float length (how long the note decays)
		helmController.NoteOn(note, 1.0f, 1.25f); 
	}
}
