using UnityEngine;
using System;
using UnityEditor;

namespace GameArchitecture
{
    [Serializable]
    public class GeneralConfiguration
    {
        private const string defaultPath = "Assets/Configurations/";
        public string ConfigurationsPath = defaultPath;
        private static GeneralConfiguration configuration = new GeneralConfiguration();

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var value = CustomProjectSettingsProvider<GeneralConfiguration>.CreateSettingsProvider();
            configuration = Game.GetConfiguration<GeneralConfiguration>();
            return value;
        }

        public static string SettingsPath
        {
            get
            {
                string path = configuration.ConfigurationsPath;

                if (!AssetDatabase.IsValidFolder(path))
                {
                    Debug.LogWarning($"Configurations Path does not exist. All configurations will be saved in {defaultPath}");
                    path = defaultPath;

                    if (!AssetDatabase.IsValidFolder(defaultPath))
                        AssetDatabase.CreateFolder("Assets", defaultPath.Replace("Assets", "").Replace("/", ""));
                }

                return path + (path.EndsWith('/') ? "" : "/") + "<<name>>.asset";
            }
        }
        public static string GetAssetName(Type type) => SettingsPath.Replace("<<name>>", type.Name.Replace("Configuration", ""));
    }
}