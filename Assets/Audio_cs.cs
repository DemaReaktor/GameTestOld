using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_cs : MonoBehaviour
{
    //xml_cs xml;
    [HideInInspector] public bool xod;
    [HideInInspector] public bool povilno;
    [HideInInspector] public bool shvydko;
    [HideInInspector]public float razbros;
    [HideInInspector] public bool pal;
    [Space]
    [SerializeField] Transform[] zvukyObj;
    Transform[] zvukStr=new Transform[51];
     bool[] paus;
    int muz, palmuz;
    bool spov;
    //int ii;

    bool[] bo;
    void Start()
    {
        muz = Random.Range(0,3);
        zvukyObj[11].GetChild(muz).GetComponent<AudioSource>().Play();
        bo = new bool[zvukyObj.Length];
        paus = new bool[zvukyObj.Length];
        for (int i = 0; i < bo.Length; i++)
        {
            bo[i] = false;
            paus[i] = false;
        }
for(int i=0;i<51;i++)     
        {
            zvukStr[i] = Instantiate(new GameObject().transform,new Vector3(),new Quaternion());
            zvukStr[i].gameObject.AddComponent(typeof(AudioSource));
            zvukStr[i].GetComponent<AudioSource>().clip = zvukyObj[5].GetComponent<AudioSource>().clip;
        }
    }

    void Update()
    {
      //  if (!GetComponent<xml_cs>().vid)
       // {
            for (int i = 0; i < bo.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (xod != bo[0] || bo[1] != shvydko)
                        {
                            if (xod)
                            {
                                if (shvydko)
                                {
                                    zvukyObj[3].GetComponent<AudioSource>().Play();
                                    zvukyObj[0].GetComponent<AudioSource>().Stop();
                                }
                                else
                                {
                                    zvukyObj[3].GetComponent<AudioSource>().Stop();
                                    zvukyObj[0].GetComponent<AudioSource>().Play();
                                }
                            }
                            else
                            {
                                zvukyObj[3].GetComponent<AudioSource>().Stop();
                                zvukyObj[0].GetComponent<AudioSource>().Stop();
                            }
                        }
                        zvukyObj[i].GetComponent<AudioSource>().volume = povilno ? 0.3f : 1;
                        bo[0] = xod;
                        bo[1] = shvydko;
                        break;
                    case 4:
                        if (bo[i] != GetComponent<VoidSystem_cs>().peff.realpause)
                        {
                            if (!bo[i])
                                zvukyObj[4].GetComponent<AudioSource>().Play();
                            else
                                zvukyObj[4].GetComponent<AudioSource>().Stop();
                        }
                    if (Time.timeScale != 1)
                        for (int iii = 0; iii < zvukyObj.Length; iii++)
                            if (iii != 4)
                            {         if(iii!=11)
                                zvukyObj[iii].GetComponent<AudioSource>().Stop();
                            else                              
                                zvukyObj[iii].GetChild(muz).GetComponent<AudioSource>().Stop();

                            }

                        bo[i] = Time.timeScale != 1;
                        break;
                    case 6:
                        if (!zvukyObj[6].GetComponent<AudioSource>().isPlaying && Time.timeScale == 1)
                            zvukyObj[6].GetComponent<AudioSource>().Play();
                        break;
                    case 7:
                        if (bo[i] != pal)
                        {
                            if (pal)
                            {
                                zvukyObj[8].GetComponent<AudioSource>().Play();
                                zvukyObj[11].GetChild(muz).GetComponent<AudioSource>().Stop();
                            }
                            else
                            {
                                zvukyObj[8].GetComponent<AudioSource>().Stop();
                                zvukyObj[11].GetChild(muz).GetComponent<AudioSource>().Play();
                            }
                        }
                        bo[i] = pal;
                        break;
                    case 11:
                    if (!zvukyObj[11].GetChild(muz).GetComponent<AudioSource>().isPlaying  && !pal&&Time.timeScale==1)
                    {
                        muz = Random.Range(0, 3);
                        zvukyObj[11].GetChild(muz).GetComponent<AudioSource>().Play();
                    }  
                        break;
                }
            }
            for (int i = 0; i < zvukyObj.Length; i++)
            {
            if(i!=11)
                zvukyObj[i].GetComponent<AudioSource>().volume = GetComponent<xml_cs>().nal.zvuk;
            else
                zvukyObj[i].GetChild(muz).GetComponent<AudioSource>().volume = GetComponent<xml_cs>().nal.zvuk;
                switch (i)
                {
                    case 0:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.xodba;
                        break;
                    case 1:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.xodba;
                        break;
                    case 2:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.udary;
                        break;
                    case 3:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.xodba;
                        break;
                    case 4:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.muzika;
                        break;
                    case 5:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.strilba;
                        break;
                    case 6:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.shum * razbros;
                        break;
                    case 7:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.muzika;
                        break;
                    case 8:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.muzika;
                        break;
                    case 9:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.muzika;
                        break;
                    case 10:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.strilba * GetComponent<xml_cs>().nal.udary;
                        break;
                    case 11:
                        zvukyObj[i].GetChild(muz).GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.muzika;
                        break;
                    case 12:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.strilba;
                        break;
                    // case 13:
                    //   zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.strilba;
                    //   break;
                    // case 14:
                    //   for(int o=0;o< GetComponent<bot_creating_cs>().)
                    //zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.zvukBotiv;
                    //  break;
                    case 14:
                        zvukyObj[i].GetComponent<AudioSource>().volume *= GetComponent<xml_cs>().nal.zvukBotiv * GetComponent<xml_cs>().nal.udary;
                        break;
                }
            }
       
      //  }
    }
    public void strybokp() {
        zvukyObj[1].GetComponent<AudioSource>().Play();
    }
    public void strybokk()
    {          if (!zvukyObj[2].GetComponent<AudioSource>().isPlaying)
        zvukyObj[2].GetComponent<AudioSource>().Play();
    }
    public void pausa(bool f) {
        if (f) for (int i = 0; i < zvukyObj.Length; i++)
            {      if (i != 11)
                {
                    if ( zvukyObj[i].GetComponent<AudioSource>().isPlaying )
                        paus[i] = true;
                }
                if(i!=11)
                zvukyObj[i].GetComponent<AudioSource>().Pause();
                else
                    zvukyObj[i].GetChild(muz).GetComponent<AudioSource>().Pause();

                if (i == 13)
                {
                    for (int o = 0; o < GetComponent<VoidSystem_cs>().bcc.k; o++)
                        GetComponent<VoidSystem_cs>().bcc.n[o].GetComponent<AudioSource>().Pause();
                }
                foreach (Transform ii in zvukStr)
                    ii.GetComponent<AudioSource>().Stop();
            }
        else
            for (int i = 0; i < zvukyObj.Length; i++)
            {
                if (paus[i])
                {
                   // ii = i;
                   // Invoke("p", 0.3f);
                    StartCoroutine(p(zvukyObj[i].gameObject));
                }  
                paus[i] = false;
               // if(i==13)
                  //  for (int o = 0; o < GetComponent<VoidSystem_cs>().bcc.k; o++)
                     //   if(!GetComponent<VoidSystem_cs>().bcc.n[o].GetComponent<AudioSource>().isPlaying)
                     //   GetComponent<VoidSystem_cs>().bcc.n[o].GetComponent<AudioSource>().Play();
            }
    }
    IEnumerator p(GameObject ii)
    {
        yield return new WaitForSeconds(0.3f);
        ii.GetComponent<AudioSource>().Play();
    }
 
    public void zvudVimk(int i)
    {
        zvukyObj[i].GetComponent<AudioSource>().Play();
    }
    public void STR()
    {
foreach(Transform i in zvukStr)
            if(!i.GetComponent<AudioSource>().isPlaying)
            {
                i.GetComponent<AudioSource>().Play();
                break;
            }
            }
}
