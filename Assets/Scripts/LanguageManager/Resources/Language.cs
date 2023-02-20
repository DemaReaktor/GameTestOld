using UnityEngine;
using GameArchitecture;

namespace Language.Resources
{
    public class Language : MonoBehaviour
    {
        private ILanguageManager languageManager;
        void Start()
        {
            if (Game.IsInitialized && Game.TryGetManager(out ILanguageManager languageManager))
                this.languageManager = languageManager;
        }

        public void SetLanguage(string language)
        {
            if (languageManager != null)
                languageManager.Language = language;
        }

    }
}
