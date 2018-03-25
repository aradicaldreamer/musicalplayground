using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour {

	public AudioClip aud;
	public AudioSource source;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider col) {
		source.PlayOneShot(aud);
	}
}
