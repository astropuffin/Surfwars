using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTarget : MonoBehaviour {
	public float swimSpeed;
	private int facingDirection = 1;

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log ("COLLIDED WITH SOMETHING");
		if (collision.transform.tag == "Wall") {
			Debug.Log ("COLLIDED WITH WALL");

			facingDirection = -facingDirection;
			MoveInFacingDirection ();
		}
	}

	// Update is called once per frame
	void Update () {
		MoveInFacingDirection ();
	}

	void MoveInFacingDirection()
	{
		this.transform.Translate(new Vector3(swimSpeed*facingDirection, 0, 0));
	}
}
