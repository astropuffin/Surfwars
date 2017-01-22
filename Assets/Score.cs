using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Score : MonoBehaviour {

	public UnityEngine.UI.Image p1ScoreRect;
	public UnityEngine.UI.Image p2ScoreRect;
	private ScoreKeeper scoreKeeper;

	public Sprite[] numberedSprites;

	// Use this for initialization
	void Start () {
		scoreKeeper = FindObjectOfType<ScoreKeeper> ();
		Debug.Log ("Disable camera");
		FindObjectOfType<CameraFollow> ().enabled = false;

		// Display scores
		ShowCurrentScore (true);
		ShowCurrentScore (false);

		// Begin countdown to respawn.
		StartCoroutine(RespawnDudes (5));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowCurrentScore(bool firstPlayer)
	{
		if (firstPlayer) {
			Debug.Log("P1 Sprite is: " + numberedSprites[scoreKeeper.playerOneScore]);
			p1ScoreRect.overrideSprite = numberedSprites [scoreKeeper.playerOneScore];
		} else {
			Debug.Log("P2 Sprite is: " + numberedSprites[scoreKeeper.playerTwoScore]);
			p2ScoreRect.overrideSprite = numberedSprites [scoreKeeper.playerTwoScore];
		}
			
	}

	IEnumerator RespawnDudes(int waitSeconds)
	{
		yield return new WaitForSeconds (waitSeconds);
		Debug.Log ("Spawn the shit outta some dudes");

		// Delete existing dudes
		var dudes = GameObject.FindGameObjectsWithTag ("Player");
		foreach (var dude in dudes)	{ DestroyImmediate (dude); }

		// Find all duder spawners
		var spawners = FindObjectsOfType<DudeSpawner> ();

		// Spawn da dudes
		foreach (var spawn in spawners) {
			spawn.GetComponent<DudeSpawner> ().SpawnMyDude ();
		}

		FindObjectOfType<CameraFollow> ().enabled = true;
		// Disable da fukken game over screen.
		SceneManager.UnloadSceneAsync ("ScoreScreen");

		//SceneManager.LoadScene("Dudes");
		//gameOverTextBox.IsActive();
	}
}
