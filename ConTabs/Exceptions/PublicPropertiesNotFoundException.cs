using System;

namespace ConTabs.Exceptions
{
    public class PublicPropertiesNotFoundException : Exception
    {
        public override string Message =>
            "On Table<T> creation, no valid properties were identified. Check access modifiers.";
    }
}