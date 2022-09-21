using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sky_cs : MonoBehaviour
{
   // [SerializeField] Transform j;

    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector3(Screen.width, Screen.height, 0) ;
    }

    void Update()
    {
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

    }
}
