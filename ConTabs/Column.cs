using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using ConTabs.Attributes;

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
        internal int? InitialPosition;

        /// <summary>
        /// A List of the values stored
        /// </summary>
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

        /// <summary>
        /// Constructor used by the main table creation process, using reflection
        /// </summary>
        /// <param name="propertyInfo">Information reflected from the public property of a Table</param>
        public Column(PropertyInfo propertyInfo)
        {
            LongStringBehaviour = LongStringBehaviour.Default;
            Alignment           = Alignment.Default;
            SourceType          = propertyInfo.PropertyType;
            PropertyName        = propertyInfo.Name;
            ColumnName          = propertyInfo.Name;
            toStringMethod      = GetToStringMethod();

            // check for each of the attributes and act accordingly
            var attributes = propertyInfo.GetCustomAttributes();
            foreach (var attribute in attributes)
            {
                if (attribute is IConTabsColumnAttribute castedAttribute) castedAttribute.ActOnColumn(this);
            }
        }

        /// <summary>
        /// Constructor used when adding additional columns to a table
        /// </summary>
        /// <param name="type">The Type of the data in the column</param>
        /// <param name="name">A name for the column (shown in the header row)</param>
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