using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using GameArchitecture.Save;
using GameArchitecture;

// Create a new type of Settings Asset.
class CustomSettings : ScriptableObject
{
    public const string CustomSettingsPath = "Assets/Editor/MyCustomSettings.asset";

    [SerializeReference]
    public object Configuration;

    internal static CustomSettings GetOrCreateSettings(Type type)
    {
        var settings = AssetDatabase.LoadAssetAtPath<CustomSettings>(CustomSettingsPath);
        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<CustomSettings>();
            settings.Configuration = Activator.CreateInstance(type.GetInterfaces().First(
                                t => t.FullName.Substring(0, t.FullName.IndexOf('`')) == "GameArchitecture.IManager").GenericTypeArguments[0]);
            AssetDatabase.CreateAsset(settings, CustomSettingsPath);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }

    internal static SerializedObject GetSerializedSettings(Type type)
    {
        return new SerializedObject(GetOrCreateSettings(type));
    }
}

[CustomPropertyDrawer(typeof(CustomSettings))]
public class ManagerConfigurationEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Configuration"), new GUIContent(
        "Configuration"), true);
    }
}

    // Register a SettingsProvider using IMGUI for the drawing framework:
    public class MyCustomSettingsIMGUIRegister<T>
//where T : class
{
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        //Debug.Log(typeof(T).Name);
        var keys = SettingsProvider.GetSearchKeywordsFromGUIContentProperties<T>().ToArray();
        // First parameter is the path in the Settings window.
        // Second parameter is the scope of this setting: it only appears in the Project Settings window.
        var provider = new SettingsProvider($"Project/{typeof(T).Name}", SettingsScope.Project)
        //var provider = new SettingsProvider($"Project/{typeof(T).Name}", SettingsScope.Project)
        {
            // By default the last token of the path is used as display name if no label is provided.
            label = typeof(T).Name,
            // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
            guiHandler = (searchContext) =>
            {
                //(typeof(T).Assembly.GetCustomAttributes(true).Where(at => typeof(SerializableAttribute) ==at.GetType()).ToArray()[0] as SerializableAttribute).
                var settings = CustomSettings.GetSerializedSettings(typeof(T));
                //EditorGUILayout.PropertyField(settings.FindProperty("Configuration"));
                //EditorGUILayout.PropertyField(settings.FindProperty("Configuration"));
                var iterator = settings.GetIterator();
                iterator.NextVisible(true);
                iterator.NextVisible(true);
                while(iterator.NextVisible(true))
                    EditorGUILayout.PropertyField(iterator);

                // var a = UnityEngine.Object.Instantiate(settings.targetObject as CustomSettings);
                // a.Object = settings.targetObject as CustomSettings;
                // var b = new SerializedObject(a);
                //var c = b.FindProperty("Object");
                // Debug.Log(c.FindPropertyRelative("Configuration"));
                // Debug.Log(c.FindPropertyRelative("Object"));
                // Debug.Log(c.FindPropertyRelative("p_Configuration"));
                // Debug.Log(c.FindPropertyRelative("_Configuration"));
                // EditorGUI.PropertyField(new Rect(0, 0, 100, EditorGUIUtility.singleLineHeight), c.FindPropertyRelative("Configuration"), new GUIContent(
                //  "Configuration"), true);
                //EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("Configuration"), new GUIContent(
                // manager.Configuration.GetType().Name), true);



                //EditorGUILayout.PropertyField(settings.FindProperty("m_Number"), new GUIContent("My Number"));
                //EditorGUILayout.PropertyField(settings.FindProperty("m_SomeString"), new GUIContent("My String"));
            },


            // Populate the search keywords to enable smart search filtering and label highlighting:
            keywords = new HashSet<string>(keys)
            //keywords = new HashSet<string>(new string[] { })
        };

        return provider;
    }
}

public static class R
{
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        return MyCustomSettingsIMGUIRegister<SettingsManager>.CreateMyCustomSettingsProvider();

        //var provider = new SettingsProvider($"Project/R", SettingsScope.Project)
        //{
        //    // By default the last token of the path is used as display name if no label is provided.
        //    label = "R",
        //    // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
        //    guiHandler = (searchContext) =>
        //    {
        //        var settings = CustomSettings<SettingsConfiguration>.GetSerializedSettings();
        //        //EditorGUILayout.PropertyField(settings.FindProperty("m_Number"), new GUIContent("My Number"));
        //        //EditorGUILayout.PropertyField(settings.FindProperty("m_SomeString"), new GUIContent("My String"));
        //    },

        //    // Populate the search keywords to enable smart search filtering and label highlighting:
        //    keywords = new HashSet<string>(new string[] { })
        //};

        //return provider;
    }
}