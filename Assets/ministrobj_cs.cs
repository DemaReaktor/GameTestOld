using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ministrobj_cs : MonoBehaviour
{
    public VoidSystem_cs vs;
    public Transform miniobj;
    Transform[] mini = new Transform[5100];
    Vector3[] mv = new Vector3[5100];
  //  [HideInInspector] Vector3 vec;
    int k;
    float[] time = new float[5100];
    public float scale;
    void Start()
    {
        vs = this.transform.GetComponent<VoidSystem_cs>();
    }

    void Update()
    {
        for (int i = 0; i <k; i++)
            time[i] += Time.deltaTime;
        for (int i = 0; i < k; i++)
          //  if (mini[i] != null)
            {
                mini[i].transform.localScale = new Vector3(scale*(1-time[i]), scale * (1 -  time[i]), scale * (1 -  time[i]));

                mv[i].y -= 9.8f * 0.975f * Time.deltaTime;
                mini[i].transform.Translate(mv[i] * Time.deltaTime); 
            vs.telep(mini[i].transform);
            if (!vs.gravityper0(mini[i].transform.position)||time[i]>=1)
                {
                    Destroy(mini[i].gameObject);
                    for (int ii = i; ii < k - 1; ii++)
                    {
                        mv[ii] = mv[ii+1];
                        time[ii] = time[ii+1];
                        mini[ii] = mini[ii+1];

                    }
                        k--;
                mini[k] = null;
                   // if(mini[k]!=null)
                      //  Destroy(mini[k].gameObject);
                }
        }
    }
    public void Addmini(Vector3 p,Vector3 nap) {
        for (int i = k; i < k+100; i++)
        {
            mini[i] = Instantiate(miniobj, p, new Quaternion());
            mv[i] = new Vector3(Random.Range(-100, 100)*0.005f*nap.x, Random.Range(-100, 100) * 0.005f * nap.y, Random.Range(-100, 100)*0.005f*nap.z);
            mini[i].transform.Translate(mv[i]*0.01f);
            mini[i].transform.localScale = new Vector3(scale,scale,scale);
            time[i] = 0;
        }
        k += 100;
    }
}
