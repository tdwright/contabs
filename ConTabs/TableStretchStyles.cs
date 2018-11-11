using System;
using System.Collections.Generic;
using System.Linq;

namespace ConTabs
{
    /// <summary>
    /// Defines how should table be stretched / squeezed on a canvas
    /// </summary>
    public class TableStretchStyles
    {
        public Func<List<Column>, string, int, int, int> Method { get; set; }

        /// <summary>
        /// Does not stretch / squeeze the table
        /// </summary>
        public static TableStretchStyles Default => DoNothing;

        /// <summary>
        /// Does not stretch / squeeze the table
        /// </summary>
        public static TableStretchStyles DoNothing => new TableStretchStyles { Method = GetStaticColumnWidth };

        /// <summary>
        /// Sets all columns to the same width (approximately)
        /// </summary>
        public static TableStretchStyles EvenColumnWidth => new TableStretchStyles { Method = GetUniformColumnWidth };

        /*
        /// <summary>
        /// Stretches / squeezes all columns by approximately the same width
        /// </summary>
        public static TableStretchStyles StretchAllColumnsEvenly => new TableStretchStyles { Method = GetEvenlyStretchedColumnWidth };

        /// <summary>
        /// Stretches / squeezes all columns by approximately the same width
        /// </summary>
        public static TableStretchStyles StretchLongStrings => new TableStretchStyles { Method = GetLongStringStretchedFirstColumnWidth };
        */
        public static int GetStaticColumnWidth(List<Column> columns, string columnName, int canvasWidth, int horizontalPadding)
        {
            Column column = columns.Find(c => c.ColumnName == columnName);

            if (column.Values == null || column.Values.Count == 0) return columnName.Length;

            if (column.LongStringBehaviour.Width > 0) return column.LongStringBehaviour.Width;

            return column.Values
                .Select(v => column.StringValForCol(v))
                .Union(new List<string> { column.ColumnName })
                .Select(v => v.Length)
                .Max();
        }

        public static int GetUniformColumnWidth(List<Column> columns, string columnName, int canvasWidth, int horizontalPadding)
        {
            // calculate the minimal width
            int uniformWidth = canvasWidth / columns.Count - horizontalPadding - 1;

            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i].ColumnName == columnName)
                {
                    // if we still have space left, add width to columns starting from first one
                    if (i < canvasWidth % columns.Count - 1)
                    {
                        uniformWidth++;
                    }
                    //todo: I should not change the value of Width
                    columns[i].LongStringBehaviour.Width = uniformWidth;
                    break;
                }
            }
            return uniformWidth;
        }

    }
}
