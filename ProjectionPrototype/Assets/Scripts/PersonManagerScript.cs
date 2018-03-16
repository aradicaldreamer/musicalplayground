﻿/**
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
		drone.defaultPosX = 0.0f;
		drone.defaultPosY = 0.5f;
		drone.defaultVelX = 0.0f;
		drone.defaultVelY = 0.5f;
		Instrument bass = new Instrument();
		bass.name = "bass";
		bass.defaultVelX = 0.0f;
		bass.defaultVelY = 0.0f;
		bass.defaultPosX = 0.5f;
		bass.defaultPosY = 0.5f;
		Instrument arp = new Instrument();
		arp.name = "arp";
		arp.defaultX = 0.0f;
		arp.defaultY = 0.5f;
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
		updateInstruments();
	}
	
	void updateInstruments()
	{
		for (int i = 0; i < instruments.Length; i++)
		{
			Instrument instrument = instruments[i];
			if (instrument.personAttached == -1) {
				instrument.currPosX += (instrument.defaultPosX - instrument.currPosX) / 6; 
				instrument.currPosY += (instrument.defaultPosY - instrument.currPosY) / 6;
				instrument.currVelX += (instrument.defaultVelX - instrument.currVelX) / 6; 
				instrument.currVelY += (instrument.defaultVelY - instrument.currVelY) / 6;
			} else {
				instrument.currPosX += (instrument.newPosX - instrument.currPosX) / 3; 
				instrument.currPosY += (instrument.newPosY - instrument.currPosY) / 3;
				instrument.currVelX += (instrument.newVelX - instrument.currVelX) / 3; 
				instrument.currVelY += (instrument.newVelY - instrument.currVelY) / 3;
			}

			switch (instrument.name)
			{
				case "drone" :
					updateDrone(instrument.currPosX, instrument.currPosY, instrument.currVelX, instrument.currVelY);
					break;
				case "bass" :
					updateBass(instrument.currPosX, instrument.currPosY, instrument.currVelX, instrument.currVelY);
					break;
				case "arp" :
					updateArp(instrument.currPosX, instrument.currPosY, instrument.currVelX, instrument.currVelY);
					break;
			}
		}
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
				personMoved.instrument.newPosX = person.centroidX;
				personMoved.instrument.newPosY = person.centroidY;
				personMoved.instrument.newVelX = person.velocityX;
				personMoved.instrument.newVelY = person.velocityY;
			}
		}
	}

	private void updateDrone(float npx, float npy, float nvx, float nvy)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.DroneX = mapValue(npx, 0.0f, 1.0f);
		hms.DroneY = mapValue(npy, 0.0f, 1.0f);
	}

	private void updateBass(float npx, float npy, float nvx, float nvy)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.BassSubShuffle = mapValue(nvx, 0.0f, 1.0f);
		hms.BassOSC2tune = mapValue(nvy, -1.0f, 1.0f);
		hms.BassFeedbackTune = mapValue(npx, -1.0f, 1.0f);
		hms.BassFeedbackAmount = mapValue(npy, 0, 1.0f);
	}

	private void updateArp(float npx, float npy, float nvx, float nvy)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.ArpFeedback = mapValue(npx, 0.0f, 1.0f);
		hms.ArpStutter = mapValue(npy, 0.0f, 1.0f);
		hms.ArpStutterResample = mapValue(npy, 0.0f, 1.0f);
	}

	private float mapValue(float value, float vmin, float vmax)
	{
		return vmin + ((vmax - vmin) * value);
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
	public float defaultPosX = 0.0f;
	public float defaultPosY = 0.0f;
	public float newPosX = 0.0f;
	public float newPosY = 0.0f;
	public float currPosX = 0.0f;
	public float currPosY = 0.0f;
	public float defaultVelX = 0.0f;
	public float defaultVelY = 0.0f;
	public float newVelX = 0.0f;
	public float newVelY = 0.0f;
	public float currVelX = 0.0f;
	public float currVelY = 0.0f;
}

public class Person
{
	public int id;
	public GameObject gmo;
	public Instrument instrument = null;

}