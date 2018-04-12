using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCubeController : MonoBehaviour {

	public float id;
	public float trackingWeight = 1f;
	[SerializeField] float decaySpeed = 2f;
	Vector3 startScale = Vector3.zero;
	public CubeCollectorController myCollector;


	// Use this for initialization
	void Start () {
		startScale = transform.localScale;
		id = TimeController.main.idCounter;
		TimeController.main.idCounter++;
		print ("ID: "+ id.ToString());
		
	}
	
	// Update is called once per frame
	void Update () {
		

		transform.localScale = new Vector3 (trackingWeight * startScale.x, trackingWeight * startScale.y, trackingWeight * startScale.z);

		if (trackingWeight > 0f) {
			trackingWeight -= decaySpeed * Time.deltaTime;
		} else {

			myCollector.UnregisterTrackingCube (id);
			Destroy (gameObject);
		}
	}


}
