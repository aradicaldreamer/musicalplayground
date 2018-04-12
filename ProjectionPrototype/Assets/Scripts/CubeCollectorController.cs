using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollectorController : MonoBehaviour {

	float weightValue = 0f;

	Dictionary<float, TrackingCubeController> trackingCubes = new Dictionary<float, TrackingCubeController>();

//	void Update() {
//		foreach (int key in trackingCubes.Keys) {
//			TrackingCubeController trackingcube = trackingCubes [key];
//			weightValue + trackingcube.trackingWeight;
//		}
//	}

	void OnTriggerEnter(Collider other) {
		print ("happens");

		if (other.tag == "TrackingCube") {
			
			print ("collision");
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
