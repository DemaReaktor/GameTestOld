using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnit_cs : MonoBehaviour
{
    public roznoseff_cs rozeff;
    public VoidSystem_cs vs;
    public player pl;
  //  [HideInInspector] public Vector3 nap;
     public Transform player;
    [HideInInspector] public float time;

    void Update()
    {
        time += Time.deltaTime;
            vs.telep(transform);

        vs.bcc.magnpos = transform.position;
        pl.magnpos = transform.position;
        if (time >= 1.5f)
        {
            pl.mag = true;
            vs.bcc.mag = true;
        }
        if (time >= 5)
        {
            for(int i = 0; i < vs.bcc.k; i++)
            {
                float d = VoidSystem_cs.kvantd(vs.bcc.botpos[i], transform.position);
                if (d < 5)
                    vs.bcc.Delete(vs.bcc.n[i].transform,(int)(vs.bcc.botxp[i]*0.2f*d+3- vs.xml.nal.vazhkist));
            }
            vs.bcc.mag = false;
            pl.mag = false;
            if (VoidSystem_cs.Dist(player.transform.position, transform.position) < 5)
                rozeff.roznos +=(vs.xml.nal.vazhkist+1) *4*(5- VoidSystem_cs.Dist(player.transform.position, transform.position));
            VoidSystem_cs.Destr(this.gameObject);//gameObject.SetActive(false);
        }


    }
}
