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

        [SerializeReference]
        public object Configuration;

        internal static CustomProjectSettings GetOrCreateSettings(Type type)
        {
            var settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(CustomSettingsPath.Replace("<<name>>", type.Name.Replace("Configuration", "")));
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<CustomProjectSettings>();
                settings.Configuration = Activator.CreateInstance(type);
                AssetDatabase.CreateAsset(settings, CustomSettingsPath.Replace("<<name>>", type.Name.Replace("Configuration", "")));
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        internal static SerializedObject GetSerializedSettings(Type type)
        {
            return new SerializedObject(GetOrCreateSettings(type));
        }
    }

    public class ProjectSettingsRegister<T>
    {
        public static SettingsProvider CreateSettingsProvider()
        {
            var keys = SettingsProvider.GetSearchKeywordsFromGUIContentProperties<T>().ToArray();
            var provider = new SettingsProvider($"Project/Configurations/{typeof(T).Name.Replace("Configuration", "")}", SettingsScope.Project)
            {
                label = typeof(T).Name.Replace("Configuration", ""),
                guiHandler = (searchContext) =>
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    var iterator = CustomProjectSettings.GetSerializedSettings(typeof(T)).GetIterator();
                    iterator.NextVisible(true);
                    iterator.NextVisible(true);
                    while (iterator.NextVisible(true))
                        EditorGUILayout.PropertyField(iterator);
                },
                keywords = new HashSet<string>(keys)
            };

            return provider;
        }
    }
}