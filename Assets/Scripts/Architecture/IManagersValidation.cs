namespace GameArchitecture
{
    /// <summary>
    /// this interface is needed to check 
    /// if current characters are validated
    /// </summary>
    public interface IManagersValidation
    {
        /// <summary>
        /// check if current characters are validated
        /// </summary>
        /// <param name="characters">characters of managers which have type of manager and configuration</param>
        /// <returns>true if characters gone rules</returns>
        static bool Validate(ManagerCharacter[] characters) => true;
    }
}
