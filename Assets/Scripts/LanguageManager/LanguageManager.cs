using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using GameArchitecture;
using GameArchitecture.Save;

namespace Language
{
    public class LanguageManager : ILanguageManager
    {
        public LanguageConfiguration Configuration { get; private set; }
        /// <summary>
        /// when language is changed
        /// </summary>
        public event Action<string> OnChangeLanguage;
        public string Language { get => language;
            set {
                if (!Configuration.AllLanguages.Contains(value))
                    throw new ArgumentException($"Game have not '{value}' language");

                language = value;

                Game.GetManager<ISaveManager<SettingsConfiguration>>().Set(language, "language");
                Game.GetManager<ISaveManager<SettingsConfiguration>>().Save();

                OnChangeLanguage?.Invoke(language);
            } }

        private string language;

        public void Initialize(LanguageConfiguration configuration)
        {
            Configuration = configuration;

            if (Configuration.AllLanguages is null)
                Configuration.AllLanguages = new string[] { Configuration.DefaultLanguage };

            //if defaulta language is not in array It will be setted
            if (!Configuration.AllLanguages.Contains(Configuration.DefaultLanguage))
            {
                List<string> languages = Configuration.AllLanguages.ToList();
                languages.Add(Configuration.DefaultLanguage);

                Configuration.AllLanguages = languages.ToArray();
            }

            language = Configuration.DefaultLanguage;

            //when game finish initialization this manager set language which is in ISaveManager
            Game.OnInitializeFinish += SetLanguage;
        }

        private void SetLanguage() {
            if (!Game.TryGetManager(out ISaveManager<SettingsConfiguration> manager))
                throw new Exception("LanguageManger can`t be initialized because GameInitializer should have ISaveManager<SettingsConfiguration>");

            if (manager.TryGet(out string language, "language"))
            {
                this.language = language;
                OnChangeLanguage?.Invoke(Language);
            }
        }
                    
        [ManagerValidation]
        public static bool ValidateManagers(List<Type> characters)
        {
            //if one of managers is ISaveManager<SettingsConfiguration>
            foreach (var character in characters)
                if (character.GetInterfaces().Any(i=> i == typeof(ISaveManager<SettingsConfiguration>)))
                    return true;

            Debug.Log("LanguageManger can`t be added because GameInitializer should have ISaveManager<SettingsConfiguration> for LanguageManger");
            return false;
        }
    }
}
