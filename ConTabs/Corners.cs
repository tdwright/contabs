namespace ConTabs
{
    public class Corners
    {
        private readonly char[,] cornerChars = new char[3, 3];

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

        /// <summary>
        /// The character representing the top-left corner
        /// </summary>
        public char CornerTopLeft { get { return cornerChars[0, 0]; } set { cornerChars[0, 0] = value; } }

        /// <summary>
        /// The character representing the top-right corner
        /// </summary>
        public char CornerTopRight { get { return cornerChars[2, 0]; } set { cornerChars[2, 0] = value; } }

        /// <summary>
        /// The character representing the bottom-left corner
        /// </summary>
        public char CornerBottomLeft { get { return cornerChars[0, 2]; } set { cornerChars[0, 2] = value; } }

        /// <summary>
        /// The character representing the bottom-right corner
        /// </summary>
        public char CornerBottomRight { get { return cornerChars[2, 2]; } set { cornerChars[2, 2] = value; } }

        /// <summary>
        /// The character representing intersections
        /// </summary>
        public char Intersection { get { return cornerChars[1, 1]; } set { cornerChars[1, 1] = value; } }

        /// <summary>
        /// The character representing the intersections on the top
        /// </summary>
        public char TeeNoUp { get { return cornerChars[1, 0]; } set { cornerChars[1, 0] = value; } }

        /// <summary>
        /// The character representing the intersections on the right
        /// </summary>
        public char TeeNoRight { get { return cornerChars[2, 1]; } set { cornerChars[2, 1] = value; } }

        /// <summary>
        /// The character representing the intersections on the bottom
        /// </summary>
        public char TeeNoDown { get { return cornerChars[1, 2]; } set { cornerChars[1, 2] = value; } }

        /// <summary>
        /// The character representing the intersections on the left
        /// </summary>
        public char TeeNoLeft { get { return cornerChars[0, 1]; } set { cornerChars[0, 1] = value; } }

        public char this[int i, int j]
        {
            get
            {
                return cornerChars[i, j];
            }
        }

        /// <summary>
        /// Define all corner characters
        /// </summary>
        /// <param name="corner">The character to set all corners to</param>
        public void SetAllCorners(char corner)
        {
            for (int i = 0; i < cornerChars.GetLength(0); i++)
            {
                for (int j = 0; j < cornerChars.GetLength(1); j++)
                {
                    cornerChars[i, j] = corner;
                }
            }
        }
    }
}