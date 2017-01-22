using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Score : MonoBehaviour {
	public int playerOneScore = 0;
	public int playerTwoScore = 0;
	public UnityEngine.UI.Image p1ScoreRect;
	public UnityEngine.UI.Image p2ScoreRect;

	public Sprite[] numberedSprites;

	// Use this for initialization
	void Start () {

		// Display scores
		ShowCurrentScore (true);
		ShowCurrentScore (false);

		// Begin countdown to respawn.
		StartCoroutine(RespawnDudes (5));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncrementPlayerScore(bool firstPlayer)
	{
		if (firstPlayer)
			playerOneScore++;
		else
			playerTwoScore++;
	}

	public void ShowCurrentScore(bool firstPlayer)
	{
		if (firstPlayer) {
			Debug.Log("P1 Sprite is: " + numberedSprites[playerOneScore]);
			p1ScoreRect.overrideSprite = numberedSprites [playerOneScore];
		} else {
			Debug.Log("P2 Sprite is: " + numberedSprites[playerTwoScore]);
			p2ScoreRect.overrideSprite = numberedSprites [playerTwoScore];
		}
			
	}

	IEnumerator RespawnDudes(int waitSeconds)
	{
		yield return new WaitForSeconds (waitSeconds);
		Debug.Log ("Spawn the shit outta some dudes");

		// Delete existing dudes
		var dudes = GameObject.FindGameObjectsWithTag ("Player");
		foreach (var dude in dudes)	{ Destroy (dude); }

		// Find all duder spawners
		var spawners = FindObjectsOfType<DudeSpawner> ();

		// Spawn da dudes
		foreach (var spawn in spawners) {
			spawn.GetComponent<DudeSpawner> ().SpawnMyDude ();
		}

		// Disable da fukken game over screen.
		SceneManager.UnloadSceneAsync ("ScoreScreen");

		//SceneManager.LoadScene("Dudes");
		//gameOverTextBox.IsActive();
	}
}
