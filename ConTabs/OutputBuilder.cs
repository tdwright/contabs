using System;
using System.Linq;
using System.Text;

namespace ConTabs
{
    internal class OutputBuilder<T> where T:class
    {
        private StringBuilder sb;
        private Table<T> table;
        private Style style;

        public static string BuildOutput(Table<T> t, Style s)
        {
            var instance = new OutputBuilder<T>(t, s);
            return instance.sb.ToString();
        }

        private OutputBuilder(Table<T> t, Style s)
        {
            table = t;
            style = s;
            sb = new StringBuilder();
            HLine(TopMidBot.Top); NewLine();
            Headers(); NewLine();
            HLine(TopMidBot.Mid); NewLine();
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
            HLine(TopMidBot.Bot);
        }

        private void NewLine()
        {
            sb.Append(Environment.NewLine);
        }

        private void HLine(TopMidBot v)
        {
            sb.Append(GetCorner(v, LeftCentreRight.Left));
            for (int i = 0; i < table._colsShown.Count; i++)
            {
                sb.Append(new string(style.Floor, table._colsShown[i].MaxWidth + 2));
                if (i < table._colsShown.Count - 1) sb.Append(GetCorner(v, LeftCentreRight.Centre));
            }
            sb.Append(GetCorner(v, LeftCentreRight.Right));
        }

        private void NoDataLine()
        {
            var noDataText = "no data";
            int colWidths = table._colsShown.Sum(c => c.MaxWidth);
            int innerWidth = colWidths + (3 * table._colsShown.Count) - 1;
            int leftPad = (innerWidth - noDataText.Length) / 2;
            int rightPad = innerWidth - (leftPad + noDataText.Length);
            sb.Append(style.Wall + new String(' ', leftPad) + noDataText + new string(' ', rightPad) + style.Wall);
        }

        private void Headers()
        {
            sb.Append(style.Wall);
            foreach (var col in table._colsShown)
            {
                sb.Append(" " + col.ColumnName + new string(' ', col.MaxWidth - col.ColumnName.Length) + " " + style.Wall);
            }
        }

        private void DataLine(int i)
        {
            sb.Append(style.Wall);
            foreach (var col in table._colsShown)
            {
                var value = col.Values[i].ToString();
                sb.Append(" " + value + new string(' ', col.MaxWidth - value.Length) + " " + style.Wall);
            }
        }

        private enum TopMidBot
        {
            Top,
            Mid,
            Bot
        }

        private enum LeftCentreRight
        {
            Left,
            Centre,
            Right
        }

        private char GetCorner(TopMidBot v, LeftCentreRight h)
        {
            return style.Corners[(int)h, (int)v];
        }
    }
}
