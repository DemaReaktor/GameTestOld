using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public VoidSystem_cs vs;
    public magnit_cs mc;
    public roznoseff_cs rozeff;

    public Material sh;
    public Material sh2;
    public Material stelia;

  //  [SerializeField] AudioClip[] a;
   // float at;

    //public float sylatertia;
    //public float magnsyla;
    public Transform prytc;
    [HideInInspector] public bool mag;
    [HideInInspector] public Vector3 magnpos;
    public Transform camerac;
    [HideInInspector] public Vector3 V;
    [HideInInspector] public bool gravity;
    float sp;
   // Vector3 position;
    Vector3 f;
    public float speed;
    public float pryskor;
    public float ypov;
  //  Vector3 cc;
    [HideInInspector] public Vector3 pov;
    Vector3 forward;
    Vector3 right;
    Vector3 magnv;
    bool pry;
    public GameObject lazer;
    GameObject la;
    float time;
    int fwiew;
    bool zxod;
  //  bool systempauk=false;
    void Start()
    {
        magnv = new Vector3();
        zxod = false;
        fwiew = 60;
        time = 0;
        pov = new Vector3();
        V = new Vector3();
        la = new GameObject();
        gravity = true;
        stelia.SetVector("_T", new Vector4(500, 0, 500, 0));
    }

    void Update()
    {
        bool j = gravity;
        time += Time.deltaTime;
       // at -= Time.deltaTime;

       // if (at > 0)
       //     GetComponent<AudioSource>().enabled = true;
      //  else
       //     GetComponent<AudioSource>().enabled = false;

        if (Input.GetKeyDown(vs.xml.nal.prytsil))
            pry = !pry;

        if (Time.timeScale != 0)
        {
            if (!vs.cnv.pause)
                pov += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            else
                pov += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * 0.05f;
        }
        vs.cc.pov = pov.y;
        camerac.transform.eulerAngles = pov;
        forward = new Vector3(Mathf.Sin(pov.y / 57), 0, Mathf.Cos(pov.y / 57));
        right = new Vector3(Mathf.Sin((pov.y + 90) / 57), 0, Mathf.Cos((pov.y + 90) / 57));

        if (la != null)
            Destroy(la);
       

        sh.SetVector("_V", transform.position);
        sh2.SetVector("_V", transform.position);


        f = transform.position;
        // position = new Vector3(f.x, 0, f.z);

        if (gravity)
        {
            // cc = transform.position;
            V -= transform.up * 9.8f * 0.975f * Time.deltaTime;
            f += V * Time.deltaTime;
            if (f.y > 10)
            {
                rozeff.roznos += 15;
                stelia.SetFloat("_Ti", time);
                stelia.SetVector("_T", new Vector4(transform.position.x, 0, transform.position.z, 0));
                f.y = 10;
                V = new Vector3();
            }
        }
        else
        {

            V = new Vector3();
            if (Input.GetKey(vs.xml.nal.big) && vs.zar.pryskor * 5 <= vs.zar.zariadka)
            {
                vs.zar.zariadka -= vs.zar.pryskor * Time.deltaTime;
                vs.ac.povilno = false;
                sp = pryskor;
                vs.ac.shvydko = true;
            }
            else 
                 if (Input.GetKey(vs.xml.nal.povzania))
            {
                sp = ypov;
                vs.ac.povilno = true;
                vs.ac.shvydko = false;
            }
            else
            {
                vs.ac.povilno = false;
                vs.ac.shvydko = false;
                sp = 1;
            }


            if (!Input.GetKey(vs.xml.nal.maybStrybok) || vs.zar.zariadka <= vs.zar.strybok)
            {
                bool g = false ;
                if (Input.GetKey(vs.xml.nal.vpered) && vs.zar.zariadka >= vs.zar.xod)
                {
                    if (!zxod)
                        vs.ac.xod = true;
                g = true;
                    vs.zar.zariadka -= Time.deltaTime * vs.zar.xod;
                    f += forward * speed * sp * Time.deltaTime;

                    if (Input.GetKey(vs.xml.nal.SPauk)) {
                        if (pov.x <= 0)
                        {
                            if (vs.gravityper(new Vector3(f.x, f.y + Time.deltaTime * speed * sp, f.z), 0.5f))
                                f.y = Mathf.Floor(f.y) + 0.51f;
                            else
                                f.y += Time.deltaTime * speed * sp;
                        }
                        else
                            f.y -= Time.deltaTime * speed * sp; }

                }
                if (Input.GetKey(vs.xml.nal.nazadR) && vs.zar.zariadka >= vs.zar.xod)
                {
                    if (!zxod)
                        vs.ac.xod = true;
                    g = true;
                    vs.zar.zariadka -= Time.deltaTime * vs.zar.xod;
                    f -= forward * speed * sp * Time.deltaTime;
                    if (Input.GetKey(vs.xml.nal.SPauk))
                    {
                        if (pov.x >= 0)
                        {
                            if (vs.gravityper(new Vector3(f.x, f.y + Time.deltaTime * speed * sp, f.z), 0.5f))
                                f.y = Mathf.Floor(f.y) + 0.51f;
                            else
                                f.y += Time.deltaTime * speed * sp;
                        }
                        else
                            f.y -= Time.deltaTime * speed * sp;
                    }

                }
                if (Input.GetKey(vs.xml.nal.vlivo) && vs.zar.zariadka >= vs.zar.xod)
                {
                    if (!zxod)
                        vs.ac.xod = true;
                    g = true;
                    vs.zar.zariadka -= Time.deltaTime * vs.zar.xod;
                    f -= right * speed * sp * Time.deltaTime;
                }
                if (Input.GetKey(vs.xml.nal.vpravo) && vs.zar.zariadka >= vs.zar.xod)
                {
                    if (!zxod)
                        vs.ac.xod = true;
                    g = true;
                    vs.zar.zariadka -= Time.deltaTime * vs.zar.xod;
                    f += right * speed * sp * Time.deltaTime;
                }
                zxod = g;
                if(!g)
                    vs.ac.xod = false;
            }
            else vs.ac.xod = false;

            if (Input.GetKey(vs.xml.nal.vpered))
                V += forward;
            if (Input.GetKey(vs.xml.nal.nazadR))
                V -= forward;
            if (Input.GetKey(vs.xml.nal.vlivo))
                V -= right;
            if (Input.GetKey(vs.xml.nal.vpravo))
                V += right;
            V *= speed * sp * 2*0.975f;
            if (sp == pryskor)
                V *= 0.8f;
        }
        if (mag)
        {
            float p = VoidSystem_cs.kvantd(magnpos, transform.position)+0.1f;

           for (int ii = 1; ii <= (int)(100 * Time.deltaTime) + 1; ii++)
            {
                Vector3 a = (vs.kvant(magnpos) - transform.position) *120 / p/ (0.5f + p) / Mathf.Sqrt(0.5f + p);
                magnv += a * 0.01f;
            }
            magnv = VoidSystem_cs.Dist(new Vector3(), magnv) > 20 ? magnv * 20 / VoidSystem_cs.Dist(new Vector3(), magnv) : magnv;
        }
        if (!vs.gravityper(transform.position, 0.5f))
            {
                float dm = VoidSystem_cs.Dist(new Vector3(), magnv) + 0.1f;
                magnv *= 1 -0.95f * Time.deltaTime / dm;
            }
            magnv *= 0.975f;
        magnv = VoidSystem_cs.Dist(new Vector3() ,magnv) > 20 ? magnv*20/ VoidSystem_cs.Dist(new Vector3(), magnv) : magnv;
      
       // Debug.Log(magnv);
        f += magnv * Time.deltaTime;
        

        if (f.x >=200|| f.x <0|| f.z >= 200|| f.z <0)
        {
           // cubecr.per();
            // transform.position = f;
            if (f.x >= 200)
            {
                vs.perem(new Vector3(-200, 0, 0));
                f += new Vector3(-200, 0, 0);
            }
            if (f.x < 0)
            {
                vs.perem(new Vector3(200, 0, 0));
                f += new Vector3(200, 0, 0);
            }
            if (f.z >= 200)
            {
                vs.perem(new Vector3(0, 0,-200));
                f += new Vector3(0, 0, -200);
            }
            if (f.z < 0)
            {
                vs.perem(new Vector3(0, 0, 200));
                f += new Vector3(0, 0, 200);
            }
        }
        Vector3 tenpos = (f -VoidSystem_cs.kvant2(transform.position,f))*0.1f;
        f = transform.position;
        for(int i=0;i<10;i++)
        f = vs.gravityno(f+tenpos);
        if ((Input.GetKeyUp(vs.xml.nal.maybStrybok) || Input.GetKeyUp(vs.xml.nal.strybok)) && vs.zar.zariadka >= vs.zar.strybok && !gravity)
        {
            vs.ac.strybokp();
            vs.zar.zariadka -= vs.zar.strybok;
           // if(Input.GetKeyUp(KeyCode.Space))
            V += new Vector3(0, 8.5f * Mathf.Sin((45-pov.x)  / 180 * Mathf.PI), 0);

           // V = new Vector3(V.x* Mathf.Cos((-pov.x + 45) / 180 * Mathf.PI),V.y , V.z * Mathf.Cos((-pov.x + 45) / 180 * Mathf.PI));
            gravity = true;
        }
        else
            gravity = vs.gravityper(f,0.5f);
        if (j && !gravity&&V.y<-2)
            vs.ac.strybokk();

        transform.position = f;
        vs.cc.Updat();

        prytc.transform.position = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
            if (pry)
        {
            if (vs.gOp.k == 2)
            {
                Vector3 pos;
                Vector3 v;
                pos = camerac.position;
                v = camerac.forward * 0.8f;
                while (vs.gravityper(pos, 0.01f))
                {
                    v.y -= 9.8f* 0.975f *0.02f;
                    int ri = 1;
                    while (ri <= 20 && vs.gravityper(pos, 0.01f))
                    {
                        ri++;
                        if (!vs.gravityper(pos+v * 0.05f, 0.01f))
                        {
                            la = Instantiate(lazer, pos + v * 0.05f, new Quaternion());//Quaternion.Euler(v)
                            la.transform.localScale *= 0.012f * VoidSystem_cs.Dist(pos + v * 0.05f, transform.position);
                        }
                        pos += v * 0.05f;
                    }
                }
            }
            fwiew = 20;
        }
        else
            if(Input.GetKey(vs.xml.nal.big) && vs.zar.pryskor * 5 <= vs.zar.zariadka)
            fwiew = 80;
        else
             if (Input.GetKey(vs.xml.nal.povzania))
            fwiew = 50;    else
        fwiew = 60;
        
        if (fwiew != (int)(camerac.GetComponent<Camera>().fieldOfView))
        {
            if (fwiew < camerac.GetComponent<Camera>().fieldOfView)
                camerac.GetComponent<Camera>().fieldOfView-=5;
            else
                camerac.GetComponent<Camera>().fieldOfView+=5;
        }
    }

   

    }


