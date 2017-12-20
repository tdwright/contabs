using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConTabs.Exceptions;

namespace ConTabs
{
    public class TableBuilder<T> where T : class
    {
        private IEnumerable<T> _data;
        private IEnumerable<PropertyInfo> _propertyInfo;
        private readonly Table<T> _table;

        private TableBuilder(IEnumerable<T> data, IEnumerable<PropertyInfo> propertyInfo, Table<T> table)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _propertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            _table = table ?? throw new ArgumentNullException(nameof(table));
        }


        public static TableBuilder<T> Initialize(IEnumerable<T> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            
            var propertyInfo = typeof(T)
                .GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetMethod.IsPublic);
            return new TableBuilder<T>(data, propertyInfo, Table<T>.Create(data));
        }
        public TableBuilder<T> HideColumn(string name)
        {
            try
            {
                _table.Columns.First(c => c.PropertyName == name).Hide = true;
            }
            catch 
            {
                throw new ColumnNotFoundException(name);
            }
            return this;
        }

        public Table<T> Build() => _table;
    }
}