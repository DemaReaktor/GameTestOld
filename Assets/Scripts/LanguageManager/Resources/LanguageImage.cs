using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;
using GameArchitecture;

namespace Language.Resources
{
    public class LanguageImage : Image
    {
        public string Path = "Assets/";
        protected override void Start()
        {
            sprite = null;
            StartCoroutine("SetEvent");
        }
        private IEnumerator SetEvent()
        {
            while (!Game.IsInitialized) yield return null;

            if (Game.TryGetManager(out ILanguageManager languageManager))
            {
                languageManager.OnChangeLanguage += (string language) => SetImage(language,languageManager.Configuration.DefaulLanguage);
                SetImage(languageManager.Language, languageManager.Configuration.DefaulLanguage);
            }
        }

        private void OnApplicationQuit()
        {
            sprite = null;
        }

        protected override void OnEnable()
        {
            sprite = null;
        }

        /// <summary>
        /// set loaded texture into sprite
        /// </summary>
        /// <param name="language">language. If texture with this language dont exist texture will be loaded with default language</param>
        public void SetImage(string language, string defaultLanguage)
        {
            //if object dont exist by path and language object will be loaded by default language
            if (!LoadResourceByLanguage.TryLoadObject(Path, language,out object resource))
                resource = LoadResourceByLanguage.LoadObject(Path, defaultLanguage);

            sprite = Sprite.Create(resource as Texture2D, new Rect(0, 0, (resource as Texture2D).width, (resource as Texture2D).height), sprite != null ? sprite.pivot : new Vector2());
        }
    }
    [CustomEditor(typeof(LanguageImage))]
    public class LanguageImageEditor : ImageEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            (serializedObject.targetObject as LanguageImage).Path = EditorGUILayout.TextField( new GUIContent("Path", "path to texture." +
                " If texture with this path and language in ILanguageManager dont exist texture will be loaded with default language"), serializedObject.FindProperty("Path").stringValue);
        }
    }
}