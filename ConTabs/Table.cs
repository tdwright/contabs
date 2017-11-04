using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConTabs
{
    public class Table<T> where T:class
    {
        public List<Column> Columns { get; set; }

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
                    });
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
            var props = typeof(T).GetTypeInfo().DeclaredProperties;
            Columns = props
                .Where(p => p.GetMethod.IsPublic)
                .Select(p => new Column(p.PropertyType, p.Name))
                .ToList();
        }

        private char Vertex = '+';
        private char HLineChar = '-';
        private char VLineChar = '|';

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(HLine() + Environment.NewLine);
            sb.Append(Headers() + Environment.NewLine);
            sb.Append(HLine());
            return sb.ToString();
        }

        private string HLine()
        {
            int colWidths = Columns.Sum(c => c.MaxWidth);
            int innerWidth = colWidths + (3 * Columns.Count) - 1;
            return Vertex + new String(HLineChar, innerWidth) + Vertex;
        }

        private string Headers()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(VLineChar);
            foreach(var col in Columns)
            {
                sb.Append(" " + col.ColumnName + new string(' ', col.MaxWidth - col.ColumnName.Length) + " " + VLineChar);
            }
            return sb.ToString();
        }
    }
}
