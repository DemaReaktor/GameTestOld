using System.Collections.Generic;
using System;

public class SaveManager : ISaveManager
{
    private Dictionary<string, string> dictionary;

    public void Initialize()
    {
        dictionary = new Dictionary<string, string>();
    }

    public void Set<T>(T element, string key) => dictionary[key] = element.ToString();

    public bool TryGet<T>(out T element, string key) where T : class
    {
        try
        {
            element = Convert.ChangeType(dictionary[key], typeof(T)) as T;
        }
        catch
        {
            element = null;
            return false;
        }

        return true;
    }
    public void Save() { }
}
