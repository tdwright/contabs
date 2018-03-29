using System;
using System.Collections.Generic;
using System.Text;

namespace ConTabs
{
    public class Padding
    {
        /// <summary>
        /// Padding to the left
        /// </summary>
        public byte Left { get; set; }

        /// <summary>
        /// Padding to the right
        /// </summary>
        public byte Right { get; set; }

        /// <summary>
        /// Padding above
        /// </summary>
        public byte Top { get; set; }

        /// <summary>
        /// Padding below
        /// </summary>
        public byte Bottom { get; set; }

        /// <summary>
        /// Creates a new padding element
        /// </summary>
        /// <param name="horizontalPadding">The amount of padding to the left and right</param>
        public Padding(byte horizontalPadding)
        {
            Top = 0;
            Right = horizontalPadding;
            Bottom = 0;
            Left = horizontalPadding;
        }

        /// <summary>
        /// Creates a new padding element
        /// </summary>
        /// <param name="verticalPadding">The amount of padding above and below</param>
        /// <param name="horizontalPadding">The amount of padding to the left and right</param>
        public Padding(byte verticalPadding, byte horizontalPadding)
        {
            Top = verticalPadding;
            Bottom = verticalPadding;
            Right = horizontalPadding;
            Left = horizontalPadding;
        }

        /// <summary>
        /// Creates a new padding element
        /// </summary>
        /// <param name="top">The amount of padding above</param>
        /// <param name="right">The amount of padding to the right</param>
        /// <param name="bottom">The amount of padding below</param>
        /// <param name="left">The amount of padding to the left</param>
        public Padding(byte top = 0, byte right = 1, byte bottom = 0, byte left = 1)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
}