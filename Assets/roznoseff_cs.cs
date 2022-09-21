using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roznoseff_cs : MonoBehaviour
{
    public VoidSystem_cs vs;
    //public Transform r;
    // public Transform g;

    // Texture2D rtex;
    // Texture2D gtex;
    Texture2D tex;

    //Color[] rp;
    //Color[] gp;

    Color[] pix;

    [HideInInspector] public float roznos;
    float roztime;
    float rph;
    int d;
    Texture2D sT;
    Vector2Int p;

    void Start()
    {
        p = new Vector2Int((int)(0.2f * Screen.width), (int)(0.2f * Screen.height));

        //r.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        //g.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        // rtex = new Texture2D(p.x,p.y);
        // gtex = new Texture2D(p.x, p.y);
        pix = new Color[p.x * p.y];

        tex = new Texture2D(p.x, p.y);

        tex.SetPixels(pix);
        roznos = roztime =rph= 0;
    }

    public IEnumerator roz()
    {
        yield return new WaitForEndOfFrame();

        sT = new Texture2D(Screen.width, Screen.height);
        sT.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        sT.Apply();
      //  rp = new Color[p.x*p.y];
       // gp = new Color[p.x * p.y];

        pix = new Color[p.x * p.y];

        float h = (1+ vs.xml.nal.vazhkist)*0.3f* roznos*0.02f;
        if (h > 0.96f)
            h = 0.96f;
        vs.ac.razbros = h;
        // int h = Mathf.Abs(d);
        for (int y = 0; y < p.y; y++)
            for (int x = 0; x < p.x; x++)
            {
                pix[x + y * p.x].a = h;
                pix[x + y * p.x].b = sT.GetPixel(x * 5, y * 5).b;
            }
        if (d >= 0) {
            while (d >= p.x)
                d -= p.x;

            for (int y = 0; y < p.y; y++)
                for (int x = 0; x <d; x++)
                {
                    pix[x + y * p.x].r = sT.GetPixel((x - d+p.x) * 5, y * 5).r;
                    pix[x + y * p.x].g = sT.GetPixel((x + d) * 5, y * 5).g;
                }
            for (int y = 0; y < p.y; y++)
                for (int x = d; x < p.x-d; x++)
                {
                    pix[x + y * p.x].r = sT.GetPixel((x - d) * 5, y * 5).r;
                    pix[x + y * p.x].g = sT.GetPixel((x + d) * 5, y * 5).g;
                }
            for (int y = 0; y < p.y; y++)
                for (int x = p.x-d; x < p.x; x++)
                {
                    pix[x + y * p.x].r = sT.GetPixel((x - d) * 5, y * 5).r;
                    pix[x + y * p.x].g = sT.GetPixel((x + d-p.x) * 5, y * 5).g;
                }
        }
        else
        {
            while (d <= -p.x)
                d += p.x;

            for (int y = 0; y < p.y; y++)
                for (int x = 0; x < -d; x++)
                {
                    pix[x + y * p.x].r = sT.GetPixel((x - d) * 5, y * 5).r;
                    pix[x + y * p.x].g = sT.GetPixel((x + d+p.x) * 5, y * 5).g;
                }
            for (int y = 0; y < p.y; y++)
                for (int x = -d; x < p.x + d; x++)
                {
                    pix[x + y * p.x].r = sT.GetPixel((x - d) * 5, y * 5).r;
                    pix[x + y * p.x].g = sT.GetPixel((x + d) * 5, y * 5).g;
                }
            for (int y = 0; y < p.y; y++)
                for (int x = p.x + d; x < p.x; x++)
                {
                    pix[x + y * p.x].r = sT.GetPixel((x - d-p.x) * 5, y * 5).r;
                    pix[x + y * p.x].g = sT.GetPixel((x + d) * 5, y * 5).g;
                }
        }

           /* for (int y = h; y < p.y-h; y++)
                for (int x = h; x < p.x-h; x++)
                {
                //   rp[x + y * Screen.width].r = sT.GetPixel(x, y).r;
                // gp[]
                // rp[x + y * Screen.width].g = 0;
                // rp[x + y * Screen.width].b = 0;

                // gp[x + y * Screen.width].r = 0;

                pix[x + y * p.x].a = 0.85f;
                pix[x + y * p.x].r = sT.GetPixel((x-d) * 5, y * 5).r;
                pix[x + y * p.x].g = sT.GetPixel((x+d) * 5, y * 5).g;
                pix[x + y * p.x].b = sT.GetPixel(x * 5, y * 5).b;

                /* rp[x + y *p.x].r = sT.GetPixel(x*4, y*4).r;
                 rp[x + y * p.x].g = 0;
                 rp[x + y * p.x].b = 0;

                 gp[x + y * p.x].r = 0;
                 gp[x + y * p.x].b = 0;
                 gp[x + y *p.x].g = sT.GetPixel(x*4, y*4).g;

                 gp[x + y *p.x].a = 0.5f;
                 rp[x + y *p.x].a = 0.5f;
            }*/
        // rtex.SetPixels(rp);
        //rtex.Apply();

        // gtex.SetPixels(gp);
        // gtex.Apply();

        tex.SetPixels(pix);
        tex.Apply();

        // r.GetComponent<Image>().material.mainTexture = rtex;
        // g.GetComponent<Image>().material.mainTexture = gtex;

      //  GetComponent<Image>().material.mainTexture = tex;

    }
    void Update()
    {
        //r.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        // g.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        if (vs.zar.zariadka < 35)
            roznos +=( (35 - vs.zar.zariadka)  * 0.05f+4.95f )* Time.deltaTime;
        GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        if (roznos > 0)
        {
            roztime += Time.deltaTime;
            rph += Time.deltaTime;
            d = (int)(Mathf.Sin(roztime) * roznos*0.5f);

            if (rph >= 0.25f)
            {
                rph = 0;
                StartCoroutine(roz());
            }

            // r.GetComponent<RectTransform>().localPosition = new Vector3(d*2,0,0);
            // g.GetComponent<RectTransform>().localPosition = new Vector3(-d*2,0,0);

            //r.GetComponent<Image>().material.mainTexture = rtex;
            // g.GetComponent<Image>().material.mainTexture = gtex;

            GetComponent<Image>().material.mainTexture = tex;

            // r.GetComponent<Image>().enabled = true;
            // g.GetComponent<Image>().enabled = true;

            GetComponent<Image>().enabled = true;
            roznos -= Time.deltaTime * 5;
            if (roznos < 0)
                roznos = 0;
        }
        else
        {
            roztime = 0;
            rph = 0;

            //r.GetComponent<RectTransform>().localPosition = new Vector3(d, 0, 0);
            //g.GetComponent<RectTransform>().localPosition = new Vector3(-d, 0, 0);

            // r.GetComponent<Image>().enabled = false;
            // g.GetComponent<Image>().enabled = false;

            GetComponent<Image>().enabled = false;
        }

    }
}
