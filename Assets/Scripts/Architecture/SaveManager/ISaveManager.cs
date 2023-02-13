namespace GameArchitecture.Save
{
    /// <summary>
    /// save and load different things
    /// </summary>
    public interface ISaveManager<T> : IManager<T>
        where T : class
    {
        /// <summary>
        /// set element to other elements
        /// </summary>
        /// <param name="element">element that is added</param>
        /// <param name="key">name of this element</param>
        public void Set<M>(M element, string key);
        /// <summary>
        /// load element as class
        /// </summary>
        /// <param name="element">element that should be load</param>
        /// <param name="key">name of this element</param>
        /// <returns>true if loading is successfull</returns>
        public bool TryGet<M>(out M element, string key) where M : class;
        /// <summary>
        /// load string
        /// </summary>
        /// <param name="element">element that should be load</param>
        /// <param name="key">name of this element</param>
        /// <returns>true if loading is successfull</returns>
        public bool TryGet(out string element, string key);
        public void Save();
    }
}