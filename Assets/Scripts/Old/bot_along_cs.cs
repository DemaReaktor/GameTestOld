using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot_along_cs : MonoBehaviour
{
    public VoidSystem_cs vs;
    [Space]

    //[HideInInspector] public Transform pl;
    //  [SerializeField] Material mat;
    [HideInInspector] public Shader shbot;
   // [SerializeField] Shader shbot1;
    //[SerializeField] Transform botf;

  //  Vector3 pos;

    // [SerializeField] Vector4 color;

    GameObject[] mo;

    [HideInInspector] public int trk;

    [HideInInspector] public float[] td;
    [HideInInspector] public Vector3[] cp;
    [HideInInspector] public Color[] color;

  //  [HideInInspector] public int[] toch;
  //  [HideInInspector] public Vector3[] t;

    Vector3[] rand;
    Vector3[] poch;

    //   int tt;
    // int post;
    //  int postr;
    int podiya;
    float time;
    int t1,t2;

    bool zv;
    //Mesh mesh;

    [HideInInspector] public float pal;
    //Transform n;
    Vector4 botma;
   // [HideInInspector]  public   Vector3 posit;
    void Start()
    {
        zv = false;
      //  posit =transform.position;
        pal = 0;
        mo = new GameObject[trk];

        podiya = 2;
        float[] d = new float[trk];
        rand = new Vector3[trk];
        poch = new Vector3[trk];
        for (int i = 0; i < trk; i++)
            d[i] = VoidSystem_cs.Dist(new Vector3(), cp[i]);

        for (int i = 0; i < trk - 1; i++)
            for (int ii = i + 1; ii < trk; ii++)
                if (d[i] > d[ii])
                {
                    Vector3 g;
                    g = cp[ii];
                    cp[ii] = cp[i];
                    cp[i] = g;

                    float g2;
                    g2 = td[i];
                    td[i] = td[ii];
                    td[ii] = g2;

                    Color g1;
                    g1 = color[ii];
                    color[ii] = color[i];
                    color[i] = g1;

                    g2 = d[i];
                    d[i] = d[ii];
                    d[ii] = g2;
                }
        t1 =t2= 0;
        for (int i = 0; i < trk; i++)
        {
            mo[i] = transform.GetChild(0).GetChild(6 + i).gameObject;
            poch[i] = mo[i].transform.position - transform.position;
        }
        /*   transform.GetChild(0).GetComponent<fireffect_cs>().ena
           transform.GetChild(1).GetComponent<fireffect_cs>().k = true;
           transform.GetChild(2).GetComponent<fireffect_cs>().k = true;
           transform.GetChild(3).GetComponent<fireffect_cs>().k = true;       */

    }

    void Update()
    {
        if(!zv)//&&!vs.xml.vid)
        {
            zv = true;
            GetComponent<AudioSource>().Play();
        }
       // transform.position = vs.kvant(posit);

        /* if (vs.Dist(transform.position, pl.transform.position) < 30)
             for (int i = 0; i < trk; i++)
                 mo[i].GetComponent<MeshRenderer>().enabled = true ;
         else for (int i = 0; i < trk; i++)
                 mo[i].GetComponent<MeshRenderer>().enabled = false;    */

        if (pal <= 1)
            botma = new Vector4(pal, 1, 0, 1);
        else
            botma = new Vector4(1, 2 - pal, 0, 1);
        transform.GetChild(0).GetChild(5).GetComponent<MeshRenderer>().material.SetColor("_C", botma);

        if (podiya == 1)
        {
            if (time <= 2)
            {
                for (int i = t1; i < trk * time * 0.5f; i++)
                {

                 //   mesh = new Mesh();

                  //  mesh.vertices = t;
                  //  mesh.triangles = toch;

                    mo[i].transform.GetComponent<MeshRenderer>().material.SetFloat("_R", 1);
                    mo[i].transform.GetComponent<MeshRenderer>().enabled = true;
                   // mo[i].transform.GetComponent<MeshFilter>().mesh = mesh;
                    mo[i].transform.position = transform.position+cp[i];
                    mo[i].transform.localScale = new Vector3(td[i], td[i], td[i]);
                }
                if((int)(trk * time * 0.5f) == trk * time * 0.5f)
                t1 = (int)(trk * time * 0.5f);
                else
                    t1 = (int)(trk * time * 0.5f)+1;

            }
            else
            {
                if (t1 != trk)
                {
                    for (int i = t1; i < trk; i++)
                    {

                     //   mesh = new Mesh();

                      //  mesh.vertices = t;
                       // mesh.triangles = toch;

                        mo[i].transform.GetComponent<MeshRenderer>().material.SetFloat("_R", 1);
                        mo[i].transform.GetComponent<MeshRenderer>().enabled = true;

                       // mo[i].transform.GetComponent<MeshFilter>().mesh = mesh;
                        mo[i].transform.position = transform.position + cp[i];
                        mo[i].transform.localScale = new Vector3(td[i], td[i], td[i]);
                    }
                    t1 = trk;
                }
               
                if (time <= 6)
                {
                    transform.GetChild(0).GetChild(4).GetComponent<MeshRenderer>().enabled = false;
                    transform.GetChild(0).GetChild(5).GetComponent<MeshRenderer>().enabled = false;
                    float d;

                    for (int i = 0; i < trk; i++)
                    {
                        float e = i/(int)(trk*0.01f)*0.01f;
                            d = time - 2 - 3 * e;
                        d = Mathf.Clamp(d,0,1);
                        mo[i].transform.position = transform.position + cp[i]+ rand[i]* d*2;
                        mo[i].transform.localScale = new Vector3((1 - d)*td[i],( 1 - d) * td[i], (1 - d) * td[i]);
                        if (!vs.gravityper(mo[i].transform.position, (1 - d) * td[i]))
                            mo[i].GetComponent<MeshRenderer>().enabled = false;
                        d = 2*time - 3 - 2 * e;
                        d = Mathf.Clamp(d, 1, 3);
                        mo[i].transform.GetComponent<MeshRenderer>().material.SetFloat("_R", d);
                    }
                }
                else
                    VoidSystem_cs.Destr(gameObject);
            }
            //for (int i = 0; i < trk; i++)
            //  mo[i].transform.GetComponent<MeshRenderer>().material.SetFloat("_R", 1 + time);
          /*  for (int i = 0; i < t1; i++)
            {
                float e = i / (int)(trk * 0.01f) * 0.01f;
                float d = 2.2f * time - 1 - 2 * e;
                d = Mathf.Clamp(d, 1, 3);
                mo[i].transform.GetComponent<MeshRenderer>().material.SetFloat("_R", d);
            }  */    
        }

         if (podiya==2) {
            
             //  GetComponent<AudioSource>().volume = vs.xml.nal.zvuk * vs.xml.nal.zvukBotiv;
            if (time <= 3)
            {

                if (!GetComponent<AudioSource>().isPlaying)
                {
                    if (!vs.peff.realpause)//&&!vs.xml.vid)
                        GetComponent<AudioSource>().Play();
                }
                else
                    if (vs.peff.realpause)
                    GetComponent<AudioSource>().Pause();
                //4/10
                for (int i = 0; i < trk; i++)
                {
                    float d;
                    float e = i / (int)(trk * 0.01f) * 0.01f;
                    d = time - 2 * e;
                    d = Mathf.Clamp(d, 0, 1);
                    mo[i].transform.localScale = new Vector3(d * td[i], d * td[i], d * td[i]);
                    mo[i].transform.position = transform.position + poch[i] + (cp[i] - poch[i]) * d;
                }
            }
            else           //4/10
            if (time <= 6) for (int i = t1; i < trk * 0.33f * (time - 3); i++)
                {
                    // Debug.Log(trk * 0.33f * (time - 3));
                    //  if(color[i]==new Color(1,1,0))
                    mo[i].GetComponent<MeshRenderer>().material.SetFloat("_R", 1);//3-(time-3)*0.66f);
                    t1 = i + 1;
                }
            else
            {
                if (t1 != trk)
                    for (int i = 0; i < trk; i++)
                    {
                        mo[i].GetComponent<MeshRenderer>().material.SetFloat("_R", 1);//3-(time-3)*0.66f);    
                        t1 = trk;
                        transform.GetChild(0).GetChild(4).GetComponent<MeshRenderer>().enabled = true;
                        transform.GetChild(0).GetChild(5).GetComponent<MeshRenderer>().enabled = true;
                    }

                if (time <= 9)
                    //7.5/10
                    for (int i = 0; i < trk * (time - 6) * 0.33f; i++)
                    {
                        mo[i].GetComponent<MeshRenderer>().enabled = false;
                        t2 = i;
                    }
                else
                if (time < 10)
                { //0/10
                    transform.GetChild(0).GetChild(0).GetComponent<fireffect_cs>().enabled = true;
                    transform.GetChild(0).GetChild(1).GetComponent<fireffect_cs>().enabled = true;
                    transform.GetChild(0).GetChild(2).GetComponent<fireffect_cs>().enabled = true;
                    transform.GetChild(0).GetChild(3).GetComponent<fireffect_cs>().enabled = true;
                    transform.GetChild(0).GetChild(0).GetComponent<fireffect_cs>().vs = vs;
                    transform.GetChild(0).GetChild(1).GetComponent<fireffect_cs>().vs = vs;
                    transform.GetChild(0).GetChild(2).GetComponent<fireffect_cs>().vs = vs;
                    transform.GetChild(0).GetChild(3).GetComponent<fireffect_cs>().vs = vs;

                    //   transform.GetChild(0).GetChild(0).GetComponent<fireffect_cs>().speed = 10;
                    // transform.GetChild(0).GetChild(1).GetComponent<fireffect_cs>().speed = 10;
                    //   transform.GetChild(0).GetChild(2).GetComponent<fireffect_cs>().speed = 10;
                    //   transform.GetChild(0).GetChild(3).GetComponent<fireffect_cs>().speed = 10;
                    for (int i = t2 + 1; i < trk; i++)
                        mo[i].GetComponent<MeshRenderer>().enabled = false;
                    time += 2;
                }
                else
                {     //13/10
                    time = 0;
                    podiya = 0;
                    vs.bcc.BotA(gameObject);
                }
            }
        }
        GetComponent<AudioSource>().volume = vs.xml.nal.zvuk * vs.xml.nal.zvukBotiv / (VoidSystem_cs.Dist(transform.position, vs.player.position) + 1);

        time += Time.deltaTime;

        transform.GetChild(0).gameObject.SetActive( vs.vydno(transform.position, 1));     


    }
  
    public void roz()
    {
        time = 0;
        // for (int i = 0; i < trk; i++)
        // trp[i] += transform.position;
        t1 = 0;
        podiya = 1;
        float[] d = new float[trk];
        rand = new Vector3[trk];
        for (int i = 0; i < trk; i++)
        {
            rand[i] = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            rand[i] = rand[i]/(VoidSystem_cs.Dist(new Vector3(), rand[i])+0.1f);

            d[i] = VoidSystem_cs.Dist( new Vector3(0, 1, 0), cp[i]);
        }

        for (int i = 0; i < trk - 1; i++)
            for (int ii = i + 1; ii < trk; ii++)
                if (d[i] > d[ii])
                {
                    Vector3 g;
                    g = cp[ii];
                    cp[ii] = cp[i];
                    cp[i] = g;

                    float g2;
                    g2 = td[i];
                    td[i] = td[ii];
                    td[ii] = g2;

                    Color g1;
                    g1 = color[ii];
                    color[ii] = color[i];
                    color[i] = g1;

                    g2 = d[i];
                    d[i] = d[ii];
                    d[ii] = g2;
                }
t1 = 0;
      /*  for (int i = 0; i < trk; i++)
        {
            mesh = new Mesh();
            Vector3[] ttoch =new Vector3[8];

            for (int ii = 0; ii < 8; ii++)
                ttoch[ii] = tochtr[i*8+ii];
            mesh.vertices = ttoch;
            mesh.triangles = toch;

            mo[i] = new GameObject();
            mo[i].transform.position = transform.position;
            // mo[i].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            mo[i].AddComponent(typeof(MeshFilter));
            mo[i].AddComponent(typeof(MeshRenderer));
            mo[i].transform.GetComponent<MeshRenderer>().material = new Material(shbot);
            if(color[i]==new Color(1,1,0))
            mo[i].transform.GetComponent<MeshRenderer>().material.SetColor("_C", botma);
            else
            mo[i].transform.GetComponent<MeshRenderer>().material.SetColor("_C", color[i]);
            mo[i].transform.GetComponent<MeshRenderer>().material.SetFloat("_R", 0);
            mo[i].transform.GetComponent<MeshFilter>().mesh = mesh;
            mo[i].transform.SetParent(transform);
        }  */
        transform.GetChild(0).GetChild(0).GetComponent<fireffect_cs>().k = 2;
        transform.GetChild(0).GetChild(1).GetComponent<fireffect_cs>().k = 2;
        transform.GetChild(0).GetChild(2).GetComponent<fireffect_cs>().k = 2;
        transform.GetChild(0).GetChild(3).GetComponent<fireffect_cs>().k = 2;
        //transform.GetChild(4).GetComponent<MeshRenderer>().enabled = false;
       // transform.GetChild(5).GetComponent<MeshRenderer>().enabled = false;
    }  
   /* public void zye(Vector3 o)
    {
       // pos = o;
        podiya = 2;
    }                          */
}
