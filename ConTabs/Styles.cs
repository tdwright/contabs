using System;

namespace ConTabs
{
    public class Style
    {
        public char Wall { get; set; }
        public char Floor { get; set; }

        public Corners Corners { get; set; }

        public Style(char wall, char floor, char corners)
        {
            Wall = wall;
            Floor = floor;

            Corners = new Corners();
            Corners.SetAllCorners(corners);
        }

        public Style(char wall, char floor, Corners corners)
        {
            Wall = wall;
            Floor = floor;
            Corners = corners;
        }

        // Built-in styles
        public static Style Default => new Style('|', '-', '+');

        public static Style Heavy => new Style('#', '=', '#');

        public static Style UnicodePipes => new Style('║', '═', new Corners {
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

        public static Style UnicodeLines => new Style('│', '─', new Corners {
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

        public static Style UnicodeArcs => new Style('│', '─', new Corners{
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

        // Deprecated setters/getters
        [Obsolete("Use Corners.CornerTopLeft")]
        public char CornerTopLeft { get { return Corners[0, 0]; } set { Corners.CornerTopLeft = value; } }
        [Obsolete("Use Corners.CornerTopRight")]
        public char CornerTopRight { get { return Corners[2, 0]; } set { Corners.CornerTopRight = value; } }
        [Obsolete("Use Corners.CornerBottomLeft")]
        public char CornerBottomLeft { get { return Corners[0, 2]; } set { Corners.CornerBottomLeft = value; } }
        [Obsolete("Use Corners.CornerBottomRight")]
        public char CornerBottomRight { get { return Corners[2, 2]; } set { Corners.CornerBottomRight = value; } }
        [Obsolete("Use Corners.Intersection")]
        public char Intersection { get { return Corners[1, 1]; } set { Corners.Intersection = value; } }
        [Obsolete("Use Corners.TeeNoUp")]
        public char TeeNoUp { get { return Corners[1, 0]; } set { Corners.TeeNoUp = value; } }
        [Obsolete("Use Corners.TeeNoRight")]
        public char TeeNoRight { get { return Corners[2, 1]; } set { Corners.TeeNoRight = value; } }
        [Obsolete("Use Corners.TeeNoDown")]
        public char TeeNoDown { get { return Corners[1, 2]; } set { Corners.TeeNoDown = value; } }
        [Obsolete("Use Corners.TeeNoLeft")]
        public char TeeNoLeft { get { return Corners[0, 1]; } set { Corners.TeeNoLeft = value; } }

        // Old constructor with too many params
        [Obsolete]
        public Style(char wall, char floor, char tl, char tr, char bl, char br, char i, char tnu, char tnr, char tnd, char tnl)
        {
            Wall = wall;
            Floor = floor;

            Corners.CornerTopLeft = tl;
            Corners.CornerTopRight = tr;
            Corners.CornerBottomLeft = bl;
            Corners.CornerBottomRight = br;
            Corners.Intersection = i;
            Corners.TeeNoUp = tnu;
            Corners.TeeNoRight = tnr;
            Corners.TeeNoDown = tnd;
            Corners.TeeNoLeft = tnl;
        }

    }
}
