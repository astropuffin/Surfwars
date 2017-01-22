using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

    public LineRenderer line;

    // Use this for initialization
    public bool left;
	void Start () {
        line = FindObjectOfType<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float sum = 0;
        Vector3 up;
        if( left )
        {
            Vector3 a = line.GetPosition(1);
            Vector3 b = line.GetPosition(2);
            var tangent = (a - b).normalized;
            up = Vector3.Cross(tangent, Vector3.forward);

            sum += a.y;
            sum += b.y;
            sum += line.GetPosition(3).y;
        }
        else
        {
            Vector3 a = line.GetPosition(97);
            Vector3 b = line.GetPosition(98);
            var tangent = (a - b).normalized;
            up = Vector3.Cross(tangent, Vector3.forward);

            sum += a.y;
            sum += b.y;
            sum += line.GetPosition(96).y;
        }
       
        Vector3 buoyPos = transform.position;
        buoyPos.y = sum /3;

        transform.position = Vector3.Lerp(transform.position, buoyPos, Time.deltaTime * 10 );
        transform.up = Vector3.Lerp(transform.up, up, Time.deltaTime * 2 );
	}
}
