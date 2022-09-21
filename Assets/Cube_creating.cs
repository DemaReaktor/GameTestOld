using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_creating : MonoBehaviour
{
    public VoidSystem_cs vs;

    public Transform plane;
    public Material i;
    public Material i2;
    public Material ii;
    public Material ii2;

    public Transform player;
    [HideInInspector] public float pov;      
    public GameObject cube;
    [HideInInspector] public int[,] h = new int[200, 200];
    [HideInInspector] public bool[,,] c = new bool[200,8,200];
    [HideInInspector] public GameObject[,,] cub = new GameObject[200,200,7];
    [HideInInspector] public int[,] cubk = new int[200,200];

     Vector2Int ppos;
   // float kyt;
    Vector2Int plpos;

    void Awake()
    {
        vs = this.transform.GetComponent<VoidSystem_cs>();

        ppos = new Vector2Int((int)(player.transform.position.x), (int)(player.transform.position.z));
        switch (Random.Range(0,3)) {
            case 0:
        for (int y = 0; y < 200; y++)
        {
            for (int x = 0; x < 200; x++)
            {
                h[x, y] = 0;
                if (Random.Range(0, 10) == 0)
                    h[x, y] = Random.Range(0, 6);

                if (Random.Range(0, 47) == 0)
                    h[x, y] += Random.Range(1, 3);

                    }
        }
                break;
            case 1:
                for (int y = 0; y < 200; y++)
                {
                    for (int x = 0; x < 200; x++)
                    {
                        h[x, y] = 0;
                        if (Random.Range(0, 13) == 0)
                           h[x, y]  =  Random.Range(0, 3);
                    }
                }
                for(int i = 0; i < Random.Range(510, 1350); i++)
                {
                    int x, y,r;
                    x = Random.Range(0,200);
                    y = Random.Range(0,200);
                    r = Random.Range(1,7);
                    for (int xx = 0; xx < 5; xx++)
                        for (int yy = 0; yy < 5; yy++)
                        {
                            int g, g1;
                            g = x - 2 + xx;
                            g1 = y - 2 + yy;
                            if (g < 0)
                                g += 200;
                            if (g > 199)
                                g -= 200;
                            if (g1 < 0)
                                g1 += 200;
                            if (g1 > 199)
                                g1 -= 200;
                            h[g,g1] += Random.Range(r / 3 * (2 - (int)(Mathf.Sqrt(((2 - xx)* (2 - xx)+ (2 - yy)* (2 - yy))/2))), r/3 * (3 - (int)(Mathf.Sqrt(((2 - xx) * (2 - xx) + (2 - yy) * (2 - yy)) / 2))));
                        }
                    for (int yy = 0; yy < 200; yy++)
                        for (int xx = 0; xx < 200; xx++) { 
                            if (h[xx, yy] > 6)
                                h[xx, yy] = Random.Range(0,7);
                   }
                }
                break;
            case 2:
                for (int y = 0; y < 200; y++)
                    for (int x = 0; x < 200; x++)
                    {
                        if (Random.Range(0, 30) == 0)
                            h[x, y] = Random.Range(4, 7);
                        else
                        if (Random.Range(0, 70) == 0)
                            h[x, y] = Random.Range(1, 2);
                    }
                break;
    }
        for (int y = 0; y < 200; y++)
            for (int x = 0; x < 200; x++)
            {
                if (VoidSystem_cs.Dist2(new Vector2(x, y), new Vector2(100, 100)) <= 8)
                    h[x, y] = 0;
                int g = 0;
                for (int ii = 0; ii < h[x, y]; ii++)
                    if (ii + 1 == h[x, y] || h[(int)(VoidSystem_cs.diapazon(x - 1)), (int)(VoidSystem_cs.diapazon(y))] <= ii || h[(int)(VoidSystem_cs.diapazon(x)), (int)(VoidSystem_cs.diapazon(y - 1))] <= ii || h[(int)(VoidSystem_cs.diapazon(x + 1)), (int)(VoidSystem_cs.diapazon(y))] <= ii || h[(int)(VoidSystem_cs.diapazon(x)), (int)(VoidSystem_cs.diapazon(y + 1))] <= ii)
                    {
                        cub[x, y, g] = Instantiate(cube, new Vector3(x + 0.5f, ii + 0.521f, y + 0.5f), new Quaternion(), transform);

                       // if(vs.Dist(new Vector3(x+0.5f,ii+0.521f,y+0.5f),new Vector3(100,0,100))<100)
                        cub[x, y, g].SetActive(true);

                        g++;
                    }
                cubk[x, y] = g;
            }
        vs.h = h;
        Cc();
    }
    public void min()
    {
        if (vs.xml.nal.yakistSpets != 0)
        {
            plane.GetComponent<MeshRenderer>().material = i;
            for (int o = 0; o < plane.childCount; o++)
                plane.GetChild(o).GetComponent<MeshRenderer>().material = i;
            for (int y = 0; y < 200; y++)
                for (int x = 0; x < 200; x++)
                    for (int i = 0; i < cubk[x, y]; i++)
                        cub[x, y, i].GetComponent<MeshRenderer>().material = ii;
        }
        else
        {
            plane.GetComponent<MeshRenderer>().material = i2;
            for (int o = 0; o < plane.childCount; o++)
                plane.GetChild(o).GetComponent<MeshRenderer>().material = i2;
            for (int y = 0; y < 200; y++)
                for (int x = 0; x < 200; x++)
                    for (int i = 0; i < cubk[x, y]; i++)
                        cub[x, y, i].GetComponent<MeshRenderer>().material = ii2;
        }
      }
    private void Update()
    {

         plpos = new Vector2Int((int)(player.transform.position.x), (int)(player.transform.position.z));    

          if (plpos != ppos )
          {
              if (ppos.x != plpos.x)
                  if (plpos.x > ppos.x)
                {
                    for (int z = plpos.y - 100; z < plpos.y + 100; z++)
                        for (int x = ppos.x - 100; x < plpos.x - 99; x++)
                            for (int i = 0; i < cubk[(VoidSystem_cs.diap(x)), (VoidSystem_cs.diap(z))]; i++)
                                vs.telep(cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform);
                }
                else 
                    for (int z = plpos.y - 100; z < plpos.y + 100; z++)
                        for (int x = plpos.x - 100; x < ppos.x - 99; x++)
                                for (int i = 0; i < cubk[(VoidSystem_cs.diap(x)), (VoidSystem_cs.diap(z))]; i++)
                                vs.telep(cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform);




            if (ppos.y != plpos.y)
                if (plpos.y > ppos.y)
                {
                    for (int x = plpos.x - 100; x < plpos.x + 100; x++)
                        for (int z = ppos.y - 100; z < plpos.y - 99; z++)
                            for (int i = 0; i < cubk[(VoidSystem_cs.diap(x)), (VoidSystem_cs.diap(z))]; i++)
                                vs.telep(cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform);
                }
                else
                    for (int x = plpos.x - 100; x < plpos.x + 100; x++)
                        for (int z = plpos.y - 100; z < ppos.y - 99; z++)
                            for (int i = 0; i < cubk[(VoidSystem_cs.diap(x)), (VoidSystem_cs.diap(z))]; i++)
                                // cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform.Translate(new Vector3(0, 0, -200));
                               // cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform.position = vs.kvant(cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform.position);
                                vs.telep(cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform);
            ppos = plpos;
          }     
    }
    public void Updat()
    {

        plpos = new Vector2Int((int)(player.transform.position.x), (int)(player.transform.position.z));

        if (plpos != ppos)
        {
            if (ppos.x != plpos.x)
                if (plpos.x > ppos.x)
                {
                    for (int z = plpos.y - 100; z < plpos.y + 100; z++)
                        for (int x = ppos.x - 100; x < plpos.x - 99; x++)
                            for (int i = 0; i < cubk[(VoidSystem_cs.diap(x)), (VoidSystem_cs.diap(z))]; i++)
                                vs.telep(cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform);
                }
                else
                    for (int z = plpos.y - 100; z < plpos.y + 100; z++)
                        for (int x = plpos.x - 100; x < ppos.x - 99; x++)
                            for (int i = 0; i < cubk[(VoidSystem_cs.diap(x)), (VoidSystem_cs.diap(z))]; i++)
                                vs.telep(cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform);




            if (ppos.y != plpos.y)
                if (plpos.y > ppos.y)
                {
                    for (int x = plpos.x - 100; x < plpos.x + 100; x++)
                        for (int z = ppos.y - 100; z < plpos.y - 99; z++)
                            for (int i = 0; i < cubk[(VoidSystem_cs.diap(x)), (VoidSystem_cs.diap(z))]; i++)
                                vs.telep(cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform);
                }
                else
                    for (int x = plpos.x - 100; x < plpos.x + 100; x++)
                        for (int z = plpos.y - 100; z < ppos.y - 99; z++)
                            for (int i = 0; i < cubk[(VoidSystem_cs.diap(x)), (VoidSystem_cs.diap(z))]; i++)
                                // cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform.Translate(new Vector3(0, 0, -200));
                                // cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform.position = vs.kvant(cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform.position);
                                vs.telep(cub[VoidSystem_cs.diap(x), VoidSystem_cs.diap(z), i].transform);
            ppos = plpos;
        }
    }
    public void per()
    {
        if(player.position.x>=200)
                for (int z = 0; z < 200; z++)
                    for (int x = 0; x < 200; x++)
                    for (int i = 0; i < cubk[x,z]; i++)
                        cub[x,z, i].transform.Translate(new Vector3(-200, 0, 0));
        if (player.position.x < 0)
            for (int z = 0; z < 200; z++)
                for (int x = 0; x < 200; x++)
                    for (int i = 0; i < cubk[x, z]; i++)
                        cub[x, z, i].transform.Translate(new Vector3(200, 0, 0));
        if (player.position.z >= 200)
            for (int z = 0; z < 200; z++)
                for (int x = 0; x < 200; x++)
                    for (int i = 0; i < cubk[x, z]; i++)
                        cub[x, z, i].transform.Translate(new Vector3(0, 0, -200));
        if (player.position.z < 0)
            for (int z = 0; z < 200; z++)
                for (int x = 0; x < 200; x++)
                    for (int i = 0; i < cubk[x, z]; i++)
                        cub[x, z, i].transform.Translate(new Vector3(0, 0, 200));

        ppos = new Vector2Int((int)(VoidSystem_cs.diap(player.position.x)), (int)(VoidSystem_cs.diap(player.position.z)));

    }
    public void Cc()
    {
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 200; x++)
                for (int z = 0; z < 200; z++)
                    if (h[x, z] > y)
                        c[x, y, z] = false;
                    else
                        if (y == 0)
                        c[x, 0, z] = true;
                    else
                    {
                        c[x, y, z] = false;
                        int x1, x2, z2, z1;

                        if (x == 0)
                            x1 = 200;
                        else
                            x1 = x;

                        if (x == 199)
                            x2 = -1;
                        else
                            x2 = x;

                        if (z == 0)
                            z1 = 200;
                        else
                            z1 = z;

                        if (z == 199)
                            z2 = -1;
                        else
                            z2 = z;

                        if (h[x1 - 1, z1 - 1] + 1 > y) c[x, y, z] = true;
                        else if (h[x1 - 1, z] + 1 > y) c[x, y, z] = true;
                        else if (h[x1 - 1, z2 + 1] + 1 > y) c[x, y, z] = true;
                        else if (h[x, z1 - 1] + 1 > y) c[x, y, z] = true;
                        else if (h[x, z] + 1 > y) c[x, y, z] = true;
                        else if (h[x, z2 + 1] + 1 > y) c[x, y, z] = true;
                        else if (h[x2 + 1, z1 - 1] + 1 > y) c[x, y, z] = true;
                        else if (h[x2 + 1, z] + 1 > y) c[x, y, z] = true;
                        else if (h[x2 + 1, z2 + 1] + 1 > y) c[x, y, z] = true;

                    }
    }

}
