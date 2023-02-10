using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapOperator_cs : MonoBehaviour
{
    public VoidSystem_cs vs;

    public Material mat;
    [HideInInspector] public Material mat2;


   // public Transform zariad;
    public Transform player;
    public player pl;
    public int r;
    int n;
    int[] nnomer=new int[60];
    Vector4[] npos = new Vector4[60];
    float  max;
    float x, y;

    private void Start()
    {
      vs = this.transform.GetComponent<VoidSystem_cs>() ;
}

    void Update()
    {
        if (Input.GetKey(vs.xml.nal.Rvceb)&& vs.zar.map <= vs.zar.zariadka) { 
                max = max < 4 ? max + Time.deltaTime : 4;
            vs.zar.zariadka -= vs.zar.map * Time.deltaTime;
            if(vs.zar.map > vs.zar.zariadka)
                vs.cnv.zanovo("Режим <Всебачення>{статус: деактивовано;}.");
        }
        else
            max = max >0 ? max - Time.deltaTime : 0;
        if (Input.GetKeyDown(vs.xml.nal.Rvceb) && vs.zar.map <= vs.zar.zariadka)
            vs.cnv.zanovo("Режим <Всебачення>{статус: активовано;}.");

        if (Input.GetKeyUp(vs.xml.nal.Rvceb))
            vs.cnv.zanovo("Режим <Всебачення>{статус: деактивовано;}.");

            mat2.SetFloat("_V", max * 24);
            mat2.SetVector("_P", player.transform.position);
            n = 0;
            for (int i = 0; i < vs.bcc.k; i++)
                if (VoidSystem_cs.Dist(player.transform.position, vs.bcc.n[i].transform.position) <= r)
                {
                    nnomer[n] = i;
                    n++;
                }
            for (int i = 0; i < 60; i++)
            {
                if (i < n)
                {
                    npos[i] = (vs.bcc.n[nnomer[i]].transform.position - player.transform.position) / r * 0.5f;
                    x = npos[i].x * Mathf.Cos((pl.pov.y + vs.gOp.povz) / 57.3f) - npos[i].z * Mathf.Sin((pl.pov.y + vs.gOp.povz) / 57.3f);
                    y = npos[i].z * Mathf.Cos((pl.pov.y + vs.gOp.povz) / 57.3f) + npos[i].x * Mathf.Sin((pl.pov.y + vs.gOp.povz) / 57.3f);
                    npos[i] = new Vector4(x, 0, y, 0);
                }
                else
                    npos[i] = new Vector4(0, 0, 0, 0);
            }
            /*  Vector3 v=(zariad.transform.position - player.transform.position) / r * 0.5f;
                  x = v.x * Mathf.Cos((pl.pov.y + gop.povz) / 57.3f) - v.z * Mathf.Sin((pl.pov.y + gop.povz) / 57.3f);
                  y = v.z * Mathf.Cos((pl.pov.y + gop.povz) / 57.3f) + v.x * Mathf.Sin((pl.pov.y + gop.povz) / 57.3f);
                  v = new Vector4(x, 0, y, 0);
              mat.SetVector("_Z",v);  */

            if (n > 0)
                mat.SetVectorArray("_V", npos);
            else
                mat.SetVectorArray("_V", new Vector4[60]);

            mat.SetFloat("_N", max);
            // mat.SetVectorArray("_V", npos);

        }
    }
