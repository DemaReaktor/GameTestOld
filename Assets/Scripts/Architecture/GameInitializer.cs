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
        [SerializeField] private MonoScript[] managers;

        private void Awake()
        {
            LinkedList<IManager> classes = new LinkedList<IManager>();

            foreach (var element in managers)
            {
                Type type = element.GetClass();

                if (!type.GetInterfaces().Contains(typeof(IManager)))
                    throw new Exception("every monoscript should has interface IManager");

                classes.AddLast(type.GetConstructor(new Type[] { }).Invoke(new Type[] { }) as IManager);
            }

            Game.Initialize(classes);
        }
    }
}
