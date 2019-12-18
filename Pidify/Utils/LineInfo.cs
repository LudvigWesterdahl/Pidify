using System.Drawing;

namespace Pidify.Utils
{
    /// <summary>
    /// Container type for the visual properties of a line.
    /// </summary>
    public sealed class LineInfo
    {
        /// <summary>
        /// Solid black line with thickness 1.
        /// </summary>
        public static LineInfo Default = NewInstance(Color.Black, 1, 1);

        /// <summary>
        /// 25% dashed black line with thickness 0.5.
        /// </summary>
        public static LineInfo DefaultDashed = NewInstance(Color.Black, .5f, .25f);

        /// <summary>
        /// The line color.
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// The line thickness.
        /// </summary>
        public float Thickness { get; }

        /// <summary>
        /// The percentage filled.
        /// </summary>
        public float UnitsOn { get; }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="color">the color</param>
        /// <param name="thickness">the thickness</param>
        /// <param name="unitsOn">the percentage filled</param>
        private LineInfo(Color color, float thickness, float unitsOn)
        {
            Color = color;
            Thickness = thickness;
            UnitsOn = unitsOn;
        }

        /// <summary>
        /// Returns a new <see cref="LineInfo"/> instance.
        /// <para>
        /// Same as calling <see cref="NewInstance(System.Drawing.Color, float, float)"/> with thickness = 1f and unitsOn = 1f.
        /// </para>
        /// </summary>
        /// <param name="color">the color of the line</param>
        /// <returns>a new LineInfo instance</returns>
        public static LineInfo NewInstance(Color color)
        {
            ValidationUtil.RequireNonNull(color);

            return NewInstance(color, 1f, 1f);
        }

        /// <summary>
        /// Returns a new <see cref="LineInfo"/> instance.
        /// <para>
        /// Same as calling <see cref="NewInstance(System.Drawing.Color, float, float)"/> with unitsOn = 1f.
        /// </para>
        /// </summary>
        /// <param name="color">the color of the line</param>
        /// <param name="thickness">the thickness of the line</param>
        /// <returns>a new LineInfo instance</returns>
        public static LineInfo NewInstance(Color color, float thickness)
        {
            ValidationUtil.RequireNonNull(color);
            ValidationUtil.RequirePositive(thickness, $"thickness({thickness}) must be positive (>0).");

            return NewInstance(color, thickness, 1f);
        }

        /// <summary>
        /// Returns a new <see cref="LineInfo"/> instance.
        /// </summary>
        /// <param name="color">the color of the line</param>
        /// <param name="thickness">the thickness of the line</param>
        /// <param name="unitsOn">percentage of line filled, rest is space (dash); 0-1</param>
        /// <returns>a new LineInfo instance</returns>
        public static LineInfo NewInstance(Color color, float thickness, float unitsOn)
        {
            ValidationUtil.RequireNonNull(color);
            ValidationUtil.RequirePositive(thickness, $"thickness({thickness}) must be positive (>0).");
            ValidationUtil.RequireBetween(unitsOn, 0, 1, $"unitsOn({unitsOn}) must be in the range [0, 1].");

            return new LineInfo(color, thickness, unitsOn);
        }
    }
}
