using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;
using GameArchitecture;
using Language;

namespace Language.Resources
{
    public class LanguageImage : Image
    {
        public string Path = "Assets/";
        protected override void Start()
        {
            StartCoroutine("SetEvent");
        }
        private IEnumerator SetEvent()
        {
            while (!Game.IsInitialized) yield return null;

            if (Game.TryGetManager(out ILanguageManager languageManager))
            {
                languageManager.OnChangeLanguage += (string language) => SetImage(LoadResourceByLanguage.LoadObject(Path, language, typeof(Texture2D)) as Texture2D);
                SetImage(LoadResourceByLanguage.LoadObject(Path, languageManager.Language, typeof(Texture2D)) as Texture2D);
            }
        }
        public void SetImage(Texture2D texture)
        {
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), sprite != null ? sprite.pivot : new Vector2());
        }

        [CustomEditor(typeof(LanguageImage))]
        public class LanguageImageEditor : ImageEditor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                EditorGUILayout.Space();
                (serializedObject.targetObject as LanguageImage).Path = EditorGUILayout.TextField("Path", serializedObject.FindProperty("Path").stringValue);
            }
        }
    }
}