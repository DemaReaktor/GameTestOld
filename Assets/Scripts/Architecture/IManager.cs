using System.Collections.Generic;

namespace GameArchitecture
{
    public interface IManager
    {
        Configuration Configuration { get; }
        /// <summary>
        /// this method will be invoked at awake if this manager is added to gameInitializer
        /// </summary>
        /// <param name="configuration"> this value set configuration to this manager</param>
        void Initialize(Configuration configuration);
    }
}
