using System;
using System.Linq;
using System.Collections.Generic;

namespace GameArchitecture
{
    /// <summary>
    /// main class that has all managers
    /// </summary>
    public class Game
    {
        private static LinkedList<Object> managers;

        /// <summary>
        /// true if Game is ready to be used
        /// </summary>
        public static bool IsInitialized { get; private set; } = false;
        /// <summary>
        /// when Game is ready to be used this will be invoked
        /// </summary>
        public static event Action OnInitializeFinish;

        internal static void Initialize(LinkedList<Object> managers)
        {
            Game.managers = managers;
            IsInitialized = true;
            OnInitializeFinish?.Invoke();
        }

        /// <summary>
        /// get manager of current class
        /// throw exception if manager of this class is not exist in GameInitializer
        /// </summary>
        /// <typeparam name="T">type of manager that is needed</typeparam>
        /// <returns>manager</returns>
        public static T GetManager<T>() where T:class
        {                            
            if(!IsInitialized)
                throw new Exception("Game is not initialized. Add GameInitializer to Scene to initialize Game or wait when It will be initialized");

            foreach (object element in managers)
                if (element as T != null)
                    return element as T;

            throw new Exception("manager of this type does not exist");
        }

        /// <summary>
        /// set manager of current class to value
        /// </summary>
        /// <typeparam name="T">type of manager</typeparam>
        /// <param name="value"> manager</param>
        /// <returns>true if this manager exist</returns>
        public static bool TryGetManager<T>(out T value) where T:class
        {
            value = default;

            if (!IsInitialized)
                return false;

            foreach (object element in managers)
                if (element as T != null)
                {
                    value = element as T;
                    return true;
                }

            return false;
        }
    }
}
