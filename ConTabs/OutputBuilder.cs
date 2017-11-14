using System;
using System.Linq;
using System.Text;

namespace ConTabs
{
    internal class OutputBuilder<T> where T:class
    {
        private const char Vertex = '+';
        private const char HLineChar = '-';
        private const char VLineChar = '|';

        private StringBuilder sb;
        private Table<T> table;

        public static string BuildOutput(Table<T> t)
        {
            var instance = new OutputBuilder<T>(t);
            return instance.sb.ToString();
        }

        private OutputBuilder(Table<T> t)
        {
            table = t;
            sb = new StringBuilder();
            HLine(); NewLine();
            Headers(); NewLine();
            HLine(); NewLine();
            if (table.Data == null || table.Data.Count() == 0)
            {
                NoDataLine(); NewLine();
            }
            else
            {
                for (int i = 0; i < table.Data.Count(); i++)
                {
                    DataLine(i); NewLine();
                }
            }
            HLine();
        }

        private void NewLine()
        {
            sb.Append(Environment.NewLine);
        }

        private void HLine()
        {
            int colWidths = table._colsShown.Sum(c => c.MaxWidth);
            int innerWidth = colWidths + (3 * table._colsShown.Count) - 1;
            sb.Append(Vertex + new String(HLineChar, innerWidth) + Vertex);
        }

        private void NoDataLine()
        {
            var noDataText = "no data";
            int colWidths = table._colsShown.Sum(c => c.MaxWidth);
            int innerWidth = colWidths + (3 * table._colsShown.Count) - 1;
            int leftPad = (innerWidth - noDataText.Length) / 2;
            int rightPad = innerWidth - (leftPad + noDataText.Length);
            sb.Append(VLineChar + new String(' ', leftPad) + noDataText + new string(' ', rightPad) + VLineChar);
        }

        private void Headers()
        {
            sb.Append(VLineChar);
            foreach (var col in table._colsShown)
            {
                sb.Append(" " + col.ColumnName + new string(' ', col.MaxWidth - col.ColumnName.Length) + " " + VLineChar);
            }
        }

        private void DataLine(int i)
        {
            sb.Append(VLineChar);
            foreach (var col in table._colsShown)
            {
                var value = col.Values[i].ToString();
                sb.Append(" " + value + new string(' ', col.MaxWidth - value.Length) + " " + VLineChar);
            }
        }
    }
}
