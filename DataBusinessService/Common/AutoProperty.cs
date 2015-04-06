using System;

namespace DataBusinessService.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AutoProperty : Attribute
    {
        public AutoProperty(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public Type Type { get; private set; }
        public string Name { get; private set; }
    }
}