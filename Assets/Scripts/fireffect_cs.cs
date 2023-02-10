using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireffect_cs : MonoBehaviour
{
     [HideInInspector] public  VoidSystem_cs vs;
    GameObject[] f;
    //Vector3[] pos = new Vector3[200];
     float[] v ;
  [SerializeField]  float d= 0.21f;
  [SerializeField]  float dr= 0.03f;
  [SerializeField]  float r= 0.014f;
   float[] df;
    [SerializeField] GameObject obj;
     public float speed= 0.5f;
     public int k;


    void Start()
    {
        k = 0;

        if (vs.xml.nal.yakistSpets == 0)
        {
            f = new GameObject[10];
            v = new float[10];
            df = new float[10];
            for (int i = 0; i < 10; i++)
            {
                f[i] = Instantiate(obj, transform.position, new Quaternion(), transform);
                f[i].transform.localScale *=3;
                v[i] = Random.Range(50, 100) * 0.01f * speed;
                df[i] = Random.Range((d - dr) * 100, Random.Range((d - dr) * 100, (d + dr) * 100)) * 0.01f;
            }
        }
        else
        { 
        f = new GameObject[200];
        v = new float[200];
        df = new float[200];
        for (int i = 0; i < 200; i++)
        {
            f[i] = Instantiate(obj, transform.position, new Quaternion(), transform);
            v[i] = Random.Range(50, 100) * 0.01f * speed;
            df[i] = Random.Range((d - dr) * 100, Random.Range((d - dr) * 100, (d + dr) * 100)) * 0.01f;
        }
    }
    }

    void Update()
    {
        if (k != 0)
            k+=6;
        for (int i = 0; i < (vs.xml.nal.yakistSpets == 0?10: 200); i++) {
            f[i].transform.Translate(transform.forward*v[i] * Time.deltaTime);
            float dd = VoidSystem_cs.Dist(f[i].transform.position, transform.position);
            if (dd>df[i]&&Random.Range(0,k)==0)
            {
                f[i].transform.position = transform.position + new Vector3(Random.Range(-r*100,r*100)*0.01f, Random.Range(-r * 100, r * 100) * 0.01f, Random.Range(-r * 100, r * 100) * 0.01f);
                df[i] = Random.Range((d - dr) * 100, Random.Range((d - dr) * 100, (d + dr) * 100)) * 0.01f;
            }
            dd /=df[i] ;//(df[i] -d)*0.4f/df[i]+0.6f;
            f[i].transform.GetComponent<MeshRenderer>().material.SetColor("_C",new Color(dd>1?1:dd,0,1,dd < 1 ? 1 :2-dd));
        }
    }
}
