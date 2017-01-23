using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class FishChaser : MonoBehaviour {
	public Transform target;
	private Rigidbody2D rigidBody;
	private SpriteRenderer sprite;
	public float swimPowerMultiplier = 5.0f;
	public float swimPowerRandomness = 0.0f;
	
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();
		StartCoroutine(Swim (1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Swim(int WaitTime)
	{
		Random.InitState ((int)Time.time);

		while (true) {
			var rnd = Random.Range (0, swimPowerRandomness);

			var moveToward = new Vector2 ((target.position.x - transform.position.x) * rnd * swimPowerMultiplier , 0);
			if (moveToward.x > 0)
				sprite.flipX = true;
			else
				sprite.flipX = false;

			rigidBody.AddForce (moveToward);
			yield return new WaitForSeconds (WaitTime);
		}
	}

}
