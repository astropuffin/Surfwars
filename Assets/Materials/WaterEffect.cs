using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaterEffect : MonoBehaviour {

    public Material mat;
    public LineRenderer line;
    public WaveSolver wave;
    public Camera cam;

    Vector3[] linePos = new Vector3[100];
    float[] heights = new float[100];

    void Start()
    {

    }
    
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        line = FindObjectOfType<LineRenderer>();
        wave = FindObjectOfType<WaveSolver>();
        line.GetPositions(linePos);
        for(int i =0; i < 100; i ++)
        {
            heights[i] = linePos[i].y;
        }

        mat.SetFloat("xLeft", line.GetPosition(0).x);
        mat.SetFloat("xRight", line.GetPosition(99).x);
        mat.SetFloat("yLeft", line.GetPosition(0).y);
        mat.SetFloat("yRight", line.GetPosition(99).y);

        mat.SetFloatArray("heights", heights);

        Vector3 lowerLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
        Vector3 upperRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.transform.position.z));

        mat.SetVector("lowerLeft", lowerLeft);
        mat.SetVector("upperRight", upperRight);
        Vector3[] corners = new Vector3[4];

        Graphics.Blit(src, dst, mat);
    }
    
}
