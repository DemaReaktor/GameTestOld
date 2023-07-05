using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryCharge : MonoBehaviour
{
    public GameObject go;
    [HideInInspector] public float charge;
    public Text t;
    public Material mat;
    public float xod;
    public float pryskor;
    public float strybok;
    public float promf;
    public float strilba;
    public float vybux;
    public float pausaa;
    public float zagr;
    public float magnit;
    public float map;
    float speedOfLosing =0.05f;


    void Start()
    {
        go.SetActive(false);
        charge = 100;
        //if(vs.xml.nal.vazhkist==1)
        //{
        //    xod*=0.6f;
        //    pryskor *= 0.6f;
        //    strybok *= 0.6f;
        //    promf *= 0.6f;
        //    strilba *= 0.6f;
        //    vybux *= 0.6f;
        //    pausaa *= 0.6f;
        //    zagr *= 0.6f;
        //    magnit *= 0.6f;
        //    map *= 0.6f;
        //    speedOfLosing = 0.03f;
        //}
        //if (vs.xml.nal.vazhkist == 0)
        //{
        //    xod *= 0.2f;
        //    pryskor *= 0.2f;
        //    strybok *= 0.2f;
        //    promf *= 0.2f;
        //    strilba *= 0.2f;
        //    vybux *= 0.2f;
        //    pausaa *= 0.2f;
        //    zagr *= 0.2f;
        //    magnit *= 0.2f;
        //    map *= 0.2f;
        //    speedOfLosing = 0.01f;
        //}
    }

    void Update()
    {
        charge -= speedOfLosing * Time.deltaTime;
        if (charge <= 0)
        {
            go.SetActive(true);
            charge = 0;
            Time.timeScale = 0;
        }
        t.text =$"{(int)charge}%";
        mat.SetFloat("_R",charge*0.01f);
    }
}
