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
    [AddComponentMenu("Game/Initializer")]
    public class GameInitializer : MonoBehaviour
    {
        [Tooltip("managers of this scene")]
        [SerializeField] internal ManagerConfiguration[] Managers;

        private void Awake()
        {
            LinkedList<System.Object> classes = new LinkedList<System.Object>();

            foreach (var element in Managers)
            {
                Type type;

                //check if this monoscript has class
                try
                {
                    type = element.MonoScript.GetClass();
                }
                catch
                {
                    Debug.LogWarning($"GameInitializer has null propery in Managers array. Index of null property: {Managers.ToList().IndexOf(element)}");
                    continue;
                }

                //if class of monoscipt has constuctor with arguments
                //manager will not have ability to be created
                if (type.GetConstructor(new Type[] { }) is null)
                    throw new Exception("every manager should have constructor without arguments or don`t have constructors");

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

            Game.Initialize(classes);
        }
    }
    }
