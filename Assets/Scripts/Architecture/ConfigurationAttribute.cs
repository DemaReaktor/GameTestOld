using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace GameArchitecture
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationAttribute : Attribute
    {
        private Type Type;
        public ConfigurationAttribute(Type type) {
            if (!type.GetInterfaces().Contains(typeof(IManager)))
                throw new ArgumentException("type should have interface IManager");
            Type = type;
        }
        public Type ManagerType => Type;
    } }
