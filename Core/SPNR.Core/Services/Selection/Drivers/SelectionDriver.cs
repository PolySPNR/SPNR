using System;

namespace SPNR.Core.Services.Selection.Drivers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SelectionDriver : Attribute
    {
        public SelectionDriver(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}