using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace GameArchitecture
{
    /// <summary>
    /// main class that has all managers
    /// </summary>
    public static class Game
    {
        private static HashSet<object> managers;

        /// <summary>
        /// true if Game is ready to be used
        /// </summary>
        public static bool IsInitialized { get; private set; } = false;
        /// <summary>
        /// when Game is ready to be used this will be invoked
        /// </summary>
        public static event Action OnInitializeFinish;

        internal static void Initialize(HashSet<object> managers)
        {
            Game.managers = managers;

            //initialize all managers
            foreach(var manager in managers)
                manager.GetType().GetMethod("Initialize").Invoke(manager, new object[] { Game.GetConfiguration(manager.GetType().GetInterfaces().First(
                                t => t.FullName.Contains('`') && t.FullName.Substring(0, t.FullName.IndexOf('`')) == "GameArchitecture.IManager").GenericTypeArguments[0]) });

            IsInitialized = true;
            OnInitializeFinish?.Invoke();
        }
        /// <summary>
        /// Allow initialize managers after Game has initialized.
        /// </summary>
        /// <typeparam name="T">configuration</typeparam>
        /// <param name="manager">manager</param>
        public static void Initialize<T>(IManager<T> manager) where T : class
        {
            if (managers.Any(m => m.GetType() == manager.GetType()))
            {
                Debug.LogError("this manager already exist");
                return;
            }
            if (!Game.TryGetConfiguration(out T configuration))
            {
                Debug.LogError("Configuration of this manager is not registered");
                return;
            }

            manager.Initialize(configuration);
        }
        /// <summary>
        /// get manager of current class
        /// throw exception if manager of this class is not exist in GameInitializer
        /// </summary>
        /// <typeparam name="T">type of manager that is needed</typeparam>
        /// <returns>manager</returns>
        public static T GetManager<T>() where T : class
        {
            if (!IsInitialized)
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
        public static bool TryGetManager<T>(out T value) where T : class
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
            CustomProjectSettings settings = Resources.Load<CustomProjectSettings>(GeneralConfiguration.GetAssetName(typeof(T)));

            if (settings is null)
                throw new Exception("configuration of this type does not exist");

            return settings.Context.Configuration as T;
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
            CustomProjectSettings settings = Resources.Load<CustomProjectSettings>(GeneralConfiguration.GetAssetName(typeof(T)));

            if (settings is null)
                return false;

            value = settings.Context.Configuration as T;
            return true;
        }
        /// <summary>
        /// get configuration of current class
        /// throw exception if configuration of this class is not exist in GameInitializer
        /// </summary>
        /// <returns>configuration</returns>
        public static object GetConfiguration(Type type)
        {
            CustomProjectSettings settings = Resources.Load<CustomProjectSettings>(GeneralConfiguration.GetAssetName(type));

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
            CustomProjectSettings settings = Resources.Load<CustomProjectSettings>(GeneralConfiguration.GetAssetName(type));

            if (settings is null)
                return false;

            value = settings.Context.Configuration;
            return true;
        }
    }
}
