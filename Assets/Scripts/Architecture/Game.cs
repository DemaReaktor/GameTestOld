using System;
using System.Collections.Generic;

namespace GameArchitecture
{
    /// <summary>
    /// main class that has all managers
    /// </summary>
    public class Game
    {
        private static LinkedList<IManager> managers;
        public static bool IsInitialized { get; private set; } = false;

        internal static void Initialize(LinkedList<IManager> managers)
        {
            Game.managers = managers;
            foreach (var element in managers)
                element.Initialize();

            IsInitialized = true;
        }

        public static T GetManager<T>() where T:IManager
        {                            
            if(!IsInitialized)
                throw new Exception("Game is not initialized. Add GameInitializer to Scene to initialize Game");

            foreach (object element in managers)
                if ((T)element != null)
                    return (T)element;

            throw new Exception("manager of this type does not exist");
        }
        public static bool TryGetManager<T>(out T value) where T : IManager
        {
            if (!IsInitialized)
                throw new Exception("Game is not initialized. Add GameInitializer to Scene to initialize Game");

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
