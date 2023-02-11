using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using System;
using System.Linq;
using Unity;
using UnityEngine.UIElements;

namespace GameArchitecture
{
    [Serializable]
    public class ManagerConfiguration
    {
        //public IManager Manager;
        //public Con
        public MonoScript MonoScript;
        [SerializeReference] public Configuration Configuration;
    }

    [CustomPropertyDrawer(typeof(ManagerConfiguration))]
    public class ManagerConfigurationEditor : PropertyDrawer
    {
        public MonoScript MonoScript = null;
        public Configuration Configuration = null;
        private bool isClickedButton = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            MonoScript = EditorGUI.ObjectField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), "Manager", MonoScript, typeof(MonoScript), false) as MonoScript;

            if (MonoScript != null)
            {
                if (GUI.Button(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), "Configuration") && !isClickedButton)
                {
                    Type type = MonoScript.GetClass();
                    string[] assets = AssetDatabase.GetAllAssetPaths().Where(name => name.EndsWith(".cs")).ToArray();

                    foreach (string asset in assets)
                    {
                        MonoScript element = AssetDatabase.LoadAssetAtPath<MonoScript>(asset);

                        if (element != null && element.GetClass() != null)
                        {
                            ConfigurationAttribute attribute = Attribute.GetCustomAttribute(element.GetClass(), typeof(ConfigurationAttribute)) as ConfigurationAttribute;

                            if (attribute != null && attribute.ManagerType == type)
                            {
                                property.FindPropertyRelative("Configuration").managedReferenceValue = Activator.CreateInstance(element.GetClass());
                                break;
                            }
                        }
                    }
                    isClickedButton = true;
                }

                if (isClickedButton)
                    EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("Configuration"), true);
            }
            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float count = 0;
            if (MonoScript != null)
                count += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            if (isClickedButton)
                count += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Configuration"));
            return EditorGUIUtility.singleLineHeight + count;
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
