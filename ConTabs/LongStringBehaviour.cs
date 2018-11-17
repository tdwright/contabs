using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConTabs
{
    /// <summary>
    /// Defines the behaviour of a cell when it contains a long string
    /// </summary>
    public class LongStringBehaviour
    {
        public Func<string, string, int, string> Method { get; set; }

        /// <summary>
        /// The width of the string
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// A property to store the correct width of column to be displayed
        /// </summary>
        public int DisplayWidth { get; set; }

        /// <summary>
        /// The ellipsis to use when the behvaiour is set to TruncateWithEliipsis
        /// </summary>
        public string EllipsisString { get; set; }

        /// <summary>
        /// Does not interpret the string
        /// </summary>
        public static LongStringBehaviour Default => DoNothing;

        /// <summary>
        /// Does not interpret the string
        /// </summary>
        public static LongStringBehaviour DoNothing => new LongStringBehaviour { Method = PassThrough };

        /// <summary>
        /// Shortens the string to LongstringBehaviour.Width
        /// </summary>
        public static LongStringBehaviour Truncate => new LongStringBehaviour { Method = TruncateString, Width = 15 };

        /// <summary>
        /// Shortens the string to LongstringBehaviour.Width and adds an ellipsis at the end
        /// </summary>
        public static LongStringBehaviour TruncateWithEllipsis => new LongStringBehaviour { Method = TruncateString, EllipsisString = "...", Width = 15 };

        /// <summary>
        /// Wraps the string onto a new line within the same cell
        /// </summary>
        public static LongStringBehaviour Wrap => new LongStringBehaviour { Method = WrapString, Width = 25 };

        private static string PassThrough(string input, string ellipsis, int width)
        {
            return input;
        }

        private static string TruncateString(string input, string ellipsis, int width)
        {
            if (input.Length <= width) return input;
            var nonNullEllipsis = ellipsis ?? "";
            return input.Substring(0, width - nonNullEllipsis.Length) + nonNullEllipsis;
        }

        private static string WrapString(string input, string ellipsis, int width)
        {
            if (input.Length <= width) return input;
            return LongStringBehaviour.WordWrap(input, width);
        }

        /// <summary>
        /// Returns a new string based upon the intended behaviour
        /// </summary>
        /// <param name="input">The cell's contents</param>
        /// <returns>A new formatted string</returns>
        public string ProcessString(string input)
        {
            return Method(input, EllipsisString, DisplayWidth);
        }

        // The following word wrapping methods inspired by an SO answer by "ICR"
        // https://stackoverflow.com/a/17635/50151

        private static readonly char[] SplitChars = new char[] { ' ', '-', '\t' };

        private static string WordWrap(string str, int width)
        {
            string[] words = Explode(str, SplitChars);

            int curLineLength = 0;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < words.Length; i += 1)
            {
                string word = words[i];
                // If adding the new word to the current line would be too long,
                // then put it on a new line (and split it up if it's too long).
                if (curLineLength + word.Length > width)
                {
                    // Only move down to a new line if we have text on the current line.
                    // Avoids situation where wrapped whitespace causes emptylines in text.
                    if (curLineLength > 0)
                    {
                        if (SplitChars.Contains(strBuilder[strBuilder.Length - 1]) && strBuilder[strBuilder.Length - 1] != '-')
                            strBuilder.Remove(strBuilder.Length - 1, 1);
                        strBuilder.Append(Environment.NewLine);
                        curLineLength = 0;
                    }

                    // If the current word is too long to fit on a line even on it's own then
                    // split the word up.
                    while (word.Length > width)
                    {
                        strBuilder.Append(word.Substring(0, width - 1) + "-");
                        word = word.Substring(width - 1);
                        strBuilder.Append(Environment.NewLine);
                    }

                    // Remove leading whitespace from the word so the new line starts flush to the left.
                    word = word.TrimStart();
                }
                strBuilder.Append(word);
                curLineLength += word.Length;
            }

            return strBuilder.ToString();
        }

        private static string[] Explode(string str, char[] splitChars)
        {
            List<string> parts = new List<string>();
            int startIndex = 0;
            while (true)
            {
                int index = str.IndexOfAny(splitChars, startIndex);

                if (index == -1)
                {
                    parts.Add(str.Substring(startIndex));
                    return parts.ToArray();
                }

                string word = str.Substring(startIndex, index - startIndex);
                char nextChar = str.Substring(index, 1)[0];
                // Dashes and the likes should stick to the word occuring before it. Whitespace doesn't have to.
                if (char.IsWhiteSpace(nextChar))
                {
                    parts.Add(word);
                    parts.Add(nextChar.ToString());
                }
                else
                {
                    parts.Add(word + nextChar);
                }

                startIndex = index + 1;
            }
        }
    }
}