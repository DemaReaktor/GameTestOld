using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prominfuture : MonoBehaviour
{
    public VoidSystem_cs vs;
    public player pl;

    public Material mat;
    public Material mat2;

    Vector3[] pos;

    bool gr;

    void Update()
    {
        if (Input.GetKey(vs.xml.nal.maybStrybok)&&(Input.GetKey(vs.xml.nal.vpered))&& vs.zar.charge>= vs.zar.promf)
        {
            vs.zar.charge -= Time.deltaTime * vs.zar.promf;
                pos = new Vector3[5000];
            GetComponent<LineRenderer>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;

            gr = true;
            int i = 0;

            //pos[0] = pl.transform.position;
            //  GetComponent<LineRenderer>().SetPosition.
            while (gr && i < 500)
            {
                pos[i] = pl.transform.position+ new Vector3( pl.V.x * 0.975f * 0.01f * (i+10),  0.01f * (i + 10) * (8.5f * Mathf.Sin((45-pl.pov.x)  / 180 * Mathf.PI) - 0.005f*9.8f*0.975f * (i + 10)),  pl.V.z * 0.01f *0.975f* (i + 10));
                gr = vs.gravityper(pos[i],0.5f);
                i++;
            }
            this.transform.position = pos[i-1];
            GetComponent<LineRenderer>().positionCount = i;
            /*  for(int ii=0;ii<i;ii++)
                  if(ii==i-1)
                      GetComponent<LineRenderer>().SetPositions(pos);
              else
                      GetComponent<LineRenderer>().widthCurve
                      */
            GetComponent<LineRenderer>().SetPositions(pos);
            mat.SetVector("_T", new Vector4( pos[i-1].x, pos[i - 1].y, pos[i - 1].z,0));
            mat2.SetVector("_T", new Vector4( pos[i-1].x, pos[i - 1].y, pos[i - 1].z,0));
        }
        else
        {
            mat.SetVector("_T", new Vector4(1110,500,1110, 0));
            mat2.SetVector("_T", new Vector4(1110, 500, 1110, 0));
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<LineRenderer>().enabled = false;
        }
    }
}
