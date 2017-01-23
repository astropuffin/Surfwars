using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveSolver : MonoBehaviour
{

    public int cellCount;
    public GameObject leftDude, rightDude;
    public double speed;
    public LineRenderer line;
    public float heightScale;

    float dudeDistance;
    double DX;
    double[] newVels;
    double[] newRho;
    public double[] vels;
    public double[] rho;

    // Use this for initialization
    void Start()
    {
        dudeDistance = Vector3.Distance(leftDude.transform.position, rightDude.transform.position);
        DX = dudeDistance / cellCount;
        newVels = new double[cellCount];
        newRho = new double[cellCount];
        vels = new double[cellCount];
        rho = new double[cellCount];

        line.numPositions = cellCount;
        for (int i = 0; i < cellCount; i++)
        {
            rho[i] = heightScale;
        }
    }

    double Clamp(double value, double min, double max)
    {
        if (value < min)
            return min;
        else if (value > max)
            return max;
        else
            return value;
    }

    void Solve()
    {
        int N = cellCount;
        const int DIV = 3;
        const double SMOOTHING = 1.05;
        const double HALFPKT = 0.5 * (SMOOTHING / DIV);
        double LFTWALL = (1 + HALFPKT) * DX + 1e-12;
        double RGTWALL = N * DX - LFTWALL;
        double DT = Time.deltaTime;
        double CSQ = speed * speed ;

        for (int i = 0; i < N; i++)
        {
            newVels[i] = 0;
            newRho[i] = 0;
        }

        //memset(newRho, 0, N * sizeof(double));

        double x, gx, fx, u, nx, left, right, f1, f2;
        int idx, li, ri;

        for (int i = 1; i < N - 1; ++i)
            for (int j = 0; j < DIV; ++j)
            {
                x = (i + (j + 0.5) / DIV) * DX;
                gx = x / DX - 0.5;
                idx = (int)gx;
                fx = gx - idx;
                u = (1 - fx) * vels[idx] + fx * vels[idx + 1];

                nx = Clamp(x + DT * u, LFTWALL, RGTWALL);
                left = nx / DX - HALFPKT;
                right = nx / DX + HALFPKT;
                li = (int)left;
                ri = (int)right;
                f1 = (ri - left) / SMOOTHING;
                f2 = (right - ri) / SMOOTHING;
                newRho[li] += rho[i] * f1;
                newRho[ri] += rho[i] * f2;
                newVels[li] += vels[i] * f1;
                newVels[ri] += vels[i] * f2;
            }
        for (int i = 0; i < N; i++)
        {
            vels[i] = newVels[i];
            rho[i] = newRho[i];
        }

        //memcpy(vels, newVels, N * sizeof(double));
        //memcpy(rho, newRho, N * sizeof(double));
        double s = CSQ * DT / DX * 0.5;
        vels[1] -= (rho[2] - rho[1]) * s;
        for (int i = 2; i < N - 2; ++i)
            vels[i] -= (rho[i + 1] - rho[i - 1]) * s;
        vels[N - 2] -= (rho[N - 2] - rho[N - 3]) * s;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Solve();
        for(int i = 0; i < cellCount; i ++)
        {
            line.SetPosition(i, new Vector3(i * (dudeDistance / cellCount),(float) rho[i]));
        }
    }
}
