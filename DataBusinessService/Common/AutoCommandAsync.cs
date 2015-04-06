using System;

namespace DataBusinessService.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AutoCommandAsync : Attribute
    {
        private readonly string _name;

        public AutoCommandAsync(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}