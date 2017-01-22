using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{

    public ScoreKeeper score;
    public GameObject[] numbers;
    public bool left;

    // Use this for initialization
    void Start()
    {
        score = FindObjectOfType<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        int scoreValue;

        if( left )
        {
            scoreValue = score.playerOneScore;
        }
        else
        {
            scoreValue = score.playerTwoScore;
        }

        for(int i = 0; i < numbers.Length; i ++)
        {
            if( i == scoreValue)
            {
                numbers[i].SetActive(true);
            }
            else
            {
                numbers[i].SetActive(false);
            }
        }

    }
}
