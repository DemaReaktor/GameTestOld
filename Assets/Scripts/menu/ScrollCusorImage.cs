using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEditor.UI;
using KeyCodeController;

public class ScrollCusorImage : Image
{
    public Sprite DefaultSprite;
    public Sprite KeyCodeSetterSprite;

    private List<RaycastResult> list;
    private PointerEventData pointerData;

    protected override void Start()
    {
        list = new List<RaycastResult>();
        pointerData = new PointerEventData(EventSystem.current);

        base.Start();
    }
    void Update()
    {
        if (Application.isPlaying)
        {
            pointerData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointerData, list);

            sprite = list.Count != 0 && list.First().gameObject.transform.TryGetComponent(out KeyCodeSetter keyCodeSetter) ? KeyCodeSetterSprite : DefaultSprite;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ScrollCusorImage))]
public class ScrollCusorImageEditor : ImageEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        (serializedObject.targetObject as ScrollCusorImage).DefaultSprite = EditorGUILayout.ObjectField(new GUIContent("DefaultSprite", ""),
            serializedObject.FindProperty("DefaultSprite").objectReferenceValue,typeof(Sprite),true) as Sprite;
        (serializedObject.targetObject as ScrollCusorImage).KeyCodeSetterSprite = EditorGUILayout.ObjectField(new GUIContent("KeyCodeSetterSprite", ""),
            serializedObject.FindProperty("KeyCodeSetterSprite").objectReferenceValue, typeof(Sprite), true) as Sprite;
    }

}
#endif

