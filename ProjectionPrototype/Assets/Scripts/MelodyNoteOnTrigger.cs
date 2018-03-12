using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyNoteOnTrigger : MonoBehaviour {

	public AudioHelm.HelmController helmController; //ref to helm controller to play synth
	public int note = 60; //Is the midi note 0 to 127
	void OnCollisionEnter(Collision collision)
	{
		// int note, float velocity (how hard note is hit), float length (how long the note decays)
		helmController.NoteOn(note, 1.0f, 0.5f); 
	}
}
