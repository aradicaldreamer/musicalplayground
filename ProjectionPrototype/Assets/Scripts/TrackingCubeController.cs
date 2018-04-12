using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCubeController : MonoBehaviour {

    public float trackingWeight;

	void Update () {

        transform.localScale = new Vector3(trackingWeight, trackingWeight, trackingWeight);

        if (trackingWeight > 0f )
        {
            trackingWeight -= Time.deltaTime;
        } else
        {
            Destroy(gameObject);
        }

	}
}
