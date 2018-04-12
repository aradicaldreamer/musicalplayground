using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TimeController : MonoBehaviour {


	public Stopwatch timer = new Stopwatch();
	public int idCounter = 0;

	public static TimeController main;

	// Use this for initialization
	void Awake () {
		main = this;
		timer.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
