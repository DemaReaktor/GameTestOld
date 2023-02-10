namespace GameArchitecture
{
    public interface IManager
    {
        /// <summary>
        /// this method will be invoked at awake if this manager is added to gameInitializer
        /// </summary>
        void Initialize();
    }
}
