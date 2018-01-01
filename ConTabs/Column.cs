using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ConTabs
{
    [DebuggerDisplay("Column for '{PropertyName}'")]
    public class Column
    {
        public Type SourceType { get; set; }
        public string PropertyName { get; private set; }
        public string ColumnName { get; set; }
        public string FormatString { get; set; }
        public bool Hide { get; set; }
        public LongStringBehaviour LongStringBehaviour { get; set; }

        public List<Object> Values { get; set; }
        public int MaxWidth
        {
            get
            {
                if (Values == null || Values.Count() == 0) return ColumnName.Length;

                if (LongStringBehaviour.Width > 0) return LongStringBehaviour.Width;

                return Values
                .Select(v => StringValForCol(v))
                .Union(new List<string> { ColumnName })
                .Select(v => v.Length)
                .Max();
            }
        }

        public Column(Type type, string name)
        {
            LongStringBehaviour = LongStringBehaviour.Default;
            SourceType = type;
            PropertyName = name;
            ColumnName = name;
        }

        public string StringValForCol(Object o)
        {
            var casted = Convert.ChangeType(o, SourceType);
            if (casted is string)
            {
                return LongStringBehaviour.ProcessString(casted as string);
            }
            else
            {
                var ToStringMethod = SourceType.GetTypeInfo().DeclaredMethods.FirstOrDefault(m =>
                    m.Name == "ToString" &&
                    m.IsPublic &&
                    m.ReturnType == typeof(string) &&
                    m.GetParameters().Count() == 1 &&
                    m.GetParameters()[0].ParameterType == typeof(string));
                if (ToStringMethod == null)
                {
                    return casted.ToString();
                }
                else
                {
                    return (string)ToStringMethod.Invoke(o, new object[] { FormatString });
                }
            }
        }
    }
}