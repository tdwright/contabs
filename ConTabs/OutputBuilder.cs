using System;
using System.Linq;
using System.Text;

namespace ConTabs
{
    public partial class Table<T>
    {
        private sealed class OutputBuilder<T2> where T2 : class
        {
            private readonly StringBuilder sb;
            private readonly Table<T2> table;
            private readonly Style style;

            internal static string BuildOutput(Table<T2> t, Style s)
            {
                var instance = new OutputBuilder<T2>(t, s);
                return instance.sb.ToString();
            }

            private OutputBuilder(Table<T2> t, Style s)
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
                    var value = col.StringValForCol(col.Values[i]);
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
}
