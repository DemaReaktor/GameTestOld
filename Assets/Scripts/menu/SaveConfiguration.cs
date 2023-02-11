using System;
using UnityEngine;
using GameArchitecture;

[Serializable]
[Configuration(typeof(SaveManager))]
public class SaveConfiguration : Configuration
{
    [SerializeField]
    public string Value;
}
