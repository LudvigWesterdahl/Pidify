using Pidify.Utils;
using System.Collections.Generic;
using System.Drawing;

namespace Pidify
{
    /// <summary>
    /// Container type for <see cref="IPlottable"/> instances that is used to define an area
    /// on the page that they are plotted inside.
    /// <para>
    /// With <see cref="ModeParam.Boxed"/> or <see cref="ModeParam.BoxedCalibration"/> the area
    /// draws a black border of thickness 1 around the box that the plottables are contained in.
    /// </para>
    /// </summary>
    public sealed class Area
    {
        private readonly List<IPlottable> plottables = new List<IPlottable>();

        private readonly PointPair box;

        private readonly int page;

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="box">the area on the page</param>
        /// <param name="page">the page id</param>
        private Area(PointPair box, int page)
        {
            this.box = box;
            this.page = page;
        }

        /// <summary>
        /// Creates a new Area defined by the box on the specified page.
        /// </summary>
        /// <param name="box">the area that the plottables will be plotted in</param>
        /// <param name="page">the page on the pdf</param>
        /// <returns>new Area instance</returns>
        public static Area NewInstance(PointPair box, int page)
        {
            ValidationUtil.RequireNonNull(box);

            return new Area(box, page);
        }

        /// <summary>
        /// Adds a IPlottable.
        /// </summary>
        /// <param name="plottable">the plottable to add</param>
        /// <returns>this instance for chaining</returns>
        public Area AddPlottable(IPlottable plottable)
        {
            ValidationUtil.RequireNonNull(plottable);

            plottables.Add(plottable);

            return this;
        }

        /// <summary>
        /// Draws all the added <see cref="IPlottable"/> instances.
        /// </summary>
        /// <param name="canvasConfig">the canvas config use to get the canvas for drawing</param>
        public void Draw(ICanvasConfig canvasConfig)
        {
            ValidationUtil.RequireNonNull(canvasConfig);

            canvasConfig.AddPage(page);

            var canvas = canvasConfig.UsePageArea(box, page);

            if (canvas.GetModeParam() == ModeParam.Boxed 
                || canvas.GetModeParam() == ModeParam.BoxedCalibration)
            {
                canvas.SetColor(Color.Black);
                canvas.DrawRectangle(PointPair.Full, 1f, 1f);
            }

            foreach (var plottable in plottables)
            {
                plottable.Plot(canvas);
            }
        }
    }
}
