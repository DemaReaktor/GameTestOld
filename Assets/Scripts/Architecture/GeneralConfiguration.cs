using UnityEngine;
using System;
using System.IO;
using UnityEditor;

namespace GameArchitecture
{
    [Serializable]
    public class GeneralConfiguration
    {
        private const string defaultPath = "Configurations";
        [Tooltip("Path started in Assets/Resources/")]
        public string ConfigurationsPath = defaultPath;
        private static GeneralConfiguration configuration = new GeneralConfiguration();

#if UNITY_EDITOR
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var value = CustomProjectSettingsProvider<GeneralConfiguration>.CreateConfiguration();
            configuration = Game.GetConfiguration<GeneralConfiguration>();
            Directory.Move(Application.dataPath + "/Assets/Resources/" + defaultPath, Application.dataPath + "/Assets/Resources/" + configuration.ConfigurationsPath);
            return value;
        }
#endif
        public static string SettingsPath
        {
            get
            {
                string path = configuration.ConfigurationsPath;
                if (!Directory.Exists(Application.dataPath + "/Resources/" + path))
                {
                    Debug.LogWarning($"Configurations Path does not exist. All configurations will be saved in {defaultPath}");
                    path = configuration.ConfigurationsPath = defaultPath;

                    if (!Directory.Exists(Application.dataPath + "/Resources"))
                        AssetDatabase.CreateFolder("Assets", "Resources");
                    if (!Directory.Exists(Application.dataPath + "/Resources/" + path))
                        AssetDatabase.CreateFolder("Assets/Resources", path);
                }

                return path + (path.EndsWith('/') ? "" : "/") + "<<name>>.asset";
            }
        }
        public static string GetAssetName(Type type) => SettingsPath.Replace("<<name>>", type.Name.Replace("Configuration", ""));
#if UNITY_EDITOR
        public static string GetAssetNameForEditor(Type type) =>"Assets/Resources/" +GetAssetName(type);
#endif
    }
}