using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMover : MonoBehaviour {
    public float amplitude;
    public float frequency;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(0, Mathf.Sin(Time.realtimeSinceStartup * frequency) * amplitude);
	}
}
