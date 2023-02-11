using UnityEngine;
using GameArchitecture;

[SelectionBase]
[Configuration(typeof(IManager))]
public class SaveConfiguration : Configuration
{
    public string Value;
}
