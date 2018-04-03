using System;

namespace ConTabs.Exceptions
{
    public class EmptyTableException : Exception
    {
        private readonly Type _type;

        public EmptyTableException(Type t) =>_type = t.GenericTypeArguments[0];

        public override string Message =>
        $"The Table<{_type.Name}> has no visible columns. Check column's Hide properties.";

    }
}