using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zariadobj_cs : MonoBehaviour
{
    public VoidSystem_cs vs;
   // public player pl;
    public zariad_cs zar;
    public Cube_creating cc;

    public Transform cam;
    float zariad;
    public Transform player;
     Vector3[] pos = new Vector3[2000];
    Vector3[] kpos=new Vector3[100];
    GameObject[] n = new GameObject[20];
    GameObject[,] nclon = new GameObject[8,20];
    Vector3 v;
    public Material mat;
    float time;
    Vector3[] p = new Vector3[2000];
    Vector3[] pm = new Vector3[2000];
    bool j;
    Vector3 zmina;
    GameObject[] clon = new GameObject[8];
    Vector3[] clonp=new Vector3[8];
    //bool[,,] polez = new bool[50, 50, 50];

    void Start()
    {
        int p = 0;
        Vector3Int[] mozh = new Vector3Int[1500];
        for (int y = 0; y < 6; y++)
            for (int z = 0; z < 200; z++)
                for (int x = 0; x < 200; x++)
                    if (p < 1500 && cc.c[x, y, z])
                    {
                        float kyt = 180 / Mathf.PI * Mathf.Atan2(x - player.transform.position.x, z - player.transform.position.z);
                        if (kyt < 0)
                            kyt += 360;
                        float kytc = Mathf.Abs(kyt - cam.transform.localEulerAngles.y);
                        if (kytc > 60 && kytc < 300)
                        {
                            mozh[p] = new Vector3Int(x, y, z);
                            p++;
                        }
                    }
        zmina = mozh[Random.Range(0, 1500)] + new Vector3(0.5f, 0.5f, 0.5f) - transform.position;
        transform.Translate(zmina);
        GameObject f = new GameObject();
        for (int k=0;k<20;k++)
        {
             v = new Vector3(Random.Range(-100,101), Random.Range(-100, 101), Random.Range(-100, 101));
            v = new Vector3(v.x/ VoidSystem_cs.Dist(new Vector3(), v), v.y / VoidSystem_cs.Dist(new Vector3(), v), v.z / VoidSystem_cs.Dist(new Vector3(), v));
            pos[k*100] = transform.position;
            kpos[0] = pos[k * 100 ];
            j = false;
            for (int i = 1; i < 100; i++)
            {
                pm[k * 100 + i] = new Vector3();
                if (!j)
                    pos[k * 100 + i] = transform.position +i * v*0.05f +new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)) * 0.02f;
else
                    pos[k * 100 + i] = pos[k * 100 + i - 1];
                if (!vs.gravityper0(pos[k * 100 + i])&&!j)
                {
                    j = true;
                    pos[k * 100 + i] = pos[k * 100 + i - 1];
                }
                kpos[i] = pos[k*100+i];
            }
            n[k] = Instantiate(f,transform.position,new Quaternion(),this.transform);
            n[k].AddComponent(typeof(LineRenderer));
            n[k].GetComponent<LineRenderer>().positionCount = 100;
            n[k].GetComponent<LineRenderer>().SetPositions(kpos);
            n[k].GetComponent<LineRenderer>().material=mat;
            n[k].GetComponent<LineRenderer>().startWidth = 0.02f;
            n[k].GetComponent<LineRenderer>().endWidth = 0.01f;
        }
        for (int i = 0; i < 8; i++)
        {
            clon[i] = Instantiate(this.gameObject, transform.position, new Quaternion());
            Destroy(clon[i].GetComponent<zariadobj_cs>());
            switch (i)
            {
                case 0:
                    clonp[i]=new Vector3(200,0,0);
                    break;
                case 1:
                    clonp[i] = new Vector3(200, 0, 200);
                    break;
                case 2:
                   clonp[i] = new Vector3(200, 0, -200);
                    break;
                case 3:
                    clonp[i] = new Vector3(0, 0, 200);
                    break;
                case 4:
                    clonp[i] = new Vector3(0, 0, -200);
                    break;
                case 5:
                   clonp[i] = new Vector3(-200, 0, 200);
                    break;
                case 6:
                    clonp[i] = new Vector3(-200, 0, 0);
                    break;
                case 7:
                    clonp[i] = new Vector3(-200, 0, -200);
                    break;
            }
            clon[i].transform.Translate(clonp[i]);
            for (int ii = 0; ii < 20; ii++)
            {
                nclon[i, ii] = clon[i].transform.GetChild(ii).gameObject;
                Vector3[] pp=new Vector3[100];
                for (int i2 = 0; i2 < 100; i2++)
                    pp[i2] = n[ii].GetComponent<LineRenderer>().GetPosition(i2) + clonp[i];
                nclon[i,ii].GetComponent<LineRenderer>().SetPositions(pp);
            }
        }
        Destroy(f);
        zmina = new Vector3();
    }

    void Update()
    {


        if (VoidSystem_cs.Dist(transform.position, player.transform.position) <= 2)
        {
            zariad = Random.Range(5, Random.Range(35, 98));
            zar.zariadka += zariad;
            if (zar.zariadka > 100)
                zar.zariadka = 100;
            int p = 0;
            Vector3Int[] mozh = new Vector3Int[1500];
            for (int y = 0; y < 6; y++)
                for (int z = 0; z < 200; z++)
                    for (int x = 0; x < 200; x++)
                        if (p < 1500 && cc.c[x,y,z])
                        {
                            float kyt = 180 / Mathf.PI * Mathf.Atan2(x - player.transform.position.x, z - player.transform.position.z);
                            if (kyt < 0)
                                kyt += 360;
                            float kytc = Mathf.Abs(kyt - cam.transform.localEulerAngles.y);
                            if (kytc > 60 && kytc < 300)
                            {
                                mozh[p] = new Vector3Int(x, y, z);
                                p++;
                            }
                        }
            zmina = mozh[Random.Range(0, 1500)] + new Vector3(0.5f, 0.5f, 0.5f)-transform.position;
            transform.Translate(zmina);

            for (int i = 0; i < 2000; i++)
                pos[i] += zmina;
        }

        time += Time.deltaTime;

        if (time >= 0.25f) {
            for (int i = 0; i < 2000; i++)
            {
                pm[i] = p[i];
                p[i] = new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)) * 0.06f;
            }
            time = 0;
            }
        for (int k = 0; k < 20; k++)
        {
            kpos[0] = transform.position;
for(int i = 1; i < 100; i++)
                            kpos[i] = pos[k * 100 + i]+p[k*100+i] * time+pm[k * 100 + i]*(0.25f-time);
            n[k].GetComponent<LineRenderer>().SetPositions(kpos);
        }
        for (int i = 0; i < 8; i++)
        {
            clon[i].transform.Translate(zmina);

            for (int ii = 0; ii < 20; ii++)
            {
                Vector3[] pp = new Vector3[100];
                for (int i2 = 0; i2 < 100; i2++)
                    pp[i2] = n[ii].GetComponent<LineRenderer>().GetPosition(i2) + clonp[i];
                nclon[i, ii].GetComponent<LineRenderer>().SetPositions(pp);
            }
        }
        /*  for (int i = 0; i < 8; i++)
          {
              clon[i] = Instantiate(this.gameObject, new Vector3(), new Quaternion());
              clon[i].GetComponent<LineRenderer>().SetPositions(kpos);
          }*/
    }

}
