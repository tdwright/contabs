using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConTabs
{
    public partial class Table<T>
    {
        /// <summary>
        /// Creates the string output of a table
        /// </summary>
        private sealed class OutputBuilder<T2> where T2 : class
        {
            private readonly StringBuilder sb;
            private readonly Table<T2> table;
            private readonly Style style;
            private readonly int leftOffset;

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

                int tableWidth = GetOptimalTableWidth();

                if (table.CanvasWidth > 0)
                {
                    leftOffset =
                        table.TableAlignment.Equals(Alignment.Right) ? table.CanvasWidth - tableWidth :
                        table.TableAlignment.Equals(Alignment.Center) ? (table.CanvasWidth - tableWidth) / 2 :
                        0;

                    sb.Append(new string(' ', leftOffset));
                }
                else leftOffset = 0;

                HLine(TopMidBot.Top);
                InsertVerticalPadding(table.Padding.Top, style.Wall); NewLine();
                Headers();
                InsertVerticalPadding(table.Padding.Bottom, style.Wall); NewLine();
                HLine(TopMidBot.Mid);

                if (table.Data == null || table.Data.Count() == 0)
                {
                    InsertVerticalPadding(table.Padding.Top, ' '); NewLine();
                    NoDataLine();
                    InsertVerticalPadding(table.Padding.Bottom, ' '); NewLine();
                }
                else
                {
                    for (int i = 0; i < table.Data.Count(); i++)
                    {
                        InsertVerticalPadding(table.Padding.Top, style.Wall); NewLine();
                        DataRow(i);
                    }
                    InsertVerticalPadding(table.Padding.Bottom, style.Wall); NewLine();
                }
                HLine(TopMidBot.Bot);

                //release suppressed 
                var suppressedCols = table.Columns.Where(c => c.Suppressed);
                foreach (var col in suppressedCols)
                {
                    col.Suppressed = false;
                }
            }

            private void InsertVerticalPadding(byte padding, char columnSeparator)
            {
                for (int paddingLevel = 0; paddingLevel < padding; paddingLevel++)
                {
                    NewLine();
                    sb.Append(style.Wall);
                    for (int i = 0; i < table.ColsShown.Count; i++)
                    {
                        sb.Append(new string(' ', table.ColsShown[i].LongStringBehaviour.DisplayWidth + table.Padding.GetHorizontalPadding()));

                        if (i < table.ColsShown.Count - 1)
                        {
                            sb.Append(columnSeparator);
                        }
                    }
                    sb.Append(style.Wall);
                }
            }

            private void NewLine()
            {
                sb.Append(Environment.NewLine);
                sb.Append(new string(' ', leftOffset));
            }

            private void HLine(TopMidBot v)
            {
                sb.Append(GetCorner(v, LeftCentreRight.Left));

                for (int i = 0; i < table.ColsShown.Count; i++)
                {
                    sb.Append(new string(style.Floor, table.ColsShown[i].LongStringBehaviour.DisplayWidth + table.Padding.GetHorizontalPadding()));

                    if (i < table.ColsShown.Count - 1)
                    {
                        sb.Append(GetCorner(v, LeftCentreRight.Centre));
                    }
                }
                sb.Append(GetCorner(v, LeftCentreRight.Right));
            }

            private void NoDataLine()
            {
                var noDataText = "no data";
                int colWidths = table.ColsShown.Sum(c => c.LongStringBehaviour.DisplayWidth);
                int innerWidth = colWidths + (table.ColsShown.Count * table.Padding.GetHorizontalPadding()) + table.ColsShown.Count - 1;
                int leftPad = (innerWidth - noDataText.Length) / 2;
                int rightPad = innerWidth - (leftPad + noDataText.Length);
                sb.Append(style.Wall + new String(' ', leftPad) + noDataText + new string(' ', rightPad) + style.Wall);
            }

            private void Headers()
            {
                sb.Append(style.Wall);
                foreach (var col in table.ColsShown)
                {
                    sb.Append(GetPaddingString(table.Padding.Left) + table.HeaderAlignment.ProcessString(col.ColumnName, col.LongStringBehaviour.DisplayWidth) + GetPaddingString(table.Padding.Right) + style.Wall);
                }
            }

            private void DataRow(int i)
            {
                var cols = table.ColsShown.Select(c => new CellParts(c.StringValForCol(c.Values[i]), c.LongStringBehaviour.DisplayWidth, c.Alignment)).ToList();

                var maxLines = cols.Max(c => c.LineCount);

                for (int j = 0; j < maxLines; j++)
                {
                    DataLine(cols, j);
                    if (j != maxLines - 1)
                    {
                        NewLine();
                    }
                }
            }

            private void DataLine(List<CellParts> parts, int line)
            {
                sb.Append(style.Wall);
                foreach (var part in parts)
                {
                    string val = part.GetLine(line);
                    sb.Append(GetPaddingString(table.Padding.Left)
                        + part.Alignment.ProcessString(val, part.ColMaxWidth)
                        + GetPaddingString(table.Padding.Right)
                        + style.Wall);
                }
            }

            private int GetWidthOfPaddingAndBorders()
            {
                return table.ColsShown.Count * table.Padding.GetHorizontalPadding() + table.ColsShown.Count + 1;
            }

            private int GetTableWidthAfterApplyingStretchStyles()
            {
                int colWidths = 0;
                foreach (Column column in table.ColsShown)
                {
                    int colWidth = table.TableStretchStyles.CalculateOptimalWidth(column, table.CanvasWidth);
                    column.LongStringBehaviour.DisplayWidth = colWidth;
                    colWidths += colWidth;
                }
                int realWidth = colWidths + GetWidthOfPaddingAndBorders();
                int adaptedWidth = table.TableStretchStyles.CalculateAdditionalWidth(table.ColsShown, realWidth, table.CanvasWidth);
                return adaptedWidth + GetWidthOfPaddingAndBorders();
            }

            private int GetOptimalTableWidth()
            {
                int tableWidth = GetTableWidthAfterApplyingStretchStyles();

                bool recalculationNeeded = false;

                while (table.CanvasWidth > 0 && table.CanvasWidth < tableWidth && table.ColsShown.Count > 0)
                {
                    var columnToSuppress = table.ColsShown[table.ColsShown.Count - 1];
                    tableWidth -= columnToSuppress.LongStringBehaviour.DisplayWidth + table.Padding.GetHorizontalPadding() + 1;
                    columnToSuppress.Suppressed = true;
                    recalculationNeeded = true;
                }

                if (recalculationNeeded) //if we hid some of the columns, we should adapt the display widths again
                {
                    tableWidth = GetTableWidthAfterApplyingStretchStyles();
                }
                return tableWidth;
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

            private sealed class CellParts
            {
                internal CellParts(string value, int width, Alignment alignment)
                {
                    _value = value;
                    ColMaxWidth = width;
                    Alignment = alignment;
                }

                internal int ColMaxWidth { get; private set; }
                internal Alignment Alignment { get; private set; }
                internal int LineCount => _lines.Length;

                internal string GetLine(int i)
                {
                    if (_lines.Length > i) return _lines[i];
                    return String.Empty;
                }

                private string _value { get; set; }
                private string[] _lines => _value.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            }

            private char GetCorner(TopMidBot v, LeftCentreRight h)
            {
                return style.Corners[(int)h, (int)v];
            }

            private static string GetPaddingString(int length)
            {
                return new String(' ', length);
            }
        }
    }
}