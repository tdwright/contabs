using System;
using ConTabs;

namespace ConTabs.Attributes
{
    public interface IConTabsColumnAttribute
    {
        void ActOnColumn(Column column);
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ConTabsColumnHide : Attribute, IConTabsColumnAttribute
    {
        public void ActOnColumn(Column column)
        {
            column.Hide = true;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ConTabsColumnName : Attribute, IConTabsColumnAttribute
    {
        public string ColumnName { get; }

        public ConTabsColumnName(string name)
        {
            ColumnName = name;
        }

        public void ActOnColumn(Column column)
        {
            column.ColumnName = ColumnName;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ConTabsColumnPosition : Attribute, IConTabsColumnAttribute
    {
        public int ColumnPosition { get; }

        public ConTabsColumnPosition(int position)
        {
            ColumnPosition = position;
        }

        public void ActOnColumn(Column column)
        {
            column.InitialPosition = ColumnPosition;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ConTabsColumnFormatString : Attribute, IConTabsColumnAttribute
    {
        public string FormatString { get; }

        public ConTabsColumnFormatString(string formatString)
        {
            FormatString = formatString;
        }

        public void ActOnColumn(Column column)
        {
            column.FormatString = FormatString;
        }
    }
}
