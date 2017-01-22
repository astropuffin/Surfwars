using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoPusher : MonoBehaviour {

	public Rigidbody2D torso;
	public float pushPower;


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

#if DEBUG
	void OnGUI()
	{
		GUILayout.Label(Input.GetAxis("Horizontal").ToString());
	}
#endif

	void FixedUpdate()
	{
		var move = Input.GetAxis ("Horizontal");

		Vector2 impulse = new Vector2(move*pushPower, 0);
		if(move < 0)
			torso.AddForce(-impulse);
		else
			torso.AddForce(impulse);

#if UNITY_EDITOR
		//if(move < 0)
		//	Debug.DrawLine(torso.position, torso.position - impulse);
		//else
			Debug.DrawLine(torso.position, torso.position + impulse);
#endif
	}
}
