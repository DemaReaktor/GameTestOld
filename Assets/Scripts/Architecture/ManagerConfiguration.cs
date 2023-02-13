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
        private class PropertyDrawerObject
        {
            public MonoScript MonoScript { 
                get => monoScript; 
                set {
                    MonoScript old = monoScript;

                    if (value is null || (value.GetClass().IsClass && (value.GetClass().GetInterfaces().Any(i => i.Name == "IManager") || value.GetClass().GetInterfaces().Any(t => t.Name.Substring(0, t.Name.IndexOf('`')) == "IManager"))))
                        monoScript = value;

                    if(monoScript != old)
                    {
                        IsConfiguration = false;

                        if (monoScript != null && !monoScript.GetClass().GetInterfaces().Any(i => i.Name == "IManager"))
                        {
                            IsConfiguration = true;
                            Configuration = Activator.CreateInstance(monoScript.GetClass().GetInterfaces().Where(t => t.Name.Substring(0, t.Name.IndexOf('`')) == "IManager").First().GenericTypeArguments[0]);
                        }
                    }
                } 
            }
            private MonoScript monoScript;
            private bool isNeedChangeConfiguration = true;
            public bool IsConfiguration { get; private set; }
            public object Configuration;
            public bool IsNeedChangeConfiguration
            {
                get
                {
                    if(isNeedChangeConfiguration)
                    {
                        isNeedChangeConfiguration = false;
                        return true;
                    }

                    return false;
                }
            }
        }

        private Dictionary<string, PropertyDrawerObject> objects = new Dictionary<string, PropertyDrawerObject>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            if (!objects.Keys.Contains(property.propertyPath))
                objects[property.propertyPath] = new PropertyDrawerObject();

            objects[property.propertyPath].MonoScript = EditorGUI.ObjectField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
            new GUIContent(objects[property.propertyPath].MonoScript is null?"Null": objects[property.propertyPath].MonoScript.name, "class should be direved by IManager or IManager<(any object)>"), property.FindPropertyRelative("MonoScript").objectReferenceValue, typeof(MonoScript), false) as MonoScript;

            property.FindPropertyRelative("MonoScript").objectReferenceValue = objects[property.propertyPath].MonoScript;

            if (objects[property.propertyPath].IsConfiguration)
            {
                EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("Configuration"), new GUIContent(
                    objects[property.propertyPath].Configuration.GetType().Name), true);

                if (objects[property.propertyPath].IsNeedChangeConfiguration)
                    property.FindPropertyRelative("Configuration").managedReferenceValue = objects[property.propertyPath].Configuration;

                objects[property.propertyPath].Configuration = property.FindPropertyRelative("Configuration").managedReferenceValue;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //if configuration is
            if (objects.Keys.Contains(property.propertyPath)&& objects[property.propertyPath].IsConfiguration)
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Configuration"));

            return EditorGUIUtility.singleLineHeight;
        }
    }
}
