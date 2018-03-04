using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialColour : MonoBehaviour {
	Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.material.color = Random.ColorHSV(0f,1f,0f,1f,0.5f,1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
