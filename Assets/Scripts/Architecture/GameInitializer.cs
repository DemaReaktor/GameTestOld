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

                if (!type.GetInterfaces().Contains(typeof(IManager)))
                    throw new Exception("every monoscript should has interface IManager");

                classes.AddLast(type.GetConstructor(new Type[] { typeof(System.Object) }).Invoke(new Type[] { element.Configuration.GetType() }) as IManager);
            }

            Game.Initialize(classes);
        }
    }
}
