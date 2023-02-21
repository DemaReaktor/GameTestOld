using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace GameArchitecture.Save
{
    public class SettingsManager : ISaveManager<SettingsConfiguration>
    {
        /// <summary>
        /// this class is needed to set KeyValuePair into xml files
        /// </summary>
        public class Pair
        {
            public string Key;
            public string Value;

            public static implicit operator KeyValuePair<string, string>(Pair value) => new KeyValuePair<string, string>(value.Key, value.Value);
            public static explicit operator Pair(KeyValuePair<string, string> value) => new Pair() { Key = value.Key, Value = value.Value };
        }

        public SettingsConfiguration Configuration { get; private set; }
        //this dictionary has not saved data
        private Dictionary<string, string> dictionary;
        //this dictionary has data in file
        private Dictionary<string, string> LoadDictionary;

        public void Initialize(SettingsConfiguration configuration)
        {
            //in order file could be created It should have format xml
            if (!configuration.FileName.EndsWith(".xml"))
                configuration.FileName += ".xml";
            if (!configuration.DefaultFileName.EndsWith(".xml"))
                configuration.DefaultFileName += ".xml";

            Configuration = configuration;
            dictionary = new Dictionary<string, string>();
            LoadDictionary = new Dictionary<string, string>();

            //if file exist we will set data to dictionary
            if (File.Exists(Configuration.FileName))
                using (var xml = new FileStream(Configuration.FileName, FileMode.Open, FileAccess.Read))
                    (new XmlSerializer(typeof(Pair[])).Deserialize(xml) as Pair[]).ToList().ForEach(pair => LoadDictionary[pair.Key] = pair.Value);
            else
                if (File.Exists(Configuration.DefaultFileName))
                using (var xml = new FileStream(Configuration.DefaultFileName, FileMode.Open, FileAccess.Read))
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

            //change type from dictionary to list and then to array(below)
            //xml file can save only array
            LinkedList<Pair> list = new LinkedList<Pair>();
            LoadDictionary.ToList().ForEach(keyValuePair => list.AddLast((Pair)keyValuePair));

            //set data to file
            using (var xml = new FileStream(Configuration.FileName, FileMode.Create, FileAccess.Write))
                new XmlSerializer(typeof(Pair[])).Serialize(xml, list.ToArray());
        }
    }
}