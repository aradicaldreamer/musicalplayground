using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReset : MonoBehaviour {

	Vector3 startPos;
	Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		startPos = transform.position;
		rb = GetComponent<Rigidbody> ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision col) 
	{
		if (col.transform.tag == "clipping_floor") 
		{
			ResetPos();
		}
	}
	void ResetPos() 
	{
		rb.velocity = Vector3.zero;
		transform.position = startPos;
	}
}
