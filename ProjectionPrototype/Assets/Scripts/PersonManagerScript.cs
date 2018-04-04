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

public class PersonManagerScript : MonoBehaviour  {
	
	private Dictionary<int,Person> persons = new Dictionary<int,Person>();

	public GameObject instrumentManager;

	void Start() {
		
	}
			
	void FixedUpdate () {
		
	}

	void OnDrawGizmos()
	{
		foreach (int key in persons.Keys)
		{
			Person person = persons[key];
			Gizmos.color = person.color;
			Gizmos.DrawSphere(person.position, .1f);
		}
	}

	public void addPerson(TrackedSpaceScript.TrackedPerson tperson)
	{
		Person person = new Person();
		person.id = tperson.id;
		person.position = new Vector3(tperson.positionX, 0.0f, tperson.positionY);
		//person.instrument = instrumentManager.GetComponent<InstrumentManagerScript>().getNextInstrument(person.id);
		persons[person.id] = person;
	}

	public void updatePerson(TrackedSpaceScript.TrackedPerson tperson)
	{
		if (persons.ContainsKey(tperson.id))
		{
			persons[tperson.id].position = new Vector3(tperson.positionX, 0.0f, tperson.positionY);
		}
	}

	public void removePerson(TrackedSpaceScript.TrackedPerson tperson)
	{
		persons.Remove(tperson.id);
	}

	public class Person
	{
		public int id;
		public Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

		public Color color = Color.HSVToRGB(UnityEngine.Random.Range(0.0f, 1.0f), 1.0f, 1.0f);

		public Instrument instrument;

	}
}

