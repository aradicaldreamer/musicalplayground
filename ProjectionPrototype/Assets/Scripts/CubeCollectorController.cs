using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollectorController : MonoBehaviour {

	public float weightValue = 0f;

	Dictionary<float, TrackingCubeController> trackingCubes = new Dictionary<float, TrackingCubeController>();

	void Update() {

		weightValue = 0f;

		foreach (int key in trackingCubes.Keys) {
			TrackingCubeController trackingcube = trackingCubes [key];
			weightValue += trackingcube.trackingWeight;
		}

		if (weightValue < 0) {
			weightValue = 0;
		}
	}


	void OnTriggerEnter(Collider other) {
		

		if (other.tag == "TrackingCube") {
			
			
			TrackingCubeController trackingcube = other.GetComponent<TrackingCubeController> ();
			trackingcube.myCollector = this;

			if (!trackingCubes.ContainsKey (trackingcube.id)) {
				trackingCubes.Add (trackingcube.id, trackingcube);  
			}

			other.tag = "TaggedCube";
		}
	}

	public void UnregisterTrackingCube(float id) {
		if (trackingCubes.ContainsKey(id)) {
			trackingCubes.Remove (id);
		}
		
	}
}
