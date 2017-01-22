using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurferInput : MonoBehaviour {

	private Rigidbody2D myRigidBody;
	public Vector2 leftPushDownOffset;
	public Vector2 rightPushDownOffset;

	// Use this for initialization
	void Start () {
		myRigidBody = this.GetComponent<Rigidbody2D>();
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

		//myRigidBody.AddForceAtPosition ();

		Vector2 impulse = new Vector2(0, -move);
		if(move < 0)
			myRigidBody.AddForceAtPosition(-impulse, leftPushDownOffset);
		else
			myRigidBody.AddForceAtPosition(impulse, rightPushDownOffset);
			

		//Debug.DrawLine(Vector3.zero+leftPushDownOffSet, Vector3.zero+leftPushDownOffSet + 

#if UNITY_EDITOR
		if(move < 0)
			Debug.DrawLine(leftPushDownOffset, leftPushDownOffset - impulse);
		else
			Debug.DrawLine(rightPushDownOffset, rightPushDownOffset + impulse);
#endif
	}
}
