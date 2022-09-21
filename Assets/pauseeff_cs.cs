using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Threading;

public class pauseeff_cs : MonoBehaviour
{
    public VoidSystem_cs vs;

    [SerializeField] GameObject menu;            
    [HideInInspector] public bool realpause;
    int d;
    // public Material mat;
    //public Camera cam;
    Texture2D sT;
    Texture2D sourceTex;
    Color[] pix;
    Texture2D g;
    //int[] l=new int[(int)(Screen.width*0.2f)];
    // [SerializeField] bool effect1;
    // [SerializeField] bool effect2;
    [SerializeField] [Range(0, 2)] float zamorozmax;
    [SerializeField] [Range(0, 1)] float zamorozmin;
    [SerializeField] Color zamorozcolor;
    float time;
    int vysota;
    int v;
    int chas;
    Color[] pixef2;
    float livetime;
   [HideInInspector] public int k;
    Vector2Int p;
    int yy;

    void Start()
    {
        realpause = false;
        p = new Vector2Int(Screen.width, Screen.height);
        k = 0;
        GetComponent<Image>().material.mainTexture = g;
        GetComponent<Image>().enabled = false;
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        g = new Texture2D(Screen.width, Screen.height);
    }
    public IEnumerator Starteff(bool poch)
    {
        if (poch)
        {
            vs.gOp.enabled = false;
            realpause = true;
            livetime = 0.5f;
            Time.timeScale = 0;
            yield return new WaitForEndOfFrame();
            GetComponent<Image>().enabled = true;
            sT = new Texture2D(Screen.width, Screen.height);
            
            sT.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            sT.Apply();
            p = new Vector2Int((int)( sT.width/yy), (int)( sT.height/yy));
            Color[] u = new Color[p.x * p.y];
            for (int y = 0; y < p.y; y++)
                for (int x = 0; x < p.x; x++)
                    u[x + y * p.x] = sT.GetPixel(x * yy, y * yy);
            sourceTex = new Texture2D(p.x, p.y);
            sourceTex.SetPixels(u);
            sourceTex.Apply();
            g = new Texture2D(sourceTex.width, sourceTex.height);
            menu.SetActive ( false);
            pix = sourceTex.GetPixels(0, 0, sourceTex.width, sourceTex.height);
            pixef2 = new Color[pix.Length];
            for (int y = 0; y < sourceTex.height; y++)
                for (int x = 0; x < sourceTex.width; x++)
                {
                    float dist = Mathf.Sqrt((sourceTex.width * 0.5f - x) * (sourceTex.width * 0.5f - x) + (sourceTex.height * 0.5f - y) * (sourceTex.height * 0.5f - y)) * 2 / (sourceTex.height + sourceTex.width);
                    pixef2[y * sourceTex.width + x] = new Color(zamorozcolor.r * (zamorozmin + dist * dist * (zamorozmax - zamorozmin)), zamorozcolor.g * (zamorozmin + dist * dist * (zamorozmax - zamorozmin)), zamorozcolor.b * (zamorozmin + dist * dist * (zamorozmax - zamorozmin)));
                }
            time = 0;
            chas = 0;
           // g = new Texture2D(sourceTex.width, sourceTex.height);

            g.SetPixels(pix);
            g.Apply();
            k = 1;
            menu.SetActive(true);
        }
        else
            k = 2;
    }

    void Update()
    {
        yy = (4 - vs.xml.nal.yakistSpets);
        if (k == 1)
        {
            time += 0.05f;
            if (time <= 1.5f)
            {
                int x, y;
                for (int i = 0; i <80000; i++)
                {
                    x = Random.Range(0, sourceTex.width);
                    y = Random.Range(0, sourceTex.height);
                    pix[y * sourceTex.width + x] += pixef2[y * sourceTex.width + x] * 0.09f * Random.Range(0.7f, 1.65f);
                }
            }
            else
            {
                for (int y = 0; y < sourceTex.height; y++)
                    for (int x = 0; x < sourceTex.width; x++)
                        pixef2[y * sourceTex.width + x] = pix[y * sourceTex.width + x];
                if (time >= chas + 1)
                {
                    chas++;
                    if (chas % 2 == 1)
                    {
                        v = (int)(Random.Range(-10, 10) * sourceTex.width * 0.15f);
                        vysota = Random.Range(0, 20);
                    }
                    else
                    {
                        vysota = Random.Range(0, 20);
                        v = (int)(Random.Range(-10, 10) * sourceTex.height * 0.15f);
                    }
                }
                else
                {
                    if (chas % 2 == 1)//width
                    {
                        for (int y = (int)(vysota * 0.05f * sourceTex.height); y < (int)((vysota + 1) * 0.05f * sourceTex.height); y++)
                            for (int x = 0; x < sourceTex.width; x++)
                            {
                                if (v >= 0)
                                {
                                    if ((int)(v * 0.02f)+x < sourceTex.width)
                                        pix[y * sourceTex.width + x] = pixef2[y * sourceTex.width + x + (int)(v * 0.02f)];
                                    else
                                        pix[y * sourceTex.width + x] = pixef2[(y - 1) * sourceTex.width + x + (int)(v * 0.02f)];
                                }
                                else
                                {
                                    if (x + (int)(v * 0.02f) >= 0)
                                        pix[y * sourceTex.width + x] = pixef2[y * sourceTex.width + x + (int)(v * 0.02f)];
                                    else
                                        pix[y * sourceTex.width + x] = pixef2[(y + 1) * sourceTex.width + x + (int)(v * 0.02f)];
                                }
                            }
                    }
                    else
                    {
                        for (int y = 0; y < sourceTex.height; y++)
                            for (int x = (int)(vysota * 0.05f * sourceTex.width); x < (int)((vysota + 1) * 0.05f * sourceTex.width); x++)
                            {
                                if (v >= 0)
                                {
                                    if ((int)(v * 0.02f + y) < sourceTex.height)
                                        pix[y * sourceTex.width + x] = pixef2[(int)(v * 0.02f + y) * sourceTex.width + x];
                                    else
                                        pix[y * sourceTex.width + x] = pixef2[(int)(v * 0.02f + y - sourceTex.height) * sourceTex.width + x];
                                }
                                else
                                {
                                    if ((int)(v * 0.02f + y) >= 0)
                                        pix[y * sourceTex.width + x] = pixef2[(int)(v * 0.02f + y) * sourceTex.width + x];
                                    else
                                        pix[y * sourceTex.width + x] = pixef2[(int)(v * 0.02f + y + sourceTex.height) * sourceTex.width + x];
                                }
                            }
                    }

                }
            }
            g.SetPixels(pix);
            g.Apply();
        }
        else
        if (k == 2)
        {//(int)(0.5f * sT.width), (int)(0.5f * sT.height)
            for (int y = 0; y < p.y; y++)
                for (int x = 0; x < p.x; x++)
                    pix[y * p.x + x] += 0.45f * (sT.GetPixel(x * yy, y * yy) - pix[y * p.x + x]);

            g.SetPixels(pix);
            g.Apply();
            livetime -= 0.1f;
            if (livetime < 0)
            {
                k = 0;
                Time.timeScale = 1;
                realpause =false;
                menu.SetActive(false);
                vs.gOp.enabled = true;
            }
        }
        else
            GetComponent<Image>().enabled = false;
        GetComponent<Image>().material.mainTexture = g;
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

    }


}