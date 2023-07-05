using System.Collections;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Video;



public class XML
{
    public int riven;

    public int mova;

    public int vazhkist;

    public int yakistText;
    public int yakistSpets;
    public int rozmirEkrana;

    public float zvuk;
    public float muzika;
    public float strilba;
    public float zvukBotiv;
    public float xodba;
    public float udary;
    public float shum;

    public KeyCode vpered;
    public KeyCode nazadR;
    public KeyCode vlivo;
    public KeyCode vpravo;
    public KeyCode pauza;
    public KeyCode strilbaK;
    public KeyCode prytsil;
    public KeyCode Rsp;
    public KeyCode big;
    public KeyCode povzania;
    public KeyCode Zbroya1;
    public KeyCode Zbroya2;
    public KeyCode Zbroya3;
    public KeyCode Zbroya4;
    public KeyCode Rvceb;
    public KeyCode strybok;
    public KeyCode maybStrybok;
    public KeyCode SPauk;
    public KeyCode DdoZbroyi;
    public bool x;
    public XML()
    {
        riven = 0;

        mova = 0;

        vazhkist = 1;

        yakistSpets = 0;
        yakistText = 0;
        rozmirEkrana = 3;

        zvuk = 1;
        muzika = 0.5f;
        strilba = 0.5f;
        zvukBotiv = 0.5f;
        xodba = 0.5f;
        udary = 0.5f;
        shum = 0.5f;

        vpered = KeyCode.W;  //
        nazadR = KeyCode.S;  //
        vlivo = KeyCode.A; //
        vpravo = KeyCode.D; //
        pauza = KeyCode.Escape;   //
        strilbaK = KeyCode.Mouse0;  //
        prytsil = KeyCode.Mouse1;   //
        Rsp = KeyCode.Tab;   //
        big = KeyCode.LeftShift; //
        povzania = KeyCode.LeftControl; //
        Zbroya1 = KeyCode.Alpha1; //
        Zbroya2 = KeyCode.Alpha2;//
        Zbroya3 = KeyCode.Alpha3;  //
        Zbroya4 = KeyCode.Alpha4; //
        Rvceb = KeyCode.Z;
        strybok = KeyCode.Space;  //
        maybStrybok = KeyCode.E;  //
        SPauk = KeyCode.F;  //
        DdoZbroyi = KeyCode.Q;  //
        x = true;
    }
}

public class xml_cs : MonoBehaviour
{
    [HideInInspector] public bool vid;
    public XML nal;
    [SerializeField] Transform videoObj;
    [SerializeField] Transform canvasObj;
   // [HideInInspector] public Vector2Int screen;
    void Awake()
    {
        var xml = new XmlSerializer(typeof(XML));
         nal = new XML();
        //if (transform.GetComponent<VoidSystem_cs>())
        //    transform.GetComponent<VoidSystem_cs>().Star();
        if (File.Exists("Nalashtuvania.xml"))
        {
            using (var c = new FileStream("Nalashtuvania.xml", FileMode.Open, FileAccess.Read))
            {
                nal = xml.Deserialize(c) as XML;
            }
            zmina(nal);
            //if (transform.GetComponent<VoidSystem_cs>())
            //    f();
        }
        else
        {
            Screen.SetResolution(1280, 720, true);
            using (var c = new FileStream("Nalashtuvania.xml", FileMode.Create, FileAccess.Write))
            {
                xml.Serialize(c, nal);
            }
            // SceneManager.LoadScene(2);
            if (videoObj != null)
            {

                //Time.timeScale = 0;
                vid = true;
                //GetComponent<VoidSystem_cs>().peff.realpause = true;
                videoObj.GetComponent<VideoPlayer>().Play();
                for (int i = 0; i < canvasObj.childCount - 2; i++)
                    canvasObj.GetChild(i).gameObject.SetActive(false);
            }
            else
                vid = false;
        }
        if (nal.yakistSpets != 2)
        {
            //GetComponent<VoidSystem_cs>().mc.enabled = false;
            //GetComponent<VoidSystem_cs>().pc.enabled = false;
            //if (nal.yakistSpets != 1)
            //{
            //    GetComponent<VoidSystem_cs>().mc.enabled = false;
            //    GetComponent<VoidSystem_cs>().pc.enabled = false;
            //}
        }

        Transform.FindObjectOfType<VoidSystem_cs>().StartAfterXML();
    }
    public void zmina(XML obj,bool a=false)
    {
       nal= pryrivnity(obj);
        var xml = new XmlSerializer(typeof(XML));
        using (var c = new FileStream("Nalashtuvania.xml", FileMode.Create, FileAccess.Write))
        {
            xml.Serialize(c, nal);
        }
        if (a)
        {
            Vector2Int screen = new Vector2Int(1920, 1080);
            switch (nal.rozmirEkrana)
            {
                case 1:
                    screen = new Vector2Int(1920, 1080);
                    break;
                case 2:
                    screen = new Vector2Int(1366, 768);
                    break;
                case 3:
                    screen = new Vector2Int(1280, 720);
                    break;
                case 4:
                    screen = new Vector2Int(1024, 768);
                    break;
                case 5:
                    screen = new Vector2Int(768, 576);
                    break;
                case 6:
                    screen = new Vector2Int(720, 480);
                    break;
            }
            Screen.SetResolution(screen.x, screen.y, true);
            SceneManager.LoadScene(1);
        }
    }
    public XML pryrivnity(XML a)
    {
       var b = new XML();
        b.mova = a.mova;

        b.vazhkist = a.vazhkist;

        b.yakistText = a.yakistText;
        b.yakistSpets = a.yakistSpets;
        b.rozmirEkrana = a.rozmirEkrana;

        b.zvuk = a.zvuk;
        b.muzika = a.muzika;
        b.strilba = a.strilba;
        b.zvukBotiv = a.zvukBotiv;
        b.xodba = a.xodba;
        b.udary = a.udary;
        b.shum = a.shum;

        b.vpered = a.vpered;
        b.nazadR = a.nazadR;
        b.vlivo = a.vlivo;
        b.vpravo = a.vpravo;
        b.pauza = a.pauza;
        b.strilbaK = a.strilbaK;
        b.prytsil = a.prytsil;
        b.Rsp = a.Rsp;
        b.big = a.big;
        b.povzania = a.povzania;
        b.Zbroya1 = a.Zbroya1;
        b.Zbroya2 = a.Zbroya2;
        b.Zbroya3 = a.Zbroya3;
        b.Zbroya4 = a.Zbroya4;
        b.Rvceb = a.Rvceb;
        b.strybok = a.strybok;
        b.maybStrybok = a.maybStrybok;
        b.SPauk = a.SPauk;
        b.DdoZbroyi = a.DdoZbroyi;
        return b;
    }


    private void Update()
    {
        if (videoObj != null)
        {
            //videoObj.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, GetComponent<VoidSystem_cs>().xml.nal.zvuk);
            if ((!videoObj.GetComponent<VideoPlayer>().isPlaying || Input.GetKeyUp(KeyCode.Return)) && vid)
                f();
        }
    }
     void f()
    {
        Time.timeScale = 1;
        vid = false;
        //GetComponent<VoidSystem_cs>().peff.realpause = false;
        for (int i = 0; i < canvasObj.childCount - 2; i++) 
            if(i!=3)
            canvasObj.GetChild(i).gameObject.SetActive(true);
        //GetComponent<VoidSystem_cs>().ac.enabled = true;
        //GetComponent<VoidSystem_cs>().bcc.enabled = true;
        //GetComponent<VoidSystem_cs>().gOp.enabled = true;
        //GetComponent<VoidSystem_cs>().mp.enabled = true;
        //GetComponent<VoidSystem_cs>().cnv.enabled = true;

        //GetComponent<VoidSystem_cs>().pl.enabled = true;
        //GetComponent<VoidSystem_cs>().pl.transform.GetChild(0).GetComponent<prominfuture>().enabled = true;


        videoObj.gameObject.SetActive(false);
    }
}
