using System;
using UnityEngine;
using UnityEditor;

namespace GameArchitecture.Save
{
    [Serializable]
    public class SettingsConfiguration
    {
        [Tooltip("name of file. Format of this file should be xml. Format is not needed to be written(It will be written automaticallly)")]
        public string FileName = "Settings";
        public string DefaultFileName = "DefaultSettings";

#if UNITY_EDITOR
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return CustomProjectSettingsProvider<SettingsConfiguration>.CreateSettingsProvider();
        }
#endif
    }
}
