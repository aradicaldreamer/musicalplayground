using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordMarkerScript : MonoBehaviour {

	private int[] chordvalues = new int[7];  

	// Use this for initialization
	void Start () {
		chordvalues[0] = HelmManagerScript.main.ChordA6B;
		chordvalues[1] = HelmManagerScript.main.ChordAbmin9;
		chordvalues[2] = HelmManagerScript.main.ChordAmaj69;
		chordvalues[3] = HelmManagerScript.main.ChordCmaj69;
		chordvalues[4] = HelmManagerScript.main.ChordEbmaj69;
		chordvalues[5] = HelmManagerScript.main.ChordFmin9;
		chordvalues[6] = HelmManagerScript.main.ChordGbmaj69;



	}


	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "PersonMarker") {
			print ("yay");
//			for (int i = 0; i < chordvalues.Length; i++) {
//				chordvalues [i] = 0;
//			}
//			chordvalues [Random.Range (0, chordvalues.Length - 1)] = 1;
			destroyMarker ();

		}
	}

	private void destroyMarker()
	{
		int ran = (int)Mathf.Floor(Random.Range(0.0f, 6.999f));
		for (int i = 0; i < 7; i++)
		{
			chordvalues[i] = (ran == i) ? 1 : 0;
		}
		Destroy(gameObject);
	}
}
