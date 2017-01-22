using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeSpawner : MonoBehaviour {
	public GameObject dude1Prefab;
    public GameObject dude2Prefab;


    // Use this for initialization
    void Start () {
        SpawnMyDude();
    }

	public void SpawnMyDude()
	{
        Instantiate(dude1Prefab);
        Instantiate(dude2Prefab);
        FindObjectOfType<CameraFollow>().SetPlayers();
    }
}
