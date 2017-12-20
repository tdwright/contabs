using System;

namespace ConTabs.Exceptions
{
    public class ColumnNotFoundException : Exception
    {
        public ColumnNotFoundException(string columnName)
        {
            _columnName = columnName;
        }

        public override string Message => 
            $"Column with name '{_columnName}' was not found in Table<T>. Please check your spelling.";
        
        private readonly string _columnName;
    }
}