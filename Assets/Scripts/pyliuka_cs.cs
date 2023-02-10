using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pyliuka_cs : MonoBehaviour
{
    public VoidSystem_cs vs;

    [SerializeField] Transform player;

    [SerializeField] GameObject obj;
    GameObject[] pul = new GameObject[200];
    float[] t = new float[200];
    float[] time = new float[200];
    float[] v = new float[200];
    Vector3[] nap = new Vector3[200];

    void Start()
    {
        vs = this.transform.GetComponent<VoidSystem_cs>();
        for (int i = 0; i < 200; i++)
        {
            time[i] = 0;
            t[i] = Random.Range(3, 165) * 0.01f;
            v[i] = Random.Range(1, 18) * 0.05f;
            nap[i] = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
            if (nap[i] != new Vector3())
                nap[i] /= VoidSystem_cs.Dist(new Vector3(), nap[i]);

            pul[i] = Instantiate(obj, new Vector3(Random.Range(-600, 600) * 0.1f, Random.Range(3, 85)  * Random.Range(1, 10) * 0.01f, Random.Range(-600, 600) * 0.1f) * Random.Range(5, 100) * 0.01f + player.position-new Vector3(0,7,0),new Quaternion(),transform);
        }
    }

    void Update()
    {
        for (int i = 0; i < 200; i++)
        {
            time[i] += Time.deltaTime;
            if (time[i] >= t[i])
            {
                time[i] = 0;
                t[i] = Random.Range(2,65)*0.1f;
                v[i] = Random.Range(1,12)*0.1f;
                nap[i] = new Vector3(Random.Range(-100,100), Random.Range(-100, -1), Random.Range(-100, 100));
                nap[i]/= VoidSystem_cs.Dist(new Vector3(),nap[i]);
            }
            pul[i].transform.Translate((nap[i]*v[i]+new Vector3(Random.Range(-5,5)*0.05f, Random.Range(-5, 5) * 0.05f, Random.Range(-5, 5) * 0.05f)) * Time.deltaTime);
            if (!vs.gravityper(pul[i].transform.position, 0.001f) || VoidSystem_cs.Dist(player.position, pul[i].transform.position) <= 0.5f)
                pul[i].transform.position = new Vector3(Random.Range(-600, 600) * 0.1f, Random.Range(3, 85) * Random.Range(1, 10)  * 0.01f, Random.Range(-600, 600) * 0.1f) * Random.Range(5, 100) * 0.01f + player.position;
            if(Mathf.Abs( player.position.x-pul[i].transform.position.x)>30|| Mathf.Abs(player.position.z - pul[i].transform.position.z)>30)
            {
                if (player.position.x > 30 + pul[i].transform.position.x)
                    pul[i].transform.Translate(new Vector3(30,0,0));
                if (player.position.x  +30 < pul[i].transform.position.x)
                    pul[i].transform.Translate(new Vector3(-30, 0, 0));
                if (player.position.z > 30 + pul[i].transform.position.z)
                    pul[i].transform.Translate(new Vector3(0, 0, 30));
                if (player.position.z + 30 < pul[i].transform.position.z)
                    pul[i].transform.Translate(new Vector3(0, 0, -30));
            }
            vs.telep(pul[i].transform);
        }
    }
}
