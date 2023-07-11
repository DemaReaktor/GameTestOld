using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameArchitecture
{
    class CustomProjectSettings : ScriptableObject
    {
        public const string CustomSettingsPath = "Assets/Editor/<<name>>.asset";

        [Serializable]
        public class SettingsContext
        {
            [SerializeReference]
            public object Configuration;
        }

        [SerializeReference]
        public SettingsContext Context;

        internal static CustomProjectSettings GetOrCreateSettings(Type type)
        {
            var settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(CustomSettingsPath.Replace("<<name>>", type.Name.Replace("Configuration", "")));
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<CustomProjectSettings>();
                settings.Context = new SettingsContext() { Configuration = Activator.CreateInstance(type) };
                AssetDatabase.CreateAsset(settings, CustomSettingsPath.Replace("<<name>>", type.Name.Replace("Configuration", "")));
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        internal static void Save(Type type, CustomProjectSettings settings)
        {
            AssetDatabase.SaveAssetIfDirty(settings);
            //AssetDatabase.SaveAssets();
        }

        internal static SerializedObject GetSerializedSettings(Type type)
        {
            return new SerializedObject(GetOrCreateSettings(type));
        }
    }
    //[CustomPropertyDrawer(typeof(CustomProjectSettings))]
    //public class CustomProjectSettingsEditor : PropertyDrawer
    //{
    //    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //    {
    //        EditorGUI.PropertyField(position, property.FindPropertyRelative("Context"), label, true);
    //    }
    //}

    public class ProjectSettingsRegister<T>
    {
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new ConfigurationSettingsProvider<T>($"Project/Configurations/{typeof(T).Name.Replace("Configuration", "")}", SettingsScope.Project,
              new HashSet<string>(SettingsProvider.GetSearchKeywordsFromGUIContentProperties<T>().ToArray()));

            return provider;
        }
    }

    public class ConfigurationSettingsProvider<T>: SettingsProvider
    {
        private object target;
        private SerializedProperty configuration;
        public ConfigurationSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null):base(path,scopes,keywords) {
            label = typeof(T).Name.Replace("Configuration", "");
            //keywords = new HashSet<string>(SettingsProvider.GetSearchKeywordsFromGUIContentProperties<T>().ToArray());
        }

        public override void OnGUI(string searchContext)
        {
            var settings = CustomProjectSettings.GetOrCreateSettings(typeof(T));
            if(configuration is null)
                configuration = new SerializedObject(settings).FindProperty("Context").FindPropertyRelative("Configuration");
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(configuration, new GUIContent("Configuration"), true);
            
            settings.Context.Configuration = configuration.managedReferenceValue;

            if (target is null || settings.Context.Configuration != target)
                Debug.Log("yes");

            target = settings.Context.Configuration;

            AssetDatabase.SaveAssets();
        }
    }
}