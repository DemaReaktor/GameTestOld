using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

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
    //[CustomEditor(typeof(GameInitializer))]
    //public class GameInitializerEditor : Editor
    [CustomEditor(typeof(GameInitializer))]
    public class GameInitializerEditor : Editor
    {
        SerializedProperty SerializedProperty;

        private void OnEnable()
        {
            SerializedProperty = serializedObject.FindProperty("Managers");
        }
        public override void OnInspectorGUI()
        //public override void OnInspectorGUI()
        {
            serializedObject.Update();
            for (int i = SerializedProperty.arraySize-1; i >0; i++)
            //var r = property.FindPropertyRelative("Mangers");
            //for (int i = 0; i < property.FindPropertyRelative("Mangers").arraySize; i++)
            {
                //var monoscript = SerializedProperty.GetArrayElementAtIndex(i).objectReferenceValue as MonoScript;
                //if (monoscript is null ||
                //    (monoscript.GetClass().IsClass && (monoscript.GetClass().GetInterfaces().Any(i => i == typeof(IManager)) || (
                //        monoscript.GetClass().GetInterfaces().Any(t => t.FullName.Contains('`') && t.FullName.Substring(0, t.FullName.IndexOf('`')) == "GameArchitecture.IManager")))))
                //    continue;

                //SerializedProperty.DeleteArrayElementAtIndex(i);
                //SerializedProperty.arraySize--;
            }
            EditorGUILayout.PropertyField(SerializedProperty);


            serializedObject.ApplyModifiedProperties();
            //EditorGUILayout.PropertyField(r);
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("Managers"));
            //EditorGUI.BeginProperty(position, label, property);

            //MonoScript[] array = property.FindPropertyRelative("Managers").managedReferenceValue as MonoScript[];

            //MonoScript monoScript = property.FindPropertyRelative("Managers").objectReferenceValue as MonoScript;
            //ManagerProperty manager = new ManagerProperty(property);

            //set monoscript
            //manager.MonoScript = EditorGUI.ObjectField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
            //new GUIContent(monoScript is null ? "Null" : monoScript.name, "class should be direved by IManager or IManager<(any object)>"), property.FindPropertyRelative("MonoScript").objectReferenceValue, typeof(MonoScript), false) as MonoScript;

            //void DeleteCopyElement()
            //{
            //    //get types of all managers in GameInitializer
            //    Type[] list = new Type[array.Length];
            //    for (int i = 0; i < array.Length; i++)
            //            list[i] = array[i] is null?null: array[i].GetClass();

            //    //if here are the same types of managers in GameInitializer
            //    for (int x = array.Length - 1; x > 0; x--)
            //        for (int i = 0; i < x; i++)
            //        {
            //                if (list[x] == list[i] && list[x] != null)
            //            {
            //                //delete last manager of two same managers
            //                array[x] = null;

            //                Debug.LogWarning("GameInitializer can not have the same managers (if you just add new element to array you can ignore this)");
            //            }
            //        }
            //}
            //DeleteCopyElement();

            //property.serializedObject.Update();

            //foreach(var element in array)
            //    if(element != null)
            //        property.
            //        property.FindPropertyRelative("Managers").fin
            ////property.FindPropertyRelative("MonoScript").objectReferenceValue = manager.MonoScript;

        }

    }
}
