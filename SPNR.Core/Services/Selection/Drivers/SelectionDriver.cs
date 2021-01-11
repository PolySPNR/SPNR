using System;

namespace SPNR.Core.Services.Selection.Drivers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SelectionDriver : Attribute
    {
        public string Name { get; }

        public SelectionDriver(string name)
        {
            Name = name;
        }
    }
}