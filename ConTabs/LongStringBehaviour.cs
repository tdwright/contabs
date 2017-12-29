using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConTabs
{
    public class LongStringBehaviour
    {
        public Func<string,string,int,string> Method { get; set; }
        public int Width { get; set; }
        public string EllipsisString { get; set; }

        public static LongStringBehaviour Default => DoNothing;

        public static LongStringBehaviour DoNothing => new LongStringBehaviour { Method = PassThrough };
        public static LongStringBehaviour Truncate => new LongStringBehaviour { Method = TruncateString, Width = 15 };
        public static LongStringBehaviour TruncateWithEllipsis => new LongStringBehaviour { Method = TruncateString, EllipsisString = "...", Width = 15 };
        public static LongStringBehaviour Wrap => new LongStringBehaviour { Method = WrapString, Width = 25 };

        private static string PassThrough(string input,string ellipsis, int width)
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

        public string ProcessString(string input)
        {
            return Method(input, EllipsisString, Width);
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

                        if (SplitChars.Contains(strBuilder[strBuilder.Length - 1]) && strBuilder[strBuilder.Length - 1] != '-')
                            strBuilder.Remove(strBuilder.Length - 1, 1);
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
