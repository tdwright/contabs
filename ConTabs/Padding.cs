using System;
using System.Collections.Generic;
using System.Text;

namespace ConTabs
{
    public class Padding
    {
        public byte Left { get; set; }
        public byte Right { get; set; }
        public byte Top { get; set; }
        public byte Bottom { get; set; }

        public Padding(byte horizontalPadding)
        {
            Top = 0;
            Right = horizontalPadding;
            Bottom = 0;
            Left = horizontalPadding;
        }

        public Padding(byte verticalPadding, byte horizontalPadding)
        {
            Top = verticalPadding;
            Bottom = verticalPadding;
            Right = horizontalPadding;
            Left = horizontalPadding; 
        }

        public Padding(byte top = 0, byte right = 1, byte bottom = 0, byte left = 1)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
}
