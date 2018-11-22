using System;

namespace ConTabs
{
    /// <summary>
    /// The properties used to define a table's visual styling
    /// </summary>
    public class Style
    {
        /// <summary>
        /// The character used to represent the walls of the table
        /// </summary>
        public char Wall { get; set; }

        /// <summary>
        /// The character used to represent the floors of the table
        /// </summary>
        public char Floor { get; set; }

        /// <summary>
        /// The character used to represent the corners of the table
        /// </summary>
        public Corners Corners { get; set; }

        /// <summary>
        /// Creates a new style
        /// </summary>
        /// <param name="wall">The character used to represent the walls of the table</param>
        /// <param name="floor">The character used to represent the floors of the table</param>
        /// <param name="corners">The character used to represent the corners of the table</param>
        public Style(char wall, char floor, char corners)
        {
            Wall = wall;
            Floor = floor;

            Corners = new Corners();
            Corners.SetAllCorners(corners);
        }

        /// <summary>
        /// Creates a new style
        /// </summary>
        /// <param name="wall">The character used to represent the walls of the table</param>
        /// <param name="floor">The character used to represent the floors of the table</param>
        /// <param name="corners">The characters used to represent the corners of the table</param>
        public Style(char wall, char floor, Corners corners)
        {
            Wall    = wall;
            Floor   = floor;
            Corners = corners;
        }

        /// <summary>
        /// Built-in style
        /// </summary>
        public static Style Default => new Style('|', '-', '+');

        /// <summary>
        /// Built-in style
        /// </summary>
        public static Style Heavy => new Style('#', '=', '#');

        /// <summary>
        /// Built-in style
        /// </summary>
        public static Style Whitespace => new Style(' ', ' ', ' ');

        /// <summary>
        /// Built-in style
        /// <para />
        /// *May require Console.OutputEncoding = Encoding.Unicode;
        /// </summary>
        public static Style UnicodePipes => new Style('║', '═', new Corners
        {
            CornerTopLeft     = '╔',
            CornerTopRight    = '╗',
            CornerBottomLeft  = '╚',
            CornerBottomRight = '╝',
            Intersection      = '╬',
            TeeNoUp           = '╦',
            TeeNoLeft         = '╠',
            TeeNoDown         = '╩',
            TeeNoRight        = '╣'
        });

        /// <summary>
        /// Built-in style
        /// <para />
        /// *May require Console.OutputEncoding = Encoding.Unicode;
        /// </summary>
        public static Style UnicodeLines => new Style('│', '─', new Corners
        {
            CornerTopLeft     = '┌',
            CornerTopRight    = '┐',
            CornerBottomLeft  = '└',
            CornerBottomRight = '┘',
            Intersection      = '┼',
            TeeNoUp           = '┬',
            TeeNoLeft         = '├',
            TeeNoDown         = '┴',
            TeeNoRight        = '┤'
        });

        /// <summary>
        /// Built-in style
        /// <para />
        /// *May require Console.OutputEncoding = Encoding.Unicode;
        /// </summary>
        public static Style UnicodeArcs => new Style('│', '─', new Corners
        {
            CornerTopLeft     = '╭',
            CornerTopRight    = '╮',
            CornerBottomLeft  = '╰',
            CornerBottomRight = '╯',
            Intersection      = '┼',
            TeeNoUp           = '┬',
            TeeNoLeft         = '├',
            TeeNoDown         = '┴',
            TeeNoRight        = '┤'
        });

        /// <summary>
        /// Built-in style: Dots
        /// </summary>
        public static Style Dots => new Style(':', '.', new Corners
        {
            CornerTopLeft     = '.',
            CornerTopRight    = '.',
            CornerBottomLeft  = ':',
            CornerBottomRight = ':',
            Intersection      = ':',
            TeeNoUp           = '.',
            TeeNoLeft         = ':',
            TeeNoDown         = ':',
            TeeNoRight        = ':'
        });
    }
}