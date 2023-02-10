using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameArchitecture;

public class SaveManager : ISaveManager
{
    private Dictionary<string,object> dictionary;

    public void Initialize() {
        dictionary = new Dictionary<string, object>();
    }

    public bool Set<T>(T element, string key) { return false; }
    public bool Get<T>(out T element, string key) { element = default; return false; }
    public void Save() { }
}
