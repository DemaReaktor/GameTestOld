using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

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
            var settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(CustomSettingsPath.Replace("<<name>>", RemoveConfiguration(type.Name)));
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<CustomProjectSettings>();
                settings.Context = new SettingsContext() { Configuration = Activator.CreateInstance(type) };
                AssetDatabase.CreateAsset(settings, CustomSettingsPath.Replace("<<name>>", RemoveConfiguration(type.Name)));
                AssetDatabase.SaveAssets();


            }
            return settings;
        }

        internal static bool Exist(Type type) => AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(CustomSettingsPath.Replace("<<name>>", RemoveConfiguration(type.Name))) != null;

        internal static string RemoveConfiguration(string text) => text.Replace("Configuration", "");
    }

    public class CustomProjectSettingsProvider<T> : SettingsProvider
    {
        private SerializedObject serializedObject;
        public CustomProjectSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {
            label = typeof(T).Name.Replace("Configuration", "");
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            SetProperties();
            base.OnActivate(searchContext, rootElement);
        }

        public override void OnGUI(string searchContext)
        {
            if (!CustomProjectSettings.Exist(typeof(T)))
                SetProperties();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Context").FindPropertyRelative("Configuration"), new GUIContent("Configuration"), true);

            serializedObject.ApplyModifiedProperties();
        }

        void SetProperties()
        {
            serializedObject = new SerializedObject(CustomProjectSettings.GetOrCreateSettings(typeof(T)));
        }
        public static SettingsProvider CreateSettingsProvider()
        {
            var keys = new HashSet<string>(SettingsProvider.GetSearchKeywordsFromGUIContentProperties<T>().ToArray());
            var provider = new CustomProjectSettingsProvider<T>($"Project/Configurations/{CustomProjectSettings.RemoveConfiguration(typeof(T).Name)}", SettingsScope.Project, keys);

            return provider;
        }
    }
}