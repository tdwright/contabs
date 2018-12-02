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
        private static readonly int MIN_WIDTH = 2; //if set to 1, word wrap in long strings would fail

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

        /// <summary>
        /// Sets all columns to the same width (approximately)
        /// </summary>
        public static TableStretchStyles EvenColumnWidth => new TableStretchStyles { CalculateOptimalWidth = GetUniformColumnWidth, CalculateAdditionalWidth = StretchOrSqueezeDisplayWidths };

        /// <summary>
        /// Stretches / squeezes all columns by approximately the same width
        /// </summary>
        public static TableStretchStyles StretchOrSqueezeAllColumnsEvenly => new TableStretchStyles { CalculateOptimalWidth = GetOptimalColumnWidth, CalculateAdditionalWidth = StretchOrSqueezeDisplayWidths };

        /// <summary>
        /// Stretches / squeezes all columns by approximately the same width
        /// </summary>
        public static TableStretchStyles SqueezeAllColumnsEvenly => new TableStretchStyles { CalculateOptimalWidth = GetOptimalColumnWidth, CalculateAdditionalWidth = SqueezeDisplayWidths };

        /// <summary>
        /// Stretches / squeezes long strings by approximately the same width
        /// </summary>
        public static TableStretchStyles StretchOrSqueezeLongStrings => new TableStretchStyles { CalculateOptimalWidth = GetOptimalColumnWidth, CalculateAdditionalWidth = StretchOrSqueezeLongStringDisplayWidths };

        /// <summary>
        /// Stretches / squeezes long strings by approximately the same width
        /// </summary>
        public static TableStretchStyles SqueezeLongStrings => new TableStretchStyles { CalculateOptimalWidth = GetOptimalColumnWidth, CalculateAdditionalWidth = SqueezeLongStringDisplayWidths };

        private static int UseDefaultDisplayWidths(List<Column> columns, int totalWidth, int canvasWidth)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].LongStringBehaviour.DisplayWidth = GetOptimalColumnWidth(columns[i]);
            }
            return columns
                .Select(v => v.LongStringBehaviour.DisplayWidth)
                .Sum();
        }

        private static int StretchOrSqueezeDisplayWidths(List<Column> columns, int totalWidth, int canvasWidth)
        {
            int difference = canvasWidth - totalWidth;
            if (difference > 0)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    columns[i].LongStringBehaviour.DisplayWidth += difference / columns.Count;
                    if (i < difference % columns.Count)
                    {
                        columns[i].LongStringBehaviour.DisplayWidth++;
                    }
                }
            }
            else if (difference < 0)
            {
                List<Column> squeezableColumns = columns.Where(c => c.LongStringBehaviour.DisplayWidth > MIN_WIDTH).ToList();
                var columnIndex = 0;
                while (difference < 0 && squeezableColumns.Count > 0)
                {
                    squeezableColumns[columnIndex].LongStringBehaviour.DisplayWidth--;
                    difference++;
                    if (squeezableColumns[columnIndex].LongStringBehaviour.DisplayWidth == MIN_WIDTH)
                    {
                        squeezableColumns.RemoveAt(columnIndex);
                    }
                    else
                    {
                        columnIndex++;
                    }
                    if (columnIndex >= squeezableColumns.Count) columnIndex = 0;
                }
            }
            return columns
                .Select(v => v.LongStringBehaviour.DisplayWidth)
                .Sum();
        }

        private static int StretchOrSqueezeLongStringDisplayWidths(List<Column> columns, int totalWidth, int canvasWidth)
        {
            var longStringColumns = columns.Where(c => c.LongStringBehaviour.Width > 0).ToList();
            StretchOrSqueezeDisplayWidths(longStringColumns, totalWidth, canvasWidth);
            return columns
                .Select(v => v.LongStringBehaviour.DisplayWidth)
                .Sum();
        }

        private static int SqueezeLongStringDisplayWidths(List<Column> columns, int totalWidth, int canvasWidth)
        {
            var longStringColumns = columns.Where(c => c.LongStringBehaviour.Width > 0).ToList();
            SqueezeDisplayWidths(longStringColumns, totalWidth, canvasWidth);
            return columns
                .Select(v => v.LongStringBehaviour.DisplayWidth)
                .Sum();
        }

        private static int SqueezeDisplayWidths(List<Column> columns, int totalWidth, int canvasWidth)
        {
            int difference = canvasWidth - totalWidth;
            return difference >= 0
                ? UseDefaultDisplayWidths(columns, totalWidth, canvasWidth)
                : StretchOrSqueezeDisplayWidths(columns, totalWidth, canvasWidth);
        }

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
