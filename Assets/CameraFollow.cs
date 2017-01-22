using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Transform player1;
	private Transform player2;
	public double innerSweetSpot = 0.1;
	public double outerSweetSpot = 0.2;
	public float cameraZoomSpeed = 1.0f;
	private Vector2 midpointBetweenPlayers;
	private Transform cameraTransform;
	public float zoomInLimit = -20.5f;
    public float yOffset;
    public Camera cam;
    public float desiredEdgeDistance;
    
	//public float zoomOutLimit = -56.0f;

	// Use this for initialization
	void Start () {
		midpointBetweenPlayers = Vector3.zero;
		cameraTransform = this.GetComponent<Camera>().transform;

		var players = GameObject.FindGameObjectsWithTag ("Board");
		if (players.Length < 1)
		{
			Debug.LogError("Not enough players! Dynamic camera cannot be set");
			player1 = player2 = cameraTransform;
		}

		// hack hack hack *cough*
		player1 = players[0].transform;
		player2 = players[1].transform;
	}
		
	// Update is called once per frame
	void Update () {
		midpointBetweenPlayers = Vector2.Lerp (player1.position, player2.position, 0.5f);

		var newPos = new Vector3 (midpointBetweenPlayers.x, 
			midpointBetweenPlayers.y + yOffset,
			cameraTransform.position.z);

		cameraTransform.position = newPos;

        Vector3 screenEdge = cam.ViewportToWorldPoint( new Vector3(0, 0, -cam.transform.position.z));
        Vector3 leftMostPlayerPos;
		if(player1.transform.position.x < player2.transform.position.x)
        {
            leftMostPlayerPos = player1.transform.position;
        }
        else
        {
            leftMostPlayerPos = player2.transform.position;
        }

        float distanceToEdge = screenEdge.x - leftMostPlayerPos.x;
        float delta = distanceToEdge - desiredEdgeDistance;
        Vector3 cameraPos = cam.transform.position;
        cam.transform.position = Vector3.Lerp(cameraPos, cameraPos - Vector3.forward * delta, 0.33f);

        /*
			screenPoint = this.GetComponent<Camera>().WorldToScreenPoint(player1.transform.position);
		else
			screenPoint = this.GetComponent<Camera>().WorldToScreenPoint(player2.transform.position);

		// Zoom out if off-screen.
		if (screenPoint.x < innerSweetSpot) {
			cameraTransform.Translate (new Vector3(0,0,-cameraZoomSpeed * Time.deltaTime));
		}

		// Zoom in if we're too far out.
		if(screenPoint.x > outerSweetSpot){
			cameraTransform.Translate (new Vector3(0,0,cameraZoomSpeed * Time.deltaTime));
		}
        */
		// Keep the camera in bounds
		if(cameraTransform.position.z > zoomInLimit)
			cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, zoomInLimit);
		//else if(cameraTransform.position.z < -56.0f)
		//		cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, -56.0f);

        
	}
}
