using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cnopkovod_cs : MonoBehaviour
{
  //  public zariad_cs zar;
    public pauseeff_cs peff;
    public VoidSystem_cs vs;
  //  public xml_cs xml;
  //  public bot_creating_cs bcs;

    //  public KeyCode vpered;
    //  public KeyCode nazad;
    // public KeyCode vlivo;
    //  public KeyCode napravo;

    //  public KeyCode strybok;
    //  public KeyCode verx;

    //  public KeyCode bih;
    //public KeyCode upovilnenia;

    //  public KeyCode pausa;
    // public KeyCode realpausa;
    //  public KeyCode vyxid;

    //  public KeyCode prominfuture;

    //  public KeyCode prycil;
    //  public KeyCode vogon;

    //  public KeyCode map;

    [HideInInspector] public bool pause;
   // Transform n;
    public Transform botnavod;
    public Text pomichnyk;
    float time;
    string tex;
    string tex1;
    int texint;
    public int drukv;
    void Start()
    {
      vs = this.transform.GetComponent<VoidSystem_cs>();
    texint = 0;
        time = 0;
        tex = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(vs.xml.nal.SPauk))
            zanovo("Система <Паук>{статус: активовано;}.");
        if (Input.GetKeyUp(vs.xml.nal.SPauk))
            zanovo("Система <Паук>{статус: деактивовано;}.");
        time += Time.deltaTime / Time.timeScale;
        if (tex == null)
        {
            texint = 0;
            tex1 = null;
        }
        else
        if (time * drukv >= 1&& texint != tex.Length)
        {
            time = 0;
            tex1 += tex[texint];
            texint++;
            pomichnyk.text = string.Format("{0}{1}", tex1, "_");
            // tex.Length--;
        }
        if (Input.GetKeyDown(vs.xml.nal.pauza))
            vs.Pausa();
        if (Input.GetKeyDown(vs.xml.nal.Rsp))
        {
            pause = !pause;
            if (pause)
            {
                Time.timeScale = 0.05f;
                zanovo("Режим <Сповільнення>{статус: активовано;}.");
            }
            else { zanovo("Режим <Сповільнення>{статус: деактивовано;}.");
                Time.timeScale = 1;
            }

        }


        if (pause)  
            vs.zar.zariadka -= vs.zar.pausaa * Time.deltaTime;
        
       
    }
    public void zanovo(string teext)
    {
        if (teext != tex)
        {
            texint = 0;
            tex1 = null;
            tex = teext;
        }
    }
 
}
