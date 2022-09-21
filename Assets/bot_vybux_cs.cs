using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot_vybux_cs : MonoBehaviour
{
    public player pl;
    public zariad_cs zar;
    public roznoseff_cs rozeff;

    Vector3[] tochky = new Vector3[322];
    float time;
    Mesh m;
    public float d;
    bool bvb;
    public Transform player;

    void Start()
    {
        bvb = false;
            int[] tryk = new int[1920];
            tochky[0] = new Vector3(0, -0.5f, 0);
            tochky[321] = new Vector3(0, 0.5f, 0);
            for (int y = 0; y < 16; y++)
                for (int x = 0; x < 20; x++)
                    tochky[x + y * 20 + 1] = new Vector3(Mathf.Cos(x * 0.314f) * Mathf.Sqrt(0.25f - (0.0588f * (y + 1) - 0.5f) * (0.0588f * (y + 1) - 0.5f)), -0.5f + 0.0588f * (y + 1), Mathf.Sin(x * 0.314f) * Mathf.Sqrt(0.25f - (0.0588f * (y + 1) - 0.5f) * (0.0588f * (y + 1) - 0.5f)));//Mathf.Sqrt(8f*y-y*y*-9)*0.1f

            for (int t = 0; t < 20; t++)
            {
                tryk[t * 3] = 0;
                tryk[t * 3 + 1] = t + 1;
                if (t != 19)
                    tryk[t * 3 + 2] = t + 2;
                else
                    tryk[t * 3 + 2] = 1;


                if (t != 19)
                {
                    tryk[t * 3 + 1862] = t + 302;
                    tryk[t * 3 + 1861] = 321;
                    tryk[t * 3 + 1860] = t + 301;
                }
                else
                {
                    tryk[t * 3 + 1862] = 321;
                    tryk[t * 3 + 1861] = t + 301;
                    tryk[t * 3 + 1860] = 301;
                }
            }
            for (int t = 1; t <= 300; t++)
            {
                tryk[t * 3 + 59] = t;
                if (t % 20 != 0)
                    tryk[t * 3 + 58] = t + 1;
                else
                    tryk[t * 3 + 58] = t - 19;
                tryk[t * 3 + 57] = t + 20;

                if (t % 20 != 0)
                {
                    tryk[t * 3 + 959] = t + 1;
                    tryk[t * 3 + 958] = t + 21;
                    tryk[t * 3 + 957] = t + 20;
                }
                else
                {
                    tryk[t * 3 + 957] = t + 1;
                    tryk[t * 3 + 958] = t - 19;
                    tryk[t * 3 + 959] = t + 20;
                }
            }
            m = new Mesh();
            m.vertices = tochky;
            m.triangles = tryk;
            GetComponent<MeshFilter>().mesh = m;
        

    }

    void Update()
    {
        time += Time.deltaTime;
        if (time <= 1.5f)
            for (int y = 1; y < 15; y += 2)
                for (int x = 0; x < 20; x += 2)
                    tochky[x + y * 20 + 1] = new Vector3((time / 6 + 1) * Mathf.Cos(x * 0.314f) * Mathf.Sqrt(0.25f - (0.0588f * (y + 1) - 0.5f) * (0.0588f * (y + 1) - 0.5f)), (-0.5f + 0.0588f * (y + 1)) * (time / 6 + 1), (time / 6 + 1) * Mathf.Sin(x * 0.314f) * Mathf.Sqrt(0.25f - (0.0588f * (y + 1) - 0.5f) * (0.0588f * (y + 1) - 0.5f)));
        else
        if(time<=2)
        {
            for (int y = 1; y < 15; y += 2)//1.25  1.5
                for (int x = 0; x < 20; x += 2)//0  2
                    tochky[x + y * 20 + 1] = new Vector3((5-time*2.5f) * Mathf.Cos(x * 0.314f) * Mathf.Sqrt(0.25f - (0.0588f * (y + 1) - 0.5f) * (0.0588f * (y + 1) - 0.5f)), (-0.5f + 0.0588f * (y + 1)) * (1.75f - time * 0.375f), (1.75f - time * 0.375f) * Mathf.Sin(x * 0.314f) * Mathf.Sqrt(0.25f - (0.0588f * (y + 1) - 0.5f) * (0.0588f * (y + 1) - 0.5f)));

            for (int y = 2; y < 15; y += 2)
                for (int x = 1; x < 20; x += 2)
                    tochky[x + y * 20 + 1] = new Vector3(((time-1.5f)*2*(d-1)+1) * Mathf.Cos(x * 0.314f) * Mathf.Sqrt(0.25f - (0.0588f * (y + 1) - 0.5f) * (0.0588f * (y + 1) - 0.5f)), (-0.5f + 0.0588f * (y + 1)) * ((time - 1.5f) * 2 * (d - 1) + 1), ((time - 1.5f) * 2 * (d - 1) + 1) * Mathf.Sin(x * 0.314f) * Mathf.Sqrt(0.25f - (0.0588f * (y + 1) - 0.5f) * (0.0588f * (y + 1) - 0.5f)));
        }


        if (time >= 2)
            VoidSystem_cs.Destr(this.gameObject);
        m.vertices = tochky;
        if (time >= 1.75f && !bvb)
        {
            bvb = true;
            float d = VoidSystem_cs.Dist(player.transform.position, transform.position);// Mathf.Sqrt((player.transform.position.x - transform.position.x) * (player.transform.position.x - transform.position.x) + (player.transform.position.y - transform.position.y) * (player.transform.position.y - transform.position.y) + (player.transform.position.z - transform.position.z) * (player.transform.position.z - transform.position.z));
            if (d < 8)
            {
                zar.zariadka -= zar.vybux * (1 - d * 0.125f);
                rozeff.roznos += 65 * (1 - d * 0.125f);
                pl.gravity = true;
                pl.V += (player.transform.position - transform.position + new Vector3(0, 0.5f, 0)) / d * 24 / (d + 1) + new Vector3(0, 2, 0);
            }
        }
    }
}
