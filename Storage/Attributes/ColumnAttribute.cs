using System;

namespace Storage.Attributes
{
    internal class ColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public ColumnAttribute(string name)
        {
            this.ColumnName = name;
        }
    }
}