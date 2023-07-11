using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;

namespace GameArchitecture
{
    /// <summary>
    /// main class that has all managers
    /// </summary>
    public static class Game
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

        /// <summary>
        /// get configuration of current class
        /// throw exception if configuration of this class is not exist in GameInitializer
        /// </summary>
        /// <typeparam name="T">type of configuration that is needed</typeparam>
        /// <returns>configuration</returns>
        public static T GetConfiguration<T>() where T : class
        {
            if (!IsInitialized)
                throw new Exception("Game is not initialized. Add GameInitializer to Scene to initialize Game or wait when It will be initialized");

            CustomProjectSettings settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(CustomProjectSettings.CustomSettingsPath.Replace("<<name>>", CustomProjectSettings.RemoveConfiguration(typeof(T).Name)));

            if(settings is null)
                throw new Exception("configuration of this type does not exist");

            return settings.Context.Configuration as T;

            throw new Exception("configuration of this type does not exist");
        }

        /// <summary>
        /// set configuration of current class to value
        /// </summary>
        /// <typeparam name="T">type of configuration</typeparam>
        /// <param name="value"> configuration</param>
        /// <returns>true if this configuration exist</returns>
        public static bool TryGetConfiguration<T>(out T value) where T : class
        {
            value = default;

            if (!IsInitialized)
                return false;

            CustomProjectSettings settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(CustomProjectSettings.CustomSettingsPath.Replace("<<name>>", CustomProjectSettings.RemoveConfiguration(typeof(T).Name)));

            if (settings is null)
                return false;

            value = settings.Context.Configuration as T;

            return false;
        }
        /// <summary>
        /// get configuration of current class
        /// throw exception if configuration of this class is not exist in GameInitializer
        /// </summary>
        /// <returns>configuration</returns>
        public static object GetConfiguration(Type type)
        {
            CustomProjectSettings settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(CustomProjectSettings.CustomSettingsPath.Replace("<<name>>", CustomProjectSettings.RemoveConfiguration(type.Name)));

            if (settings is null)
                throw new Exception("configuration of this type does not exist");

            return settings.Context.Configuration;
        }
        /// <summary>
        /// set configuration of current class to value
        /// </summary>
        /// <param name="value"> configuration</param>
        /// <returns>true if this configuration exist</returns>
        public static bool TryGetConfiguration(Type type, out object value)
        {
            value = default;

            CustomProjectSettings settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(CustomProjectSettings.CustomSettingsPath.Replace("<<name>>", CustomProjectSettings.RemoveConfiguration(type.Name)));

            if (settings is null)
                return false;

            value = settings.Context.Configuration;

            return false;
        }
    }
}
