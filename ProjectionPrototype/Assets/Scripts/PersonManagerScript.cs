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
	
	public Dictionary<int,TrackedPerson> persons = new Dictionary<int,TrackedPerson>();
	public Dictionary<int,TrackedPerson> recentlyRemoved = new Dictionary<int,TrackedPerson>();

	public static PersonManagerScript main;

	public GameObject instrumentManager;

	public GameObject trackedEffect;
	public Dictionary<int,GameObject> trackedEffects = new Dictionary<int,GameObject>();

	public GameObject trackingCube;
	public float trackingCubeTimer = 0f;
	public float setTrackingCubeTimer = 0.5f;
    //[SerializeField] GameObject trackingCube;
    //[SerializeField] float setTrackingCubeTime = 0.5f;
    //[SerializeField] float trackingCubeTimer;

	void Awake() {
		Cursor.visible = false;
		main = this;
	}
		
	void FixedUpdate () {
		instrumentManager.GetComponent<InstrumentManagerScript>().updatePersons (persons);

		foreach (int key in persons.Keys) {
			TrackedPerson tp = persons[key];
			GameObject te = trackedEffects [key];
			te.transform.position = new Vector3(tp.positionX, 1.0f, tp.positionY);

			/*if (trackingCubeTimer <= 0f) {
				// creating ML tracking cubes
				Instantiate(trackingCube, new Vector3 (tp.positionX, 0f, tp.positionY), Quaternion.identity);
			}*/
		}

		foreach (int key in recentlyRemoved.Keys) {
			TrackedPerson tp = recentlyRemoved[key];
			tp.dead += Time.fixedDeltaTime;
			if (tp.dead > 5.0f)
			{
				deletePerson(tp);
			}
		}

		/*// reset timer
		if (trackingCubeTimer <= 0f) {
			trackingCubeTimer = setTrackingCubeTimer;
		} else {
			trackingCubeTimer -= Time.deltaTime;
		}*/

		/*if (trackingCubeTimer <= 0f) {
			foreach (int key in persons.Keys) {
				TrackedPerson person = persons [key];
				Instantiate (trackingCube, new Vector3 (person.positionX, 0f, person.positionY), Quaternion.identity);
			}

			// reset timer
			trackingCubeTimer = setTrackingCubeTimer;

		} else {
			trackingCubeTimer -= Time.deltaTime;
		}*/


		/*
		foreach (int key in persons.Keys)
		{
			TrackedPerson person = persons[key];
			Instantiate(trackingCube, new Vector3(person.positionX, 0f, person.positionY), Quaternion.identity);
		}

		/*
        //making tracking cubes.
        if (trackingCubeTimer <= 0f)
        {
            foreach (int key in persons.Keys)
            {
                TrackedPerson person = persons[key];
                Instantiate(trackingCube, new Vector3(person.positionX, 0f, person.positionY), Quaternion.identity);
            }

            //reset the timer
            trackingCubeTimer = setTrackingCubeTime;
        } else
        {
            trackingCubeTimer -= Time.deltaTime;
        }
		*/
        

	}

	void OnDrawGizmos()
	{
		foreach (int key in persons.Keys)
		{
			TrackedPerson person = persons[key];
			Gizmos.color = person.color;
			Gizmos.DrawSphere(new Vector3(person.positionX, 0.0f, person.positionY), .1f);
		}
	}

	public void addPerson(TrackedPerson tperson)
	{
		if (recentlyRemoved.Count > 0)
		{
			foreach (int key in recentlyRemoved.Keys)
			{
				TrackedPerson person = recentlyRemoved[key];

				if (Vector2.Distance(new Vector2(tperson.positionX, tperson.positionY), new Vector2(person.positionX, person.positionY)) < 0.3f)
				{
					GameObject te = trackedEffects[person.id];
					trackedEffects.Remove(person.id);
					persons.Remove(person.id);
					recentlyRemoved.Remove(person.id);
					person.id = tperson.id;
					persons[person.id] = person;
					trackedEffects[person.id] = te;

					break;
				}
			}
		} else {
			persons[tperson.id] = tperson;
			instrumentManager.GetComponent<InstrumentManagerScript>().assignInstrument(tperson.id);
			GameObject te = Instantiate(trackedEffect, new Vector3(tperson.positionX, 1.0f, tperson.positionY), Quaternion.identity);
			trackedEffects[tperson.id] = te;
			ParticleSystem ps = te.GetComponent<ParticleSystem>();
			var em = ps.emission;
			em.rateOverDistance = 150;
		}
	}

	public void removePerson(TrackedPerson tperson)
	{
		//deletePerson(tperson);
		recentlyRemoved[tperson.id] = tperson;
	}

	public void deletePerson(TrackedPerson tperson)
	{
		instrumentManager.GetComponent<InstrumentManagerScript>().removeInstrument(tperson.id);
		recentlyRemoved.Remove(tperson.id);
		persons.Remove(tperson.id);
		GameObject te = trackedEffects[tperson.id];
		Destroy(te);
		trackedEffects.Remove(tperson.id);
	}
}

