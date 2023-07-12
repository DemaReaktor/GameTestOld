using System;
using UnityEditor;


namespace GameArchitecture
{
    /// <summary>
    /// object that do special things in game
    /// can be initialized using GameInitializer to be in Game
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IManager<T> where T : class
    {
        T Configuration { get; }
        /// <summary>
        /// this method will be invoked at awake if this manager is added to gameInitializer
        /// </summary>
        /// <param name="configuration"> this value set configuration to this manager
        /// configuration save parametres of manager</param>
        void Initialize(T configuration);
    }
    /// <summary>
    /// object that do special things in game
    /// can be initialized using GameInitializer to be in Game
    /// </summary>
    public interface IManager 
    {
        /// <summary>
        /// this method will be invoked at awake if this manager is added to gameInitializer
        /// </summary>
        void Initialize();
    }
}
