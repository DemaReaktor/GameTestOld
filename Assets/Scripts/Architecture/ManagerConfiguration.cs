using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

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
        private class ManagerProperty
        {
            private SerializedProperty property;
            private MonoScript monoScript;

            public ManagerProperty(SerializedProperty property) { this.property = property; }

            public MonoScript MonoScript
            {
                get => monoScript;
                set
                {
                    MonoScript old = property.FindPropertyRelative("MonoScript").objectReferenceValue as MonoScript;

                    if (value != null)
                    {
                        ManagerConfiguration[] managerConfigurations = (property.serializedObject.targetObject as GameInitializer).Managers;
                        Type[] managerTypes = new Type[managerConfigurations.Length];
                        for (int i = 0; i < managerTypes.Length; i++)
                            if (managerConfigurations[i].MonoScript != null)
                                managerTypes[i] = managerConfigurations[i].MonoScript.GetClass();

                        if (value.GetClass().IsClass && (value.GetClass().GetInterfaces().Any(i => i.Name == "IManager") || value.GetClass().GetInterfaces().Any(t => t.Name.Substring(0, t.Name.IndexOf('`')) == "IManager")))
                        {
                            if (!value.GetClass().GetInterfaces().Any(i => i.Name == "IManagersValidation") || (bool)(value.GetClass().GetMethod("Validate").Invoke(null, new object[] { managerTypes })))
                                monoScript = value;
                        }
                        else
                            Debug.LogWarning("Here can be only classes derived by IManager or IManager<> in this property");
                    }

                    if (monoScript != old)
                    {
                        IsConfiguration = false;

                        if (monoScript != null && !monoScript.GetClass().GetInterfaces().Any(i => i.Name == "IManager"))
                        {
                            IsConfiguration = true;
                            Configuration = Activator.CreateInstance(monoScript.GetClass().GetInterfaces().Where(t => t.Name.Substring(0, t.Name.IndexOf('`')) == "IManager").First().GenericTypeArguments[0]);
                        }
                    }
                    else
                    {
                        IsConfiguration = monoScript != null && !monoScript.GetClass().GetInterfaces().Any(i => i.Name == "IManager");
                        if (IsConfiguration)
                            Configuration = property.FindPropertyRelative("Configuration").managedReferenceValue;
                    }
                }
            }
            public bool IsConfiguration { get; private set; }
            public object Configuration { get; set; }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            ManagerConfiguration[] array = (property.serializedObject.targetObject as GameInitializer).Managers;

            void DeleteCopyElement()
            {
                Type[] list = new Type[array.Length];
                for (int i = 0; i < array.Length; i++)
                    if (array[i].MonoScript != null)
                        list[i] = array[i].MonoScript.GetClass();

                for (int x = array.Length - 1; x > 0; x--)
                    for (int i = 0; i < x; i++)
                        if (list[x] == list[i] && list[x] != null)
                        {
                            array[x] = null;
                            Debug.LogWarning("GameInitializer can not have the same managers (if you just add new element to array you can ignore this)");
                        }
            }

            MonoScript monoScript = property.FindPropertyRelative("MonoScript").objectReferenceValue as MonoScript;
            ManagerProperty manager = new ManagerProperty(property);

            manager.MonoScript = EditorGUI.ObjectField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
            new GUIContent(monoScript is null ?"Null": monoScript.name, "class should be direved by IManager or IManager<(any object)>"), property.FindPropertyRelative("MonoScript").objectReferenceValue, typeof(MonoScript), false) as MonoScript;

            DeleteCopyElement();
            property.serializedObject.Update();

            property.FindPropertyRelative("MonoScript").objectReferenceValue = manager.MonoScript;

            if (manager.IsConfiguration)
            {
                EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("Configuration"), new GUIContent(
                    manager.Configuration.GetType().Name), true);

                property.FindPropertyRelative("Configuration").managedReferenceValue = manager.Configuration;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            MonoScript monoScript = property.FindPropertyRelative("MonoScript").objectReferenceValue as MonoScript;

            //if configuration is
            if (monoScript != null && !monoScript.GetClass().GetInterfaces().Any(i => i.Name == "IManager"))
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Configuration"));

            return EditorGUIUtility.singleLineHeight;
        }
    }
}
