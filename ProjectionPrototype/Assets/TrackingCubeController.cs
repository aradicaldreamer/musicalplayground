using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCubeController : MonoBehaviour {

	float trackingWeight = 1f;
	[SerializeField] float decaySpeed = 2f;
	Vector3 startScale = Vector3.zero;


	// Use this for initialization
	void Start () {
		startScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		

		transform.localScale = new Vector3 (trackingWeight * startScale.x, trackingWeight * startScale.y, trackingWeight * startScale.z);

		if (trackingWeight > 0f) {
			trackingWeight -= decaySpeed * Time.deltaTime;
		} else {
			Destroy (gameObject);
		}
	}
}
