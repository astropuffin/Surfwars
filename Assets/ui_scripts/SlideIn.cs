using UnityEngine;
using System.Collections;

public class SlideIn : MonoBehaviour {
	private Vector3 originalPosition;
	public Transform targetPosition;
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		originalPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		var newPos = Vector3.Lerp (this.transform.position, targetPosition.position, moveSpeed);
		this.transform.position = newPos;
	}
}
