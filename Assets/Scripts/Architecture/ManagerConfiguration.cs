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
            private SerializedProperty property;
            private MonoScript monoScript;
            private bool isNeedChangeConfiguration = true;

            public PropertyDrawerObject(SerializedProperty property) { this.property = property; }

            public MonoScript MonoScript
            {
                get => monoScript;
                set
                {
                    MonoScript old = monoScript;

                    if (value is null || (value.GetClass().IsClass && (value.GetClass().GetInterfaces().Any(i => i.Name == "IManager") || value.GetClass().GetInterfaces().Any(t => t.Name.Substring(0, t.Name.IndexOf('`')) == "IManager"))))
                        monoScript = value;

                    if (monoScript != old)
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
            public bool IsConfiguration { get; private set; }
            public object Configuration { get; set; }
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

        //private Dictionary<string, PropertyDrawerObject> objects = new Dictionary<string, PropertyDrawerObject>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            ManagerConfiguration[] array = (property.serializedObject.targetObject as GameInitializer).Managers;

            void DeleteCopyElements()
            {
                Type[] list = new Type[array.Length];
                for (int i = 0; i < array.Length; i++)
                    list[i] = array[i]!=null? array[i].MonoScript != null ? array[i].MonoScript.GetClass() : null:null;

                for (int x = array.Length - 1; x > 0; x--)
                    for (int i = 0; i < x; i++)
                        if (list[x] == list[i] && list[x] != null)
                            array[x] = null;
            }

            DeleteCopyElements();

            //ManagerConfiguration[] d = GetArray((property.serializedObject.targetObject as GameInitializer).Managers);


            //if (!objects.Keys.Contains(property.propertyPath))
            //    propertyDrawerObject = new PropertyDrawerObject();

            MonoScript monoScript = property.FindPropertyRelative("MonoScript").objectReferenceValue as MonoScript;
            PropertyDrawerObject propertyDrawerObject = new PropertyDrawerObject(property);

            propertyDrawerObject.MonoScript = EditorGUI.ObjectField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
            new GUIContent(monoScript is null ?"Null": monoScript.name, "class should be direved by IManager or IManager<(any object)>"), property.FindPropertyRelative("MonoScript").objectReferenceValue, typeof(MonoScript), false) as MonoScript;

            property.FindPropertyRelative("MonoScript").objectReferenceValue = propertyDrawerObject.MonoScript;

            if (propertyDrawerObject.IsConfiguration)
            {
                EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("Configuration"), new GUIContent(
                    propertyDrawerObject.Configuration.GetType().Name), true);

                if (propertyDrawerObject.IsNeedChangeConfiguration)
                    property.FindPropertyRelative("Configuration").managedReferenceValue = propertyDrawerObject.Configuration;

                propertyDrawerObject.Configuration = property.FindPropertyRelative("Configuration").managedReferenceValue;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            MonoScript monoScript = property.FindPropertyRelative("MonoScript").objectReferenceValue as MonoScript;
            //if configuration is
            if (monoScript !=null && !monoScript.GetClass().GetInterfaces().Any(i => i.Name == "IManager"))
            //if (objects.Keys.Contains(property.propertyPath)&& propertyDrawerObject.IsConfiguration)
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Configuration"));

            return EditorGUIUtility.singleLineHeight;
        }
    }
}
