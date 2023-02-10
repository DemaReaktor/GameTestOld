using System.Collections.Generic;
using System;
using System.IO;
using System.Xml.Serialization;

public class SaveManager : ISaveManager
{
    private Dictionary<string, string> dictionary;
    private Dictionary<string, string> LoadDictionary;

    public void Initialize(Dictionary<string,object> Configuration)
    {
        dictionary  = new Dictionary<string, string>();
        LoadDictionary = new Dictionary<string, string>();
    }

    public void Set<T>(T element, string key) => dictionary[key] = element.ToString();

    public bool TryGet<T>(out T element, string key) where T : class
    {
        element = null;

        if (!LoadDictionary.ContainsKey(key))
            return false;

        try
        {
            element = Convert.ChangeType(LoadDictionary[key], typeof(T)) as T;
        }
        catch
        {
            return false;
        }

        return true;
    }
    public bool TryGet(out string element, string key) 
    {
        element = null;

        if (!LoadDictionary.ContainsKey(key))
            return false;

        element = LoadDictionary[key];
        return true;
    }
    public void Save() {
        LoadDictionary = new Dictionary<string, string>();

        foreach (var element in dictionary)
            LoadDictionary[element.Key] = element.Value;

        new XmlSerializer(typeof(Dictionary<string, string>)).Serialize(new FileStream("Settings.xml", FileMode.Create, FileAccess.Write),dictionary);
    }
}
