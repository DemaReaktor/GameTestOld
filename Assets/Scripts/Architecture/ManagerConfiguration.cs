using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameArchitecture
{
    [Serializable]
    public class ManagerConfiguration
    {
        public IManager Manager;
    }

    [Serializable]
    public class Pair
    {
        public string Key;
        public string Value;
    }


}
