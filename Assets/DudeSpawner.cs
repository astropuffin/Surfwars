using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeSpawner : MonoBehaviour {
	public GameObject dudePrefab;

	// Use this for initialization
	void Start () {
        SpawnMyDude();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnMyDude()
	{
		Instantiate (dudePrefab);
	}
}
