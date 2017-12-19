using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConTabs.Exceptions;

namespace ConTabs
{
    public class Table<T> where T:class
    {
        private Table()
        {
            TableStyle = Style.Default;
            Columns = GetPublicPropertiesForColumns();
            
        }

        private static List<Column> GetPublicPropertiesForColumns()
        {
            var tableProperties = typeof(T).GetTypeInfo().DeclaredProperties;
            var propertyList = tableProperties
                .Where(p => p.GetMethod.IsPublic)
                .Select(p => new Column(p.PropertyType, p.Name))
                .ToList();
            
            if (!propertyList.Any()) throw new PublicPropertiesNotFoundException();
            
            return propertyList;
        }

        public List<Column> Columns { get; }
        public Style TableStyle { get; set; }
        
        public static Table<T> Create()
        {
            return new Table<T>();
        }
        
        public static Table<T> Create(IEnumerable<T> source)
        {
            return new Table<T>()
            {
                Data = source
            };
        }
        
        public override string ToString() => OutputBuilder<T>.BuildOutput(this, TableStyle);
        
        public IEnumerable<T> Data
        {
            get => _data;
            private set
            {
                _data = value;
                foreach (var col in Columns)
                {
                    col.Values = _data.Select(d =>
                    {
                        var t = typeof(T);
                        var p = t.GetRuntimeProperty(col.PropertyName);
                        return p.GetValue(d);
                    }).ToList();
                }
            }
        }
        
        private IEnumerable<T> _data;
        internal List<Column> _colsShown => Columns.Where(c => !c.Hide).ToList();
    }
}