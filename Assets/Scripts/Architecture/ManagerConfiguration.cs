using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

namespace GameArchitecture
{
    [Serializable]
    public class ManagerConfiguration
    {
        public MonoScript MonoScript;
        [SerializeReference] public object Configuration;
    }

    [CustomPropertyDrawer(typeof(ManagerConfiguration))]
    public class ManagerConfigurationEditor : PropertyDrawer
    {
        private MonoScript MonoScript = null;
        private MonoScript NewMonoScript = null;
        private bool isClickedButton = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            MonoScript = EditorGUI.ObjectField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), "Manager", MonoScript, typeof(MonoScript), false) as MonoScript;

            if (MonoScript!=null&& !MonoScript.GetClass().GetInterfaces().Contains(typeof(IManager)) && !MonoScript.GetClass().GetInterfaces().Any(t => t.Name.Substring(0, t.Name.IndexOf('`')) == typeof(IManager).Name))
                MonoScript = null;

            if (MonoScript != NewMonoScript)
            {
                property.FindPropertyRelative("Configuration").managedReferenceValue = null;
                isClickedButton = false;
                NewMonoScript = MonoScript;
            }

            if (MonoScript != null)
            {
                if (!isClickedButton && GUI.Button(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), "Add existing configuration"))
                {
                    Type type = MonoScript.GetClass();
                    string[] assets = AssetDatabase.GetAllAssetPaths().Where(name => name.EndsWith(".cs")).ToArray();

                    foreach (string asset in assets)
                    {
                        MonoScript element = AssetDatabase.LoadAssetAtPath<MonoScript>(asset);

                        if (element != null && element.GetClass() != null)
                        {
                            Attribute attribute = Attribute.GetCustomAttribute(element.GetClass(), typeof(ConfigurationAttribute));

                            if (attribute != null && attribute as ConfigurationAttribute !=null && (attribute as ConfigurationAttribute).ManagerType == type)
                            {
                                property.FindPropertyRelative("Configuration").managedReferenceValue = Activator.CreateInstance(element.GetClass());
                                isClickedButton = true;
                                break;
                            }
                        }
                    }

                    //if (!isClickedButton)
                        //Debug.LogWarning("configuration class for this IManager is not ");
                    //    EditorGUI.HelpBox(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight),"this class is not derived of IManager",MessageType.Warning);
                        //EditorGUI.HelpBox(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight),"this class does not have attribute Configuration",MessageType.Warning);
                }

                if (isClickedButton)
                    EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("Configuration"), true);
            }
            else
                isClickedButton = false;

            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (isClickedButton)
                return EditorGUIUtility.singleLineHeight*2 + EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Configuration")) + EditorGUIUtility.standardVerticalSpacing;

            if (MonoScript != null)
                return EditorGUIUtility.singleLineHeight *2 + EditorGUIUtility.standardVerticalSpacing;

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
