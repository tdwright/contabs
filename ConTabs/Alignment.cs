using System;

namespace ConTabs
{
    public class Alignment
    {
        public Func<string, int, string> Method { get; set; }

        public static Alignment Default => Left;

        public static Alignment Left => new Alignment { Method = AlignLeft };
        public static Alignment Right => new Alignment { Method = AlignRight };
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
            return new string(' ', amount);
        }

        public string ProcessString(string input, int colMaxWidth)
        {
            if (input == string.Empty)
            {
                GetPaddingSpaces(colMaxWidth);
            }

            return Method(input, colMaxWidth);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var comp = obj as Alignment;

            return Method.Equals(comp.Method);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Method.GetHashCode();
        }
    }
}