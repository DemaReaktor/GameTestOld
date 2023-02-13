using System;
using GameArchitecture;

namespace Language {
    public interface ILanguageManager : IManager<LanguageConfiguration>
    {
        /// <summary>
        /// when language is changed
        /// </summary>
        event Action<string> OnChangeLanguage; 
        string Language { get; }
        void SetLanguage(string value);
    }
}