using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

public class ScrollWithAcceleration : ScrollRect
{
    public float Acceleration = 0;
    public bool NonNegative = true;
    public float TimeWait = 0.5f;

    private float standardSpeed;
    private float time;

    protected override void Start()
    {
        standardSpeed = scrollSensitivity;
    }
    void Update()
    {
        if (Application.isPlaying)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                time = 0;
                scrollSensitivity += Acceleration * Time.deltaTime;
                if (NonNegative && scrollSensitivity < 0)
                    scrollSensitivity = 0f;
                return;
            }
            if (time > TimeWait)
            {
                scrollSensitivity = standardSpeed;
                return;
            }

            time += Time.deltaTime;
        }
    }
}

[CustomEditor(typeof(ScrollWithAcceleration))]
public class ScrollWithAccelerationEditor : ScrollRectEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        (serializedObject.targetObject as ScrollWithAcceleration).Acceleration = EditorGUILayout.FloatField(new GUIContent("Acceleration", "Acceleration of scrollSensitivity" +
            "When user scroll some time scrollSensitivity is changed by Acceleration"), serializedObject.FindProperty("Acceleration").floatValue);

        float time = EditorGUILayout.FloatField(new GUIContent("TimeWait", "Time." +
            "When this time is end and user don`t use scroll scrollSensitivity became standard"), serializedObject.FindProperty("TimeWait").floatValue);
        if (time < 0f)
            time = 0f;
        if (time > 2f)
            time = 2f;
        (serializedObject.targetObject as ScrollWithAcceleration).TimeWait = time;

        EditorGUILayout.Space();
        (serializedObject.targetObject as ScrollWithAcceleration).NonNegative = EditorGUILayout.Toggle(new GUIContent("NonNegative",
           "ScrollSensitivity can not be negative by Acceleration if this value is true"), serializedObject.FindProperty("NonNegative").boolValue);
    }
}
