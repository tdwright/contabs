using System;
using System.Collections.Generic;
using System.Linq;

namespace ConTabs
{
    public class Column
    {
        public Type SourceType { get; set; }
        public string PropertyName { get; private set; }
        public string ColumnName { get; set; }
        public string FormatString { get; set; }
        public bool Hide { get; set; }
        
        public List<Object> Values { get; set; }
        public int MaxWidth => (Values == null || Values.Count() == 0 )
            ? ColumnName.Length
            : Values
                .Select(v => v.ToString())
                .Union(new List<string> { ColumnName })
                .Select(v => v.Length)
                .Max();
        
        public Column(Type type, string name)
        {
            SourceType = type;
            PropertyName = name;
            ColumnName = name;
        }
    }
}