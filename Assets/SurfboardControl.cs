using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfboardControl : MonoBehaviour
{

    public bool left;
    Rigidbody2D rb;
    Rigidbody2D torsoRb;
    public float downForce;
    public float separation;
    public Rigidbody2D headRB;
    public float duckForce;
    public FixedJoint2D leftShin, rightShin;
    public HingeJoint2D torso;
    public float regularBuyoancy, duckBuyoancy, regularSeparation, duckSeparation;
    public Surfboard board;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        torsoRb = torso.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {



        var leftButton = left ? KeyCode.A : KeyCode.J;
        var rightButton = left ? KeyCode.D : KeyCode.L;
        var downButton = left ? KeyCode.S : KeyCode.K;
        var upButton = left ? KeyCode.W : KeyCode.I;

        if (Input.GetKey(leftButton))
        {
            rb.AddForceAtPosition(Vector2.down * downForce * Time.deltaTime, transform.position - transform.right * separation);
        }

        if (Input.GetKey(rightButton))
        {
            rb.AddForceAtPosition(Vector2.down * downForce * Time.deltaTime, transform.position + transform.right * separation);
        }

        if (Input.GetKey(downButton))
        {
            headRB.AddForce(Vector2.down * duckForce * Time.deltaTime);
            torso.enabled = true;
            leftShin.enabled = false;
            rightShin.enabled = false;
            board.density = duckBuyoancy;
            separation = duckSeparation;
        }
        if( Input.GetKeyUp(downButton) )
        {
            torso.enabled = false;
            leftShin.enabled = true;
            rightShin.enabled = true;
            leftShin.transform.rotation = Quaternion.identity;
            rightShin.transform.rotation = Quaternion.identity;
            board.density = regularBuyoancy;
            separation = regularSeparation;
        }


        if (Input.GetKeyDown(upButton))
        {
            StartCoroutine(Jump());
        }
    }

    public float jumpVelocity;

    IEnumerator Jump()
    {
        // t = (-2 * v)/ a
        // float jumpTime = 
        leftShin.enabled = false;
        rightShin.enabled = false;
        torsoRb.velocity = Vector2.up * jumpVelocity;
        yield return null;
    }

}

