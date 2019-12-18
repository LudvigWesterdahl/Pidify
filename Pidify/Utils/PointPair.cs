using System;
using System.Drawing;

namespace Pidify.Utils
{
    /// <summary>
    /// Container for two points that can be used as a line or a rectangle.
    /// </summary>
    public sealed class PointPair
    {
        /// <summary>
        /// A line on the top of a rectangle.
        /// </summary>
        public static PointPair LineTop = NewInstance(0f, 0f, 1f, 0f);

        /// <summary>
        /// A line on the bottom of a rectangle.
        /// </summary>
        public static PointPair LineBottom = NewInstance(0f, 1f, 1f, 1f);

        /// <summary>
        /// A line on the left side of a rectangle.
        /// </summary>
        public static PointPair LineLeft = NewInstance(0f, 0f, 0f, 1f);

        /// <summary>
        /// A line on the right of a rectangle.
        /// </summary>
        public static PointPair LineRight = NewInstance(1f, 0f, 1f, 1f);

        /// <summary>
        /// A full rectangle.
        /// </summary>
        public static PointPair Full = NewInstance(0, 0, 1, 1);


        /// <summary>
        /// Gets the start x-axis coordinate.
        /// </summary>
        public float FromX { get; }

        /// <summary>
        /// Gets the start y-axis coordinate.
        /// </summary>
        public float FromY { get; }

        /// <summary>
        /// Gets the end x-axis coordinate.
        /// </summary>
        public float ToX { get; }

        /// <summary>
        /// Gets the end y-axis coordinate.
        /// </summary>
        public float ToY { get; }

        /// <summary>
        /// Gets the width of the line.
        /// </summary>
        public float Width => Math.Abs(ToX - FromX);

        /// <summary>
        /// Gets the height of the line.
        /// </summary>
        public float Height => Math.Abs(ToY - FromY);

        /// <summary>
        /// Gets the length of the line.
        /// </summary>
        public float LineLength => (float)Math.Sqrt(Width * Width + Height * Height);

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="fromX">the start x-axis coordinate</param>
        /// <param name="fromY">the start y-axis coordinate</param>
        /// <param name="toX">the end x-axis coordinate</param>
        /// <param name="toY">the end y-axis coordinate</param>
        private PointPair(float fromX, float fromY, float toX, float toY)
        {
            FromX = fromX;
            FromY = fromY;
            ToX = toX;
            ToY = toY;
        }

        /// <summary>
        /// Returns a new PointPair.
        /// </summary>
        /// <param name="fromX">the start x-axis coordinate</param>
        /// <param name="fromY">the start y-axis coordinate</param>
        /// <param name="toX">the end x-axis coordinate</param>
        /// <param name="toY">the end y-axis coordinate</param>
        /// <returns></returns>
        public static PointPair NewInstance(float fromX, float fromY, float toX, float toY)
        {
            ValidationUtil.RequireBetween(fromX, 0, 1, $"fromX({fromX}) must be in range [0, 1].");
            ValidationUtil.RequireBetween(fromY, 0, 1, $"fromY({fromY}) must be in range [0, 1].");
            ValidationUtil.RequireBetween(toX, 0, 1, $"toX({toX}) must be in range [0, 1].");
            ValidationUtil.RequireBetween(toY, 0, 1, $"toY({toY}) must be in range [0, 1].");

            ValidationUtil.RequirePositive(fromX + toX + fromY + toY, $"fromX({fromX}) + toX({toX}) + fromY({fromY}) + toY({toY}) must positive (>0).");

            return new PointPair(fromX, fromY, toX, toY);
        }

        /// <summary>
        /// Splits this PointPair into rows and columns with a internal padding between the boxes.
        /// </summary>
        /// <param name="rows">number of rows</param>
        /// <param name="cols">number of columns</param>
        /// <param name="horizontalPadding">padding between boxes horizontally</param>
        /// <param name="verticalPadding">padding between boxes vertically</param>
        /// <returns>array indexes by row then column containing the split boxes</returns>
        public PointPair[,] Split(int rows, int cols, float horizontalPadding, float verticalPadding)
        {
            var split = new PointPair[rows, cols];

            // Removes padding from width and height.
            var totalWidthPadding = (cols - 1) * horizontalPadding;
            var totalHeightPadding = (rows - 1) * verticalPadding;

            // Calculates the width and height of each split box.
            var splitWidth = (Width - totalWidthPadding) / cols;
            var splitHeight = (Height - totalHeightPadding) / rows;

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    var fromX = FromX + c * (splitWidth + horizontalPadding);
                    var toX = fromX + splitWidth;
                    var fromY = FromY + r * (splitHeight + verticalPadding);
                    var toY = fromY + splitHeight;
                    split[r, c] = NewInstance(fromX, fromY, toX, toY);
                }
            }

            return split;
        }

        /// <summary>
        /// Converts the x-axis coordinate x to an absolute x-axis value inside source.
        /// <para>
        /// For example: given that source is a (width, height) (100, 50) rectangle starting at (x,y) (20, 15) and x is 0.5.
        /// </para>
        /// <para>
        /// Then this function would return 20 + 100 * 0.5 = 70.
        /// </para>
        /// </summary>
        /// <param name="x">the value to convert</param>
        /// <param name="source">the rectangle</param>
        /// <returns>the converted x-axis coordinate</returns>
        public static float ConvertX(float x, RectangleF source)
        {
            ValidationUtil.RequireBetween(x, 0, 1, $"x({x}) must be in range [0, 1].");

            return source.X + source.Width * x;
        }

        /// <summary>
        /// Converts the y-axis coordinate y to an absolute y-axis value inside source.
        /// <para>
        /// For example: given that source is a (width, height) (100, 50) rectangle starting at (x,y) (20, 15) and y is 0.5.
        /// </para>
        /// <para>
        /// Then this function would return 15 + 50 * 0.5 = 40.
        /// </para>
        /// </summary>
        /// <param name="y">the value to convert</param>
        /// <param name="source">the rectangle</param>
        /// <returns>the converted y-axis coordinate</returns>
        public static float ConvertY(float y, RectangleF source)
        {
            ValidationUtil.RequireBetween(y, 0, 1, $"y({y}) must be in range [0, 1].");

            return source.Y + source.Height * y;
        }

        /// <summary>
        /// Converts this PointPair FromX given the source.
        /// </summary>
        /// <param name="source">the rectangle</param>
        /// <returns>the converted FromX coordinate</returns>
        public float ConvertFromX(RectangleF source)
        {
            return ConvertX(FromX, source);
        }

        /// <summary>
        /// Converts this PointPair FromY given the source.
        /// </summary>
        /// <param name="source">the rectangle</param>
        /// <returns>the converted FromY coordinate</returns>
        public float ConvertFromY(RectangleF source)
        {
            return ConvertY(FromY, source);
        }

        /// <summary>
        /// Converts this PointPair ToX given the source.
        /// </summary>
        /// <param name="source">the rectangle</param>
        /// <returns>the converted ToX coordinate</returns>
        public float ConvertToX(RectangleF source)
        {
            return ConvertX(ToX, source);
        }

        /// <summary>
        /// Converts this PointPair ToY given the source.
        /// </summary>
        /// <param name="source">the rectangle</param>
        /// <returns>the converted ToY coordinate</returns>
        public float ConvertToY(RectangleF source)
        {
            return ConvertY(ToY, source);
        }

        /// <summary>
        /// Converts this PointPair Width given the source.
        /// </summary>
        /// <param name="source">the rectangle</param>
        /// <returns>the converted Width</returns>
        public float ConvertWidth(RectangleF source)
        {
            return source.Width * Width;
        }

        /// <summary>
        /// Converts this PointPair Height given the source.
        /// </summary>
        /// <param name="source">the rectangle</param>
        /// <returns>the converted Height</returns>
        public float ConvertHeight(RectangleF source)
        {
            return source.Height * Height;
        }

        /// <summary>
        /// Converts this PointPair LineLength given the source.
        /// </summary>
        /// <param name="source">the rectangle</param>
        /// <returns>the converted LineLength</returns>
        public float ConvertLineLength(RectangleF source)
        {
            var w = ConvertWidth(source);
            var h = ConvertHeight(source);

            return (float)Math.Sqrt(w * w + h * h);
        }

        /// <summary>
        /// Returns a new RectangleF from this PointPair given the source.
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>new RectangleF instance</returns>
        public RectangleF ToRectangle(RectangleF source)
        {
            var smallestX = Math.Min(FromX, ToX);
            var smallestY = Math.Min(FromY, ToY);
            var newX = ConvertX(smallestX, source);
            var newY = ConvertY(smallestY, source);
            var newWidth = ConvertWidth(source);
            var newHeight = ConvertHeight(source);

            return new RectangleF(newX, newY, newWidth, newHeight);
        }

        /// <summary>
        /// Maximizes this PointPair Width inside destination while maintaining width-height ratio.
        /// </summary>
        /// <param name="destination">the rectangle</param>
        /// <returns>new PointPair instance</returns>
        private PointPair MaximizeWidth(PointPair destination)
        {
            var sourceRatio = Width / Height;

            var width = destination.Width;
            var height = destination.Width / sourceRatio;

            var x = destination.FromX + (destination.Width - width) / 2;
            var y = destination.FromY + (destination.Height - height) / 2;

            return NewInstance(x, y, x + width, y + height);

        }

        /// <summary>
        /// Maximizes this PointPair Height inside destination while maintaining width-height ratio.
        /// </summary>
        /// <param name="destination">the rectangle</param>
        /// <returns>new PointPair instance</returns>
        private PointPair MaximizeHeight(PointPair destination)
        {
            var sourceRatio = Width / Height;

            var height = destination.Height;
            var width = destination.Height * sourceRatio;

            var x = destination.FromX + (destination.Width - width) / 2;
            var y = destination.FromY + (destination.Height - height) / 2;

            return NewInstance(x, y, x + width, y + height);
        }

        /// <summary>
        /// Maximizes this PointPair inside the given destination while maintaining width-height ratio.
        /// </summary>
        /// <param name="destination">the PointPair to maximize inside</param>
        /// <returns>new PointPair instance</returns>
        public PointPair MaximizeInside(PointPair destination)
        {
            var sourceRatio = Width / Height;
            var destinationRatio = destination.Width / destination.Height;

            /*
             * Explanation
             * ----------
             * 
             * Width height ratio  - WHR   (function)
             * Rectangle           - rect  (WHR less, greater or equal to 1)
             * Laying rectangle    - Lrect (WHR greater than 1)
             * Standing rectangle  - Srect (WHR less than 1)
             * Square              - Sq    (WHR equal to 1)
             * 
             * 1. A Lrect can always match the width of any rect, 
             * except if WHR(rect) greater than WHR(Lrect).
             * 
             * 2. A Hrect can always match the height of any rect,
             * except if WHR(rect) less than WHR(Hrect).
             * 
             * 3. The two statements 3.1, 3.2 are both true, 3.1 was 
             * arbitrarily chosen here.
             * 
             *     3.1 A Sq can always match the width of any rect,
             *     except if WHR(rect) greater than 1.
             * 
             *     3.2 A Sq can always match the height of any rect,
             *     except if WHR(rect) less than 1.
             * 
             */

            // Image is Lrect.
            if (sourceRatio.CompareTo(1f) < 0)
            {
                if (destinationRatio.CompareTo(sourceRatio) > 0)
                {
                    // Maxes the height.
                    return MaximizeHeight(destination);
                }

                // Maxes the width.
                return MaximizeWidth(destination);
            }

            // Image is Srect.
            if (sourceRatio.CompareTo(1f) > 0)
            {
                if (destinationRatio.CompareTo(sourceRatio) < 0)
                {
                    // Maxes the width.
                    return MaximizeWidth(destination);
                }

                // Maxes the height.
                return MaximizeHeight(destination);
            }

            // Image is Sq.
            if (destinationRatio.CompareTo(1f) > 0)
            {
                // Maxes the height.
                return MaximizeHeight(destination);
            }

            // Maxes the width.
            return MaximizeWidth(destination);
        }

        /// <summary>
        /// Returns a new <see cref="PointPair"/> which is moved x units on the x-axis and y units on the y axis.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>new PointPair instance</returns>
        public PointPair Move(float x, float y)
        {
            ValidationUtil.RequireBetween(x, -1, 1, $"x({x}) must be in range [-1, 1].");
            ValidationUtil.RequireBetween(y, -1, 1, $"x({y}) must be in range [-1, 1].");

            var fromX = FromX + x;
            var toX = ToX + x;

            if (x.CompareTo(0f) > 0 && toX.CompareTo(1f) > 0)
            {
                // x increment is positive and out of bounds.
                var error = toX - 1f;
                fromX -= error;
                toX = 1;
            }

            if(x.CompareTo(0f) < 0 && fromX.CompareTo(0f) < 0)
            {
                // x increment is negative and out of bounds.
                var error = fromX;
                toX -= error;
                fromX = 0;
            }

            var fromY = FromY + y;
            var toY = ToY + y;

            if (y.CompareTo(0f) > 0 && toY.CompareTo(1f) > 0)
            {
                // x increment is positive and out of bounds.
                var error = toY - 1f;
                fromY -= error;
                toY = 1;
            }

            if (y.CompareTo(0f) < 0 && fromY.CompareTo(0f) < 0)
            {
                // x increment is negative and out of bounds.
                var error = fromY;
                toY -= error;
                fromY = 0;
            }

            return NewInstance(fromX, fromY, toX, toY);
        }

        /// <inheritdoc cref="GetHashCode"/>
        public override int GetHashCode()
        {
            var result = 17;

            result = 31 * result + FromX.GetHashCode();
            result = 31 * result + FromY.GetHashCode();
            result = 31 * result + ToX.GetHashCode();
            result = 31 * result + ToY.GetHashCode();

            return result;
        }

        /// <inheritdoc cref="Equals"/>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is PointPair))
            {
                return false;
            }

            var pp = (PointPair) obj;

            return pp.FromX.Equals(FromX)
                   && pp.FromY.Equals(FromY)
                   && pp.ToX.Equals(ToX)
                   && pp.ToY.Equals(ToY);
        }
    }
}