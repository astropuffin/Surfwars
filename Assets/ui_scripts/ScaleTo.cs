using UnityEngine;
using System.Collections;

public class ScaleTo : MonoBehaviour {

	public Vector3 scaleTarget;
	public float scaleSpeed;
	public float bounceDistance;
	private bool reachedEnd = false; 
	private Vector3 scaleTargetWithBounce; 
	private float floatErrorFixer = 0.1f;
	public float scaleAccelerator = 0.1f;

	// Use this for initialization
	void Start () {
		//Time.timeScale = 0.2f;
		scaleTargetWithBounce = new Vector3 (scaleTarget.x + bounceDistance, scaleTarget.y + bounceDistance, scaleTarget.y + bounceDistance);
	}
	
	// Update is called once per frame
	void Update () {
		if (!reachedEnd) {
			var newScale = Vector3.Lerp(transform.lossyScale, scaleTargetWithBounce, (scaleSpeed+=scaleAccelerator));
			transform.localScale = newScale;

			// Debug.Log (transform.lossyScale.ToString());
			if(transform.localScale.x >= scaleTargetWithBounce.x - floatErrorFixer
			   && transform.localScale.y >= scaleTargetWithBounce.y - floatErrorFixer
			   && transform.localScale.z >= scaleTargetWithBounce.z - floatErrorFixer)
			{
				reachedEnd = true;
				Debug.Log ("Reached end!");
			}
		}
		else{
			var newScale = Vector3.Lerp(transform.lossyScale, scaleTarget, scaleSpeed);
			transform.localScale = newScale;
		}
	}
}
