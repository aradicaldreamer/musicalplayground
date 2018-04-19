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
using System.Collections;
using System.Collections.Generic;
using TSPS;

public class TrackedSpaceScript : MonoBehaviour, OpenTSPSListener  {
	
	public int port = 12000; //set this from the UI to change the port

	public float originX;
	public float originY;
	public float sizeX;
	public float sizeY;
	
	private OpenTSPSReceiver receiver;

	public GameObject personManager;
	private PersonManagerScript personManagerScript;
	
	void Start() {
		personManagerScript = personManager.GetComponent<PersonManagerScript>();
		receiver = new OpenTSPSReceiver( port );
		receiver.addPersonListener( this );
		//Security.PrefetchSocketPolicy("localhost",8843);
		receiver.connect();
	//	Debug.Log("created receiver on port " + port);
	}
			
	void Update () {
		//call this to receiver messages
		receiver.update();
	}
	
	public void personEntered(OpenTSPSPerson person){
		//Debug.Log(" person entered with ID " + person.id);
		TrackedPerson newPerson = new TrackedPerson();
		newPerson.id = person.id;
		updatePerson(newPerson, person);
		personManagerScript.addPerson(newPerson);
	}

	public void personUpdated(OpenTSPSPerson person) {
		//don't need to handle the Updated method any differently for this example
		personMoved(person);
	}

	public void personMoved(OpenTSPSPerson person){
		//Debug.Log("Person updated with ID " + person.id);
		if(personManagerScript.persons.ContainsKey(person.id)){
			TrackedPerson trackedPerson = personManagerScript.persons[person.id];
			updatePerson(trackedPerson, person);
		}
	}

	public void personWillLeave(OpenTSPSPerson person){
		//Debug.Log("Person leaving with ID " + person.id);
		if(personManagerScript.persons.ContainsKey(person.id)){
			personManagerScript.removePerson(personManagerScript.persons[person.id]);
			personManagerScript.persons.Remove(person.id);
		}
	}

	public void sceneUpdated(OpenTSPSScene scene)
	{
		
	}

	private void updatePerson(TrackedPerson trackedPerson, OpenTSPSPerson person)
	{
		trackedPerson.age = person.age;
		trackedPerson.centroidX = person.centroidX;
		trackedPerson.centroidY = person.centroidY;
		trackedPerson.velocityX = person.velocityX;
		trackedPerson.velocityY = person.velocityY;
		trackedPerson.positionX = (float)(originX + (person.centroidX * sizeX));
		trackedPerson.positionY = (float)(originY + ((1.0f - person.centroidY) * sizeY));
	}
}
