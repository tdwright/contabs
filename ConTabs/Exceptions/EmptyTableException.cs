using System;

namespace SandBox
{
    public class EmptyTableException : Exception
    {
        private readonly Type _type;

        public EmptyTableException(Object table)
        {
            _type = table.GetType().GetGenericArguments()[0];
        }

        public override string Message =>
        $"The Table<{_type.Name}> has no visible columns. Check column's Hide properties.";

    }
}