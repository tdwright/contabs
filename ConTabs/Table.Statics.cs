using System.Collections.Generic;

namespace ConTabs
{
    public static class Table
    {
        public static Table<TResult> Create<TResult>(IEnumerable<TResult> source) where TResult : class
        {
            return Table<TResult>.Create(source);
        }

        public static Table<TResult> AsTable<TResult>(this IEnumerable<TResult> source) where TResult : class
        {
            return Table<TResult>.Create(source);
        }
    }
}
