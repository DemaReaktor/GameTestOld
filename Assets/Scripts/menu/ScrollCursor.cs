using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ScrollCursor : MonoBehaviour
{
    public Graphic Image;
    public bool WhenNullHide = false;
    public Transform Transform;

    private List<RaycastResult> list;
    private PointerEventData pointerData;
    private RectTransform RectTransform;

    protected void Start()
    {
        list = new List<RaycastResult>();
        pointerData = new PointerEventData(EventSystem.current);
        RectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (Application.isPlaying && Image != null)
        {
            Image.enabled = true;
            pointerData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointerData, list);

            if (list.Count != 0 && Transform != null)
                foreach (var element in list)
                    if (element.gameObject.transform.IsChildOf(Transform))
                    {
                        RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, element.gameObject.transform.position.y);
                        return;
                    }

            if (WhenNullHide)
                Image.enabled = false;
        }
    }
}
[CustomEditor(typeof(ScrollCursor))]
public class LanguageImageEditor : Editor
{
    private void OnEnable()
    {
        if ((serializedObject.targetObject as ScrollCursor).Image is null && (serializedObject.targetObject as ScrollCursor).TryGetComponent(out Graphic graphic))
            (serializedObject.targetObject as ScrollCursor).Image = graphic;
    }
}
