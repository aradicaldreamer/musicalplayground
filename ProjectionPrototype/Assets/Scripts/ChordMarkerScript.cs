﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordMarkerScript : MonoBehaviour {

	HelmManagerScript hm = HelmManagerScript.main;

    void OnTriggerEnter(Collider other) {
		
		if (other.tag == "PersonMarker") {
			hm.BassEnable ();
			hm.ArpEnable ();
			hm.DroneEnable ();
			hm.AirDroneEnable ();
			hm.LeadEnable ();
			hm.CymbalHitEnable ();
			destroyMarker ();

		}
	}

	private void destroyMarker()
	{
        
        int crrChord = 0;

        for (int i = 0; i < hm.chords.Length; i++)
        {
            if (hm.chords[i])
            {
                crrChord = i;
            }
            hm.chords[i] = false;
        }

        int random = 0;
        random = Random.Range(0, hm.chords.Length);

        while (random == crrChord)
        {
            random = Random.Range(0, hm.chords.Length);
        }

        hm.chords[random] = true;
		Destroy(gameObject);
	}
}
