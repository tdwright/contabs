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
        public Func<Column, int> CalculateOptimalWidth { get; set; }
        public Func<List<Column>, int, int, int> CalculateAdditionalWidth { get; set; }

        /// <summary>
        /// Does not stretch / squeeze the table
        /// </summary>
        public static TableStretchStyles Default => DoNothing;

        /// <summary>
        /// Does not stretch / squeeze the table
        /// </summary>
        public static TableStretchStyles DoNothing => new TableStretchStyles { CalculateOptimalWidth = GetOptimalColumnWidth, CalculateAdditionalWidth = UseDefaultDisplayWidths };

        private static int UseDefaultDisplayWidths(List<Column> columns, int totalWidth, int canvasWidth)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].LongStringBehaviour.DisplayWidth = GetOptimalColumnWidth(columns[i]);
            }
            return totalWidth;
        }

        private static int AdaptDisplayWidths(List<Column> columns, int totalWidth, int canvasWidth)
        {
            int difference = canvasWidth - totalWidth;
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].LongStringBehaviour.DisplayWidth += difference / columns.Count;
                if (i < difference % columns.Count)
                {
                    columns[i].LongStringBehaviour.DisplayWidth++;
                }
            }
            return canvasWidth;
        }

        /// <summary>
        /// Sets all columns to the same width (approximately)
        /// </summary>
        public static TableStretchStyles EvenColumnWidth => new TableStretchStyles { CalculateOptimalWidth = GetUniformColumnWidth, CalculateAdditionalWidth = AdaptDisplayWidths };

        /// <summary>
        /// Stretches / squeezes all columns by approximately the same width
        /// </summary>
        public static TableStretchStyles StretchOrSqueezeAllColumnsEvenly => new TableStretchStyles { CalculateOptimalWidth = GetOptimalColumnWidth, CalculateAdditionalWidth = AdaptDisplayWidths };

        /*
        /// <summary>
        /// Stretches / squeezes long strings by approximately the same width
        /// </summary>
        public static TableStretchStyles StretchLongStrings => new TableStretchStyles { Method = GetLongStringStretchedFirstColumnWidth };
        */
        private static int GetOptimalColumnWidth(Column column)
        {
            if (column.Values == null || column.Values.Count == 0) return column.ColumnName.Length;

            if (column.LongStringBehaviour.Width > 0) return column.LongStringBehaviour.Width;

            return column.Values
                .Select(v => column.StringValForCol(v))
                .Union(new List<string> { column.ColumnName })
                .Select(v => v.Length)
                .Max();
        }

        private static int GetUniformColumnWidth(Column column)
        {
            return byte.MinValue;
        }
    }
}
