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

        public static Style Default => new Style('|', '-', '+');

        public static Style Heavy => new Style('#', '=', '#');

        public static Style UnicodePipes => new Style('║', '═', new Corners {
            CornerTopLeft      = '╔',
            CornerTopRight     = '╗',
            CornerBottomLeft   = '╚',
            CornerBottomRight  = '╝',
            Intersection       = '╬',
            TeeNoUp            = '╦',
            TeeNoLeft          = '╠',
            TeeNoDown          = '╩',
            TeeNoRight         = '╣'
        });
        
    }
}
