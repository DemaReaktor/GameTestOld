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

        internal static void Initialize(LinkedList<Object> managers)
        {
            Game.managers = managers;
            IsInitialized = true;
        }

        /// <summary>
        /// get manager of current class
        /// throw exception if manager of this class is not exist in GameInitializer
        /// </summary>
        /// <typeparam name="T">type of manager that is needed</typeparam>
        /// <returns>manager</returns>
        public static T GetManager<T>()
        {                            
            if(!IsInitialized)
                throw new Exception("Game is not initialized. Add GameInitializer to Scene to initialize Game or wait when It will be initialized");

            foreach (object element in managers)
                if ((T)element != null)
                    return (T)element;

            throw new Exception("manager of this type does not exist");
        }

        /// <summary>
        /// set manager of current class to value
        /// </summary>
        /// <typeparam name="T">type of manager</typeparam>
        /// <param name="value"> manager</param>
        /// <returns>true if this manager exist</returns>
        public static bool TryGetManager<T>(out T value)
        {
            if (!IsInitialized)
                throw new Exception("Game is not initialized. Add GameInitializer to Scene to initialize Game or wait when It will be initialized");

            foreach (object element in managers)
                if ((T)element != null)
                {
                    value = (T)element;
                    return true;
                }

            value = default;
            return false;
        }
    }
}
