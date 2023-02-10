using GameArchitecture;

/// <summary>
/// save and load different things
/// </summary>
public interface ISaveManager : IManager
{
    /// <summary>
    /// set element to other elements
    /// </summary>
    /// <param name="element">element that is added</param>
    /// <param name="key">name of this element</param>
    public void Set<T>(T element,string key);
    /// <summary>
    /// load element
    /// </summary>
    /// <param name="element">element that should be load</param>
    /// <param name="key">name of this element</param>
    /// <returns>true if loading is successfull</returns>
    public bool TryGet<T>(out T element, string key) where T : class;
    public void Save();
}
