using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLine : MonoBehaviour {

    struct SegmentData
    {
        public float height;
        public float velocity;
    }


    public GameObject leftDude, rightDude;
    public LineRenderer line;

    public int lineSegments;
    public float speedOfSound;
    public float springyness;
    public float density;
    public float damping;

    float[] velocities, heights, newHeights, newVelocities;

  
    float distanceBetweenDudes;
    float distanceBetweenSegments;
    float previousTimeStep;

	// Use this for initialization
	void Start () {
        line.numPositions = lineSegments;
        velocities = new float[lineSegments];
        heights = new float[lineSegments];
        newHeights = new float[lineSegments];

        distanceBetweenDudes = Vector3.Distance(leftDude.transform.position, rightDude.transform.position);
        distanceBetweenSegments = distanceBetweenDudes / lineSegments;

        previousTimeStep = Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {
        line.SetPosition(0, leftDude.transform.position);
        line.SetPosition(lineSegments -1, rightDude.transform.position);

        heights[0] = leftDude.transform.position.y;
        heights[lineSegments-1] = rightDude.transform.position.y;

        for(int i = 1; i < lineSegments -1; i ++)
        {
            
            float leftHeight = heights[i - 1];
            float rightHeight = heights[i + 1];
            float desiredHeight = (leftHeight + rightHeight) / 2;
            float centerHeight = heights[i];
            
            float force = speedOfSound * speedOfSound * (leftHeight + rightHeight - 2 * centerHeight)
                / (distanceBetweenSegments * distanceBetweenSegments);

            velocities[i] = (velocities[i] + force * Time.deltaTime) * damping;
            newHeights[i] = centerHeight + velocities[i] * Time.deltaTime;


            line.SetPosition(i, new Vector3(i * distanceBetweenSegments, newHeights[i]));
        }

        for(int i = 0; i < lineSegments - 1; i ++)
        {
            heights[i] = newHeights[i];
        }
	}
}
