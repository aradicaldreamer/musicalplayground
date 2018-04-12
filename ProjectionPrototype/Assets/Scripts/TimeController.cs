using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {



	public int idCounter = 0;

	public static TimeController main;

	// Use this for initialization
	void Awake () {
		main = this;

	}
	
	// Update is called once per frame
	void Update () {
		// the maximum value of an integer 2147483647

		if (idCounter > 2147483647 - 1) {
			idCounter = 0;
		}
	}
}
