using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace GameArchitecture
{
    /// <summary>
    /// class that initialize Game class
    /// </summary>
    [AddComponentMenu("Game/Initializer")]
    public class GameInitializer : MonoBehaviour
    {
        [Tooltip("names of mana")]
        [SerializeField] private MonoScript[] managers;
        private LinkedList<IManager> classes;

        private void Awake()
        {
            classes = new LinkedList<IManager>();

            //find every element in 
            foreach (var element in managers)
                classes.AddLast(element.GetClass().GetConstructor(new Type[] { }).Invoke(new Type[] { }) as IManager);

            Game.Initialize(classes);
        }
    }
}
