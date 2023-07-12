using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;

namespace GameArchitecture
{
    [Serializable]
    public class GeneralConfiguration
    {
        private const string defaultPath = "Configurations";
        [Tooltip("Path started in Assets/Resources/")]
        [SerializeField] internal string configurationsFolder = defaultPath;
        public string Path { get; internal set; } = defaultPath;
        internal static GeneralConfiguration configuration = new GeneralConfiguration();

#if UNITY_EDITOR
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var value = GeneralProvider.CreateGeneralSettingsProvider();
            configuration = Game.GetConfiguration<GeneralConfiguration>();
            return value;
        }
#endif
        public void Apply()
        {
            string path = Path;
            Path = configurationsFolder;

            FileUtil.MoveFileOrDirectory(Application.dataPath + "/Resources/" + path, Application.dataPath + "/Resources/" + configurationsFolder);
            configuration = Game.GetConfiguration<GeneralConfiguration>();
            Debug.Log("2");
        }

        public static string CurrentOrDefaultPath
        {
            get
            {
                string path = configuration.Path;
                if (!Directory.Exists(Application.dataPath + "/Resources/" + path))
                {
                    Debug.LogWarning($"Configurations Path does not exist. All configurations will be saved in {defaultPath}");
                    path = configuration.configurationsFolder = configuration.Path = defaultPath;

                    if (!Directory.Exists(Application.dataPath + "/Resources"))
                        AssetDatabase.CreateFolder("Assets", "Resources");
                    if (!Directory.Exists(Application.dataPath + "/Resources/" + path))
                        AssetDatabase.CreateFolder("Assets/Resources", path);
                }

                return path + (path.EndsWith('/') ? "" : "/");
            }
        }
        public static string GetAssetName(Type type) => CurrentOrDefaultPath + type.Name.Replace("Configuration", "");
#if UNITY_EDITOR
        public static string GetAssetNameForEditor(Type type) => "Assets/Resources/" + GetAssetName(type)+ ".asset";
#endif
    }

#if UNITY_EDITOR
    public class GeneralProvider : CustomProjectSettingsProvider<GeneralConfiguration>
    {
        public GeneralProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {}

        public override void OnGUI(string searchContext)
        {
            base.OnGUI(searchContext);

            EditorGUILayout.Space();
            if (GUILayout.Button("Apply"))
            {
                var obj = (serializedObject.targetObject as CustomProjectSettings).Context.Configuration as GeneralConfiguration;
                string path = obj.Path;
                obj.Path = obj.configurationsFolder;
                serializedObject.ApplyModifiedProperties();

                FileUtil.MoveFileOrDirectory(Application.dataPath + "/Resources/" + path, Application.dataPath + "/Resources/" + obj.configurationsFolder);
                GeneralConfiguration.configuration = Game.GetConfiguration<GeneralConfiguration>();
                while (!Directory.Exists(Application.dataPath + "/Resources/" + obj.configurationsFolder))
                { }
            }
            //((serializedObject.targetObject as CustomProjectSettings).Context.Configuration as GeneralConfiguration).Apply();
        }


        public static SettingsProvider CreateGeneralSettingsProvider()
        {
            var keys = new HashSet<string>(SettingsProvider.GetSearchKeywordsFromGUIContentProperties<GeneralConfiguration>().ToArray());
            var provider = new GeneralProvider($"Project/Configurations/General", SettingsScope.Project, keys);
            CustomProjectSettings.GetOrCreateSettings(typeof(GeneralConfiguration));

            return provider;
        }
    }
#endif
}