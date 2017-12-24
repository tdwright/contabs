using System;

namespace ConTabs
{
    public class LongStringBehaviour
    {
        public Func<string,string,int,string> Method { get; set; }
        public int Width { get; set; }
        public string EllipsisString { get; set; }

        public static LongStringBehaviour Default => DoNothing;

        public static LongStringBehaviour DoNothing => new LongStringBehaviour { Method = PassThrough };
        public static LongStringBehaviour Truncate => new LongStringBehaviour { Method = TruncateString, Width = 15};
        public static LongStringBehaviour TruncateWithEllipsis => new LongStringBehaviour { Method = TruncateString, EllipsisString = "...", Width = 15 };

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

        public string ProcessString(string input)
        {
            return Method(input, EllipsisString, Width);
        }
    }
}
