using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using Unity;

namespace GameArchitecture
{
    [Serializable]
    public class ManagerConfiguration
    {
        public IManager Manager;
        //public Con
        //public Pair[] Configuration;
    }

    [CustomPropertyDrawer(typeof(ManagerConfiguration))]
    public class ManagerConfigurationEditor : PropertyDrawer
    {
        public MonoScript MonoScript = null;
        //public Pair[] Configuration = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //int count = 0;
            //if (Configuration != null)
            //    count = Configuration.Length;

            //property.rectValue = new Rect(property.rectValue.x, property.rectValue.y, property.rectValue.width, property.rectValue.height + position.height*2);

            EditorGUI.BeginProperty(position, label, property);

            MonoScript = EditorGUI.ObjectField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), "Manager", MonoScript, typeof(MonoScript)) as MonoScript;

            EditorGUI.BeginDisabledGroup(MonoScript!=null);
            if (GUI.Button(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight*2, position.width, EditorGUIUtility.singleLineHeight),"Configuration"))
            {
                Type type = MonoScript.GetClass();
                UnityEngine.Object[] objects = AssetDatabase.LoadAllAssetsAtPath("Assets/");

                foreach (UnityEngine.Object element in objects)
                    if (element as MonoScript != null)
                    {
                        Attribute attribute = Attribute.GetCustomAttribute((element as MonoScript).GetClass(), typeof(ConfigurationAttribute));

                        if (attribute != null && (attribute as ConfigurationAttribute).ManagerType == type)
                        {
                            Mo

                            break;
                        }
                    }
            }
            EditorGUI.EndDisabledGroup();
            //EditorGUILayout.EndFoldoutHeaderGroup();

            //EditorGUI.LabelField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 3, position.width/2, EditorGUIUtility.singleLineHeight),"Key");
            //EditorGUI.LabelField(new Rect(position.x + position.width/2, position.y + EditorGUIUtility.singleLineHeight * 3, position.width/2, EditorGUIUtility.singleLineHeight),"Value");
            //GUI.Button(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 4, position.width, EditorGUIUtility.singleLineHeight), "+");
            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int count = 0;
            //if (Configuration != null)
            //    count = Configuration.Length;

            return EditorGUIUtility.singleLineHeight +
                                                       (4 + count) *
                (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
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
