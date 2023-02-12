using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
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
        [SerializeField] private ManagerConfiguration[] Managers;

        private void Awake()
        {
            LinkedList<System.Object> classes = new LinkedList<System.Object>();

            foreach (var element in Managers)
            {
                Type type;
                try
                {
                    type = element.MonoScript.GetClass();
                }
                catch
                {
                    Debug.LogWarning($"GameInitializer has null propery in Managers array. Index of null property: {Managers.ToList().IndexOf(element)}");
                    continue;
                }

                //if (type.GetInterfaces().Contains(typeof(IManager)))
                //classes.AddLast(type.GetConstructor(new Type[] { }).Invoke(new Type[] { }));
                //else
                //if (type.GetInterfaces().Any(t => t.Name.Substring(0, t.Name.IndexOf('`')) == "IManager"))
                if (type.GetConstructor(new Type[] { }) is null)
                    throw new Exception("every manager should have constructor without arguments or don`t have constructors");
                System.Object t = Activator.CreateInstance(type);
                //System.Object t = type.GetConstructor(new Type[] { }).Invoke(new Type[] { });

                if (type.GetInterface("IManager") != null)
                    t.GetType().GetMethod("Initialize").Invoke(t, new object[] { });
                else
                    t.GetType().GetMethod("Initialize").Invoke(t, new object[] { element.Configuration });
                //(t as IManager<System.Object>).Initialize(element.Configuration/* as System.Object*/);
                classes.AddLast(t);
                //classes.AddLast(type.GetConstructors().Where(c => c.IsPublic&&c.GetGenericArguments().Length == 1&& c.GetGenericArguments().First().IsClass).First().Invoke( new System.Object[] { element.Configuration }));
                //classes.AddLast(type.GetConstructor(new Type[] { typeof(object) }).Invoke(new Type[] { element.Configuration.GetType() }));
                //else
                //classes.AddLast(type.GetConstructor(new Type[] {}).Invoke(new Type[] {}));
            }

            Game.Initialize(classes);
        }
    }
}
