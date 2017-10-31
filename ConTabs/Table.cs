using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConTabs
{
    public class Table<T> where T:class
    {
        string acc;

        public List<Column> Columns { get; set; }

        public static Table<T> Create()
        {
            return new Table<T>();
        }

        public static Table<T> Create(IEnumerable<T> Source)
        {
            return new Table<T>();
        }

        private Table()
        {
            acc = typeof(T).Name;
            var props = typeof(T).GetTypeInfo().DeclaredProperties;
            Columns = props
                .Where(p => p.GetMethod.IsPublic)
                .Select(p => new Column(p.PropertyType, p.Name))
                .ToList();
        }

        public override string ToString()
        {
            return acc;
        }
    }
}
