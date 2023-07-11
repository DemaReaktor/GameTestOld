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


        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return CustomProjectSettingsProvider<GeneralConfiguration>.CreateSettingsProvider();
        }

        public static string SettingsPath
        {
            get
            {
                string path = defaultPath;
                if (Game.TryGetConfiguration(out GeneralConfiguration generalConfiguration))
                    path = generalConfiguration.ConfigurationsPath;

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