using System;

namespace DataBusinessService.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AutoCommand : Attribute
    {
        private readonly string _name;

        public AutoCommand(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}