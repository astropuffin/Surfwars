using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageParent : MonoBehaviour {

    public Kill parent;

	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<Kill>();
	}
	
    void OnCollisionEnter2D(Collision2D c)
    {
        if( parent )
            parent.KillMessage(c);
    }

}
