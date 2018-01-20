using ConTabs.Exceptions;
using System.Collections.Generic;

namespace ConTabs
{
    public class Columns : List<Column>
    {
        public Columns(List<Column> columns)
        {
            AddRange(columns);
        }

        new public Column this[int index]
        {
            get
            {
                try
                {
                    return base[index];
                }
                catch
                {
                    throw new ColumnNotFoundException(index);
                }
            }
        }

        public Column this[string name]
        {
            get
            {
                return FindColByName(name);
            }
        }

        private Column FindColByName(string name)
        {
            Column backup = null;
            foreach (var col in this)
            {
                if (col.PropertyName == name) return col;
                if (col.ColumnName == name) backup = col;
            }
            if (backup != null) return backup;
            throw new ColumnNotFoundException(name);
        }
    }
}
