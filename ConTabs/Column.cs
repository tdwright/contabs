using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ConTabs
{
    [DebuggerDisplay("Column for '{PropertyName}'")]

    /// <summary>
    /// Acts as a column within a table
    /// </summary>
    public class Column
    {
        /// <summary>
        /// The defined type of the given property
        /// </summary>
        public Type SourceType { get; set; }

        /// <summary>
        /// The column header, by default is the name of the associated property
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// The name of the selected column
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// The provided formatting to display the cell's value
        /// </summary>
        public string FormatString { get; set; }

        /// <summary>
        /// A control to show/hide the column
        /// </summary>
        public bool Hide { get; set; }

        /// <summary>
        /// The method the column uses to display long strings
        /// </summary>
        public LongStringBehaviour LongStringBehaviour { get; set; }

        /// <summary>
        /// The columns alignment 
        /// </summary>
        public Alignment Alignment { get; set; }

        private readonly MethodInfo toStringMethod;

        /// <summary>
        /// A List of the values stored
        /// </summary>
        public List<Object> Values { get; set; }

        public Column(Type type, string name)
        {
            LongStringBehaviour = LongStringBehaviour.Default;
            Alignment           = Alignment.Default;
            SourceType          = type;
            PropertyName        = name;
            ColumnName          = name;
            toStringMethod      = GetToStringMethod();
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
                if (toStringMethod == null)
                {
                    return (casted ?? string.Empty).ToString();
                }
                else
                {
                    return (string)toStringMethod.Invoke(o, new object[] { FormatString });
                }
            }
        }

        private MethodInfo GetToStringMethod()
        {
            return SourceType.GetTypeInfo().DeclaredMethods.FirstOrDefault(m =>
                m.Name == "ToString" &&
                m.IsPublic &&
                m.ReturnType == typeof(string) &&
                m.GetParameters().Count() == 1 &&
                m.GetParameters()[0].ParameterType == typeof(string));
        }
    }
}