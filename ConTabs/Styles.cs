namespace ConTabs
{
    public class Style
    {
        
        public char Wall { get; set; }
        public char Floor { get; set; }

        public char[,] Corners = new char[3, 3];

        /*
         *  ╔═══╤═════╤═════╤═════╗
         *  ║   │ 0   │ 1   │ 2   ║
         *  ╠═══╪═════╪═════╪═════╣
         *  ║ 0 │ TL  │ TNU │ TR  ║
         *  ╟───┼─────┼─────┼─────╢
         *  ║ 1 │ TNR │  I  │ TNL ║
         *  ╟───┼─────┼─────┼─────╢
         *  ║ 2 │ BL  │ TND │ BR  ║
         *  ╚═══╧═════╧═════╧═════╝
         * 
         */

        public char CornerTopLeft { get { return Corners[0, 0]; } set { Corners[0, 0] = value; } }
        public char CornerTopRight { get { return Corners[2, 0]; } set { Corners[2, 0] = value; } }
        public char CornerBottomLeft { get { return Corners[0, 2]; } set { Corners[0, 2] = value; } }
        public char CornerBottomRight { get { return Corners[2, 2]; } set { Corners[2, 2] = value; } }

        public char Intersection { get { return Corners[1, 1]; } set { Corners[1, 1] = value; } }
        public char TeeNoUp { get { return Corners[1, 0]; } set { Corners[1, 0] = value; } }
        public char TeeNoRight { get { return Corners[0, 1]; } set { Corners[0, 1] = value; } }
        public char TeeNoDown { get { return Corners[1, 2]; } set { Corners[1, 2] = value; } }
        public char TeeNoLeft { get { return Corners[2, 1]; } set { Corners[2, 1] = value; } }

        public Style(char wall, char floor, char corners)
        {
            Wall = wall;
            Floor = floor;
            
            CornerTopLeft = corners;
            CornerTopRight = corners;
            CornerBottomLeft = corners;
            CornerBottomRight = corners;

            Intersection = corners;
            TeeNoUp = corners;
            TeeNoRight = corners;
            TeeNoDown = corners;
            TeeNoLeft = corners;
        }

        public Style(char wall, char floor, char tl, char tr, char bl, char br, char i, char tnu, char tnr, char tnd, char tnl)
        {
            Wall = wall;
            Floor = floor;

            CornerTopLeft = tl;
            CornerTopRight = tr;
            CornerBottomLeft = bl;
            CornerBottomRight = br;

            Intersection = i;
            TeeNoUp = tnu;
            TeeNoRight = tnr;
            TeeNoDown = tnd;
            TeeNoLeft = tnl;
        }

        public static Style Default => new Style('|', '-', '+');

        public static Style Heavy => new Style('#', '=', '#');

        public static Style UnicodePipes => new Style('║', '═', '╔', '╗', '╚', '╝', '╬', '╦', '╠', '╩', '╣');


    }
}
