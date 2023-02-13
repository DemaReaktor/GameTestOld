using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameArchitecture;
using GameArchitecture.Save;

namespace Language
{
    public class LanguageManager : ILanguageManager
    {
        public LanguageConfiguration Configuration { get; private set; }
        public event Action<string> OnChangeLanguage;
        public string Language { get; private set; }

        public void Initialize(LanguageConfiguration configuration) { 
            Configuration = configuration;

            if (Configuration.AllLanguages is null)
                Configuration.AllLanguages = new string[] { Configuration.DefaulLanguage };

            if (!Configuration.AllLanguages.Contains(Configuration.DefaulLanguage))
            {
                List<string> languages = Configuration.AllLanguages.ToList();
                languages.Add(Configuration.DefaulLanguage);

                Configuration.AllLanguages = languages.ToArray();
            }

            if (!Game.TryGetManager(out SettingsManager manager))
                throw new Exception("LanguageManger can`t be initialized because GameInitializer should have SettingsManager(if SettingsManager is in GameInitializer there try set It upper then LanguageManger in list)");

            if(manager.TryGet(out string language,"language"))
            {
                SetLanguage(language);
                return;
            }

            Language = Configuration.DefaulLanguage;
        }

        public void SetLanguage(string value) 
        {
            if (!Configuration.AllLanguages.Contains(value))
                throw new ArgumentException($"Game have not '{value}' language");

            Language = value;
            OnChangeLanguage?.Invoke(Language);
        }
    }
}
