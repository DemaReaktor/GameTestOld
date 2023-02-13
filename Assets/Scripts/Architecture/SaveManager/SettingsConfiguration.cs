using System;
using UnityEngine;

[Serializable]
public class SettingsConfiguration
{
    [Tooltip("name of file. Format of this file should be xml. Format is not needed to be written(It will be written automaticallly)")]
    public string FileName;
}
