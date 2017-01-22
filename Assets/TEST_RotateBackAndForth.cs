using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_RotateBackAndForth : MonoBehaviour {
	float i = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.Rotate(new Vector3(0, 0, Mathf.Sin(i += 0.01f)/4));

	}
}
