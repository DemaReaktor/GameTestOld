using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gunOperator_cs : MonoBehaviour
{
    public VoidSystem_cs vs;
    public player pl;
    public strilba_efect_cs sec;
    public roznoseff_cs rozeff;

    // [ SerializeField] float lifetime;
    public Transform prytcilui;
    //public Transform player;
    public float gunspeed1;
    [HideInInspector] public int k;
    float[] timestr=new float[4];
    public Transform camerac;
    public Transform player;
    public Transform prytc;
    public Transform streff;
    public Transform streff2;
  //  public float uron;
    public float uronpr;
    public Transform mat;
    [HideInInspector] public float povz;
    Quaternion q;
    float perezariadka;
    float per;
   // public GameObject magnit;
    public int kytrozhod;
    float razbros;

    Vector3 napp;
    int obj;
    Vector3 opos;
    Vector2Int u=new Vector2Int();

    void Start()
    {
      vs = this.transform.GetComponent<VoidSystem_cs>();
    razbros = 0;
        povz = 0;
        k = 0;
        for (int i = 0; i < 4; i++)
            timestr[i] = 0;
        sec.prytc = prytc;
        perezariadka = 0;
        per = 0;
    }

    void Update()
    {
            if (Input.GetKeyDown(vs.xml.nal.Zbroya1)) {
            if (k != 0)
                vs.ac.zvudVimk(13);
            k = 0;
        }
        if (Input.GetKeyDown(vs.xml.nal.Zbroya2))
        {
            if (k != 1)
                vs.ac.zvudVimk(13);
            k = 1;
        }
        if (Input.GetKeyDown(vs.xml.nal.Zbroya3))
        {
            if (k != 2)
                vs.ac.zvudVimk(13);
            k = 2;
        }
        // if (Input.GetKeyDown(xml.nal.Zbroya4))
        //     k = 3;                                
        int k120 = k * 120;
        switch (k120)
        {
            case 0:
                if (povz >= 180)
                {
                    povz += Time.deltaTime * 360;
                    if (povz > 360)
                        povz = 0;
                }
                else { 
                    povz -= Time.deltaTime * 360;
                    if (povz < 0)
                        povz = 0;
                }
                break;
            case 120:
                if (povz >= 300||povz<120)
                {
                    povz += Time.deltaTime * 360;
                    if (povz > 120)
                        povz = 120;
                }
                else
                {
                    povz -= Time.deltaTime * 360;
                    if (povz < 120)
                        povz = 120;
                }
                break;
            case 240:
                if (povz >= 60 || povz < 240)
                {
                    povz += Time.deltaTime * 360;
                    if (povz > 240)
                        povz = 240;
                }
                else
                {
                    povz -= Time.deltaTime * 360;
                    if (povz < 240)
                        povz = 240;
                }
                break;
        }
        if (povz >= 360)
            povz -= 360;
        if (povz < 0)
            povz += 360;
        mat.GetComponent<Image>().material.SetFloat("_R",povz);
        q.eulerAngles = new Vector3(0, 0, -povz);
        mat.transform.rotation=q;

        for (int i = 0; i < 4; i++)
            timestr[i] += Time.deltaTime;
        switch (k) {
            case 0:
                if (Input.GetKey(vs.xml.nal.strilbaK) && vs.zar.charge >= vs.zar.strilba)
                razbros += Time.deltaTime *0.2f* kytrozhod;
                else
                                    if (razbros > 0)
                {
                    razbros -= Time.deltaTime  * kytrozhod;
                    if (razbros < 0)
                        razbros = 0;
                }

                if (razbros > kytrozhod)
                    razbros = kytrozhod;

                if (Input.GetKey(vs.xml.nal.strilbaK) && vs.zar.charge >= vs.zar.strilba * 10 && timestr[0] >= gunspeed1)
                {
                    timestr[0] = 0;
                    per = 0;
                    // Vector2 f = new Vector2(Random.Range( -Random.Range(0, razbros) , Random.Range(0, razbros)), Random.Range(-Random.Range(0, razbros), Random.Range(0, razbros)));

                    GameObject k = new GameObject();
                    k.transform.rotation = Quaternion.Euler(camerac.transform.rotation.eulerAngles + new Vector3(Random.Range(-razbros, razbros), Random.Range(-razbros, razbros), Random.Range(-razbros, razbros)));
                    stril(camerac.transform.position, k.transform.forward, camerac.transform.position, 0);
                    Destroy(k);

                    if (Input.GetKey(vs.xml.nal.DdoZbroyi))
                    {
                        vs.ac.zvudVimk(12);
                        vs.zar.charge -= vs.zar.strilba * 10;
                        if (obj == 2)
                            vs.bcc.Delete(vs.bcc.n[u.x].transform, 8);
                        timestr[0] = -2;
                    }
                    else
                    {
                        vs.ac.STR();
                        vs.zar.charge -= vs.zar.strilba;
                        if (obj == 2)
                            vs.bcc.Delete(vs.bcc.n[u.x].transform, 1);
                    }

                    if (obj == 0 && vs.xml.nal.yakistSpets == 2)
                        vs.mcAdd(opos, napp);
                    // bcc.Delete(k.transform, 1);
                    Transform jj;

                    if (vs.xml.nal.yakistSpets != 0) {
                        jj = Instantiate(streff, opos, new Quaternion(), this.transform);
                        jj.GetComponent<strilba_efect_cs>().vs = vs;
                    }
                    else {
                        jj = Instantiate(streff2, opos, new Quaternion(), this.transform);
                        jj.GetComponent<str2_cs>().vs = vs;
                        jj.GetComponent<str2_cs>().prytc = prytc;
                    }
                }
                
                prytcilui.GetComponent<RectTransform>().localScale = new Vector3(45 + razbros/kytrozhod*262.08f, 45 + razbros*262.08f/ kytrozhod, 1);
                perezariadka = timestr[k] /gunspeed1;
                break;
            case 1:
                if (Input.GetKeyDown(vs.xml.nal.strilbaK) && vs.zar.charge >= vs.zar.strilba*100 && timestr[1] >= 21)
                {
                    stril(camerac.transform.position, camerac.transform.forward, camerac.transform.position, 0);
                    if (obj != 1) {
                        per = 0;
                        timestr[1] = 0;
                        //  p = new Vector3(vs.diap(p.x),p.y,vs.diap(p.z));
                            player.position = new Vector3(VoidSystem_cs.diap(opos.x), opos.y, VoidSystem_cs.diap(opos.z));
                        rozeff.roznos += 35;

                        if (Input.GetKey(vs.xml.nal.DdoZbroyi))
                        {
                            vs.zar.charge -= vs.zar.strilba * 90;
                            for (int i = 0; i < vs.bcc.k; i++)
                            {
                                float d = VoidSystem_cs.Dist(camerac.position, vs.bcc.n[i].transform.position);
                                if (d <= 10)
                                    vs.bcc.Delete(vs.bcc.n[i].transform, vs.bcc.botxp[i]);
                                        }
                            for (int x = -4; x < 5; x++)
                                for (int z = -4; z < 5; z++) 
                                if(VoidSystem_cs.Dist2(x,z)<=10)
                                    { 
                                    for (int ii = 0; ii < vs.cc.cubk[ (int)VoidSystem_cs.diap(x+opos.x), (int)VoidSystem_cs.diap(z + opos.z)]; ii++) 
                                        VoidSystem_cs.Destr(vs.cc.cub[(int)VoidSystem_cs.diap(x + opos.x), (int)VoidSystem_cs.diap(z + opos.z),ii]);
                                        vs.cc.cubk[(int)VoidSystem_cs.diap(x + opos.x), (int)VoidSystem_cs.diap(z + opos.z)] = 0;
                                        vs.cc.h[(int)VoidSystem_cs.diap(x + opos.x), (int)VoidSystem_cs.diap(z + opos.z)] = 0;
                                }
                            vs.cc.Cc();
                        }    else
                        {
                            vs.ac.zvudVimk(10);
                            vs.zar.charge -= vs.zar.strilba * 60;
                            for (int i = 0; i < vs.bcc.k; i++)
                            {
                                float d = VoidSystem_cs.Dist(camerac.position, vs.bcc.n[i].transform.position);
                                if (d <= 10)
                                    vs.bcc.botxp[i] -= (int)((130 * d - 1300) / 19);
                            }
                        }
                    }
                }
                
                perezariadka = timestr[1] /21;

                break;
            case 2:
                if (Input.GetKeyDown(vs.xml.nal.strilbaK) && vs.zar.charge >= vs.zar.magnit * 1.5f && timestr[2] >= 5)
                {
                    per = 0;
                    timestr[2] = 0;
                    GameObject magnit = new GameObject();
                    magnit.AddComponent(typeof(magnit_cs));
                    magnit.GetComponent<magnit_cs>().rozeff = rozeff;
                    magnit.GetComponent<magnit_cs>().vs = vs;
                    magnit.GetComponent<magnit_cs>().player = camerac;
                    magnit.GetComponent<magnit_cs>().pl = pl;

                    if (Input.GetKey(vs.xml.nal.DdoZbroyi)) {
                        vs.zar.charge -= vs.zar.magnit*1.5f;
                        stril(camerac.position, camerac.forward, camerac.position, 0);
                        magnit.GetComponent<magnit_cs>().time = 1.5f;
                            magnit.transform.position =obj!=1? opos: camerac.position + camerac.forward * 6;
                    }
                    else
                    {
                        vs.zar.charge -= vs.zar.magnit;
                        magnit.transform.position = camerac.position;
                        magnit.GetComponent<magnit_cs>().time = 0;
                    } 
                }
                perezariadka = timestr[2] * 0.2f;

                break;
        }
        if (perezariadka > 1)
            perezariadka = 1;
        if (per  > perezariadka)
        {
            per -= Time.deltaTime * 3;

            if (per < perezariadka)
                per = perezariadka;
        }
        else if (per < perezariadka)
        {
            per += Time.deltaTime * 3;

            if (per > perezariadka)
                per = perezariadka;
        }
        mat.GetComponent<Image>().material.SetFloat("_R2",per);
    }

    //r startpos
    void stril(Vector3 r,Vector3 nap,Vector3 t,float d) {
        int i = 1;
        bool tt = false;
        Vector3 pos =new Vector3();

        while (!tt)
        {
             pos =t+ nap * i*0.01f;

           


            if (pos.x > 200)
            {
                stril(pos + r - t, nap, pos + r - t - new Vector3(200, 0, 0), d + i * 0.01f);
                tt=true;
            }
            else
            if (pos.x < 0)
            {
                stril(pos + r - t, nap, pos + r - t + new Vector3(200, 0, 0), d + i * 0.01f);
                tt = true;
            }
            else
            if (pos.z > 200)
            {
                stril(pos + r - t, nap, pos + r - t - new Vector3(0, 0, 200), d + i * 0.01f);
                tt = true;
            }
            else
            if (pos.z <0)
            {
                stril(pos + r - t, nap, pos + r - t + new Vector3(0, 0, 200), d + i * 0.01f);
                tt = true;
            }
            else
            if (d + i * 0.01f >= 100)
            {
                opos = r + pos - t;
                obj = 1;    //povitria
                tt = true;
            }
            else
            if (pos.y < 0)
            {
                //pidloga
                obj = 0;
                opos = r + pos - t;
                napp = new Vector3(1, 15, 1);
                tt = true;
            }
            else
            if (pos.y > 7)
            {
                //stelia
                obj = 1;
                opos = r + nap * (100 - d);
                tt = true;
            }
            else
            if (vs.h[(int)(pos.x), (int)(pos.z)] >= pos.y)
            {
                //cub
                obj = 0;
                opos = r + pos - t;    
                if ((int)( t.x + nap.x * i * 0.01f)>(int)(t.x + nap.x * (i-1) * 0.01f))
                napp = new Vector3(-15, 1,1);     else
                          if ((int)(t.x + nap.x * i * 0.01f) < (int)(t.x + nap.x * (i - 1) * 0.01f))
                    napp = new Vector3(15, 1, 1);
                else if ((int)(t.z + nap.z * i * 0.01f) > (int)(t.z + nap.z * (i - 1) * 0.01f))
                    napp = new Vector3(1, 1, -15);
                else if ((int)(t.z + nap.z * i * 0.01f) < (int)(t.z + nap.z * (i - 1) * 0.01f))
                    napp = new Vector3(1, 1, 15);
                else
                    napp = new Vector3(1, 15, 1);
                tt = true;
            }

            int ii = 0;
            while ( ii < vs.bcc.k&&!tt)//  for(int ii=0;ii<bcc.k;ii++)     
            {
                if(VoidSystem_cs.botPopad(vs.bcc.botpos[ii], pos + r - t)!=0)
               //if (vs.kvantd(bcc.botpos[ii], pos + r - t) <= 0.5f)
                {
                    u =new Vector2Int(ii, VoidSystem_cs.botPopad(vs.bcc.botpos[ii], pos + r - t));
                    obj = 2;
                    opos = r + pos - t;
                    tt = true;
                }
                ii++;
            }
            i++;
        }

    }
}
