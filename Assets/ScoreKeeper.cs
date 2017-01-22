using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour {
	public int playerOneScore = 0;
	public int playerTwoScore = 0;
	public int maxScore = 1;

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("Dudes", LoadSceneMode.Additive);
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

		if (DidPlayerWin (firstPlayer)) {
			StartCoroutine (StartGameOver (5));
		}
	}

	public bool DidPlayerWin(bool leftPlayer)
	{
		if (leftPlayer) {
			if (playerOneScore >= maxScore)
				return true;
		} else {
			if (playerTwoScore >= maxScore)
				return true;
		}

		return false;
	}

	IEnumerator StartGameOver(int WaitTime)
	{
		yield return new WaitForSeconds(WaitTime);
		playerOneScore = playerTwoScore = 0;
			
	}
}
