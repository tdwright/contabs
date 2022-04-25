﻿using System;

namespace ConTabs
{
    /// <summary>
    /// Contains the alignment properties of a column
    /// </summary>
    public class Alignment
    {
        public Func<string, int, string> Method { get; set; }

        public static Alignment Default => Left;

        /// <summary>
        /// Aligns the cell value to the left
        /// </summary>
        public static Alignment Left => new Alignment { Method = AlignLeft };

        /// <summary>
        /// Aligns the cell value to the right
        /// </summary>
        public static Alignment Right => new Alignment { Method = AlignRight };

        /// <summary>
        /// Aligns the cell value to the center
        /// </summary>
        public static Alignment Center => new Alignment { Method = AlignCenter };

        private static string AlignLeft(string input, int colMaxWidth)
        {
            return input + GetPaddingSpaces(colMaxWidth - input.Length);
        }

        private static string AlignRight(string input, int colMaxWidth)
        {
            return GetPaddingSpaces(colMaxWidth - input.Length) + input;
        }

        private static string AlignCenter(string input, int colMaxWidth)
        {
            var padLeft = (colMaxWidth - input.Length) / 2;
            var padRight = (colMaxWidth - input.Length) % 2 == 0 ? padLeft : padLeft + 1;

            return GetPaddingSpaces(padLeft) + input + GetPaddingSpaces(padRight);
        }

        private static string GetPaddingSpaces(int amount)
        {
            return new string(' ', amount > 0 ? amount : 0);
        }

        /// <summary>
        /// Apply styling to a string
        /// </summary>
        /// <param name="input">The target string</param>
        /// <param name="colMaxWidth">The maximum width of the column</param>
        /// <returns></returns>
        public string ProcessString(string input, int colMaxWidth)
        {
            if (input == string.Empty)
            {
                GetPaddingSpaces(colMaxWidth);
            }
            else if (input.Length > colMaxWidth)
            {
                //todo: maybe default long string behavior?
                input = input.Substring(0, colMaxWidth);
            }

            return Method(input, colMaxWidth);
        }

        /// <summary>
        /// Compares two alignments
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Alignment comp && Method.Equals(comp.Method);
        }
    }
}