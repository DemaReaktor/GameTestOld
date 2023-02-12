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
        [SerializeField] private ManagerConfiguration[] ManagerConfiguration;

        private void Awake()
        {
            LinkedList<IManager> classes = new LinkedList<IManager>();

            foreach (var element in ManagerConfiguration)
            {
                Type type = element.MonoScript.GetClass();

                if(type.GetInterfaces().Any(t => t.Name.Substring(0, t.Name.IndexOf('`')) == typeof(IManager).Name))
                classes.AddLast(type.GetConstructor(new Type[] { typeof(object) }).Invoke(new Type[] { element.Configuration.GetType() }) as IManager);
                else
                classes.AddLast(type.GetConstructor(new Type[] {}).Invoke(new Type[] {}) as IManager);
            }

            Game.Initialize(classes);
        }
    }
}
