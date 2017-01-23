using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOver : MonoBehaviour {

	//public GameObject gameOverText;
	private bool leftPlayerScored;
	public GameObject leftPlayerPrefab;

	public void ShowGameOverScreen(bool isLeftPlayer)
	{
		//gameOverText.SetActive (true);
		leftPlayerScored = isLeftPlayer;
		SceneManager.LoadScene ("ScoreScreen",LoadSceneMode.Additive);
		FindObjectOfType<ScoreKeeper>().IncrementPlayerScore(!isLeftPlayer);
		//SceneManager.UnloadSceneAsync ("main");
	}


}
