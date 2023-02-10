using System.Collections.Generic;

namespace GameArchitecture
{
    public interface IManager
    {
        /// <summary>
        /// this method will be invoked at awake if this manager is added to gameInitializer
        /// </summary>
        /// <param name="value"> this dictionary set setting to this manager</param>
        void Initialize(Dictionary<string,object> value);
    }
}
