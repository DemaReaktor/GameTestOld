using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class str2_cs : MonoBehaviour
{
    public VoidSystem_cs vs;
    public Transform prytc;
    int dd;
    float time;
    Vector3[] mol;
    public Vector3 ff;

    void Start()
    {
        ff = new Vector3();
        time = 0;
        float d =1+ VoidSystem_cs.Dist(prytc.transform.position, transform.position);// Mathf.Sqrt((prytc.transform.position.x - transform.position.x) * (prytc.transform.position.x - transform.position.x) + (prytc.transform.position.y - transform.position.y) * (prytc.transform.position.y - transform.position.y) + (prytc.transform.position.z - transform.position.z) * (prytc.transform.position.z - transform.position.z));
        Vector3 nap = (prytc.transform.position - transform.position) / d;
        dd = (int)(d);
        mol = new Vector3[dd];

        for (int i = 0; i < dd; i++)
            mol[i] = transform.position + new Vector3(Random.Range(Random.Range(-100, 0), Random.Range(0, 100)) * 0.007f + nap.x * i * 0.25f, Random.Range(Random.Range(-100, 0), Random.Range(0, 100)) * 0.007f + nap.y * i * 0.25f, Random.Range(Random.Range(-100, 0), Random.Range(0, 100)) * 0.007f + nap.z * i * 0.25f);
        this.GetComponent<LineRenderer>().positionCount = dd;
        this.GetComponent<LineRenderer>().SetPositions(mol);
        transform.position = mol[0];
    }

    void Update()
    {
        if (transform.position != mol[0])
        {
            Vector3 p = transform.position - mol[0];

            for (int i = 0; i < this.GetComponent<LineRenderer>().positionCount; i++)
                mol[i] += p;

            this.GetComponent<LineRenderer>().SetPositions(mol);
        }
        time += Time.deltaTime;
        if (time >= 1)
        {
            time = 1;
            VoidSystem_cs.Destr(this.gameObject);
        }

        int n;
        n = (int)(dd * (1 - time));
        this.GetComponent<LineRenderer>().positionCount = n;

        if (n - 2 >= 0)
            this.GetComponent<LineRenderer>().SetPosition(n - 1, mol[n - 1] + (mol[n - 2] - mol[n - 1]) * (time * dd / 1 - (int)(time * dd)));    //*/
    }

}
