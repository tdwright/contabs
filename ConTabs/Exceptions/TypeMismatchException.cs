using System;

namespace ConTabs.Exceptions
{
    public class TypeMismatchException : Exception
    {
        public string Column { get; private set; }
        public Type ExpectedType { get; private set; }
        public Type ActualType { get; private set; }

        public TypeMismatchException(string colName, Type expected, Type actual)
        {
            Column = colName;
            ExpectedType = expected;
            ActualType = actual;
        }

        public override string Message => $@"Computed column expected type '{ExpectedType.Name}', but column '{Column}' is of type '{ActualType.Name}'.";
    }
}