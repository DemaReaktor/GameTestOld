using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Video;



public class zagr_cs : MonoBehaviour
{
    [SerializeField] Texture2D t;
    //[SerializeField] Texture2D t1;
   // VideoClip t1;
    float time;
    Texture2D g;
    Color[] p = new Color[230400]; //{ new Color(0,0,0,1)};
    int[] k = new int[230400];
    float kk;
    int n;
    void Start()
    {
        n = 0;
        //Time.timeScale = 0.2f;
        //1280 720 00
        time = 0;
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        g = new Texture2D(640, 360);

        for (int i = 0; i < 230400; i++)
            k[i] = (t.GetPixel(i*2 % 1280, (int)(i / 640)*2).r != 0 || t.GetPixel(i*2 % 1280, (int)(i / 640) * 2).b != 0|| t.GetPixel(i*2 % 1280, (int)(i / 640) * 2).g != 0) ?Random.Range(Random.Range(1,5),5):0;

            for (int i = 0; i < 230400; i++)
            p[i] = new Color(0,0,0,1);
        g.SetPixels(p);
        g.Apply();
        GetComponent<Image>().material.mainTexture = g;
        kk = VoidSystem_cs.Dist(new Vector3(),new Vector3(640,360));
    }

    void Update()
    {
        Cursor.visible = false;

        time += Time.deltaTime;
        if(time>=3.5f||Input.GetKeyUp(KeyCode.Return))
        {
            if (File.Exists("Nalashtuvania.xml"))
                SceneManager.LoadScene(1);
            else
            {
                 SceneManager.LoadScene(2);
            }
        }
        if (time <= 2.5f)
        {
            int nomer = 1;

            float r = time-0.5f;
            while (r >= 0.5f)
            {
                r -= 0.5f;
                nomer++;
            }
            if (n != nomer&&r>=0.125f)
            {
                n = nomer;
                GetComponent<AudioSource>().Play();
            }
            // r *= 2.5f;
            r *= 3.2f;
            r -= 0.25f;
            // if(nomer==1)
            for (int y = 0; y < 360; y++)
                for (int x = 0; x < 640; x++)
                {
                    float d = VoidSystem_cs.Dist(new Vector3(nomer == 1 || nomer == 4 ? 0 : 640, nomer <= 2 ? 360 : 0), new Vector3(x, y)) / kk;
                    //(k[x*Screen.height+y])
                    //  if(x==1100&&y==160)
                    //  Debug.Log(d);
                    // float e=;
                    int f = x + 640 * y;


                    if (k[f] < nomer && k[f] != 0||(k[f]==nomer&&d<=r))
                        p[f] = t.GetPixel(x * 2, y * 2);
                    else
                        if (Mathf.Abs(d - r) > 0.25f)
                        p[f] = new Color(0, 0, 0, 1);
                    else
                        if (k[f] == nomer)
                        p[f] = new Color(t.GetPixel(x * 2, y * 2).r * Mathf.Cos((d - r) * 6.28f), t.GetPixel(x * 2, y * 2).g * Mathf.Cos((d - r) * 6.28f), t.GetPixel(x * 2, y * 2).b * Mathf.Cos((d - r) * 6.28f), 1);
                    else
                     //   if(k[f]==0)
                    p[f] = new Color(Random.Range(0, 50) * 0.01f * Mathf.Cos((d - r) * 6.28f), Random.Range(0, 50) * 0.01f * Mathf.Cos((d - r) * 6.28f), Random.Range(0, 50) * 0.01f * Mathf.Cos((d - r) * 6.28f), 1);
                         // else
                        //p[f] = new Color(0, 0, 0, 1);
                }
            g.SetPixels(p);
            g.Apply();
        }
        else
        {
            float d = time - 1.5f;
            p = new Color[(int)(360*d)*(int)(640*d)];
            for (int y = 0; y < (int)(360*d); y++)
                for (int x = 0; x < (int)(640*d); x++)
                    p[x + (int)(640*d) * y] = t.GetPixel((int)(x*2/d),(int)(y*2/d));
            g = new Texture2D((int)(640 * d), (int)(360 * d));
            g.SetPixels(p);
            g.Apply();
        }
        
    }
}
