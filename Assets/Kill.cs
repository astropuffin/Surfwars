using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {

    public GameObject enemyBoard;
    public ParticleSystem particles;
    private float bleedTime = .3f;
    public FixedJoint2D hinge1, hinge2;
	gameOver gameOverScript;
	private float gameOverTime = 4;
	private bool dead = false;
    public float hitForceFactor;
    public float iFrames;
    public float blinkRate;
    private bool invincible;
    public SurfboardControl boardControl;

	void Start()
	{
		gameOverScript = FindObjectOfType<gameOver> ();
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach( var p in players )
        {
            if (p != gameObject)
                enemyBoard = p;
        }
	}


    IEnumerator bleed()
    {
        particles.enableEmission = true;
        yield return new WaitForSeconds(bleedTime);
        particles.enableEmission = false;
    }

	IEnumerator gameOverScreen(bool leftPlayer)
	{
		yield return new WaitForSeconds (gameOverTime);
		gameOverScript.ShowGameOverScreen (leftPlayer);
	}


    IEnumerator Blink()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        while (true)
        {
            foreach (var sr in srs)
            {
                var color = sr.color;
                color.a = 0.5f;
                sr.color = color;
            }
            yield return new WaitForSeconds(blinkRate);

            foreach (var sr in srs)
            {
                var color = sr.color;
                color.a = 1f;
                sr.color = color;
            }

            yield return new WaitForSeconds(blinkRate);
        }
    }


    IEnumerator EnableIframes()
    {
        invincible = true;
        var co = StartCoroutine(Blink());
        yield return new WaitForSeconds(iFrames);
        StopCoroutine( co );
        invincible = false;

        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in srs)
        {
            var color = sr.color;
            color.a = 1f;
            sr.color = color;
        }

    }


    public void KillMessage( Collision2D other )
    {
		if (dead)
			return;
		


        if (other.transform.gameObject == enemyBoard && !invincible)
        {
            Vector2 hitForce = other.relativeVelocity * hitForceFactor;
            Vector2 leftPos = hinge1.connectedAnchor;
            Vector2 rightPos = hinge2.connectedAnchor;

            if( boardControl.left )
            {
                leftPos.x += hitForce.magnitude;
                rightPos.x += hitForce.magnitude;
            }
            else
            {
                leftPos.x -= hitForce.magnitude;
                rightPos.x -= hitForce.magnitude;
            }

            if( hinge1.enabled)
                hinge1.connectedAnchor = leftPos;

            hinge2.connectedAnchor = rightPos;

            StartCoroutine(EnableIframes());

            if (Mathf.Abs(hinge2.connectedAnchor.x) < 20 && Mathf.Abs(hinge1.connectedAnchor.x) < 20)
            {
                return;
            }

            var joints = GetComponentsInChildren<Joint2D>();
            foreach( var j in joints )
            {
                j.enabled = false;
            }

            Debug.Log("Dead");

            dead = true;
            StartCoroutine(bleed());
            hinge1.enabled = false;
            hinge2.enabled = false;
			StartCoroutine (gameOverScreen (gameObject.GetComponent<SurfboardControl>().left));

		}
    }
}
