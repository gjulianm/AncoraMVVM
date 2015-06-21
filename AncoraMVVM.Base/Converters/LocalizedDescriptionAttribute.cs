using System;

namespace AncoraMVVM.Base.Converters
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class LocalizedDescriptionAttribute : Attribute
    {
        private readonly string description;
        public string Description { get { return description; } }

        public LocalizedDescriptionAttribute(string description)
        {
            this.description = description;
        }
    }
}
