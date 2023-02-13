using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class SaveManager : ISaveManager<SaveConfiguration>
{
    public class Pair
    {
        public string Key;
        public string Value;

        public static implicit operator KeyValuePair<string,string>(Pair value) => new KeyValuePair<string, string>(value.Key,value.Value);
        public static explicit operator Pair(KeyValuePair<string,string> value) => new Pair() { Key = value.Key, Value = value.Value };
    }

    public SaveConfiguration Configuration { get; private set; }
    private Dictionary<string, string> dictionary;
    private Dictionary<string, string> LoadDictionary;

    public void Initialize(SaveConfiguration configuration)
    {
        if (!configuration.FileName.EndsWith(".xml"))
            configuration.FileName += ".xml";

        Configuration = configuration;
        dictionary = new Dictionary<string, string>();
        LoadDictionary = new Dictionary<string, string>();

        if (File.Exists(Configuration.FileName))
            using (var xml = new FileStream(Configuration.FileName, FileMode.Open, FileAccess.Read))
                (new XmlSerializer(typeof(Pair[])).Deserialize(xml) as Pair[]).ToList().ForEach(pair => LoadDictionary[pair.Key] = pair.Value);

        foreach (var element in LoadDictionary)
            dictionary[element.Key] = element.Value;

        Save();
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
    public void Save()
    {
        LoadDictionary = new Dictionary<string, string>();

        foreach (var element in dictionary)
            LoadDictionary[element.Key] = element.Value;

        LinkedList<Pair> list = new LinkedList<Pair>();
        LoadDictionary.ToList().ForEach(keyValuePair => list.AddLast((Pair)keyValuePair));

        using (var xml = new FileStream(Configuration.FileName, FileMode.Create, FileAccess.Write))
            new XmlSerializer(typeof(Pair[])).Serialize(xml, list.ToArray());
    }
}
