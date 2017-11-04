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
        private List<Column> _colsShown => Columns.Where(c => !c.Hide).ToList();

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
            sb.Append(HLine() + Environment.NewLine);
            if(Data == null || Data.Count() == 0)
            {
                sb.Append(NoDataLine() + Environment.NewLine);
            }
            else
            {
                for(int i =0; i<Data.Count();i++)
                {
                    sb.Append(DataLine(i) + Environment.NewLine);
                }
            }
            sb.Append(HLine());
            return sb.ToString();
        }

        private string HLine()
        {
            int colWidths = _colsShown.Sum(c => c.MaxWidth);
            int innerWidth = colWidths + (3 * _colsShown.Count) - 1;
            return Vertex + new String(HLineChar, innerWidth) + Vertex;
        }

        private string NoDataLine()
        {
            var noDataText = "no data";
            int colWidths = _colsShown.Sum(c => c.MaxWidth);
            int innerWidth = colWidths + (3 * _colsShown.Count) - 1;
            int leftPad = (innerWidth - noDataText.Length) / 2;
            int rightPad = innerWidth - (leftPad + noDataText.Length);
            return VLineChar + new String(' ', leftPad) + noDataText + new string(' ',rightPad) + VLineChar;
        }

        private string Headers()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(VLineChar);
            foreach(var col in _colsShown)
            {
                sb.Append(" " + col.ColumnName + new string(' ', col.MaxWidth - col.ColumnName.Length) + " " + VLineChar);
            }
            return sb.ToString();
        }

        private string DataLine(int i)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(VLineChar);
            foreach (var col in _colsShown)
            {
                var value = col.Values[i].ToString();
                sb.Append(" " + value + new string(' ', col.MaxWidth - value.Length) + " " + VLineChar);
            }
            return sb.ToString();
        }
    }
}
