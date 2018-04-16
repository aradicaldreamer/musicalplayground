using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeVisualizerScript : MonoBehaviour {

	LineRenderer shape;
	// Use this for initialization
	void Start () {
		shape = gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		int index = 0;
		shape.positionCount = PersonManagerScript.main.persons.Count;
		shape.useWorldSpace = false;
		TrackedPerson person;
		foreach (int key in PersonManagerScript.main.persons.Keys) {
			person = PersonManagerScript.main.persons [key];
			shape.SetPosition(index, new Vector3(person.positionX, 0.0f, person.positionY));
			index++;
		}
	}
}
