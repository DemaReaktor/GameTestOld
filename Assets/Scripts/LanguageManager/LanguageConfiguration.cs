using GameArchitecture;
using UnityEditor;

namespace Language
{
    public class LanguageConfiguration
    {
        /// <summary>
        /// language which is used when game start or when some language dont have resources
        /// </summary>
        public string DefaulLanguage = "English";
        /// <summary>
        /// all languages which are in game for now
        /// </summary>
        public string[] AllLanguages;

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return SettingsRegister<LanguageConfiguration>.CreateSettingsProvider();
        }
    }
}
