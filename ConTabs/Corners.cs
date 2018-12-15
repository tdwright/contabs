namespace ConTabs
{
    public class Corners
    {
        private readonly char[,] _cornerChars = new char[3, 3];

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
        public char CornerTopLeft { get { return _cornerChars[0, 0]; } set { _cornerChars[0, 0] = value; } }

        /// <summary>
        /// The character representing the top-right corner
        /// </summary>
        public char CornerTopRight { get { return _cornerChars[2, 0]; } set { _cornerChars[2, 0] = value; } }

        /// <summary>
        /// The character representing the bottom-left corner
        /// </summary>
        public char CornerBottomLeft { get { return _cornerChars[0, 2]; } set { _cornerChars[0, 2] = value; } }

        /// <summary>
        /// The character representing the bottom-right corner
        /// </summary>
        public char CornerBottomRight { get { return _cornerChars[2, 2]; } set { _cornerChars[2, 2] = value; } }

        /// <summary>
        /// The character representing intersections
        /// </summary>
        public char Intersection { get { return _cornerChars[1, 1]; } set { _cornerChars[1, 1] = value; } }

        /// <summary>
        /// The character representing the intersections on the top
        /// </summary>
        public char TeeNoUp { get { return _cornerChars[1, 0]; } set { _cornerChars[1, 0] = value; } }

        /// <summary>
        /// The character representing the intersections on the right
        /// </summary>
        public char TeeNoRight { get { return _cornerChars[2, 1]; } set { _cornerChars[2, 1] = value; } }

        /// <summary>
        /// The character representing the intersections on the bottom
        /// </summary>
        public char TeeNoDown { get { return _cornerChars[1, 2]; } set { _cornerChars[1, 2] = value; } }

        /// <summary>
        /// The character representing the intersections on the left
        /// </summary>
        public char TeeNoLeft { get { return _cornerChars[0, 1]; } set { _cornerChars[0, 1] = value; } }

        public char this[int i, int j]
        {
            get
            {
                return _cornerChars[i, j];
            }
        }

        /// <summary>
        /// Define all corner characters
        /// </summary>
        /// <param name="corner">The character to set all corners to</param>
        public void SetAllCorners(char corner)
        {
            for (var i = 0; i < _cornerChars.GetLength(0); i++)
            {
                for (var j = 0; j < _cornerChars.GetLength(1); j++)
                {
                    _cornerChars[i, j] = corner;
                }
            }
        }
    }
}