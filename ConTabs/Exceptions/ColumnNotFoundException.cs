using System;

namespace ConTabs.Exceptions
{
    public class ColumnNotFoundException : Exception
    {
        private readonly bool _named = false;
        private readonly string _colName;
        private readonly int _index;

        public ColumnNotFoundException(int index)
        {
            _index = index;
        }

        public ColumnNotFoundException(string columnName)
        {
            _colName = columnName;
            _named = true;
        }

        public override string Message
        {
            get
            {
                if (_named)
                {
                    return $"Column with name '{_colName}' was not found.";
                }
                else
                {
                    return $"Column at index {_index} was not found.";
                }
            }
        }
    }
}