using Pidify.Utils;

namespace Pidify.Plottables
{
    /// <summary>
    /// Plots a line.
    /// </summary>
    public sealed class LinePlottable : IPlottable
    {
        /// <summary>
        /// Property for the line.
        /// </summary>
        public PointPair Line { get; }

        /// <summary>
        /// Property for the line info.
        /// </summary>
        public LineInfo Info { get; }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="line">the line</param>
        /// <param name="info">the line info</param>
        private LinePlottable(PointPair line, LineInfo info)
        {
            Line = line;
            Info = info;
        }

        /// <summary>
        /// Creates a new LinePlottable.
        /// </summary>
        /// <param name="line">the line</param>
        /// <param name="info">the line info</param>
        /// <returns>new LinePlottable instance</returns>
        public static LinePlottable NewInstance(PointPair line, LineInfo info)
        {
            ValidationUtil.RequireNonNull(line);
            ValidationUtil.RequireNonNull(info);
            return new LinePlottable(line, info);
        }

        /// <summary>
        /// Plots this LinePlottable on the <see cref="ICanvas"/>.
        /// </summary>
        /// <param name="canvas">the canvas to plot on</param>
        public void Plot(ICanvas canvas)
        {
            canvas.SetColor(Info.Color);
            canvas.DrawLine(Line, Info.Thickness, Info.UnitsOn);
        }
    }
}