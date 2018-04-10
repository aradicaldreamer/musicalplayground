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

public class InstrumentManagerScript : MonoBehaviour  {
	//game engine stuff for the example
	public GameObject helmManager;
	public GameObject bpmController;
	private Instrument[] instruments;

	void Start() {
		// Create an array of instrument objects
		Instrument drums = new Instrument();
		drums.name = "drums";
		drums.defaultPosX = 0.0f;
		drums.defaultPosY = 0.0f;
		drums.defaultVelX = 0.4f;
		drums.defaultVelY = 0.4f;
		//drums.defaultCol = 0.0f; //collision 0 -1
		Instrument bass = new Instrument();
		bass.name = "bass";
		bass.defaultPosX = 0.0f;
		bass.defaultPosY = 0.0f;
		bass.defaultVelX = 0.0f;
		bass.defaultVelY = 0.0f;
		//bass.defaultCol = 0.0f; //collision 0 -1
		Instrument drone = new Instrument();
		drone.name = "drone";
		drone.defaultPosX = 0.0f;
		drone.defaultPosY = 0.5f;
		drone.defaultVelX = 0.0f;
		drone.defaultVelY = 0.0f;
		//drone.defaultCol = 1.0f; collision 1 - 0
		Instrument airDrone = new Instrument();
		airDrone.name = "airDrone";
		airDrone.defaultPosX = 1.0f;
		airDrone.defaultPosY = 0.0f;
		airDrone.defaultVelX = 1.0f;
		airDrone.defaultVelY = 0.0f;
		//airDrone.defaultCol = 0.5f; //collision 0.5 - 0.6?
		Instrument arp = new Instrument();
		arp.name = "arp";
		arp.defaultPosX = 1.0f;
		arp.defaultPosY = 0.0f;
		arp.defaultVelX = 0.5f;
		arp.defaultVelY = 0.5f;
		//arp.defaultCol = 0.0f; //collision 0 - 1
		Instrument lead = new Instrument();
		lead.name = "lead";
		lead.defaultPosX = 0.0f;
		lead.defaultPosY = 0.0f;
		lead.defaultVelX = 0.1f;
		lead.defaultVelY = 0.0f;
		//lead.defaultCol = 0.5f; //collision 0.5 - 1
		Instrument bpm = new Instrument();
		bpm.name = "bpm";
		bpm.defaultPosX = 1.4f;

		//instruments = new Instrument[7] { drums, bass, drone, airDrone, arp, lead, bpm };
		instruments = new Instrument[1] { arp };
		//instruments = new Instrument[1] { bpm};
	}
			
	void Update () {
		updateInstruments();
	}

	public void updatePersons(Dictionary<int,TrackedPerson> persons)
	{
		for (int i = 0; i < instruments.Length; i++)
		{
			Instrument instrument = instruments[i];
			if (instrument.personAttached > -1) {
				instrument.newPosX = persons [instrument.personAttached].centroidX;
				instrument.newPosY = persons [instrument.personAttached].centroidY;
				instrument.newVelX = persons [instrument.personAttached].velocityX;
				instrument.newVelY = persons [instrument.personAttached].velocityY;
			}
		}
	}
	
	private void updateInstruments()
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
				case "drums" :
					updateDrums(instrument.currPosX, instrument.currPosY, instrument.currVelX, instrument.currVelY);
					break;
				case "bass" :
					updateBass(instrument.currPosX, instrument.currPosY, instrument.currVelX, instrument.currVelY);
					break;
				case "drone" :
					updateDrone(instrument.currPosX, instrument.currPosY, instrument.currVelX, instrument.currVelY);
					break;
				case "airDrone" :
					updateAirDrone(instrument.currPosX, instrument.currPosY, instrument.currVelX, instrument.currVelY);
					break;
				case "arp" :
					updateArp(instrument.currPosX, instrument.currPosY, instrument.currVelX, instrument.currVelY);
					break;
				case "lead" :
					updateLead(instrument.currPosX, instrument.currPosY, instrument.currVelX, instrument.currVelY);
					break;
				case "bpm" :
					updateBPM(instrument.currPosX, instrument.currPosY, instrument.currVelX, instrument.currVelY);
					break;

			}
		}
	}

	public void setInstrumentEnabled(Instrument instrument, bool value = true)
	{
		Debug.Log (instrument.name + " " + value);
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		switch(instrument.name)
		{
			case "drums" :
				if (value) {
					hms.DrumsEnable();
				} else {
					hms.DrumsDisable();
				}
				break;
			case "bass" :
				if (value) {
					hms.BassEnable();
				} else {
					hms.BassDisable();
				}
				break;
			/*case "drone" :
				if (value) {
					hms.DroneEnable();
				} else {
					hms.DroneDisable();
				}
				break;*/
			case "airDrone" :
				if (value) {
					hms.AirDroneEnable();
				} else {
					hms.AirDroneDisable();
				}
				break;
			case "arp" :
				if (value) {
					hms.ArpEnable();
				} else {
					hms.ArpDisable();
				}
				break;
			case "lead" :
				if (value) {
					hms.LeadEnable();
				} else {
					hms.LeadDisable();
				}
				break;
		}
	}

	public void assignInstrument(int personID)
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
			Instrument instrument = (Instrument)instrumentsNotAssigned[r];
			setInstrumentEnabled (instrument, true);
			instrument.personAttached = personID;
		}
	}

	public void removeInstrument(int personID)
	{
	//	List<Instrument> instrumentsNotAssigned = new List<Instrument>();
		for (int i = 0; i < instruments.Length; i++)
		{
			Instrument instrument = instruments[i];
			if (instrument.personAttached == personID)
			{
				setInstrumentEnabled (instrument, false);
				instrument.personAttached = -1;
				break;
			}
		}
	}

	public void instrumentUpdate(Instrument instrument, float npx, float npy, float nvx, float nvy)
	{	
		if (instrument != null) {
			instrument.newPosX = npx;
			instrument.newPosY = npy;
			instrument.newVelX = nvx;
			instrument.newVelY = nvy;
		}
	}

	private void updateDrums(float npx, float npy, float nvx, float nvy)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.SnareDelayMix = mapValue(npx, -1.0f, 1.0f); //posX
		hms.HihatDelayFeedback = mapValue(npx, 0.0f, 0.8f); //posX combined
		hms.HihatDelayMix = mapValue(npy, -1.0f, 1.0f); //posY
		hms.SnareDelayFeedback = mapValue(npy, 0.0f, 0.8f); //posY combined
		hms.SnareDelaySync = mapValue(nvx, 0.4f, 0.45f); //velX
		hms.HihatDelaySync = mapValue(nvy, 0.4f, 0.45f); //velY
		//hms.CymbalHitEnable(); //collision
	}

	private void updateBass(float npx, float npy, float nvx, float nvy)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.BassFeedbackTune = mapValue(npx, 0.0f, 1.0f);
		hms.BassFeedbackAmount = mapValue(npy, 0.0f, 1.0f);
		hms.BassSubShuffle = mapValue(nvx, 0.0f, 1.0f);
		hms.BassOSC2tune = mapValue(nvy, 0.0f, 1.0f);
		//hms.BassReso = mapValue(?, 0.0f, 1.0f); //collision
	}

	private void updateDrone(float npx, float npy, float nvx, float nvy)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.DroneX = mapValue(npx, 0.0f, 0.9f);
		hms.DroneY = mapValue(npy, 0.9f, 0.0f);
		hms.DroneFeedback = mapValue (nvx, 0.0f, -0.1f);
		hms.DroneMod = mapValue (nvy, 0.0f, 0.2f);
		//hms.AirDronefilterBlend = mapValue (? , 1.0f, 0.0f); //collision
	}

	private void updateAirDrone(float npx, float npy, float nvx, float nvy)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.AirDroneX = mapValue(npx, 1.0f, 0.0f);
		hms.AirDroneY = mapValue(npy, 0.0f, 1.0f);
		hms.AirDronefilterBlend = mapValue(nvx, 1.0f, 0.0f);
		hms.AirDroneArpOn = mapValue(nvy, 0.0f, 1.0f);
		//hms.AirDroneDelayTempo = mapValue(nvx, 0.0f, 0.0f); //collision
	}

	private void updateArp(float npx, float npy, float nvx, float nvy)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.ArpStutter = mapValue(npx, 0.0f, 1.0f); //posX
		hms.ArpSub = mapValue(npy, 0.0f, 1.0f); //posY
		hms.ArpFeedback = mapValue(nvx, 0.5f, 0.6f); //velX
		hms.ArpDelayFeedback = mapValue(nvy, 0.5f, 0.6f); //velY
		//hms.ArpSustain = mapValue(?, 0.0f, 1.0f); //collision
	}

	private void updateLead(float npx, float npy, float nvx, float nvy)
	{
		HelmManagerScript hms = helmManager.GetComponent<HelmManagerScript>();
		hms.LeadMix = mapValue(npx, 0.0f, 1.0f); //posX
		hms.LeadSub = mapValue(npy, 1.0f, 0.0f); //posY
		hms.LeadSustain = mapValue(nvx, 0.1f, 0.5f); //velX
		hms.LeadAttack = mapValue(nvy,0.0f,0.2f); //velY
	//	hms.LeadDelaySync = mapValue(?, 0.5f, 1.0f); //collision ?
	//	hms.LeadDelayTempo = mapValue(?, 0.5f, 1.0f); //collision combined ?

	}
	private void updateBPM(float npx, float npy, float nvx, float nvy)
	{
		AudioHelm.AudioHelmClock clock = bpmController.GetComponent<AudioHelm.AudioHelmClock>();
		clock.bpm = mapValue (npx, -10.0f, 90.0f) + mapValue (npy,-10.0f, 90.0f);


	}

	private float mapValue(float value, float vmin, float vmax)
	{
		return vmin + ((vmax - vmin) * value);
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