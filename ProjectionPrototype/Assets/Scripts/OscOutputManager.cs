using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscOutputManager : MonoBehaviour {
	public OSC osc;
	public GameObject Manager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HelmManagerScript hms = Manager.GetComponent<HelmManagerScript>();
		OscMessage message = new OscMessage();
		message.address = "/wek/inputs";
	//	message.values.Add(hms.AirDroneOn);
		message.values.Add(hms.DroneX);
		message.values.Add(hms.DroneY);
		message.values.Add(hms.DroneFeedback);
		message.values.Add(hms.DroneMod);
		osc.Send(message);
	}
}
