using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordMarkerScript : MonoBehaviour {

	HelmManagerScript hm = HelmManagerScript.main;
	InstrumentManagerScript im = InstrumentManagerScript.main;

    void Start()
    {
        var ps = GetComponent<ParticleSystem>().main;
        ps.startColor = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1.0f, 1.0f);
    }

    void OnTriggerEnter(Collider other) {
		
		if (other.tag == "PersonMarker") {
			destroyMarker ();
			im.updateAssignInstrument();
			hm.CymbalHitEnable ();
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
