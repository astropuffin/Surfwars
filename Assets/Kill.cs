using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {

    public GameObject enemyBoard;
    public ParticleSystem particles, particles1, particles2;
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
    bool hit;

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
        particles1.enableEmission = true;
        particles2.enableEmission = true;
        yield return new WaitForSeconds(bleedTime);
        particles.enableEmission = false;
        particles1.enableEmission = false;
        particles2.enableEmission = false;
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

            StartCoroutine(EnableIframes());
            StartCoroutine(bleed());

            if (!hit)
            {
                hinge1.enabled = false;
                hit = true;
                return;
            }

            var joints = GetComponentsInChildren<Joint2D>();
            foreach( var j in joints )
            {
                j.enabled = false;
            }

            dead = true;
            hinge1.enabled = false;
            hinge2.enabled = false;
			StartCoroutine (gameOverScreen (gameObject.GetComponent<SurfboardControl>().left));

		}
    }
}
