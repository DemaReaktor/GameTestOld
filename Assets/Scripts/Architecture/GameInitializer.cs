using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;

namespace GameArchitecture
{
    /// <summary>
    /// class that initialize Game class
    /// </summary>
    //[AddComponentMenu("Game/GameInitializer")]
    public class GameInitializer : MonoBehaviour
    {
        [Tooltip("managers of this scene")]
        [SerializeField] internal MonoScript[] Managers;

        private void Awake()
        {
            LinkedList<System.Object> classes = new LinkedList<System.Object>();

            foreach (var element in Managers)
            {
                Type type;

                //check if this monoscript has class
                try
                {
                    type = element.GetClass();
                }
                catch
                {
                    Debug.LogWarning($"GameInitializer has null property in Managers array. Index of null property: {Managers.ToList().IndexOf(element)}");
                    continue;
                }

                //if class of monoscipt has constuctor with arguments
                //manager will not have ability to be created
                if (type.GetConstructor(new Type[] { }) is null)
                {
                    Debug.LogError("every manager should have constructor without arguments or don`t have constructors");
                    continue;
                }

                //create manager
                System.Object t = Activator.CreateInstance(type);

                //invoke method Initialize
                if (type.GetInterface("IManager") != null)
                    t.GetType().GetMethod("Initialize").Invoke(t, new object[] { });
                else
                    t.GetType().GetMethod("Initialize").Invoke(t, new object[] { Game.GetConfiguration(type.GetInterfaces().First(
                                t => t.FullName.Contains('`') && t.FullName.Substring(0, t.FullName.IndexOf('`')) == "GameArchitecture.IManager").GenericTypeArguments[0]) });

                classes.AddLast(t);
            }

            Game.Initialize(classes.ToHashSet());
        }
    }
    [CustomEditor(typeof(GameInitializer))]
    public class GameInitializerEditor : Editor
    {
        SerializedProperty SerializedProperty;

        private void OnEnable()
        {
            SerializedProperty = serializedObject.FindProperty("Managers");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            List<Type> types = new List<Type>();
            for (int i = 0; i < SerializedProperty.arraySize; i++)
            {
                var monoscript = SerializedProperty.GetArrayElementAtIndex(i).objectReferenceValue as MonoScript;

                //fields with null will not be removed
                if (monoscript is null)
                    continue;

                var type = monoscript.GetClass();
                IEnumerable<MethodInfo> validations = null;

                //fields without class or without IManager or IManager<> or with the same class will be removed
                if (!type.IsClass ||
                    (!type.GetInterfaces().Any(i => i == typeof(IManager)) &&
                    !type.GetInterfaces().Any(t => t.FullName.Contains('`') && t.FullName.Substring(0, t.FullName.IndexOf('`')) == "GameArchitecture.IManager")) ||
                    types.Contains(type) ||
                    !Validate(type, types))
                //(type.GetInterface("IManagersValidation") != null && !(bool)(type.GetMethod("ValidateManagers").Invoke(null, new object[] { types }))))
                {
                    //last field will be null
                    if (i + 1 == SerializedProperty.arraySize)
                    {
                        SerializedProperty.GetArrayElementAtIndex(i).objectReferenceValue = null;
                        break;
                    }

                    SerializedProperty.DeleteArrayElementAtIndex(i--);
                    continue;
                }

                //add class to list to check the same classes
                types.Add(monoscript.GetClass());
            }
            EditorGUILayout.PropertyField(SerializedProperty);
            serializedObject.ApplyModifiedProperties();
        }

        private bool Validate(Type type, List<Type> types)
        {
            IEnumerable<MethodInfo> validations = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(ManagerValidationAttribute), true).Length > 0);
            if (validations.Count() == 0)
                return true;

            foreach (var method in validations)
            {
                try
                {
                    return (bool)(validations.First().Invoke(null, new object[] { types }));
                }
                catch
                {
                    continue;
                }
            }

            return true;
        }
    }
}
