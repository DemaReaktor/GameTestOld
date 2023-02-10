using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidSystem_cs : MonoBehaviour
{
     public player pl;
    [HideInInspector] public xml_cs xml;
    [HideInInspector] public mapOperator_cs mp;
    [HideInInspector] public pyliuka_cs pc;
    [HideInInspector] public Cube_creating cc;
    [HideInInspector] public gunOperator_cs gOp;
    [HideInInspector] public ministrobj_cs mc;
    [HideInInspector] public bot_creating_cs bcc;
     public pauseeff_cs peff;
    [HideInInspector] public cnopkovod_cs cnv;
    [HideInInspector] public Audio_cs ac;
    [HideInInspector] public zariad_cs zar;

    [SerializeField] Material mat1;
    [SerializeField] Material mat2;


    public int[,] h = new int[200, 200];
    public Transform player;
    public void Star()
    {
        pc = this.transform.GetComponent<pyliuka_cs>();
        cc = this.transform.GetComponent<Cube_creating>();
    gOp = this.transform.GetComponent<gunOperator_cs>();
      mc = this.transform.GetComponent<ministrobj_cs>();
      bcc = this.transform.GetComponent<bot_creating_cs>();
      cnv = this.transform.GetComponent<cnopkovod_cs>();
      ac = this.transform.GetComponent<Audio_cs>();
     xml = this.transform.GetComponent<xml_cs>();
     zar = this.transform.GetComponent<zariad_cs>();
     mp = this.transform.GetComponent<mapOperator_cs>();

       
        cc.min();


        mp.mat2 = xml.nal.yakistSpets == 0 ?mat2:mat1;
    }
    private void Update()
    {
        if ((peff.realpause || cnv.pause)&&!xml.vid)
            Cursor.visible = true;
        else
            Cursor.visible = false;
    }

    public static float diap(float c)
    {
        while (c < 0)
            c += 200;
        while (c >= 200)
            c -= 200;
        return c;
    }
    public static int diap(int c)
    {
        while (c < 0)
            c += 200;
        while (c >= 200)
            c -= 200;
        return c;
    }
    public  Vector3 gravityno(Vector3 f)
    {
        f = new Vector3(diap(f.x),f.y,diap(f.z));
        Vector3Int ff = new Vector3Int(diap((int)f.x),(int)(f.y), diap((int)f.z));
        float x, z, y;

        x = f.x - Mathf.Floor(f.x);
        z = f.z - Mathf.Floor(f.z);
        y = f.y - 0.5f;

        float x1, x2, z2, z1;
        x1 = ttm(f.x);
        x2 = ttp(f.x);
        z1 = ttm(f.z);
        z2 = ttp(f.z);

        if (y < cc.h[ff.x, ff.z])
        {
            f.y = 0.5f + cc.h[ff.x, ff.z];
            y = f.y - 0.5f;
        }



        if (0.5f <= x)
        {
            if (cc.h[(int)(x2 + 1), ff.z] > y)
            {
                f.x = Mathf.Floor(f.x) + 0.5f;
                x = f.x - Mathf.Floor(f.x);
            }

            if (0.5f <= z)
            {
                if (cc.h[ff.x, (int)(z2 + 1)] > y + 0.01f)
                    f.z = Mathf.Floor(f.z) + 0.5f;
                else
                if (Dist2(new Vector3(1, 1), new Vector2(x, z)) <= 0.5f)
                    if (cc.h[(int)(x2 + 1), (int)(z2 + 1)] > y && cc.h[(int)(x2 + 1), ff.z] <= y)
                    {
                        if (x >= z)
                            f.z += -Mathf.Sqrt((x - 0.5f) * (1.5f - x)) - z + 1;
                        else
                            f.x += -Mathf.Sqrt((z - 0.5f) * (1.5f - z)) - x + 1;

                    }
            }


            else
if (cc.h[ff.x, (int)(z1 - 1)]  > y )
                f.z = Mathf.Floor(f.z) + 0.5f;
            else
                if (Dist2(new Vector3(1, 0), new Vector2(x, z)) <= 0.5f)
                if (cc.h[(int)(x2 + 1), (int)(z1 - 1)] > y && cc.h[(int)(x2 + 1), (int)(f.z)] <= y)
                {
                    if (x >= 1 - z)
                        f.z += Mathf.Sqrt((x - 0.5f) * (1.5f - x)) - z;
                    else
                        f.x += -Mathf.Sqrt(0.25f - z * z) - x + 1;
                }
        }







        else
        {
            if (cc.h[(int)(x1 - 1), ff.z]  > y )
            {
                f.x = Mathf.Floor(f.x) + 0.5f;
                x = f.x - Mathf.Floor(f.x);
            }

            if (0.5f <= z)
            {
                if (cc.h[ff.x,(int)(z2 + 1)]  > y + 0.01f)    
                    f.z = Mathf.Floor(f.z) + 0.5f;
            else
                if (Dist2(new Vector3(0, 1), new Vector2(x, z)) <= 0.5f)
                if (cc.h[(int)(x1 - 1), (int)(z2 + 1)] > y && cc.h[(int)(x1 - 1), ff.z] <= y)
                {
                    if (1 - x >= z)
                        f.z += -Mathf.Sqrt(0.25f - x * x) - z + 1;
                    else
                        f.x += Mathf.Sqrt((z - 0.5f) * (1.5f - z)) - x;
                }

            }


            else
if (cc.h[ff.x, (int)(z1 - 1)]  > y )
                f.z = Mathf.Floor(f.z) + 0.5f;
            else
                if (Dist2(new Vector3(0, 0), new Vector2(x, z)) <= 0.5f)
                if (cc.h[(int)(x1 - 1), (int)(z1 - 1)] > y && cc.h[(int)(x1 - 1), ff.z] <= y)
                {
                    if (x <= z)
                        f.z += Mathf.Sqrt(0.25f - x * x) - z;
                    else
                        f.x += Mathf.Sqrt(0.25f - z * z) - x;
                }
        }
        return f;
    }
    public static float Dist(Vector3 a, Vector3 b) {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
    }
    public static float Dist2(Vector2 a, Vector2 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }
    public static float Dist2(float a, float b)
    {
        return Mathf.Sqrt(a * a + b * b);
    }

    public static void Destr(GameObject g) {
        Destroy(g);
    }
    public static float diapazon(float c)
    {
        if (c > 199)
            return c - 200;
        if (c < 0)
            return c + 200;
        return c;
    }
    public static float ttp(float c)
    {
        if (c >= 199)
            return c - 200;
        else
            return c;
    }
    public static float ttm(float c)
    {
        if (c <= 0)
            return c + 200;
        else
            return c;
    }

    public  void perem(Vector3 f) {
        for (int i = 0; i < gOp.transform.childCount; i++)
            gOp.transform.GetChild(i).transform.Translate(f);
    }
    public bool gravityper(Vector3 q, float r)
    {
        float xx, zz;
        xx = q.x;
        zz = q.z;
        float x, y, z, x1, x2, z1, z2;

        while (xx < 0)
            xx += 200;
        while (xx >= 200)
            xx -= 200;
        while (zz < 0)
            zz += 200;
        while (zz >= 200)
            zz -= 200;

        x1 = ttp(xx);
        x2 = ttm(xx);
        z1 = ttp(zz);
        z2 = ttm(zz);

        x = xx - Mathf.Floor(xx);
        z = zz - Mathf.Floor(zz);
        y = q.y - r;

        if (y <= h[(int)(xx), (int)(zz)])
            return false;//  g = false;
        else
        if (x > 1 - r)
        {
            if (y <= h[(int)(x1 + 1), (int)(zz)])
                return false;//  g = false;
            else
            if (Dist2(new Vector3(1, 1), new Vector2(x, z)) + r <= 1 && y <= h[(int)(x1 + 1), (int)(z1 + 1)])
                return false;//    g = false;
            else
            if (Dist2(new Vector3(1, 0), new Vector2(x, z)) + r <= 1 && y <= h[(int)(x1 + 1), (int)(z2 - 1)])
                return false;//   g = false;
            else
            if (z > 1 - r)
            {
                if (y <= h[(int)(xx), (int)(z1 + 1)])
                    return false;//  g = false;
                else
                    return true;
            }
            else
                if (z < r)
            {
                if (y <= h[(int)(xx), (int)(z2 - 1)])
                    return false;//   g = false;
                else
                    return true;
            }
            else
                if (y <= h[(int)(xx), (int)(z1 + 1)] ||
                y <= h[(int)(xx), (int)(z2 - 1)])
                return false;//   g = false;
            else
                return true;//
        }
        else
        if (x < r)
        {
            if (y <= h[(int)(x2 - 1), (int)(zz)])
                return false;//   g = false;
            else
        if (Dist2(new Vector3(0, 1), new Vector2(x, z)) + r <= 1 && y <= h[(int)(x2 - 1), (int)(z1 + 1)])
                return false;//  g = false;
            else
        if (Dist2(new Vector3(0, 0), new Vector2(x, z)) + r <= 1 && y <= h[(int)(x2 - 1), (int)(z2 - 1)])
                return false;//  g = false;
            else
            if (z > 1 - r)
            {
                if (y <= h[(int)(xx), (int)(z1 + 1)])
                    return false;//  g = false;
                else
                    return true;
            }
            else
                if (z < r)
            {
                if (y <= h[(int)(xx), (int)(z2 - 1)])
                    return false;//   g = false;
                else
                    return true;
            }
            else
                if (y <= h[(int)(xx), (int)(z1 + 1)] ||
                y <= h[(int)(xx), (int)(z2 - 1)])
                return false;//   g = false;
            else
                return true;//
        }
        else
            if ((y <= h[(int)(x2 - 1), (int)(zz)] && r >= x) ||
            (y <= h[(int)(x1 + 1), (int)(zz)] && r >= 1 - x)
            )
            return false;//  g = false;
        else
            if (z > 1 - r)
        {
            if (y <= h[(int)(xx), (int)(z1 + 1)])
                return false;//  g = false;
            else
                return true;
        }
        else
                if (z < r)
        {
            if (y <= h[(int)(xx), (int)(z2 - 1)])
                return false;//   g = false;
            else
                return true;
        }
        else
                if ((y <= h[(int)(xx), (int)(z1 + 1)] && r >= z) ||
                (y <= h[(int)(xx), (int)(z2 - 1)] && r >= 1 - z))
            return false;//   g = false;
        else
            return true;//

    }
    public  bool gravityper0(Vector3 q) {
        while (q.x < 0)
            q.x += 200;
        while (q.x >= 200)
            q.x -= 200;
        while (q.z < 0)
            q.z += 200;
        while (q.z >= 200)
            q.z -= 200;

        if (q.y <= h[(int)(q.x), (int)(q.z)])
            return false;
        else
            return true;
    }
    public  void mcAdd(Vector3 p, Vector3 nap)
    {
        mc.Addmini(p, nap);
    }
    public  void Pausa()
    {
        ac.pausa(!peff.realpause);
            StartCoroutine(peff.Starteff(!peff.realpause));
    }
    public bool vydno(Vector3 pos, float r) {
        if (Dist(pos, player.transform.position) <= 10) return true;
        else
        if (Dist(pos, player.transform.position) <= 100)
        // return true;
        {
            float kk;

            float x = -player.transform.rotation.eulerAngles.y;
            while (x >= 360)
                x -= 360;
            while (x < 0)
                x += 360;

            Vector2 n;
            float m;
            m = x - 60;
            if (m < 0)
                m += 360;
            float k;
            float xx;
            k = Mathf.Tan(x / 57.3f);
            if (x < 90 || x > 270)
                xx = pos.x - Mathf.Sqrt(2 * r * r / (k * k + 1));
            else
                xx = pos.x + Mathf.Sqrt(2 * r * r / (k * k + 1));
            n = new Vector2(xx, k * (xx - pos.x) + pos.z);

            kk = Mathf.Atan2(n.y - player.transform.position.z, n.x - player.transform.position.x) * 57.3f - 90;
            if (kk < 0)
                kk += 360;
            if (kk + 240 < m)
                return true;


            if (m < 300)
            {
                if (m + 120 > kk && m < kk)
                    return true;
            }
            else
               if (m < kk)
                return true;
        }
        return false;
    }
    public Vector3 kvant(Vector3 pos) {
        float d = Dist(pos, player.transform.position);
        Vector3 p, po;
        po = pos;
        for (int i = 0; i < 8; i++)
        {
            switch (i)
            {
                case 0:
                    p = pos + new Vector3(-200, 0, -200);
                    break;
                case 1:
                    p = pos + new Vector3(-200, 0, 0);
                    break;
                case 2:
                    p = pos + new Vector3(-200, 0, 200);
                    break;
                case 3:
                    p = pos + new Vector3(0, 0, -200);
                    break;
                case 4:
                    p = pos + new Vector3(0, 0, 200);
                    break;
                case 5:
                    p = pos + new Vector3(200, 0, -200);
                    break;
                case 6:
                    p = pos + new Vector3(200, 0, 0);
                    break;
                default:
                    p = pos + new Vector3(200, 0, 200);
                    break;
            }
            if (Dist(p, player.transform.position) < d)
            {
                d = Dist(p, player.transform.position);
                po = p;
            }
        }
        return po;
    }
    public Vector3Int kvant(Vector3Int pos)
    {
        float d = Dist(pos, player.transform.position);
        Vector3Int p, po;
        po = pos;
        for (int i = 0; i < 8; i++)
        {
            switch (i)
            {
                case 0:
                    p = pos + new Vector3Int(-200, 0, -200);
                    break;
                case 1:
                    p = pos + new Vector3Int(-200, 0, 0);
                    break;
                case 2:
                    p = pos + new Vector3Int(-200, 0, 200);
                    break;
                case 3:
                    p = pos + new Vector3Int(0, 0, -200);
                    break;
                case 4:
                    p = pos + new Vector3Int(0, 0, 200);
                    break;
                case 5:
                    p = pos + new Vector3Int(200, 0, -200);
                    break;
                case 6:
                    p = pos + new Vector3Int(200, 0, 0);
                    break;
                default:
                    p = pos + new Vector3Int(200, 0, 200);
                    break;
            }
            if (Dist(p, player.transform.position) < d)
            {
                d = Dist(p, player.transform.position);
                po = p;
            }
        }
        return po;
    }
    public static Vector3 kvant2(Vector3 pos, Vector3 pos2)
    { float d = Dist(pos, pos2);
        Vector3 p, po;
        po = pos;
        for (int i = 0; i < 8; i++)
        {
            switch (i)
            {
                case 0:
                    p = pos + new Vector3(-200, 0, -200);
                    break;
                case 1:
                    p = pos + new Vector3(-200, 0, 0);
                    break;
                case 2:
                    p = pos + new Vector3(-200, 0, 200);
                    break;
                case 3:
                    p = pos + new Vector3(0, 0, -200);
                    break;
                case 4:
                    p = pos + new Vector3(0, 0, 200);
                    break;
                case 5:
                    p = pos + new Vector3(200, 0, -200);
                    break;
                case 6:
                    p = pos + new Vector3(200, 0, 0);
                    break;
                default:
                    p = pos + new Vector3(200, 0, 200);
                    break;
            }
            if (Dist(p, pos2) < d)
            {
                d = Dist(p, pos2);
                po = p;
            }
        }
        return po;
    }
    public static float kvantd(Vector3 pos, Vector3 pos2)
    {
        float d = Dist(pos, pos2);
        Vector3 p;
        for (int i = 0; i < 8; i++)
        {
            switch (i)
            {
                case 0:
                    p = pos + new Vector3(-200, 0, -200);
                    break;
                case 1:
                    p = pos + new Vector3(-200, 0, 0);
                    break;
                case 2:
                    p = pos + new Vector3(-200, 0, 200);
                    break;
                case 3:
                    p = pos + new Vector3(0, 0, -200);
                    break;
                case 4:
                    p = pos + new Vector3(0, 0, 200);
                    break;
                case 5:
                    p = pos + new Vector3(200, 0, -200);
                    break;
                case 6:
                    p = pos + new Vector3(200, 0, 0);
                    break;
                default:
                    p = pos + new Vector3(200, 0, 200);
                    break;
            }
            if (Dist(p, pos2) < d)
                d = Dist(p, pos2);
        }
        return d;
    }
    //public 
    public static int botPopad(Vector3 botpos, Vector3 pos, float toch = 0) {
        pos = new Vector3(diap(pos.x), pos.y, diap(pos.z));
        botpos.y += 0.2f;
        botpos = kvant2(botpos, pos);
        if (botpos.y + 0.137f + toch >= pos.y && Dist(botpos, pos) <= 0.22f + toch)
            return 1;

        if (Dist(new Vector3(botpos.x, botpos.y + 0.137f, botpos.z), pos) <= 0.065f + toch)
            return 2;

        float x;

        x = pos.x - botpos.x + 0.37f;

        if (x * 0.2f >= Dist2(pos.y - botpos.y, pos.z - botpos.z) && x <= 0.17f + toch)
            return 3;

        x -= 0.74f;
        if (-x * 0.2f >= Dist2(pos.y - botpos.y, pos.z - botpos.z) && x >= -0.17f + toch)
            return 3;

        x = pos.z - botpos.z + 0.37f;

        if (x * 0.2f >= Dist2(pos.y - botpos.y, pos.x - botpos.x) && x <= 0.17f + toch)
            return 3;

        x -= 0.74f;
        if (-x * 0.2f >= Dist2(pos.y - botpos.y, pos.x - botpos.x) && x >= -0.17f + toch)
            return 3;

        return 0;
    }
    public void telep(Transform tr,ref Vector3 pos){
        tr.position = pos;
        if (pos.x + 100 <= player.position.x)
            tr.position = pos + new Vector3(200,0,0);
        if (pos.x >= player.position.x+100)
            tr.position = pos + new Vector3(-200, 0, 0);
        if (pos.z + 100 <= player.position.z)
            tr.position = pos + new Vector3(0, 0, 200);
        if (pos.z>= player.position.z+100)
            tr.position = pos + new Vector3(0, 0, -200);

        pos = new Vector3(diap(pos.x),pos.y,diap(pos.z));
    }
    public void telep(Transform tr)
    {
        if (tr.position.x + 100 < player.position.x)
            tr.Translate(new Vector3(200,0,0));
        if (tr.position.x >100+ player.position.x)
            tr.Translate(new Vector3(-200, 0, 0));
        if (tr.position.z + 100 < player.position.z)
            tr.Translate(new Vector3(0, 0, 200));
        if (tr.position.z > 100 + player.position.z)
            tr.Translate(new Vector3(0, 0, -200));
    }
}
