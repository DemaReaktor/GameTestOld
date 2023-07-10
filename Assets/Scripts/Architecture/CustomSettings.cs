using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using GameArchitecture.Save;

class ManagerSettings : ScriptableObject
{
    public const string customSettingsPath = "Assets/Editor/CustomSettings.asset";

    internal static ManagerSettings GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<ManagerSettings>(customSettingsPath);
        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<ManagerSettings>();
            AssetDatabase.CreateAsset(settings, customSettingsPath);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }
}

//Register a SettingsProvider using IMGUI for the drawing framework:
static class MyCustomSettingsIMGUIRegister
{
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        // First parameter is the path in the Settings window.
        // Second parameter is the scope of this setting: it only appears in the Project Settings window.
        var provider = new SettingsProvider("Project/MyCustomIMGUISettings", SettingsScope.Project)
        {
            // By default the last token of the path is used as display name if no label is provided.
            label = "Custom IMGUI",
            // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
            guiHandler = (searchContext) =>
            {
                var settings = ManagerSettings.GetSerializedSettings();
                EditorGUILayout.PropertyField(settings.FindProperty("m_Number"), new GUIContent("My Number"));
                EditorGUILayout.PropertyField(settings.FindProperty("m_SomeString"), new GUIContent("My String"));
            },

            // Populate the search keywords to enable smart search filtering and label highlighting:
            keywords = new HashSet<string>(new[] { "Number", "Some String" })
        };

        return provider;
    }
}

// Register a SettingsProvider using UIElements for the drawing framework:
static class MyCustomSettingsUIElementsRegister
{
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        // First parameter is the path in the Settings window.
        // Second parameter is the scope of this setting: it only appears in the Settings window for the Project scope.
        var provider = new SettingsProvider("Project/MyCustomUIElementsSettings", SettingsScope.Project)
        {
            label = "Custom UI Elements",
            // activateHandler is called when the user clicks on the Settings item in the Settings window.
            activateHandler = (searchContext, rootElement) =>
            {
                var settings = ManagerSettings.GetSerializedSettings();

                // rootElement is a VisualElement. If you add any children to it, the OnGUI function
                // isn't called because the SettingsProvider uses the UIElements drawing framework.
                var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/settings_ui.uss");
                if (styleSheet != null)
                    rootElement.styleSheets.Add(styleSheet);
                var title = new Label()
                {
                    text = "Custom UI Elements"
                };
                title.AddToClassList("title");
                rootElement.Add(title);

                var properties = new VisualElement()
                {
                    style =
                    {
                        flexDirection = FlexDirection.Column
                    }
                };
                properties.AddToClassList("property-list");
                rootElement.Add(properties);

                var tf = new TextField()
                {
                    value = settings.FindProperty("m_SomeString").stringValue
                };
                tf.AddToClassList("property-value");
                properties.Add(tf);
            },

            // Populate the search keywords to enable smart search filtering and label highlighting:
            keywords = new HashSet<string>(new[] { "Number", "Some String" })
        };

        return provider;
    }
}

// Create MyCustomSettingsProvider by deriving from SettingsProvider:
class MyCustomSettingsProvider : SettingsProvider
{
    private SerializedObject m_CustomSettings;

    //class Styles
    //{
    //    public static GUIContent number = new GUIContent("My Number");
    //    public static GUIContent someString = new GUIContent("Some string");
    //}

    public MyCustomSettingsProvider(string path, SettingsScope scope = SettingsScope.User)
        : base(path, scope) { }

    public static bool IsSettingsAvailable()
    {
        return File.Exists(ManagerSettings.customSettingsPath);
    }

    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        // This function is called when the user clicks on the MyCustom element in the Settings window.
        m_CustomSettings = ManagerSettings.GetSerializedSettings();
    }

    public override void OnGUI(string searchContext)
    {
        // Use IMGUI to display UI:
        //EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_Number"), Styles.number);
        //EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_SomeString"), Styles.someString);
    }

    // Register the SettingsProvider
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        if (IsSettingsAvailable())
        {
            var provider = new MyCustomSettingsProvider("Project/MyCustomSettingsProvider", SettingsScope.Project);

            // Automatically extract all keywords from the Styles.
            //provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            provider.keywords = GetSearchKeywordsFromGUIContentProperties<SettingsConfiguration>();
            return provider;
        }

        // Settings Asset doesn't exist yet; no need to display anything in the Settings window.
        return null;
    }
}