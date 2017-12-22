using System;
using System.Collections.Generic;
using System.Linq;
using ConTabs.Exceptions;

namespace ConTabs
{
    public class TableBuilder<T> where T : class
    {
        private readonly Table<T> _table;
        private IEnumerable<T> _data;

        private TableBuilder(IEnumerable<T> data, Table<T> table)
        {
            _data = data;
            _table = table ?? throw new ArgumentNullException(nameof(table));
        }


        public static TableBuilder<T> Initialize(IEnumerable<T> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return new TableBuilder<T>(data, Table<T>.Create(data));
        }
        public TableBuilder<T> HideColumn(string columnName)
        {
            try
            {
                _table.Columns.First(c => c.PropertyName == columnName).Hide = true;
            }
            catch 
            {
                throw new ColumnNotFoundException(columnName);
            }
            return this;
        }

        public TableBuilder<T> SetColumnIndex(string columnName, int newIndex)
        {
            if (newIndex > _table.Columns.Count) throw new ArgumentOutOfRangeException(nameof(newIndex));

            var targetColumn = GetColumnByName(columnName);
            var targetColumnOriginIndex = GetColumnByObject(targetColumn);
            var tmpColumn = GetColumnByIndex(newIndex);
            
            _table.Columns[newIndex] = targetColumn;
            _table.Columns[targetColumnOriginIndex] = tmpColumn;
            
            return this;
        }

        private int GetColumnByObject(Column targetColumn)
        {
            return _table.Columns.IndexOf(targetColumn);
        }

        private Column GetColumnByIndex(int newIndex)
        {
            return _table.Columns[newIndex];
        }

        private Column GetColumnByName(string name)
        {
            return _table.Columns.First(c => c.PropertyName == name);
        }

        public Table<T> Build() => _table;
    }
}