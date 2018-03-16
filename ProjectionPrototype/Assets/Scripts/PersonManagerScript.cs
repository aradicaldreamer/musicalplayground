/**
 * OpenTSPS + Unity3d Extension
 * Created by James George on 11/24/2010
 * 
 * This example is distributed under The MIT License
 *
 * Copyright (c) 2010 James George
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TSPS;

public class PersonManagerScript : MonoBehaviour, OpenTSPSListener  {
	
	public int port = 12000; //set this from the UI to change the port
	
	//create some materials and apply a different one to each new person
	public Material	[] materials;
	
	private OpenTSPSReceiver receiver;
	//a place to hold game objects that we attach to people, maps person ID => their object
	private Dictionary<int,Person> peopleCubes = new Dictionary<int,Person>();
	
	//game engine stuff for the example
	public GameObject boundingPlane; //put the people on this plane
	public GameObject personMarker; //used to represent people moving about in our example
	
	public GameObject helmManager;
	private Instrument[] instruments;

	void Start() {
		// Create an array of instrument objects
		Instrument drone = new Instrument();
		drone.name = "drone";
		Instrument bass = new Instrument();
		bass.name = "bass";
		Instrument arp = new Instrument();
		arp.name = "arp";
		instruments = new Instrument[3] { drone, bass, arp };
		receiver = new OpenTSPSReceiver( port );
		receiver.addPersonListener( this );
		//Security.PrefetchSocketPolicy("localhost",8843);
		receiver.connect();
		Debug.Log("created receiver on port " + port);
	}
			
	void Update () {
		//call this to receiver messages
		receiver.update();
	}
	
	
	void OnGUI() {
		if( receiver.isConnected() ) {
			GUI.Label( new Rect( 10, 10, 500, 100), "Connected to TSPS on Port " + port );
		}
	}
	
	public void personEntered(OpenTSPSPerson person){
		Debug.Log(" person entered with ID " + person.id);
		Person newPerson = new Person();
		GameObject personObject = (GameObject)Instantiate(personMarker, positionForPerson(person), Quaternion.identity);
		personObject.GetComponent<Renderer>().material = materials[person.id % materials.Length];
		newPerson.gmo = personObject;
		peopleCubes[person.id] = newPerson;
		Instrument instrument = getNextInstrument();
		if (instrument != null) {
			newPerson.instrument = instrument;
			instrument.personAttached = person.id;
		}
	}

	private Instrument getNextInstrument()
	{
		List<Instrument> instrumentsNotAssigned = new List<Instrument>();
		for (int i = 0; i < instruments.Length; i++)
		{
			Instrument instrument = instruments[i];
			if (instrument.personAttached == -1)
			{
				instrumentsNotAssigned.Add(instrument);
			}
		}
		if (instrumentsNotAssigned.Count > 0) {
			System.Random rndwer = new System.Random();
			int r = rndwer.Next(instrumentsNotAssigned.Count);
			return (Instrument)instrumentsNotAssigned[r];
		}
		return null;
	}

	public void personUpdated(OpenTSPSPerson person) {
		//don't need to handle the Updated method any differently for this example
		personMoved(person);
		instrumentUpdate(person);
	}

	public void personMoved(OpenTSPSPerson person){
		Debug.Log("Person updated with ID " + person.id);
		if(peopleCubes.ContainsKey(person.id)){
			GameObject cubeToMove = peopleCubes[person.id].gmo;
			cubeToMove.transform.position = positionForPerson(person);
		}
	}

	public void instrumentUpdate(OpenTSPSPerson person){
		if(peopleCubes.ContainsKey(person.id)){
			Person personMoved = peopleCubes[person.id];
			if (personMoved.instrument != null) {
				Debug.Log(personMoved.instrument.name);
				switch(personMoved.instrument.name) {
					case "drone" :
						updateDrone(person);
						break;
					case "bass" :
						updateBass(person);
						break;
					case "arp" :
						updateArp(person);
						break;
				}
			}
		}
	}

	private void updateDrone(OpenTSPSPerson person)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.DroneX = person.centroidX;
		hms.DroneY = person.centroidY;
	}

	private void updateBass(OpenTSPSPerson person)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.BassSubShuffle = -10.0f + (person.centroidX * 20.0f);
		hms.BassOSC2tune = -10.0f + (person.centroidY * 20.0f);
	}

	private void updateArp(OpenTSPSPerson person)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.ArpFeedback = -5.0f + (person.centroidX * 10.0f);
		hms.ArpStutter = -10.0f + (person.centroidY * 30.0f);
	}

	public void personWillLeave(OpenTSPSPerson person){
		Debug.Log("Person leaving with ID " + person.id);
		if(peopleCubes.ContainsKey(person.id)){
			GameObject cubeToRemove = peopleCubes[person.id].gmo;
			Instrument instrument = peopleCubes[person.id].instrument;
			if (instrument != null) instrument.personAttached = -1;
			peopleCubes[person.id].instrument = null;

			peopleCubes.Remove(person.id);
			//delete it from the scene	
			Destroy(cubeToRemove);
		}
	}
	
	//maps the OpenTSPS coordinate system into one that matches the size of the boundingPlane
	private Vector3 positionForPerson(OpenTSPSPerson person){
		Bounds meshBounds = boundingPlane.GetComponent<MeshFilter>().sharedMesh.bounds;
		return new Vector3( (float)(.5 - person.centroidX) * meshBounds.size.x, 0.25f, (float)(person.centroidY - .5) * meshBounds.size.z );
	}
}

public class Instrument
{
	public string name = "";

	public int personAttached = -1;
}

public class Person
{
	public int id;
	public GameObject gmo;
	public Instrument instrument = null;

}