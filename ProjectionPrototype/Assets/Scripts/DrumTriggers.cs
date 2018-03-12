using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumTriggers : MonoBehaviour {
	public AudioHelm.Sequencer sequencer;
	public int note = 48;
	// Use this for initialization
	void Start () {
		
	}
	void OnCollisionEnter(Collision collision)
	{
		int seqPos = (int)sequencer.GetSequencerPosition ();
		bool eraseNote = sequencer.NoteExistsInRange (note, seqPos, seqPos + 1);
		if (eraseNote == false) {
			sequencer.AddNote (note, seqPos, seqPos + 1);
		} else if (eraseNote == true) {
			sequencer.RemoveNotesInRange(note, seqPos, seqPos + 1);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
