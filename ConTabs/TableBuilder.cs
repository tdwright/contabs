using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConTabs
{
    public class TableBuilder<T> where T : class
    {
        private IEnumerable<T> _data;
        private IEnumerable<PropertyInfo> _propertyInfo;
        private Table<T> _table;

        private TableBuilder(IEnumerable<T> data, IEnumerable<PropertyInfo> propertyInfo, Table<T> table)
        {
            _data = data;
            _propertyInfo = propertyInfo;
            _table = table;
        }


        public static TableBuilder<T> Initialize(IEnumerable<T> data)
        {
            var propertyInfo = typeof(T)
                .GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetMethod.IsPublic);
            return new TableBuilder<T>(data, propertyInfo, Table<T>.Create(data));
        }
        public TableBuilder<T> HideColumn(string name)
        {
            var columns = _table.Columns.Where(c => c.PropertyName == name).ToList();
            foreach (var column in columns)
            {
                column.Hide = true;
            }
            return this;
        }

        public Table<T> Build() => _table;
    }
}