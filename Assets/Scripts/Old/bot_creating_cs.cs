using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot_creating_cs : MonoBehaviour
{
   // public Audio_cs ac;
   // public xml_cs xml;
   // public Cube_creating cc;
   // public cnopkovod_cs cnv;
    public player pl;
   // public zariad_cs zar;
    public VoidSystem_cs vs;
    public magnit_cs mc;
    public roznoseff_cs rozeff;

   // public Texture tex;
  //  public Shader sh;
    public Transform botvygl;
    public Transform cam;
   // public Transform botv;
  //  public Transform player;
    public GameObject bot;
    //public GameObject botclon;
  //  public Shader botsh;

    [HideInInspector] public Vector3 magnpos;
    [HideInInspector] public bool mag;
    Vector3[] magnv=new Vector3[100];

    // public float time;
    // float t;
    [HideInInspector] public int k;
    [HideInInspector] public int ks;
    [HideInInspector] public int[] botxp = new int[100];
     public GameObject[] n = new GameObject[100];
    Vector3Int[] npos = new Vector3Int[100];
    [HideInInspector] public Vector3[] botpos = new Vector3[100];
   // Vector3Int[] nposp = new Vector3Int[100];
    int[] botnomer = new int[100];
   // int[] botdist = new int[60];
    [HideInInspector] public  Vector3Int plpos;
    Vector3Int plposp;
    Vector3Int plpospov;
   // Vector3 plpp;
    public float BOTspeed;
    public int[,,] ra = new int[200,10,200];
    public int distance;
    //float tt2;
    int nomer;
    [HideInInspector] public bool palyvo=false;
    bool palm;
   // [HideInInspector] public float[] botm=new float[100];
   // Material[] botmat=new Material[100] ;
     bool[] palb=new bool[100];
    float timepalyvo;
    float paltime;
    int mk;
    // public Vector2Int ii;
    
    int tk;
    int trk;
    int[] por;
    Vector3[] tochky;
    int[] toch = new int[36];
    Vector3[] t = new Vector3[8];

    Color[] color;
    Vector3[] cp;
    float[] d;

    [SerializeField] Shader sha;
    [SerializeField] Shader sha1;

    int trkk;
    int post;
    int postr;

    public GameObject bott;

    int bpk;
    float time;
    void Start()
    {
      vs = this.transform.GetComponent<VoidSystem_cs>();

    mk = vs.xml.nal.vazhkist * 2 + 1;
        time = 0;
        tk = trk = trkk = post = postr = 0;
        ttrfind(bot.transform);
        por = new int[trk * 3];
        tochky = new Vector3[tk];

        color = new Color[trk];
        cp = new Vector3[trk];
        d = new float[trk];

        componentfind(bot.transform);
        for (int i = 0; i < trk; i++)
        {
            cp[i] = (tochky[por[i * 3]] + tochky[por[i * 3 + 1]] + tochky[por[i * 3 + 2]]) * 0.333f;
            d[i] = VoidSystem_cs.Dist(cp[i], tochky[por[i * 3]]);
            if (d[i] < VoidSystem_cs.Dist(cp[i], tochky[por[i * 3 + 1]]))
                d[i] = VoidSystem_cs.Dist(cp[i], tochky[por[i * 3 + 1]]);
            if (d[i] < VoidSystem_cs.Dist(cp[i], tochky[por[i * 3 + 2]]))
                d[i] = VoidSystem_cs.Dist(cp[i], tochky[por[i * 3 + 2]]);

        }

        toch[0] = 0;
        toch[1] = 1;
        toch[2] = 2;

        toch[3] = 2;
        toch[4] = 1;
        toch[5] = 3;

        toch[6] = 5;
        toch[7] = 1;
        toch[8] = 0;

        toch[9] = 5;
        toch[10] = 0;
        toch[11] = 4;

        toch[12] = 4;
        toch[13] = 0;
        toch[14] = 2;

        toch[15] = 2;
        toch[16] = 6;
        toch[17] = 4;

        toch[18] = 6;
        toch[19] = 7;
        toch[20] = 5;

        toch[21] = 5;
        toch[22] = 4;
        toch[23] = 6;

        toch[24] = 2;
        toch[25] = 3;
        toch[26] = 7;

        toch[27] = 7;
        toch[28] = 6;
        toch[29] = 2;

        toch[30] = 5;
        toch[31] = 7;
        toch[32] = 3;

        toch[33] = 3;
        toch[34] = 1;
        toch[35] = 5;

        t[0] = new Vector3(-1, -1, -1);
        t[1] = new Vector3(-1, -1, 1);
        t[2] = new Vector3(-1, 1, -1);
        t[3] = new Vector3(-1, 1, 1);
        t[4] = new Vector3(1, -1, -1);
        t[5] = new Vector3(1, -1, 1);
        t[6] = new Vector3(1, 1, -1);
        t[7] = new Vector3(1, 1, 1);

        bott = Instantiate(bot, new Vector3(), new Quaternion());
        GameObject[] gam = new GameObject[trk];
        for (int i = 0; i < trk; i++)
        {
            gam[i] = new GameObject();
            gam[i].AddComponent(typeof(MeshFilter));
            gam[i].AddComponent(typeof(MeshRenderer));
            gam[i].GetComponent<MeshRenderer>().material = new Material(sha);
            gam[i].GetComponent<MeshRenderer>().material.SetFloat("_R", 3);
            gam[i].GetComponent<MeshRenderer>().material.SetColor("_C", color[i]);
            Mesh mesh = new Mesh();
            mesh.vertices = t;
            mesh.triangles = toch;
            gam[i].GetComponent<MeshFilter>().mesh = mesh;

            Vector3 rand;
            rand = new Vector3(Random.Range(-10, 10), Random.Range(10, 100) * 0.1f, Random.Range(-10, 10));
            float j = VoidSystem_cs.Dist2(new Vector2(), new Vector2(rand.z, rand.x)) + 0.01f;
            rand.x /= j;
            rand.z /= j;

            gam[i].transform.position = rand;
            gam[i].transform.localScale = new Vector3();
            gam[i].transform.SetParent(bott.transform.GetChild(0));
        }

        paltime = 0;
        nomer = 1;

        for(int i=0;i<vs.xml.nal.vazhkist*2;i++)
        {
            Vector3 kk;
            do
            {
                Vector3 kkk = new Vector3(Random.Range(0, 200), 0, Random.Range(0, 200));
                kk = new Vector3(kkk.x + 0.5f, vs.h[(int)(kkk.x), (int)(kkk.z)] + 0.01f, kkk.z + 0.5f);
            } while (VoidSystem_cs.Dist(kk, cam.transform.position) <= 25);
                botAdd(kk); 
        }

        bpk=1;        
        plpospov = new Vector3Int((int)(cam.transform.position.x), vs.cc.h[(int)(cam.transform.position.x), (int)(cam.transform.position.z)],(int)(cam.transform.position.z));

        r(plposp, distance);
        for(int i=0;i< 100; i++)
            magnv[i] = new Vector3();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time*(1+vs.xml.nal.vazhkist)  >= bpk*bpk &&k < 100)
        {
            Vector3 kk;
            do
            {
                Vector3 kkk = new Vector3(Random.Range(0, 200), 0, Random.Range(0, 200));
                kk = new Vector3(kkk.x + 0.5f, vs.h[(int)(kkk.x), (int)(kkk.z)] + 0.01f, kkk.z + 0.5f);
            } while (VoidSystem_cs.Dist(kk, cam.transform.position) <= 25);
            botAdd(kk);
            bpk++;
        }
        /*    for (int i = 0; i < k; i++) {
              //  botm[i] += Time.deltaTime*10;

                for (int ii = 0; ii < 8; ii++)
                    if (vs.Dist(n[i].transform.GetChild(1).GetChild(ii).position, player.transform.position) <= 100)
                        n[i].transform.GetChild(1).GetChild(ii).gameObject.SetActive(true);
                else
                        n[i].transform.GetChild(1).GetChild(ii).gameObject.SetActive(false);    
            } */
        palm = false;
        int io = 0;
        while (io < k && !palm)
        {
            if (VoidSystem_cs.kvantd(botpos[io], cam.transform.position) <= 25)
                if (str(cam.transform.position, vs.kvant(botpos[io])))
                {
                    palyvo = true;
                    paltime += Time.deltaTime;
                    palm = true;
                    vs.ac.pal = true;
                    for (int ii = 0; ii < k; ii++)
                    {
                        npos[ii].x = VoidSystem_cs.diap(npos[ii].x);
                        npos[ii].z = VoidSystem_cs.diap(npos[ii].z);
                        if (ra[npos[ii].x, npos[ii].y, npos[ii].z] > 0)
                            palb[ii] = true;
                        else
                            palb[ii] = false;
                    }
                }
            io++;
        }

        if (palyvo)
        {
            for (int i = 0; i < k; i++)
                if(palb[i])
                n[i].GetComponent<bot_along_cs>().pal = 2;
            else
                n[i].GetComponent<bot_along_cs>().pal = 1;
            if (paltime*(1+vs.xml.nal.vazhkist) >= 12)
            {
                paltime = 0;
                if (k < 100 &&Random.Range(0,vs.xml.nal.vazhkist + 2) != 0)
                {
                    Vector3 kk;
                    do
                    {
                        Vector3 kkk = new Vector3(Random.Range(0, 200), 0, Random.Range(0, 200));
                        kk = new Vector3(kkk.x + 0.5f, vs.h[(int)(kkk.x), (int)(kkk.z)] + 0.01f, kkk.z + 0.5f);
                    } while (VoidSystem_cs.Dist(kk, cam.transform.position) <= 25);
                    botAdd(kk);
                }
            }
           

            if (!palm)
            {
                timepalyvo += Time.deltaTime;
                if (timepalyvo >= 10)
                {
                    timepalyvo = 0;
                    palyvo= vs.ac.pal = false;
                    for (int i = 0; i < k; i++)
                    {
                        n[i].GetComponent<bot_along_cs>().pal = 0;
                        palb[i] = false;
                    }
                }
            }
            else
                timepalyvo = 0;
        }
        else
            paltime = 0;
            plpos = new Vector3Int((int)(cam.transform.position.x), (int)(cam.transform.position.y),(int)(cam.transform.position.z));
        if (plpos.y <= 7)
        {
            if (vs.cc.c[plpos.x, plpos.y, plpos.z])
                plpospov = plpos;
            else
                plpos = plpospov;
        }
        else
            plpos = plpospov;


            if (plposp != plpos &&palyvo)
            {
                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 200; x++)
                        for (int z = 0; z < 200; z++)
                            ra[x, y, z] = 0;
                r(plpos, distance);
                plposp = plpos;
            }
            for (int i = 0; i < k; i++)
            {
                for (int ii = i + 1; ii < k; ii++)
                    if (VoidSystem_cs.Dist(botpos[i] , botpos[ii])<=0.2f)
                    {
                        botxp[i]+=1+botxp[ii];
                    if (mk < botxp[i])
                        mk = botxp[i];
                    Destroy(n[ii].gameObject);
                    for (int iii = ii; iii < k; iii++)
                    {
                        n[iii] = n[iii + 1];
                        npos[iii] = npos[iii + 1];
                        botnomer[iii] = botnomer[iii + 1];
                        botpos[iii] = botpos[iii + 1];
                    }
                    k--;
                    n[k] = null;
                }
            //  nposp[i] = npos[i];

            if (palb[i]&&palyvo)
            {
                n[i].GetComponent<bot_along_cs>().pal = 2;
                int x1, x2, z2, z1;

                if (npos[i].x == 0)
                    x1 = 200;
                else
                    x1 = npos[i].x;

                if (npos[i].x == 199)
                    x2 = -1;
                else
                    x2 = npos[i].x;

                if (npos[i].z == 0)
                    z1 = 200;
                else
                    z1 = npos[i].z;

                if (npos[i].z == 199)
                    z2 = -1;
                else
                    z2 = npos[i].z;

                if (ra[x1 - 1, npos[i].y, npos[i].z] - 1 == ra[npos[i].x, npos[i].y, npos[i].z])
                    npos[i] = new Vector3Int(x1 - 1, npos[i].y, npos[i].z);
                else
                if (ra[x2 + 1, npos[i].y, npos[i].z] - 1 == ra[npos[i].x, npos[i].y, npos[i].z])
                    npos[i] = new Vector3Int(x2 + 1, npos[i].y, npos[i].z);
                else
                     if (ra[npos[i].x, npos[i].y, z1 - 1] - 1 == ra[npos[i].x, npos[i].y, npos[i].z])
                    npos[i] = new Vector3Int(npos[i].x, npos[i].y, z1 - 1);
                else
                     if (ra[npos[i].x, npos[i].y, z2 + 1] - 1 == ra[npos[i].x, npos[i].y, npos[i].z])
                    npos[i] = new Vector3Int(npos[i].x, npos[i].y, z2 + 1);
                else
                     if (ra[npos[i].x, npos[i].y + 1, npos[i].z] - 1 == ra[npos[i].x, npos[i].y, npos[i].z])
                    npos[i] = new Vector3Int(npos[i].x, npos[i].y + 1, npos[i].z);
                else
                if (npos[i].y > 0)
                {
                    if (ra[npos[i].x, npos[i].y - 1, npos[i].z] - 1 == ra[npos[i].x, npos[i].y, npos[i].z])
                        npos[i] = new Vector3Int(npos[i].x, npos[i].y - 1, npos[i].z);
                }
            }
               botpos[i]+=new Vector3(0.5f+npos[i].x- botpos[i].x , 0.01f+npos[i].y - botpos[i].y, 0.5f + npos[i].z - botpos[i].z) * Time.deltaTime * BOTspeed*(vs.xml.nal.vazhkist+1)*0.4f;
                if (mag)
            {
                Vector3 p = VoidSystem_cs.kvant2(magnpos, botpos[i]);
                        int pp = (int)(100 * Time.deltaTime) + 1;
                        for (int ii = 1; ii <= pp; ii++)
                        {
                               Vector3 a = (p - botpos[i]) *100 / VoidSystem_cs.Dist(botpos[i], p) / (0.5f + VoidSystem_cs.Dist(botpos[i], p)) / Mathf.Sqrt(0.5f + VoidSystem_cs.Dist(botpos[i], p));
                            magnv[i] += a * 0.01f;// Time.deltaTime;//tim;

                        }
                    }
                        float dm = VoidSystem_cs.Dist(new Vector3(), magnv[i]) + 0.001f;
                        magnv[i] *= 1 - 0.95f * Time.deltaTime / dm;
                    magnv[i] *= 0.975f;
            botpos[i] += magnv[i] * Time.deltaTime;

            botpos[i] = new Vector3(VoidSystem_cs.diapazon(botpos[i].x), botpos[i].y, VoidSystem_cs.diapazon(botpos[i].z));

            botpos[i] = vs.gravityno(botpos[i]);
            botpos[i].y -=0.49f;

            npos[i] = new Vector3Int((int)(botpos[i].x), (int)(botpos[i].y), (int)(botpos[i].z));
                //  Debug.Log(n[i].transform.position);


            if(VoidSystem_cs.Dist(cam.position,vs.kvant(botpos[i])) <2)
            {
                pl.gravity = true;
                pl.V += new Vector3(0, 2.5f, 0)+(cam.position- vs.kvant(botpos[i])) /(VoidSystem_cs.Dist(cam.position, vs.kvant(botpos[i])) +0.2f)*3;
                vs.zar.charge -= vs.zar.vybux;
                vs.ac.zvudVimk(14);
            }
            vs.telep(n[i].transform,ref botpos[i]);
           // n[i].GetComponent<bot_along_cs>().posit =botpos[i];
        }

        if (vs.cnv.pause)
        {
            float ii = 20;
            bool jj = false;
            Vector2 napr = new Vector2((Input.mousePosition.x / Screen.width - 0.5f) * 320 / 3, (Input.mousePosition.y / Screen.height - 0.5f) * 60);
            napr += new Vector2(cam.rotation.eulerAngles.y, -cam.rotation.eulerAngles.x);
            Vector3 popos,pospos;
            pospos = new Vector3(Mathf.Sin(napr.x / 57.3f) * Mathf.Cos(napr.y / 57.3f), Mathf.Sin(napr.y / 57.3f), Mathf.Cos(napr.x / 57.3f) * Mathf.Cos(napr.y / 57.3f))*0.1f;
            popos = cam.position;
            while (ii < 1000 && !jj)
            {
                popos +=  pospos;
                for (int i = 0; i < k; i++)
                    if (VoidSystem_cs.botPopad(botpos[i], popos, 0.2f) != 0)
                    {
                        botvygl.SetParent(n[i].transform);
                        botvygl.position = n[i].transform.position + new Vector3(0, -0.02f, 0);
                        botvygl.rotation = new Quaternion();
                        jj = true;
                            botinfo(n[i].transform);
                    }
                ii ++;
            }
            if (!jj)
                botvygl.SetParent(cam.GetChild(0));
            
        }
        else  botvygl.SetParent(cam.GetChild(0));

    }  
    
    public void BotA(GameObject obj)
    {
        n[k] = obj;

        botxp[k] = mk;
        if(Random.Range(0,vs.xml.nal.vazhkist+2)!=0)
        mk++;
        if (Random.Range(0, vs.xml.nal.vazhkist + 2) != 0)
            mk++;
        botnomer[k] = nomer;
        nomer++;
        npos[k] = new Vector3Int((int)(obj.transform.position.x), (int)(obj.transform.position.y), (int)(obj.transform.position.z));
        botpos[k] = obj.transform.position;
        k++;
    }
    void botAdd(Vector3 pos)
    {
        if (ks < 100)
        {
          //  n[k] = Instantiate(bot, pos, new Quaternion());       

            GameObject nn;
            nn = Instantiate(bott, pos, new Quaternion());
            nn.GetComponent<bot_along_cs>().vs = vs;

            nn.GetComponent<bot_along_cs>().trk = trk;

            nn.GetComponent<bot_along_cs>().color = color;
            nn.GetComponent<bot_along_cs>().td = d;
            nn.GetComponent<bot_along_cs>().cp = cp;

         //   nn.GetComponent<bot_along_cs>().t = t;
            //nn.GetComponent<bot_along_cs>().toch = toch;

            nn.GetComponent<bot_along_cs>().shbot = sha;
          //  nn.GetComponent<bot_along_cs>().pl = cam;
            nn.GetComponent<bot_along_cs>().enabled = true;    //  
            //vs.ac.
            ks++;
           // vs.cloncreate(n[k], 4);
           // k++;
        }

    }
    void r(Vector3Int spos, int p)
    {
        ra[spos.x, spos.y, spos.z] = p;
            if (spos.x > 0)
            {

                if (p - 1 > ra[spos.x - 1, spos.y, spos.z] && vs.cc.c[spos.x - 1, spos.y, spos.z]&&p!=1)
                    r(new Vector3Int(spos.x - 1, spos.y, spos.z), p - 1);
            }
            else
            {

                if (p - 1 > ra[199, spos.y, spos.z] && vs.cc.c[199, spos.y, spos.z] && p != 1)
                    r(new Vector3Int(199, spos.y, spos.z), p - 1);
            }

            if (spos.x < 199) { 
        
            if (p - 1 > ra[spos.x + 1, spos.y, spos.z] && vs.cc.c[spos.x + 1, spos.y, spos.z] && p != 1)
                r(new Vector3Int(spos.x + 1, spos.y, spos.z), p - 1);
            }
            else
            {

                if (p - 1 > ra[0, spos.y, spos.z] && vs.cc.c[0, spos.y, spos.z] && p != 1)
                    r(new Vector3Int(0, spos.y, spos.z), p - 1);
            }

            if (spos.z > 0) { 
        
            if (p - 1 > ra[spos.x, spos.y, spos.z - 1] && vs.cc.c[spos.x, spos.y, spos.z - 1] && p != 1)
                r(new Vector3Int(spos.x, spos.y, spos.z - 1), p - 1);
            }
            else
            {

                if (p - 1 > ra[spos.x, spos.y, 199] && vs.cc.c[spos.x, spos.y, 199] && p != 1)
                    r(new Vector3Int(spos.x, spos.y, 199), p - 1);
            }

            if (spos.z < 199) { 
        
            if (p - 1 > ra[spos.x, spos.y, spos.z + 1] && vs.cc.c[spos.x, spos.y, spos.z + 1] && p != 1)
                r(new Vector3Int(spos.x, spos.y, spos.z + 1), p - 1);
            }
            else
            {

                if (p - 1 > ra[spos.x, spos.y, 0] && vs.cc.c[spos.x, spos.y, 0] && p != 1)
                    r(new Vector3Int(spos.x, spos.y, 0), p - 1);
            }
            if (spos.y < 7)
        
            if (p - 1 > ra[spos.x, spos.y + 1, spos.z] &&vs.cc.c[spos.x, spos.y + 1, spos.z] && p != 1)
                r(new Vector3Int(spos.x, spos.y + 1, spos.z), p - 1);
        
        if (spos.y > 0)
        
            if (p - 1 > ra[spos.x, spos.y - 1, spos.z] && vs.cc.c[spos.x, spos.y - 1, spos.z] && p != 1)
                r(new Vector3Int(spos.x, spos.y - 1, spos.z), p - 1);
        
    }
    public void botinfo(Transform tr) {
        int g = 0;
        for (int i = 1; i < k; i++)
        {
            if (n[i].transform == tr)
                g = i;
        }
        vs.cnv.zanovo("об'єкт: бот{ номер: b" + botnomer[g].ToString() + "; розрядостійкість: " + botxp[g].ToString() + ";}.");
    }
    public void Delete(Transform tr,int uron)
    {

        int g = 0;
            for (int i = 1; i < k; i++)
            {
                if (n[i].transform == tr)
                    g = i;
            }

        if (botxp[g] <= uron)
        {
            Vector3 kk;
            kk = botpos[g];
            n[g].GetComponent<bot_along_cs>().roz();
            k--;
            // Debug.Log( n[k].GetComponent<MeshRenderer>().material.GetHashCode );
            //  Transform f= Instantiate(botvybux,n[g].transform.position,new Quaternion(),this.transform);
            //  f.GetComponent<bot_vybux_cs>().vs = vs;
            //  f.GetComponent<bot_vybux_cs>().pl = pl;
            //  f.GetComponent<bot_vybux_cs>().zar = zar;
            //  f.GetComponent<bot_vybux_cs>().player =player;
            // f.GetComponent<bot_vybux_cs>().rozeff = rozeff;
          //  Destroy(n[g].gameObject);
            for (int ii = g; ii < k; ii++)
            {
                n[ii] = n[ii + 1];
                npos[ii] = npos[ii + 1];
                botnomer[ii] = botnomer[ii + 1];
                botpos[ii] = botpos[ii + 1];
                        }
            n[k] = null;
            if (Random.Range(0, vs.xml.nal.vazhkist + 2) != 0)
            {
                Vector3 kkk = kk + new Vector3(4, 0, 0);
                kkk.y = vs.h[(int)(kkk.x), (int)(kkk.z)] + 0.01f;
                botAdd(kkk);
            }
                           
        }
        else
            botxp[g]-=uron;
    }
    bool str(Vector3 p1,Vector3 p2) {
        float i = 0;
        float d = VoidSystem_cs.Dist(p1,p2);
 
        while (i<d) {
            if (!vs.gravityper0(p1+i/d*(p2-p1)))
                return false;

            i +=0.1f;
        }
        return true;
    }
    void ttrfind(Transform t)
    {
        int i;
        i  = 0;
        while (i < t.childCount)
        {
            ttrfind(t.GetChild(i));
            i++;
        }
        if (t.GetComponent<MeshFilter>() != null)
        {
           // t.GetComponent<MeshFilter>().sharedMesh = t.GetComponent<MeshFilter>().mesh;
            tk += t.GetComponent<MeshFilter>().sharedMesh.vertices.Length;
            trk += (int)(t.GetComponent<MeshFilter>().sharedMesh.triangles.Length / 3);
        }
    }
    void componentfind(Transform t)
    {
        int i = 0;
        while (i < t.childCount)
        {
            componentfind(t.GetChild(i));
            i++;
        }
        if (t.GetComponent<MeshFilter>() != null)
        {
            for (int ii = 0; ii < t.GetComponent<MeshFilter>().sharedMesh.vertices.Length; ii++)
                tochky[ii + post] =t.position+ t.GetComponent<MeshFilter>().sharedMesh.vertices[ii] * 0.005f;

            for (int ii = 0; ii < t.GetComponent<MeshFilter>().sharedMesh.triangles.Length; ii++)
                por[ii + postr] = post + t.GetComponent<MeshFilter>().sharedMesh.triangles[ii];

            post += t.GetComponent<MeshFilter>().sharedMesh.vertices.Length;
            postr += t.GetComponent<MeshFilter>().sharedMesh.triangles.Length;

            for (int ii = trkk; ii < trkk + (int)(t.GetComponent<MeshFilter>().sharedMesh.triangles.Length / 3); ii++)
            {
                if (t.GetComponent<MeshRenderer>().sharedMaterial.shader == sha1)
                    color[ii] = new Color(0.5f, 0.5f, 0.5f);
                else
                    color[ii] = new Color(0,1,0);
            }
            trkk += (int)(t.GetComponent<MeshFilter>().sharedMesh.triangles.Length / 3);
        }
    }
}
