using System.Collections.Generic;

namespace ConTabs
{
    public class Table<T> where T:class
    {
        string acc;

        public static Table<T> Create()
        {
            return new Table<T>();
        }

        public static Table<T> Create(IEnumerable<T> Source)
        {
            return new Table<T>();
        }

        private Table()
        {
            acc = typeof(T).Name;
        }

        public override string ToString()
        {
            return acc;
        }
    }
}
