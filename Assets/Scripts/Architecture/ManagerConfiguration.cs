using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

namespace GameArchitecture
{
    [Serializable]
    public class ManagerConfiguration
    {
        [Tooltip("class should be direved by IManager")]
        public MonoScript MonoScript;
        [SerializeReference] public object Configuration;
    }

    [CustomPropertyDrawer(typeof(ManagerConfiguration))]
    public class ManagerConfigurationEditor : PropertyDrawer
    {
        private MonoScript MonoScript = null;
        private MonoScript OldMonoScript = null;
        private bool isConfiguration = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            MonoScript = EditorGUI.ObjectField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), "Manager", MonoScript, typeof(MonoScript), false) as MonoScript;

            if (MonoScript != null && !MonoScript.GetClass().GetInterfaces().Contains(typeof(IManager)) && !MonoScript.GetClass().GetInterfaces().Any(t => t.Name.Substring(0, t.Name.IndexOf('`')) == typeof(IManager).Name))
                MonoScript = OldMonoScript;

            if (MonoScript != OldMonoScript)
            {
                isConfiguration = false;
                OldMonoScript = MonoScript;

                if (MonoScript != null && MonoScript.GetClass().GetInterfaces().Any(t => t.Name.Substring(0, t.Name.IndexOf('`')) == typeof(IManager).Name))
                {
                    property.FindPropertyRelative("Configuration").managedReferenceValue = Activator.CreateInstance(MonoScript.GetClass().GetInterfaces().Where(t => t.Name.Substring(0, t.Name.IndexOf('`')) == typeof(IManager).Name).First().GenericTypeArguments[0]);

                    isConfiguration = true;
                }
            }

            if (isConfiguration)
                EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("Configuration"), true);

            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (isConfiguration)
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Configuration"));

            return EditorGUIUtility.singleLineHeight;
        }
    }

    [Serializable]
    public class Pair
    {
        public string Key;
        public string Value;
    }

    [CustomPropertyDrawer(typeof(Pair))]
    public class PairEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var key = property.FindPropertyRelative("Key");
            key.stringValue = EditorGUI.TextField(new Rect(position.x, position.y, position.width / 2, position.height), "", key.stringValue);

            var value = property.FindPropertyRelative("Value");
            value.stringValue = EditorGUI.TextField(new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height), "", value.stringValue);
            EditorGUI.EndProperty();
        }
    }
}
