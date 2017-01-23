using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfboardControl : MonoBehaviour
{

    public bool left;
    Rigidbody2D rb;
    public Rigidbody2D torsoRb;
    public float downForce;
    public float separation;
    public Rigidbody2D headRB;
    public float duckForce;
    public FixedJoint2D leftShin, rightShin;
    public HingeJoint2D torso;
    public float regularBuyoancy, duckBuyoancy, regularSeparation, duckSeparation;
    public Surfboard board;
    bool jumping;
    Kill kill;


    // Use this for initialization
    void Start()
    {
        torsoRb = torso.connectedBody;
        rb = GetComponent<Rigidbody2D>();
        kill = GetComponent<Kill>();
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

        if (Input.GetKey(downButton) && !jumping)
        {
            headRB.AddForce(Vector2.down * duckForce * Time.deltaTime);
            torso.enabled = true;
            leftShin.enabled = false;
            rightShin.enabled = false;
            board.density = duckBuyoancy;
            separation = duckSeparation;
        }
        if( Input.GetKeyUp(downButton) && !jumping )
        {
            torso.enabled = false;
            if(!kill.hit)
                leftShin.enabled = true;

            rightShin.enabled = true;
            leftShin.transform.rotation = Quaternion.identity;
            rightShin.transform.rotation = Quaternion.identity;
            board.density = regularBuyoancy;
            separation = regularSeparation;
        }


        if (Input.GetKeyDown(upButton) && !jumping)
        {
            StartCoroutine(Jump());
        }
    }

    public float jumpVelocity;
    public float gravity;

    IEnumerator Jump()
    {
        jumping = true;
        // t = (-2 * v)/ a
        float jumpTime = -2 * jumpVelocity / (-9.81f * gravity);
        leftShin.enabled = false;
        rightShin.enabled = false;
        float timer = 0;
        float initialy = rb.position.y;
        while( timer < jumpTime)
        {
            timer += Time.deltaTime;
            Vector3 pos = rb.position;
            pos.x = rb.position.x;
            pos.y = initialy + jumpVelocity * timer + (0.5f) * -9.81f * gravity * timer * timer;

            torsoRb.position = pos;

            yield return new WaitForFixedUpdate();
        }

        if( !kill.hit )
            leftShin.enabled = true;

        rightShin.enabled = true;
        jumping = false;

    }

}

