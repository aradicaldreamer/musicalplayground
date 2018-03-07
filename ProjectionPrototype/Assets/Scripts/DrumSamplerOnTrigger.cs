using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSamplerOnTrigger : MonoBehaviour {
	public AudioHelm.Sampler sampler; //ref to helm controller to play sampler
	public int note = 48; //Is the midi note 0 to 127 (should trigger kick on C3)

	void OnCollisionEnter(Collision collision)
	{
		// int note, float velocity (how hard note is hit), float length (how long the note decays)
		sampler.NoteOn(note); 
	}
}
