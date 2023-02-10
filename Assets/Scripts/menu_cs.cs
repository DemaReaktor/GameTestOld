using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu_cs : MonoBehaviour
{
    //public int scenen;
    // [SerializeField] int dovzh;
    public VoidSystem_cs vs;
    //Scene menu;
    // Scene gra;
    bool menuh;
    float x;
    int pos;
    int h;
   // float vx, vy;
    XML xm;
    int ps;
    float ff;
    [SerializeField] Material zn1;
    [SerializeField] Material zn2;
    [SerializeField] Material zn3;

    [SerializeField] Material pr1;
    [SerializeField] Material pr2;
    void Start()
    {
        ff = 0;
        pos = 0;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            menuh = true;
          xm=  GetComponent<xml_cs>().pryrivnity(GetComponent<xml_cs>().nal);
        }
        else {
          xm=  vs.xml.pryrivnity(vs.xml.nal);
            menuh = false;
        }
        x = Screen.width / 128;
        if (x * 9 > Screen.height / 8)
            x = Screen.height / 9 / 8;

        //  transform.localScale = new Vector3(1.5f,1.5f,1.5f);
       
        // p.GetComponent<RectTransform>().localPosition = new Vector3( Screen.width, Screen.height);
        // knop[6] = this.transform.GetChild(6);
       
        //   transform.GetComponent<RectTransform>().localScale = new Vector3(Screen.width/1280, Screen.height / 720,1);
    }
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
        transform.GetChild(xm.mova).gameObject.SetActive(true);

        for (int i = 0; i < transform.GetChild(xm.mova).childCount; i++)
            transform.GetChild(xm.mova).GetChild(i).gameObject.SetActive(false);
        transform.GetChild(xm.mova).GetChild(pos).gameObject.SetActive(true);

        mousepos(transform);
        switch (pos) {
            case 0:
                {

                    h = (int)(5 - Input.mousePosition.y * 5 / Screen.height) % 5;
                    if (h < 0)
                        h = 0;
                    if (h > 4)
                        h = 4;

                    transform.GetChild(xm.mova).GetChild(0).GetChild(5).GetComponent<RectTransform>().localPosition = new Vector2(Screen.width * 0.05f, (4.5f - h) * Screen.height * 0.2f);

                    if (Input.mousePosition.x < Screen.width * 0.1f || Input.mousePosition.x > Screen.width * 0.6f)
                        transform.GetChild(xm.mova).GetChild(0).GetChild(5).GetComponent<Image>().enabled = false;
                    else
                        transform.GetChild(xm.mova).GetChild(0).GetChild(5).GetComponent<Image>().enabled = true;

                    if (Input.GetKeyUp(KeyCode.Mouse0) && Input.mousePosition.x > Screen.width * 0.1f && Input.mousePosition.x < Screen.width * 0.6f)
                        switch (h)
                        {
                            case 0:
                                if (!menuh)
                                    vs.Pausa();
                                else
                                {
                                    Time.timeScale = 1;
                                    SceneManager.LoadScene(2);
                                }
                                break;
                            case 1:
                                break;
                            case 2:
                                pos = 1;
                                if(menuh)
                               xm=     GetComponent<xml_cs>().pryrivnity(GetComponent<xml_cs>().nal);
                                else
                              xm=  vs.xml.pryrivnity(vs.xml.nal);
                                break;
                            case 3:
                                break;
                            case 4:
                                if (!menuh)
                                    SceneManager.LoadScene(1);
                                else
                                    Application.Quit();
                                break;
                        }
                    //  if(Input.GetKeyDown(KeyCode.Mouse0)&& Input.mousePosition.y< knop[6].GetComponent<RectTransform>().sizeDelta)
                    //   if ((Input.mousePosition.y < knop[5].GetComponent<RectTransform>().localPosition.y ||
                    //      (Input.mousePosition.y < knop[6].GetComponent<RectTransform>().sizeDelta.y * 2 + knop[6].GetComponent<RectTransform>().localPosition.y && mova)) &&
                    //     Input.mousePosition.x < knop[6].GetComponent<RectTransform>().sizeDelta.x * 0.5f + knop[5].GetComponent<RectTransform>().localPosition.x &&
                    //      Input.mousePosition.x > knop[5].GetComponent<RectTransform>().localPosition.x - knop[6].GetComponent<RectTransform>().sizeDelta.x * 0.5f
                    //  )
                    //  {
                    //     mova = true;
                    //  }
                    //   else
                    //      mova = false;

                    if (h == 4 && Input.mousePosition.x > Screen.width * 0.73f && Input.mousePosition.x < Screen.width * 0.87f)
                    {
                        transform.GetChild(xm.mova).GetChild(0).GetChild(7).gameObject.SetActive(true);
                        if (Input.GetAxis("Mouse ScrollWheel") < 0)
                        {
                            transform.GetChild(xm.mova).GetChild(0).GetChild(6).GetChild(xm.mova).gameObject.SetActive(false);
                            xm.mova++;
                            if (xm.mova > 2)
                                xm.mova = 0;
                            transform.GetChild(xm.mova).GetChild(0).GetChild(6).GetChild(xm.mova).gameObject.SetActive(true);
                        }
                        if (Input.GetAxis("Mouse ScrollWheel") > 0)
                        {
                            transform.GetChild(xm.mova).GetChild(0).GetChild(6).GetChild(xm.mova).gameObject.SetActive(false);
                            xm.mova--;
                            if (xm.mova < 0)
                                xm.mova = 2;
                            transform.GetChild(xm.mova).GetChild(0).GetChild(6).GetChild(xm.mova).gameObject.SetActive(true);
                        }
                    }
                    else
                        transform.GetChild(xm.mova).GetChild(0).GetChild(7).gameObject.SetActive(false);
                }
                break;
            case 1:
                {
                    h = (int)(5 - Input.mousePosition.y * 5 / Screen.height) % 5;
                    if (h < 0)
                        h = 0;
                    if (h > 4)
                        h = 4;

                    if (!menuh?porivniania(xm, vs.xml.nal): porivniania(xm, GetComponent<xml_cs>().nal))
                        transform.GetChild(xm.mova).GetChild(1).GetChild(6).GetComponent<Image>().material = pr2;
                    else
                        transform.GetChild(xm.mova).GetChild(1).GetChild(6).GetComponent<Image>().material = pr1;

                    // Debug.Log(xm);
                    // Debug.Log(xcs.nal);
                    transform.GetChild(xm.mova).GetChild(1).GetChild(5).GetComponent<RectTransform>().localPosition = new Vector2(Screen.width * 0.05f, (4.5f - h) * Screen.height * 0.2f);
                    if (Input.mousePosition.x < Screen.width * 0.1f || Input.mousePosition.x > Screen.width * 0.6f)
                        transform.GetChild(xm.mova).GetChild(1).GetChild(5).GetComponent<Image>().enabled = false;
                    else
                        transform.GetChild(xm.mova).GetChild(1).GetChild(5).GetComponent<Image>().enabled = true;

                    if (Input.GetKeyUp(KeyCode.Mouse0) && Input.mousePosition.x > Screen.width * 0.1f && Input.mousePosition.x < Screen.width * 0.6f)
                    {
                        switch (h)
                        {
                            case 0:
                                pos = 0;
                                if(menuh)
                                 xm=   GetComponent<xml_cs>().pryrivnity(GetComponent<xml_cs>().nal);
                                else
                              xm=  vs.xml.pryrivnity(vs.xml.nal);

                                break;
                            case 1:
                                pos = 5;
                                ps = 0;
                                break;
                            case 2:
                                pos = 4;
                                ps = 0;
                                break;
                            case 3:
                                pos = 3;
                                break;
                            case 4:
                                pos = 2;
                                break;
                        }
                    }
                    if (h == 4 && Input.GetKeyUp(KeyCode.Mouse0) && Input.mousePosition.x > Screen.width * 0.6f)
                    {
                        if(menuh)
                            GetComponent<xml_cs>().zmina(xm, porivniania2(GetComponent<xml_cs>().nal, xm));
                        else
                        vs.xml.zmina(xm, porivniania2(vs.xml.nal, xm));
                    }

                    if (transform.GetChild(xm.mova).GetChild(1).childCount == 8)
                    {
                        ff+=0.3f;
                        transform.GetChild(xm.mova).GetChild(1).GetChild(7).gameObject.SetActive(h == 4 && Input.mousePosition.x > Screen.width * 0.6f && porivniania2(menuh? GetComponent<xml_cs>().nal : vs.xml.nal, xm));
                        transform.GetChild(xm.mova).GetChild(1).GetChild(7).GetComponent<Image>().material.SetFloat("_R1",Mathf.Sin(ff)*0.5f+0.5f);
                    }
                }
                break;
            case 2:
                {
                    h = (int)(5 - Input.mousePosition.y * 5 / Screen.height) % 5;
                    if (h < 0)
                        h = 0;
                    if (h > 1)
                        h = 1;

                    transform.GetChild(xm.mova).GetChild(2).GetChild(4).GetComponent<RectTransform>().localPosition = new Vector2(Screen.width * 0.05f, (4.5f - h) * Screen.height * 0.2f);
                    if (Input.mousePosition.x < Screen.width * 0.1f || Input.mousePosition.x > Screen.width * 0.6f)
                        transform.GetChild(xm.mova).GetChild(2).GetChild(4).GetComponent<Image>().enabled = false;
                    else
                        transform.GetChild(xm.mova).GetChild(2).GetChild(4).GetComponent<Image>().enabled = true;

                    if (Input.mousePosition.x < Screen.width && h == 1)
                    {
                        transform.GetChild(xm.mova).GetChild(2).GetChild(4).GetComponent<Image>().enabled = true;
                        if (Input.GetKey(KeyCode.Mouse0))
                            if (Input.mousePosition.x < Screen.width / 64 * 45)
                                xm.vazhkist = 0;
                            else
                                                    if (Input.mousePosition.x > Screen.width / 128 * 111)
                                xm.vazhkist = 2;
                            else
                                xm.vazhkist = 1;


                    }

                    if (Input.GetKeyUp(KeyCode.Mouse0) && Input.mousePosition.x > Screen.width * 0.1f && Input.mousePosition.x < Screen.width * 0.6f && h == 0)
                        pos = 1;
                    switch (xm.vazhkist)
                    {
                        case 0:
                            transform.GetChild(xm.mova).GetChild(2).GetChild(3).GetComponent<RectTransform>().localPosition = new Vector3(transform.GetChild(xm.mova).GetChild(2).GetChild(2).GetComponent<RectTransform>().localPosition.x - 0.4f * transform.GetChild(xm.mova).GetChild(2).GetChild(2).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(xm.mova).GetChild(2).GetChild(3).GetComponent<RectTransform>().localPosition.y, 0);
                            break;
                        case 1:
                            transform.GetChild(xm.mova).GetChild(2).GetChild(3).GetComponent<RectTransform>().localPosition = new Vector3(transform.GetChild(xm.mova).GetChild(2).GetChild(2).GetComponent<RectTransform>().localPosition.x, transform.GetChild(xm.mova).GetChild(2).GetChild(3).GetComponent<RectTransform>().localPosition.y, 0);
                            break;
                        case 2:
                            transform.GetChild(xm.mova).GetChild(2).GetChild(3).GetComponent<RectTransform>().localPosition = new Vector3(transform.GetChild(xm.mova).GetChild(2).GetChild(2).GetComponent<RectTransform>().localPosition.x + 0.45f * transform.GetChild(xm.mova).GetChild(2).GetChild(2).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(xm.mova).GetChild(2).GetChild(3).GetComponent<RectTransform>().localPosition.y, 0);
                            break;
                    }
                }
                break;
            case 3:
                {
                    h = (int)(5 - Input.mousePosition.y * 5 / Screen.height) % 5;
                    if (h < 0)
                        h = 0;
                    if (h > 3)
                        h = 3;

                    transform.GetChild(xm.mova).GetChild(3).GetChild(4).GetComponent<RectTransform>().localPosition = new Vector2(Screen.width * 0.05f, (4.5f - h) * Screen.height * 0.2f);
                    if (Input.mousePosition.x < Screen.width * 0.1f || Input.mousePosition.x > Screen.width * 0.6f || h == 1)
                        transform.GetChild(xm.mova).GetChild(3).GetChild(4).GetComponent<Image>().enabled = false;
                    else
                        transform.GetChild(xm.mova).GetChild(3).GetChild(4).GetComponent<Image>().enabled = true;

                    if (Input.mousePosition.x < Screen.width && h == 2)
                    {
                        transform.GetChild(xm.mova).GetChild(3).GetChild(4).GetComponent<Image>().enabled = true;
                        if (Input.GetKey(KeyCode.Mouse0))
                            if (Input.mousePosition.x < Screen.width / 64 * 45)
                                xm.yakistText = 0;
                            else
                                                    if (Input.mousePosition.x > Screen.width / 128 * 111)
                                xm.yakistText = 2;
                            else
                                xm.yakistText = 1;


                    }
                    if (Input.mousePosition.x < Screen.width && h == 3)
                    {
                        transform.GetChild(xm.mova).GetChild(3).GetChild(4).GetComponent<Image>().enabled = true;
                        if (Input.GetKey(KeyCode.Mouse0))
                            if (Input.mousePosition.x < Screen.width / 64 * 45)
                                xm.yakistSpets = 0;
                            else
                                                    if (Input.mousePosition.x > Screen.width / 128 * 111)
                                xm.yakistSpets = 2;
                            else
                                xm.yakistSpets = 1;


                    }

                    if (Input.GetKeyUp(KeyCode.Mouse0) && Input.mousePosition.x > Screen.width * 0.1f && Input.mousePosition.x < Screen.width * 0.6f && h == 0)
                        pos = 1;
                    transform.GetChild(xm.mova).GetChild(3).GetChild(9).GetChild(xm.rozmirEkrana).gameObject.SetActive(true);
                    switch (xm.yakistText)
                    {
                        case 0:
                            transform.GetChild(xm.mova).GetChild(3).GetChild(6).GetComponent<RectTransform>().localPosition = new Vector3(transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().localPosition.x - 0.4f * transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().localPosition.y, 0);
                            break;
                        case 1:
                            transform.GetChild(xm.mova).GetChild(3).GetChild(6).GetComponent<RectTransform>().localPosition = new Vector3(transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().localPosition.x, transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().localPosition.y, 0);
                            break;
                        case 2:
                            transform.GetChild(xm.mova).GetChild(3).GetChild(6).GetComponent<RectTransform>().localPosition = new Vector3(transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().localPosition.x + 0.45f * transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().localPosition.y, 0);
                            break;
                    }
                    switch (xm.yakistSpets)
                    {
                        case 0:
                            transform.GetChild(xm.mova).GetChild(3).GetChild(8).GetComponent<RectTransform>().localPosition = new Vector3(transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().localPosition.x - 0.4f * transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(xm.mova).GetChild(3).GetChild(8).GetComponent<RectTransform>().localPosition.y, 0);
                            break;
                        case 1:
                            transform.GetChild(xm.mova).GetChild(3).GetChild(8).GetComponent<RectTransform>().localPosition = new Vector3(transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().localPosition.x, transform.GetChild(xm.mova).GetChild(3).GetChild(8).GetComponent<RectTransform>().localPosition.y, 0);
                            break;
                        case 2:
                            transform.GetChild(xm.mova).GetChild(3).GetChild(8).GetComponent<RectTransform>().localPosition = new Vector3(transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().localPosition.x + 0.45f * transform.GetChild(xm.mova).GetChild(3).GetChild(5).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(xm.mova).GetChild(3).GetChild(8).GetComponent<RectTransform>().localPosition.y, 0);
                            break;
                    }
                    if (h == 1 && Input.mousePosition.x > Screen.width * 0.1f)
                    {
                        transform.GetChild(xm.mova).GetChild(3).GetChild(10).gameObject.SetActive(true);
                        if (Input.GetAxis("Mouse ScrollWheel") < 0)
                        {
                            transform.GetChild(xm.mova).GetChild(3).GetChild(9).GetChild(xm.rozmirEkrana).gameObject.SetActive(false);
                            xm.rozmirEkrana++;
                            if (xm.rozmirEkrana > 6)
                                xm.rozmirEkrana = 1;
                            transform.GetChild(xm.mova).GetChild(3).GetChild(9).GetChild(xm.rozmirEkrana).gameObject.SetActive(true);
                        }
                        if (Input.GetAxis("Mouse ScrollWheel") > 0)
                        {
                            transform.GetChild(xm.mova).GetChild(3).GetChild(9).GetChild(xm.rozmirEkrana).gameObject.SetActive(false);
                            xm.rozmirEkrana--;
                            if (xm.rozmirEkrana < 1)
                                xm.rozmirEkrana = 6;
                            transform.GetChild(xm.mova).GetChild(3).GetChild(9).GetChild(xm.rozmirEkrana).gameObject.SetActive(true);
                        }
                    }
                    else
                        transform.GetChild(xm.mova).GetChild(3).GetChild(10).gameObject.SetActive(false);
                }
                break;
            case 4:
                {
                    h = (int)(5 - Input.mousePosition.y * 5 / Screen.height) % 5;
                    if (h < 0)
                        h = 0;
                    if (h > 4)
                        h = 4;

                    transform.GetChild(xm.mova).GetChild(4).GetChild(8).GetComponent<RectTransform>().localPosition = new Vector2(Screen.width * 0.05f, (4.5f - h) * Screen.height * 0.2f);
                    if ((ps > 0 && h == 0) || (ps < 3 && h == 4))
                        transform.GetChild(xm.mova).GetChild(4).GetChild(8).GetComponent<Image>().material = zn2;
                    else transform.GetChild(xm.mova).GetChild(4).GetChild(8).GetComponent<Image>().material = zn1;

                    if (Input.GetAxis("Mouse ScrollWheel") < 0)
                        ps++;
                    if (Input.GetAxis("Mouse ScrollWheel") > 0)
                        ps--;
                    ps = Mathf.Clamp(ps, 0, 3);

                    transform.GetChild(xm.mova).GetChild(4).GetChild(0).GetComponent<RectTransform>().localPosition = new Vector2(transform.GetChild(xm.mova).GetChild(4).GetChild(0).GetComponent<RectTransform>().localPosition.x, (4.5f + ps) * Screen.height * 0.2f);
                    for (int i = 1; i < 8; i++)
                    {
                        transform.GetChild(xm.mova).GetChild(4).GetChild(i).GetComponent<RectTransform>().localPosition = new Vector2(transform.GetChild(xm.mova).GetChild(4).GetChild(i).GetComponent<RectTransform>().localPosition.x, (4.5f - i + ps) * Screen.height * 0.2f);
                        transform.GetChild(xm.mova).GetChild(4).GetChild(i + 8).GetComponent<RectTransform>().localPosition = new Vector2(transform.GetChild(xm.mova).GetChild(4).GetChild(i + 8).GetComponent<RectTransform>().localPosition.x, (4.5f - i + ps) * Screen.height * 0.2f);
                        float pp;
                        switch (i)
                        {
                            case 1: pp = xm.zvuk; break;
                            case 2: pp = xm.muzika; break;
                            case 3: pp = xm.strilba; break;
                            case 4: pp = xm.zvukBotiv; break;
                            case 5: pp = xm.xodba; break;
                            case 6: pp = xm.udary; break;
                            default: pp = xm.shum; break;
                        }
                        transform.GetChild(xm.mova).GetChild(4).GetChild(i + 8).GetChild(0).GetComponent<RectTransform>().localPosition = new Vector2((pp * 0.85f - 0.4f) * transform.GetChild(xm.mova).GetChild(4).GetChild(i + 8).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(xm.mova).GetChild(4).GetChild(i + 8).GetChild(0).GetComponent<RectTransform>().localPosition.y);
                    }

                    if (Input.GetKeyUp(KeyCode.Mouse0) && h + ps == 0) pos = 1;

                    if (Input.GetKey(KeyCode.Mouse0) && Input.mousePosition.x > Screen.width * 0.1f)
                    {
                        float pp;
                        pp = (Input.mousePosition.x / Screen.width - 0.6f) * 100 / 36;
                        pp = Mathf.Clamp(pp, 0, 1);
                        if (h + ps != 0)
                            transform.GetChild(xm.mova).GetChild(4).GetChild(h + ps + 8).GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3((pp * 0.85f - 0.4f) * transform.GetChild(xm.mova).GetChild(4).GetChild(h + ps + 8).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(xm.mova).GetChild(4).GetChild(h + ps + 8).GetChild(0).GetComponent<RectTransform>().localPosition.y, 0);

                        switch (h + ps)
                        {
                            case 1: xm.zvuk = pp; break;
                            case 2: xm.muzika = pp; break;
                            case 3: xm.strilba = pp; break;
                            case 4: xm.zvukBotiv = pp; break;
                            case 5: xm.xodba = pp; break;
                            case 6: xm.udary = pp; break;
                            case 7: xm.shum = pp; break;
                        }
                    }
                }
                break;
            case 5:
                {
                    h = (int)(5 - Input.mousePosition.y * 5 / Screen.height) % 5;
                    if (h < 0)
                        h = 0;
                    if (h > 4)
                        h = 4;

                    transform.GetChild(xm.mova).GetChild(5).GetChild(20).GetComponent<RectTransform>().localPosition = new Vector2(Screen.width * 0.05f, (4.5f - h) * Screen.height * 0.2f);
                    if ((ps > 0 && h == 0) || (ps < 15 && h == 4) || Input.mousePosition.x < Screen.width * 0.6f)
                        transform.GetChild(xm.mova).GetChild(5).GetChild(20).GetComponent<Image>().material = zn2;
                    else transform.GetChild(xm.mova).GetChild(5).GetChild(20).GetComponent<Image>().material = zn3;
                    if (ps + h == 0)
                        transform.GetChild(xm.mova).GetChild(5).GetChild(20).GetComponent<Image>().material = zn1;

                    if (Input.mousePosition.x < Screen.width * 0.6f)
                    {
                        if (Input.GetAxis("Mouse ScrollWheel") < 0)
                            ps++;
                        if (Input.GetAxis("Mouse ScrollWheel") > 0)
                            ps--;
                        ps = Mathf.Clamp(ps, 0, 15);


                        transform.GetChild(xm.mova).GetChild(5).GetChild(0).GetComponent<RectTransform>().localPosition = new Vector2(transform.GetChild(xm.mova).GetChild(4).GetChild(0).GetComponent<RectTransform>().localPosition.x, (4.5f + ps) * Screen.height * 0.2f);
                        for (int i = 1; i < 20; i++)
                            transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetComponent<RectTransform>().localPosition = new Vector2(transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetComponent<RectTransform>().localPosition.x, (4.5f - i + ps) * Screen.height * 0.2f);
                    }
                    if (Input.mousePosition.x > Screen.width * 0.6f)
                        switch (h + ps)
                        {
                            case 1: Keyt(ref xm.vpered); break;
                            case 2: Keyt(ref xm.nazadR); break;
                            case 3: Keyt(ref xm.vlivo); break;
                            case 4: Keyt(ref xm.vpravo); break;
                            case 5: Keyt(ref xm.big); break;
                            case 6: Keyt(ref xm.povzania); break;
                            case 7: Keyt(ref xm.strybok); break;
                            case 8: Keyt(ref xm.maybStrybok); break;
                            case 9: Keyt(ref xm.SPauk); break;
                            case 10: Keyt(ref xm.strilbaK); break;
                            case 11: Keyt(ref xm.prytsil); break;
                            case 12: Keyt(ref xm.DdoZbroyi); break;
                            case 13: Keyt(ref xm.Zbroya1); break;
                            case 14: Keyt(ref xm.Zbroya2); break;
                            case 15: Keyt(ref xm.Zbroya3); break;
                            case 16: Keyt(ref xm.Zbroya4); break;
                            case 17: Keyt(ref xm.Rvceb); break;
                            case 18: Keyt(ref xm.Rsp); break;
                            case 19: Keyt(ref xm.pauza); break;
                        }
                    for (int i = 1; i < 20; i++)
                    {
                        switch (i)
                        {
                            case 1: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.vpered.ToString(); break;
                            case 2: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.nazadR.ToString(); break;
                            case 3: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.vlivo.ToString(); break;
                            case 4: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.vpravo.ToString(); break;
                            case 5: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.big.ToString(); break;
                            case 6: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.povzania.ToString(); break;
                            case 7: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.strybok.ToString(); break;
                            case 8: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.maybStrybok.ToString(); break;
                            case 9: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.SPauk.ToString(); break;
                            case 10: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.strilbaK.ToString(); break;
                            case 11: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.prytsil.ToString(); break;
                            case 12: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.DdoZbroyi.ToString(); break;
                            case 13: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.Zbroya1.ToString(); break;
                            case 14: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.Zbroya2.ToString(); break;
                            case 15: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.Zbroya3.ToString(); break;
                            case 16: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.Zbroya4.ToString(); break;
                            case 17: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.Rvceb.ToString(); break;
                            case 18: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.Rsp.ToString(); break;
                            case 19: transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = xm.pauza.ToString(); break;
                        }
                        if (transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text == "JoystickButton1")
                            transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = "Mouse3";
                        if (transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text=="None")
                   transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = "???";
                        if (transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text == "Return")
                            transform.GetChild(xm.mova).GetChild(5).GetChild(i).GetChild(0).GetComponent<Text>().text = "Enter";
                    }

                    if (Input.GetKeyUp(KeyCode.Mouse0) && h + ps == 0) pos = 1;
                }

                break;
        }
        if (GetComponent<AudioSource>())
            GetComponent<AudioSource>().volume = menuh ? GetComponent<xml_cs>().nal.muzika* GetComponent<xml_cs>().nal.zvuk :vs.xml.nal.zvuk*vs.xml.nal.muzika;
    }
    void Keyt(ref KeyCode o){
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            o = KeyCode.JoystickButton1;
        else
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.A)) o = KeyCode.A;
            else
            if (Input.GetKeyDown(KeyCode.Q)) o = KeyCode.Q;
            else
            if (Input.GetKeyDown(KeyCode.E)) o = KeyCode.E;
            else
            if (Input.GetKeyDown(KeyCode.R)) o = KeyCode.R;
            else
            if (Input.GetKeyDown(KeyCode.T)) o = KeyCode.T;
            else
            if (Input.GetKeyDown(KeyCode.Y)) o = KeyCode.Y;
            else
            if (Input.GetKeyDown(KeyCode.U)) o = KeyCode.U;
            else
            if (Input.GetKeyDown(KeyCode.I)) o = KeyCode.I;
            else
            if (Input.GetKeyDown(KeyCode.O)) o = KeyCode.O;
            else
            if (Input.GetKeyDown(KeyCode.P)) o = KeyCode.P;
            else
            if (Input.GetKeyDown(KeyCode.S)) o = KeyCode.S;
            else
            if (Input.GetKeyDown(KeyCode.D)) o = KeyCode.D;
            else
            if (Input.GetKeyDown(KeyCode.F)) o = KeyCode.F;
            else
            if (Input.GetKeyDown(KeyCode.G)) o = KeyCode.G;
            else
            if (Input.GetKeyDown(KeyCode.H)) o = KeyCode.H;
            else
            if (Input.GetKeyDown(KeyCode.J)) o = KeyCode.J;
            else
            if (Input.GetKeyDown(KeyCode.K)) o = KeyCode.K;
            else
            if (Input.GetKeyDown(KeyCode.L)) o = KeyCode.L;
            else
            if (Input.GetKeyDown(KeyCode.Z)) o = KeyCode.Z;
            else
            if (Input.GetKeyDown(KeyCode.X)) o = KeyCode.X;
            else
            if (Input.GetKeyDown(KeyCode.C)) o = KeyCode.C;
            else
            if (Input.GetKeyDown(KeyCode.V)) o = KeyCode.V;
            else
            if (Input.GetKeyDown(KeyCode.B)) o = KeyCode.B;
            else
            if (Input.GetKeyDown(KeyCode.N)) o = KeyCode.N;
            else
            if (Input.GetKeyDown(KeyCode.M)) o = KeyCode.M;
            else
            if (Input.GetKeyDown(KeyCode.Alpha1)) o = KeyCode.Alpha1;
            else
            if (Input.GetKeyDown(KeyCode.Alpha2)) o = KeyCode.Alpha2;
            else
            if (Input.GetKeyDown(KeyCode.Alpha3)) o = KeyCode.Alpha3;
            else
            if (Input.GetKeyDown(KeyCode.Alpha4)) o = KeyCode.Alpha4;
            else
            if (Input.GetKeyDown(KeyCode.Alpha5)) o = KeyCode.Alpha5;
            else
            if (Input.GetKeyDown(KeyCode.Alpha6)) o = KeyCode.Alpha6;
            else
            if (Input.GetKeyDown(KeyCode.Alpha7)) o = KeyCode.Alpha7;
            else
            if (Input.GetKeyDown(KeyCode.Alpha8)) o = KeyCode.Alpha8;
            else
            if (Input.GetKeyDown(KeyCode.Alpha9)) o = KeyCode.Alpha9;
            else
            if (Input.GetKeyDown(KeyCode.F1)) o = KeyCode.F1;
            else
            if (Input.GetKeyDown(KeyCode.F2)) o = KeyCode.F2;
            else
            if (Input.GetKeyDown(KeyCode.F3)) o = KeyCode.F3;
            else
            if (Input.GetKeyDown(KeyCode.F4)) o = KeyCode.F4;
            else
            if (Input.GetKeyDown(KeyCode.F5)) o = KeyCode.F5;
            else
            if (Input.GetKeyDown(KeyCode.F6)) o = KeyCode.F6;
            else
            if (Input.GetKeyDown(KeyCode.F7)) o = KeyCode.F7;
            else
            if (Input.GetKeyDown(KeyCode.F8)) o = KeyCode.F8;
            else
            if (Input.GetKeyDown(KeyCode.F9)) o = KeyCode.F9;
            else
            if (Input.GetKeyDown(KeyCode.F10)) o = KeyCode.F10;
            else
            if (Input.GetKeyDown(KeyCode.F11)) o = KeyCode.F11;
            else
            if (Input.GetKeyDown(KeyCode.F12)) o = KeyCode.F12;
            else
            if (Input.GetKeyDown(KeyCode.Keypad0)) o = KeyCode.Keypad0;
            else
            if (Input.GetKeyDown(KeyCode.Keypad1)) o = KeyCode.Keypad1;
            else
            if (Input.GetKeyDown(KeyCode.Keypad2)) o = KeyCode.Keypad2;
            else
            if (Input.GetKeyDown(KeyCode.Keypad3)) o = KeyCode.Keypad3;
            else
            if (Input.GetKeyDown(KeyCode.Keypad4)) o = KeyCode.Keypad4;
            else
            if (Input.GetKeyDown(KeyCode.Keypad5)) o = KeyCode.Keypad5;
            else
            if (Input.GetKeyDown(KeyCode.Keypad6)) o = KeyCode.Keypad6;
            else
            if (Input.GetKeyDown(KeyCode.Keypad7)) o = KeyCode.Keypad7;
            else
            if (Input.GetKeyDown(KeyCode.Keypad8)) o = KeyCode.Keypad8;
            else
            if (Input.GetKeyDown(KeyCode.Keypad9)) o = KeyCode.Keypad9;
            else
            if (Input.GetKeyDown(KeyCode.Mouse0)) o = KeyCode.Mouse0;
            else
            if (Input.GetKeyDown(KeyCode.Mouse1)) o = KeyCode.Mouse1;
            else
            if (Input.GetKeyDown(KeyCode.LeftControl)) o = KeyCode.LeftControl;
            else
            if (Input.GetKeyDown(KeyCode.RightControl)) o = KeyCode.RightControl;
            else
            if (Input.GetKeyDown(KeyCode.LeftShift)) o = KeyCode.LeftShift;
            else
            if (Input.GetKeyDown(KeyCode.RightShift)) o = KeyCode.RightShift;
            else
            if (Input.GetKeyDown(KeyCode.LeftAlt)) o = KeyCode.LeftAlt;
            else
            if (Input.GetKeyDown(KeyCode.RightAlt)) o = KeyCode.RightAlt;
            else
            if (Input.GetKeyDown(KeyCode.CapsLock)) o = KeyCode.CapsLock;
            else
            if (Input.GetKeyDown(KeyCode.Tab)) o = KeyCode.Tab;
            else
            if (Input.GetKeyDown(KeyCode.LeftWindows)) o = KeyCode.LeftWindows;
            else
            if (Input.GetKeyDown(KeyCode.RightWindows)) o = KeyCode.RightWindows;
            else
            if (Input.GetKeyDown(KeyCode.Escape)) o = KeyCode.Escape;
            else
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) o = KeyCode.KeypadEnter;
            else
            if (Input.GetKeyDown(KeyCode.Insert)) o = KeyCode.Insert;
            else
            if (Input.GetKeyDown(KeyCode.Home)) o = KeyCode.Home;
            else
            if (Input.GetKeyDown(KeyCode.PageUp)) o = KeyCode.PageUp;
            else
            if (Input.GetKeyDown(KeyCode.PageDown)) o = KeyCode.PageDown;
            else
            if (Input.GetKeyDown(KeyCode.End)) o = KeyCode.End;
            else
            if (Input.GetKeyDown(KeyCode.Delete)) o = KeyCode.Delete;
            else
            if (Input.GetKeyDown(KeyCode.Numlock)) o = KeyCode.Numlock;
            else
            if (Input.GetKeyDown(KeyCode.Backspace)) o = KeyCode.Backspace;
            else
            if (Input.GetKeyDown(KeyCode.Print)) o = KeyCode.Print;
            else
            if (Input.GetKeyDown(KeyCode.ScrollLock)) o = KeyCode.ScrollLock;
            else
            if (Input.GetKeyDown(KeyCode.Break)) o = KeyCode.Break;
            else
            if (Input.GetKeyDown(KeyCode.UpArrow)) o = KeyCode.UpArrow;
            else
            if (Input.GetKeyDown(KeyCode.DownArrow)) o = KeyCode.DownArrow;
            else
            if (Input.GetKeyDown(KeyCode.LeftArrow)) o = KeyCode.LeftArrow;
            else
            if (Input.GetKeyDown(KeyCode.RightArrow)) o = KeyCode.RightArrow;
            else
            if (Input.GetKeyDown(KeyCode.Minus)) o = KeyCode.Minus;
            else
            if (Input.GetKeyDown(KeyCode.Plus)) o = KeyCode.Plus;
            else
            if (Input.GetKeyDown(KeyCode.KeypadDivide)) o = KeyCode.KeypadDivide;
            else
            if (Input.GetKeyDown(KeyCode.KeypadPlus)) o = KeyCode.KeypadPlus;
            else
            if (Input.GetKeyDown(KeyCode.KeypadMinus)) o = KeyCode.KeypadMinus;
            else
            if (Input.GetKeyDown(KeyCode.KeypadMultiply)) o = KeyCode.KeypadMultiply;
            else
            if (Input.GetKeyDown(KeyCode.Equals)) o = KeyCode.Equals;
            else
            if (Input.GetKeyDown(KeyCode.Return)) o = KeyCode.Return;
            else
                o = new KeyCode();
        }
}
    void mousepos(Transform n)
    {
        for (int i = 0; i < n.childCount; i++)
            mousepos(n.GetChild(i));
        if (n.GetComponent<Image>() != null)
            n.GetComponent<Image>().material.SetVector("_R", new Vector4(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 0, 0));// = new Vector2(n.GetComponent<RectTransform>().sizeDelta.x * x, n.GetComponent<RectTransform>().sizeDelta.y * x);

    }
    bool porivniania(XML a,XML b) {
        bool f = !porivniania2(a, b);
        if (a.mova!=b.mova) f = false;

        if ((int)(a.zvuk * 100) != (int)(b.zvuk * 100)) f = false;
        if ((int)(a.muzika * 100) != (int)(b.muzika * 100)) f = false;
        if ((int)(a.strilba * 100) != (int)(b.strilba * 100)) f = false;
        if ((int)(a.zvukBotiv * 100) != (int)(b.zvukBotiv * 100)) f = false;
        if ((int)(a.xodba * 100) != (int)(b.xodba * 100)) f = false;
        if ((int)(a.udary * 100) != (int)(b.udary * 100)) f = false;
        if ((int)(a.shum * 100) != (int)(b.shum * 100)) f = false;

        if (a.vpered != b.vpered) f = false;
        if (a.nazadR != b.nazadR) f = false;
        if (a.vlivo != b.vlivo) f = false;
        if (a.vpravo != b.vpravo) f = false;
        if (a.pauza != b.pauza) f = false;
        if (a.strilbaK != b.strilbaK) f = false;
        if (a.prytsil != b.prytsil) f = false;
        if (a.Rsp != b.Rsp) f = false;
        if (a.big != b.big) f = false;
        if (a.povzania != b.povzania) f = false;
        if (a.Zbroya1 != b.Zbroya1) f = false;
        if (a.Zbroya2 != b.Zbroya2) f = false;
        if (a.Zbroya3 != b.Zbroya3) f = false;
        if (a.Zbroya4 != b.Zbroya4) f = false;
        if (a.Rvceb != b.Rvceb) f = false;
        if (a.strybok != b.strybok) f = false;
        if (a.maybStrybok != b.maybStrybok) f = false;
        if (a.SPauk != b.SPauk) f = false;
        if (a.DdoZbroyi != b.DdoZbroyi) f = false;
        return f;
    }
    bool porivniania2(XML a, XML b)
    {
        bool f = false;

        if (a.vazhkist != b.vazhkist) f = true;

        if (a.yakistText != b.yakistText) f = true;
        if (a.yakistSpets != b.yakistSpets) f = true;
        if (a.rozmirEkrana != b.rozmirEkrana) f = true;

        return f;
    }
}
