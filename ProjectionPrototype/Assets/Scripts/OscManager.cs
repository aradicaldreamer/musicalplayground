using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscManager : MonoBehaviour {
	public OSC oscRef;
	public GameObject Manager;
	// Use this for initialization
	void Start () {
		//Wekinator Outputs
		oscRef.SetAddressHandler( "/wek/outputs" , OnReceive );
	}

	// Update is called once per frame
	void Update () {

		OscMessage message = new OscMessage();


		message.address = "/wek/inputs/";
		// Send data out to wekinator inputs
		foreach (int key in PersonManagerScript.main.persons.Keys)
		{
			TrackedPerson person = PersonManagerScript.main.persons[key];
			//Gizmos.color = person.color;
			//Gizmos.DrawSphere(new Vector3(person.positionX, 0.0f, person.positionY), .1f);
			SendOscMessage (person, ref message);
		}


		oscRef.Send(message);

	}
	void OnReceive(OscMessage message){
		float x = message.GetFloat(0);
		float y = message.GetFloat(1);
		//Debug.Log(" x = " + x);
		//Debug.Log(" y = " + y);

	}
	void SendOscMessage(TrackedPerson person, ref OscMessage message) {
		message.values.Add(person.positionX);
		message.values.Add (person.positionY);
		Debug.Log ("personX" + person.positionX);
		Debug.Log ("personY" + person.positionY);
	}
}
