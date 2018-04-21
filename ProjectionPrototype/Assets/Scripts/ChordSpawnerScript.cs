using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordSpawnerScript : MonoBehaviour {

	public GameObject chord;

	private float timer;
	public float nextChordReveal = 16.0f;
	// Use this for initialization
	void Start () {
        HelmManagerScript.main.chords[Random.Range(0, HelmManagerScript.main.chords.Length)] = true;

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > nextChordReveal)
		{
			if (transform.childCount == 0) spawnChord();
			nextChordReveal = Random.Range(1.0f, 10.0f);
			timer = 0.0f;
		}
	}

	private void spawnChord()
	{
		GameObject newChord = Instantiate(chord, Vector3.zero, Quaternion.identity);
		newChord.transform.parent = transform;
		newChord.transform.position = new Vector3(Random.Range(0.2f, 1.8f), 0.0f, Random.Range(0.2f, 0.8f));
		newChord.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
	}
}
