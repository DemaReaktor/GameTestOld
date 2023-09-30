using GameArchitecture;
using UnityEditor;

namespace Language
{
    public class LanguageConfiguration
    {
        /// <summary>
        /// language which is used when game start or when some language dont have resources
        /// </summary>
        public string DefaultLanguage = "English";
        /// <summary>
        /// all languages which are in game for now
        /// </summary>
        public string[] AllLanguages;

#if UNITY_EDITOR
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return CustomProjectSettingsProvider<LanguageConfiguration>.CreateSettingsProvider();
        }
#endif
    }
}
