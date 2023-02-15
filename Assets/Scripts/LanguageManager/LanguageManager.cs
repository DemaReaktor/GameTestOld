using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameArchitecture;
using GameArchitecture.Save;

namespace Language
{
    public class LanguageManager : ILanguageManager, IManagersValidation
    {
        public LanguageConfiguration Configuration { get; private set; }
        public event Action<string> OnChangeLanguage;
        public string Language { get; private set; }

        public void Initialize(LanguageConfiguration configuration)
        {
            Configuration = configuration;

            if (Configuration.AllLanguages is null)
                Configuration.AllLanguages = new string[] { Configuration.DefaulLanguage };

            if (!Configuration.AllLanguages.Contains(Configuration.DefaulLanguage))
            {
                List<string> languages = Configuration.AllLanguages.ToList();
                languages.Add(Configuration.DefaulLanguage);

                Configuration.AllLanguages = languages.ToArray();
            }

            Language = Configuration.DefaulLanguage;

            Game.OnInitializeFinish += SetLanguage;
        }
        private void SetLanguage() {
            if (!Game.TryGetManager(out ISaveManager<SettingsConfiguration> manager))
                throw new Exception("LanguageManger can`t be initialized because GameInitializer should have ISaveManager<SettingsConfiguration>");

            if (manager.TryGet(out string language, "language"))
            {
                Language = language;
                OnChangeLanguage?.Invoke(Language);
            }
        }

        public void SetLanguage(string value)
        {
            if (!Configuration.AllLanguages.Contains(value))
                throw new ArgumentException($"Game have not '{value}' language");

            Language = value;

            Game.GetManager<ISaveManager<SettingsConfiguration>>().Set(Language, "language");
            Game.GetManager<ISaveManager<SettingsConfiguration>>().Save();

            OnChangeLanguage?.Invoke(Language);
        }

        public static bool Validate(ManagerCharacter[] characters)
        {
            foreach (var character in characters)
                if (character.ManagerType!=null && character.ManagerType.GetInterfaces().Any(i=> i.Name == typeof(ISaveManager<SettingsConfiguration>).Name))
                    return true;

            Debug.Log("LanguageManger can`t be added because GameInitializer should have ISaveManager<SettingsConfiguration> for LanguageManger");
            return false;
        }
    }
}
