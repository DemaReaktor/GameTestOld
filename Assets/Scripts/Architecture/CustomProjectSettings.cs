using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameArchitecture
{
#if UNITY_EDITOR
    class CustomProjectSettings : ScriptableObject
    {
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
            var settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(GeneralConfiguration.GetAssetNameForEditor(type));
            if (settings == null)
            {
                Debug.Log(GeneralConfiguration.GetAssetNameForEditor(type).ToString() + "1");
                settings = ScriptableObject.CreateInstance<CustomProjectSettings>();
                settings.Context = new SettingsContext() { Configuration = Activator.CreateInstance(type) };
                AssetDatabase.CreateAsset(settings, GeneralConfiguration.GetAssetNameForEditor(type));
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        internal static bool Exist(Type type) => Resources.Load<CustomProjectSettings>(GeneralConfiguration.GetAssetName(type)) != null;
    }

    public class CustomProjectSettingsProvider<T> : SettingsProvider
    {
        protected SerializedObject serializedObject;
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
            var provider = new CustomProjectSettingsProvider<T>($"Project/Configurations/{typeof(T).Name.Replace("Configuration", "")}", SettingsScope.Project, keys);
            CustomProjectSettings.GetOrCreateSettings(typeof(T));

            return provider;
        }
    }
#endif
}