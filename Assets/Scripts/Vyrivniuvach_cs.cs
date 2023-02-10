using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vyrivniuvach_cs : MonoBehaviour
{
    public Vector2Int screen;
    [SerializeField] int k=0;
    void Start()
    {
        for (int i = 0; i < transform.childCount-k; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().localScale = new Vector3(transform.GetChild(i).GetComponent<RectTransform>().localScale.x /screen.x * Screen.width, transform.GetChild(i).GetComponent<RectTransform>().localScale.y / screen.y * Screen.height, 1);
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.x / screen.x * Screen.width, transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.y / screen.y * Screen.height);
        }
    }
}
