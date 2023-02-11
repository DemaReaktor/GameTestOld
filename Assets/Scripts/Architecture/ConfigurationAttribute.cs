using System;

namespace GameArchitecture
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class ConfigurationAttribute : Attribute
    {
        private Type Type;
        public ConfigurationAttribute(Type type) {
            Type = type;
        }
        public Type ManagerType => Type;
    }
}
