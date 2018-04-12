using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLearningGridController : MonoBehaviour {

	[SerializeField] float xSize = 1f;
	[SerializeField] float ySize = 1f;
	[SerializeField] float spacing = 0.1f;

	public GameObject cubeCollector;


	// Use this for initialization
	void Start () {
		float xPos,yPos = 0;

		for (float i = 0; i < xSize ; i += spacing) {
			xPos = i;
			for (float ii = 0; ii < ySize ; ii += spacing) {
				yPos = ii;

				Vector3 position = new Vector3 (xPos, 0, yPos);

				Instantiate (cubeCollector, position, Quaternion.identity);
			}
		}


	}

	// Update is called once per frame
	void Update () {
		
	}
}
