using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfboard : MonoBehaviour
{

    public LineRenderer line;
    public float density;
    public float waterPushFactor;
    public float waterAdd;
    public float rightingForce;
    public Rigidbody2D rb;
    public WaveSolver wave;
    public int displacementRadius;
    public int displacementOffset;
    public LayerMask surfboardLayer;
    public float maxSpeed;


    // Use this for initialization
    void Start()
    {
        line = FindObjectOfType<LineRenderer>();
        wave = FindObjectOfType<WaveSolver>();

    }
    Vector3[] positions = new Vector3[100];


    // Update is called once per frame
    void Update()
    {

        line.GetPositions(positions);

        float displacedWater = 0;
        int leftIndex = wave.cellCount - 1;
        int rightIndex = 0;

        for (int i = 0; i < line.numPositions; i++)
        {
            var hit = Physics2D.Raycast(positions[i], Vector2.down, 100, surfboardLayer);
            if (hit.transform && hit.transform.gameObject == gameObject)
            {
                if (leftIndex > i)
                {
                    leftIndex = i;
                }

                if (rightIndex < i)
                {
                    rightIndex = i;
                }

                var displacement = hit.distance * density * Time.deltaTime;
                rb.AddForceAtPosition(hit.normal * displacement, hit.point);
                wave.rho[i] -= hit.distance * waterPushFactor;
                displacedWater += hit.distance * waterPushFactor;
            }
        }

        if (leftIndex > displacementRadius + displacementOffset)
        {
            for (int i = 0; i < displacementRadius; i++)
            {
                wave.rho[leftIndex - i - displacementOffset] += displacedWater / (displacementRadius * 2);
            }
        }

        if (rightIndex < wave.cellCount - displacementRadius - displacementOffset)
        {
            for (int i = 0; i < displacementRadius; i++)
            {
                wave.rho[rightIndex + i + displacementOffset] += displacedWater / (displacementRadius * 2);
            }
        }

        var zee = Mathf.Sign(Vector3.Cross(transform.up, Vector3.up).z);
        var amount = Vector3.Angle(transform.up, Vector3.up) / 180;
        rb.AddTorque(zee * rightingForce * amount * Time.deltaTime);

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Min(rb.velocity.y, maxSpeed));
    }
}