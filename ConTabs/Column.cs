using System;

namespace ConTabs
{
    public class Column
    {
        public Type SourceType { get; set; }
        public string ColumnName { get; set; }
        public string FormatString { get; set; }
        public bool Hide { get; set; }
        
        public Column(Type type, string name)
        {
            SourceType = type;
            ColumnName = name;
        }
    }
}