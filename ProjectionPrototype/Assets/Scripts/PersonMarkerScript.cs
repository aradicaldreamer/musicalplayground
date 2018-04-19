using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMarkerScript : MonoBehaviour {

	private ParticleSystem particleSystem;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setColour(Gradient color)
	{
		if (particleSystem == null) particleSystem = GetComponent<ParticleSystem>();
		var col = particleSystem.colorOverLifetime;
        col.color = color;
	}
}
