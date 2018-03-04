using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoTrigger : MonoBehaviour 
{
	public AudioSource PianoSource;
	public AudioClip NoteC;
	public AudioClip NoteDb;
	public AudioClip NoteE;
	public AudioClip NoteEb;
	public AudioClip NoteF;
	public AudioClip NoteGb;
	public AudioClip NoteG;
	public AudioClip NoteAb;
	public AudioClip NoteA;
	public AudioClip NoteBb;
	public AudioClip NoteB;


	private bool hasPlayedAudio;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player") && hasPlayedAudio == false) {
			PianoSource.PlayOneShot (NoteC);
			PianoSource.PlayOneShot (NoteF);
			hasPlayedAudio = false;

		}
	}
}
