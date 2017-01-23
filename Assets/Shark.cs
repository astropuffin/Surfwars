using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {

    public GameObject jaw;
    public bool swim, eat;
    public float swimTime;
    public float waitTime;
    public float swimDistance;
    public GameObject leftEdge, rightEdge;
    public float height;

    IEnumerator Swim()
    {
        while(true)
        {
            float swimTimer = 0;
            Vector3 init = transform.position;
            Vector3 destination = Vector3.Lerp(leftEdge.transform.position, rightEdge.transform.position, Random.value);

            destination.y = height * Random.value + 15;
            while (swimTimer < swimTime)
            {
                swimTimer += Time.deltaTime;
                transform.position = Vector3.Lerp(init, destination, (swimTimer) / swimTime);
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }


    IEnumerator Eat( GameObject Target)
    {
        yield break;
        //w
        //transform.position = Vector3.Lerp()

    }

    void Start()
    {
        StartCoroutine(Swim());
    }
}
