using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConTabs.Exceptions;

namespace ConTabs
{
    public partial class Table<T> where T:class
    {
        public List<Column> Columns { get; set; }
        internal List<Column> _colsShown => Columns.Where(c => !c.Hide).ToList();
        public Style TableStyle { get; set; }

        private IEnumerable<T> _data;
        public IEnumerable<T> Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                foreach (var col in Columns)
                {
                    col.Values = _data.Select(d =>
                    {
                        Type t = typeof(T);
                        PropertyInfo p = t.GetRuntimeProperty(col.PropertyName);
                        return p.GetValue(d);
                    }).ToList();
                }
            }
        }

        public static Table<T> Create()
        {
            return new Table<T>();
        }

        public static Table<T> Create(IEnumerable<T> Source)
        {
            return new Table<T>()
            {
                Data = Source
            };
        }

        private Table()
        {
            TableStyle = Style.Default;
            var props = typeof(T).GetTypeInfo().DeclaredProperties;
            Columns = props
                .Where(p => p.GetMethod.IsPublic)
                .Select(p => new Column(p.PropertyType, p.Name))
                .ToList();
            
            if (!Columns.Any()) throw new PublicPropertiesNotFoundException();
        }

        public override string ToString()
        {
            return OutputBuilder<T>.BuildOutput(this, TableStyle);
        }
    }
}
