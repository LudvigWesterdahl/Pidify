namespace Pidify
{
    /// <summary>
    /// A type that can plot itself on a <see cref="ICanvas"/>.
    /// </summary>
    public interface IPlottable
    {
        /// <summary>
        /// Plot this IPlottable on the canvas.
        /// </summary>
        /// <param name="canvas">the canvas to plot on</param>
        void Plot(ICanvas canvas);
    }
}
