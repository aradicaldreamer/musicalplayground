using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordMarkerScript : MonoBehaviour {

	private float[] chordvalues;  

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
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PersonMarker")
		{
			destroyMarker();
		}
    }

	private void destroyMarker()
	{
		int ran = (int)Mathf.Floor(Random.Range(0.0f, 6.999f));
		for (int i = 0; i < 7; i++)
		{
			chordvalues[i] = (ran == i) ? 1 : 0;
		}
		Destroy(this);
	}
}
