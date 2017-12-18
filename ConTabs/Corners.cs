namespace ConTabs
{
    public class Corners
    {
        private char[,] cornerChars = new char[3, 3];

        /*
         *  ╔═══╤═════╤═════╤═════╗
         *  ║   │ 0   │ 1   │ 2   ║
         *  ╠═══╪═════╪═════╪═════╣
         *  ║ 0 │ TL  │ TNU │ TR  ║
         *  ╟───┼─────┼─────┼─────╢
         *  ║ 1 │ TNL │  I  │ TNR ║
         *  ╟───┼─────┼─────┼─────╢
         *  ║ 2 │ BL  │ TND │ BR  ║
         *  ╚═══╧═════╧═════╧═════╝
         * 
         */

        public char CornerTopLeft { get { return cornerChars[0, 0]; } set { cornerChars[0, 0] = value; } }
        public char CornerTopRight { get { return cornerChars[2, 0]; } set { cornerChars[2, 0] = value; } }
        public char CornerBottomLeft { get { return cornerChars[0, 2]; } set { cornerChars[0, 2] = value; } }
        public char CornerBottomRight { get { return cornerChars[2, 2]; } set { cornerChars[2, 2] = value; } }

        public char Intersection { get { return cornerChars[1, 1]; } set { cornerChars[1, 1] = value; } }
        public char TeeNoUp { get { return cornerChars[1, 0]; } set { cornerChars[1, 0] = value; } }
        public char TeeNoRight { get { return cornerChars[2, 1]; } set { cornerChars[2, 1] = value; } }
        public char TeeNoDown { get { return cornerChars[1, 2]; } set { cornerChars[1, 2] = value; } }
        public char TeeNoLeft { get { return cornerChars[0, 1]; } set { cornerChars[0, 1] = value; } }

        public char this[int i, int j]
        {
            get
            {
                return cornerChars[i, j];
            }
        }

        public void SetAllCorners(char corner)
        {
            for(int i=0;i<cornerChars.GetLength(0);i++)
            {
                for(int j =0;j<cornerChars.GetLength(1);j++)
                {
                    cornerChars[i, j] = corner;
                }
            }
        }
    }
}
