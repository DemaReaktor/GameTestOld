using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

namespace GameArchitecture
{
    /// <summary>
    /// class that save parametres of manager in inspector
    /// </summary>
    [Serializable]
    public class ManagerConfiguration
    {
        /// <summary>
        /// file that has manager
        /// </summary>
        public MonoScript MonoScript;
        /// <summary>
        /// class that has patametres of manager
        /// Can be null
        /// </summary>
        [SerializeReference] public object Configuration;
    }

    [CustomPropertyDrawer(typeof(ManagerConfiguration))]
    public class ManagerConfigurationEditor : PropertyDrawer
    {
        //dictionaries that save parametres of every SerializedProperty
        private Dictionary<string, MonoScript> OldMonoScript = new Dictionary<string, MonoScript>();
        private Dictionary<string, bool> isConfiguration = new Dictionary<string, bool>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            //set name to Monoscript proverty by name of class
            string name = "Null";
            if(property.FindPropertyRelative("MonoScript").objectReferenceValue != null)
                name = property.FindPropertyRelative("MonoScript").objectReferenceValue.name;

            //get value of class
            MonoScript MonoScript = EditorGUI.ObjectField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                new GUIContent(name, "class should be direved by IManager or IManager<(any object)>"), property.FindPropertyRelative("MonoScript").objectReferenceValue, typeof(MonoScript), false) as MonoScript;

            //check if this class is derived by IManager otherwise property dont change value
            if (MonoScript != null && (!MonoScript.GetClass().IsClass ||( !MonoScript.GetClass().GetInterfaces().Any(i => i.Name == "IManager") && !MonoScript.GetClass().GetInterfaces().Any(t => t.Name.Substring(0, t.Name.IndexOf('`')) == "IManager"))))
                    MonoScript = OldMonoScript.Keys.Contains(property.propertyPath)? OldMonoScript[property.propertyPath]: null;

            //set value
            property.FindPropertyRelative("MonoScript").objectReferenceValue = MonoScript;

            //if propery value of manager is changed
            if ((!OldMonoScript.Keys.Contains(property.propertyPath) && MonoScript != null) || (OldMonoScript.Keys.Contains(property.propertyPath) && MonoScript != OldMonoScript[property.propertyPath]))
            {
                isConfiguration[property.propertyPath] = false;

                //if manager has configuration ( if manager has generic argument)
                if (MonoScript != null && !MonoScript.GetClass().GetInterfaces().Any(i => i.Name == "IManager"))
                {
                    //crete this configuration and set into value
                    property.FindPropertyRelative("Configuration").managedReferenceValue = Activator.CreateInstance(MonoScript.GetClass().GetInterfaces().Where(t => t.Name.Substring(0, t.Name.IndexOf('`')) == "IManager").First().GenericTypeArguments[0]);

                    isConfiguration[property.propertyPath] = true;
                }
            }

            OldMonoScript[property.propertyPath] = MonoScript;

            //show property
            if (isConfiguration.Keys.Contains(property.propertyPath) && isConfiguration[property.propertyPath])
                EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("Configuration"), true);

            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //if configuration is
            if (isConfiguration.Keys.Contains(property.propertyPath) && isConfiguration[property.propertyPath])
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Configuration"));

            return EditorGUIUtility.singleLineHeight;
        }
    }
}
