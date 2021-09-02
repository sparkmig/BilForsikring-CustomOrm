using System;

namespace Storage.Attributes
{
    internal class TableAttribute : Attribute
    {
        public string TableName;
        public TableAttribute(string tablename)
        {
            this.TableName = tablename;
        }
    }
}