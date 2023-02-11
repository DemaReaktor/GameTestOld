using System;

namespace GameArchitecture
{
    public interface IManager
    {
        /// <summary>
        /// this method will be invoked at awake if this manager is added to gameInitializer
        /// </summary>
        /// <param name="configuration"> this value set configuration to this manager</param>
        void Initialize();
    }
    public interface IManager<T> where T : class
    {
        T Configuration { get; }
        /// <summary>
        /// this method will be invoked at awake if this manager is added to gameInitializer
        /// </summary>
        /// <param name="configuration"> this value set configuration to this manager</param>
        void Initialize(T configuration);
    }
}
