using Pidify.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Pidify.Plottables
{
    /// <summary>
    /// Plots a graph.
    /// </summary>
    public sealed class GraphPlottable : IPlottable
    {
        private readonly List<Tuple<List<float>, LineInfo, List<Tuple<int, Bitmap>>>> lines;

        /// <summary>
        /// Gets the lines plotted.
        /// </summary>
        public List<Tuple<List<float>, LineInfo, List<Tuple<int, Bitmap>>>> Lines => new List<Tuple<List<float>, LineInfo, List<Tuple<int, Bitmap>>>>(lines);

        /// <summary>
        /// Gets the number of markers on the x-axis.
        /// </summary>
        public int NumMarkersAxisX { get; }

        /// <summary>
        /// Gets the number of markers on the y-axis.
        /// </summary>
        public int NumMarkersAxisY { get; }

        /// <summary>
        /// Gets the x-axis marker transformation function.
        /// </summary>
        public Func<int, string> AxisX { get; }

        /// <summary>
        /// Gets the y-axis marker transformation function.
        /// </summary>
        public Func<float, string> AxisY { get; }

        /// <summary>
        /// Gets the grid.
        /// </summary>
        public LineInfo Grid { get; }

        /// <summary>
        /// Gets the number of vertical grid lines.
        /// </summary>
        public int NumVertical { get; }

        /// <summary>
        /// Gets the number of horizontal grid lines.
        /// </summary>
        public int NumHorizontal { get; }

        /// <summary>
        /// Gets if the vertical grid lines should be against the markers on the x-axis.
        /// </summary>
        public bool AgainstMarkersX { get; }

        /// <summary>
        /// Gets if the horizontal grid lines should be against the markers on the y-axis.
        /// </summary>
        public bool AgainstMarkersY { get; }

        /// <summary>
        /// Gets the background of the graph.
        /// </summary>
        public Color? Background { get; }

        /// <summary>
        /// Gets the border of the graph.
        /// </summary>
        public LineInfo Border { get; }

        /// <summary>
        /// Gets the x-axis limits.
        /// </summary>
        public Tuple<int, int> AxisLimitX { get; }

        /// <summary>
        /// Gets the y-axis limits.
        /// </summary>
        public Tuple<float, float> AxisLimitY { get; }

        private readonly List<Tuple<string, TextInfo, float, float>> legends;

        /// <summary>
        /// Gets the legends in the graph.
        /// </summary>
        public List<Tuple<string, TextInfo, float, float>> Legends => new List<Tuple<string, TextInfo, float, float>>(legends);

        private readonly List<Tuple<LineInfo, float, string>> horizontalLines;
        
        /// <summary>
        /// Gets the horizontal lines in the graph.
        /// </summary>
        public List<Tuple<LineInfo, float, string>> HorizontalLines => new List<Tuple<LineInfo, float, string>>(horizontalLines);

        private readonly List<Tuple<LineInfo, int, string>> verticalLines;

        /// <summary>
        /// Gets the vertical lines in the graph.
        /// </summary>
        public List<Tuple<LineInfo, int, string>> VerticalLines => new List<Tuple<LineInfo, int, string>>(verticalLines);

        /// <summary>
        /// Gets the horizontal fillers in the graph.
        /// </summary>
        public Tuple<float, float, Color> HorizontalFill { get; }


        /// <summary>
        /// Builder class to create <see cref="GraphPlottable"/> instances.
        /// </summary>
        public sealed class Builder
        {

            private readonly List<Tuple<List<float>, LineInfo, List<Tuple<int, Bitmap>>>> lines = new List<Tuple<List<float>, LineInfo, List<Tuple<int, Bitmap>>>>();
            /// <summary>
            /// Gets the lines plotted.
            /// </summary>
            public List<Tuple<List<float>, LineInfo, List<Tuple<int, Bitmap>>>> Lines  => new List<Tuple<List<float>, LineInfo, List<Tuple<int, Bitmap>>>>(lines);

            /// <summary>
            /// Gets the number of markers on the x-axis.
            /// </summary>
            public int NumMarkersAxisX { get; private set; }

            /// <summary>
            /// Gets the number of markers on the y-axis.
            /// </summary>
            public int NumMarkersAxisY { get; private set; }

            /// <summary>
            /// Gets the x-axis marker transformation function.
            /// </summary>
            public Func<int, string> AxisX { get; private set; }

            /// <summary>
            /// Gets the y-axis marker transformation function.
            /// </summary>
            public Func<float, string> AxisY { get; private set; }

            /// <summary>
            /// Gets the grid.
            /// </summary>
            public LineInfo Grid { get; private set; }

            /// <summary>
            /// Gets the number of vertical grid lines.
            /// </summary>
            public int NumVertical { get; private set; }

            /// <summary>
            /// Gets the number of horizontal grid lines.
            /// </summary>
            public int NumHorizontal { get; private set; }

            /// <summary>
            /// Gets if the vertical grid lines should be against the markers on the x-axis.
            /// </summary>
            public bool AgainstMarkersX { get; private set; }

            /// <summary>
            /// Gets if the horizontal grid lines should be against the markers on the y-axis.
            /// </summary>
            public bool AgainstMarkersY { get; private set; }

            /// <summary>
            /// Gets the background of the graph.
            /// </summary>
            public Color? Background { get; private set; }

            /// <summary>
            /// Gets the border of the graph.
            /// </summary>
            public LineInfo Border { get; private set; }

            /// <summary>
            /// Gets the x-axis limits.
            /// </summary>
            public Tuple<int, int> AxisLimitX { get; private set; }

            /// <summary>
            /// Gets the y-axis limits.
            /// </summary>
            public Tuple<float, float> AxisLimitY { get; private set; }

            private readonly List<Tuple<string, TextInfo, float, float>> legends = new List<Tuple<string, TextInfo, float, float>>();

            /// <summary>
            /// Gets the legends in the graph.
            /// </summary>
            public List<Tuple<string, TextInfo, float, float>> Legends => new List<Tuple<string, TextInfo, float, float>>(legends);

            private readonly List<Tuple<LineInfo, float, string>> horizontalLines = new List<Tuple<LineInfo, float, string>>();

            /// <summary>
            /// Gets the horizontal lines in the graph.
            /// </summary>
            public List<Tuple<LineInfo, float, string>> HorizontalLines => new List<Tuple<LineInfo, float, string>>(horizontalLines);

            private readonly List<Tuple<LineInfo, int, string>> verticalLines = new List<Tuple<LineInfo, int, string>>();

            /// <summary>
            /// Gets the vertical lines in the graph.
            /// </summary>
            public List<Tuple<LineInfo, int, string>> VerticalLines => new List<Tuple<LineInfo, int, string>>(verticalLines);

            /// <summary>
            /// Gets the horizontal fillers in the graph.
            /// </summary>
            public Tuple<float, float, Color> HorizontalFill { get; private set; }

            /// <summary>
            /// Constructor
            /// </summary>
            private Builder()
            {
                // empty
            }
            
            /// <summary>
            /// Returns a new Builder instance.
            /// </summary>
            /// <returns>new Builder instance</returns>
            public static Builder NewInstance()
            {
                return new Builder();
            }

            /// <summary>
            /// Adds a line with the specified line information.
            /// </summary>
            /// <param name="points">the values</param>
            /// <param name="line">the design</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if any argument is null or points contain less than 2 values</exception>
            public Builder AddLine(List<float> points, LineInfo line)
            {
                return AddLine(points, line, new List<Tuple<int, Bitmap>>());
            }

            /// <summary>
            /// Adds a line with the specified line information.
            /// </summary>
            /// <param name="points">the values</param>
            /// <param name="line">the design</param>
            /// <param name="lineMarkers">marker above the line; (point index, image)</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if any arguments is null or points contain less than 2 values</exception>
            public Builder AddLine(List<float> points, LineInfo line, List<Tuple<int, Bitmap>> lineMarkers)
            {
                ValidationUtil.RequireNonNull(points);
                ValidationUtil.RequireNonNull(line);
                ValidationUtil.RequirePositive(points.Count - 1, "There must be at least two values in points.");
                ValidationUtil.RequireNonNull(lineMarkers);

                if (lineMarkers.Any(t => t == null))
                {
                    throw new ArgumentException("No line marker can be null.");
                }

                if (lineMarkers.Any(t => t.Item2 == null))
                {
                    throw new ArgumentException("No line marker image can be null.");

                }

                if (lineMarkers.Select(t => t.Item1).Any(i => i >= points.Count || i < 0))
                {
                    throw new ArgumentException("No line marker can have an index outside of the points.");
                }

                lines.Add(new Tuple<List<float>, LineInfo, List<Tuple<int, Bitmap>>>(points, line, lineMarkers));

                return this;
            }

            /// <summary>
            /// Sets the numerical markers on the x and y axis.
            /// </summary>
            /// <param name="numAxisX">number of markers on x axis</param>
            /// <param name="numAxisY">number of markers on y axis</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if any argument is negative</exception>
            public Builder SetAxisMarkers(int numAxisX, int numAxisY)
            {
                NumMarkersAxisX = ValidationUtil.RequireNonNegative(numAxisX, $"numAxisX({numAxisX}) cannot be negative.");
                NumMarkersAxisY = ValidationUtil.RequireNonNegative(numAxisY, $"numAxisY({numAxisY}) cannot be negative.");

                return SetAxisMarkers(numAxisX, numAxisY,
                    i =>
                    {
                        return i.ToString("0", System.Globalization.CultureInfo.CurrentCulture);
                    },
                    value =>
                    {
                        var marker = value.ToString("0.###", System.Globalization.CultureInfo.CurrentCulture);
                        if (value > 1f)
                        {
                            marker = value.ToString("0.#", System.Globalization.CultureInfo.CurrentCulture);
                        }

                        return marker;
                    });
            }

            /// <summary>
            /// Sets the numerical markers on the x and y axis.
            /// </summary>
            /// <param name="numAxisX">number of markers on x axis</param>
            /// <param name="numAxisY">number of markers on y axis</param>
            /// <param name="axisX">function to transform index value to any marker</param>
            /// <param name="axisY">function to transform value to any marker</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if any argument is negative</exception>
            public Builder SetAxisMarkers(int numAxisX, int numAxisY, Func<int, string> axisX, Func<float, string> axisY)
            {
                NumMarkersAxisX = ValidationUtil.RequireNonNegative(numAxisX, $"numAxisX({numAxisX}) cannot be negative.");
                NumMarkersAxisY = ValidationUtil.RequireNonNegative(numAxisY, $"numAxisY({numAxisY}) cannot be negative.");
                AxisX = ValidationUtil.RequireNonNull(axisX);
                AxisY = ValidationUtil.RequireNonNull(axisY);

                return this;
            }

            /// <summary>
            /// Sets a grid in the graph with automatic number of horizontal lines to make a grid of squares.
            /// <para>
            /// Note that the last call to any AddGrid method is applied, rest is ignored.
            /// </para>
            /// </summary>
            /// <param name="grid">the grid lines information</param>
            /// <param name="numVertical">number of vertical lines</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if grid is null</exception>
            public Builder SetGrid(LineInfo grid, int numVertical)
            {
                Grid = ValidationUtil.RequireNonNull(grid);
                NumVertical = ValidationUtil.RequirePositive(numVertical, $"numVertical({numVertical}) must be positive..");
                NumHorizontal = -1;

                AgainstMarkersX = false;
                AgainstMarkersY = false;

                return this;
            }

            /// <summary>
            /// Sets a grid in the graph.
            /// <para>
            /// Note that the last call to any AddGrid method is applied, rest is ignored.
            /// </para>
            /// </summary>
            /// <param name="grid">the grid lines information</param>
            /// <param name="numVertical">number of vertical lines</param>
            /// <param name="numHorizontal">number of horizontal lines</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if grid is null</exception>
            public Builder SetGrid(LineInfo grid, int numVertical, int numHorizontal)
            {
                Grid = ValidationUtil.RequireNonNull(grid);
                NumVertical = ValidationUtil.RequireNonNegative(numVertical, $"numAxisY({numVertical}) cannot be negative.");
                NumHorizontal = ValidationUtil.RequireNonNegative(numHorizontal, $"numHorizontal({numHorizontal}) cannot be negative.");

                AgainstMarkersX = false;
                AgainstMarkersY = false;

                return this;
            }

            /// <summary>
            /// Sets a grid in the graph where the vertical lines (againstMarkersX) and horizontal lines (againstMarkersY)
            /// are going out from the numerical markers as set by <see cref="SetAxisMarkers(int,int)"/> or <see cref="SetAxisMarkers(int,int,Func{int,string},Func{float,string})"/>
            /// <para>
            /// Note that the last call to any AddGrid method is applied, rest is ignored.
            /// </para>
            /// </summary>
            /// <param name="grid">the grid lines information</param>
            /// <param name="againstMarkersX">if vertical lines from the markers on the x axis</param>
            /// <param name="againstMarkersY">if horizontal lines from the markers on the y axis</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if grid is null</exception>
            public Builder SetGrid(LineInfo grid, bool againstMarkersX, bool againstMarkersY)
            {
                Grid = ValidationUtil.RequireNonNull(grid);
                AgainstMarkersX = againstMarkersX;
                AgainstMarkersY = againstMarkersY;

                NumVertical = 0;
                NumHorizontal = 0;

                return this;
            }

            /// <summary>
            /// Sets a background.
            /// </summary>
            /// <param name="background">background color; defaults to null, meaning no background</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetBackground(Color background)
            {
                Background = background;

                return this;
            }

            /// <summary>
            /// Adds a border around the graph.
            /// </summary>
            /// <param name="border">border information; defaults to null, meaning no border</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetBorder(LineInfo border)
            {
                Border = border;

                return this;
            }

            /// <summary>
            /// Adds axis limits to the graph and thus to all lines added. Note that the xAxis is indexes that acts as a "valid"
            /// index window for all lines.
            /// </summary>
            /// <param name="xAxis">the limits on the x-axis; defaults to null, meaning to fit the lines</param>
            /// <param name="yAxis">the limits on the y-axis; defaults to null, meaning to fit the lines</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if any lower limit is equal or greater than the upper limit or lower x axis limit is negative</exception>
            public Builder SetAxisLimits(Tuple<int, int> xAxis, Tuple<float, float> yAxis)
            {
                
                if (xAxis != null)
                {
                    ValidationUtil.RequireNonNegative(xAxis.Item1, $"Lower x-axis limit({xAxis.Item1}) cannot be negative.");
                    ValidationUtil.RequirePositive(xAxis.Item2 - xAxis.Item1, $"Upper x-axis limit({xAxis.Item2}) must be greater than lower x-axis limit({xAxis.Item1}).");
                }

                if (yAxis != null)
                {
                    ValidationUtil.RequirePositive(yAxis.Item2 - yAxis.Item1, $"Upper y-axis limit({yAxis.Item2}) must be greater than lower y-axis limit({yAxis.Item1}).");
                }

                AxisLimitX = xAxis;
                AxisLimitY = yAxis;

                return this;
            }

            /// <summary>
            /// Adds a legend to the graph.
            /// </summary>
            /// <param name="text">the text</param>
            /// <param name="info">the text display info</param>
            /// <param name="startX">the start x position of the top left corner</param>
            /// <param name="startY">the start y position of the top left corner</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if text or info is null or startX or startY not in 0,1 interval</exception>
            public Builder AddLegend(string text, TextInfo info, float startX, float startY)
            {
                ValidationUtil.RequireNonNull(text);
                ValidationUtil.RequireNonNull(info);
                ValidationUtil.RequireBetween(startX, 0f, 1f, $"startX({startX}) has to be in the interval [0, 1].");
                ValidationUtil.RequireBetween(startY, 0f, 1f, $"startY({startY}) has to be in the interval [0, 1].");


                legends.Add(Tuple.Create(text, info, startX, startY));

                return this;
            }

            /// <summary>
            /// Adds a horizontal line across the graph.
            /// </summary>
            /// <param name="line">the line</param>
            /// <param name="yValue">the y value to start the line it</param>
            /// <param name="text">the marker to the left of the line</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if line is null</exception>
            public Builder AddHorizontalLine(LineInfo line, float yValue, string text)
            {
                ValidationUtil.RequireNonNull(line);

                horizontalLines.Add(Tuple.Create(line, yValue, text));

                return this;
            }

            /// <summary>
            /// Adds a vertical line across the graph.
            /// </summary>
            /// <param name="line">the line</param>
            /// <param name="xValue">the x value to start the line at</param>
            /// <param name="text">the marker below the line</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if line is null</exception>
            public Builder AddVerticalLine(LineInfo line, int xValue, string text)
            {
                ValidationUtil.RequireNonNull(line);

                verticalLines.Add(Tuple.Create(line, xValue, text));

                return this;
            }

            /// <summary>
            /// Fills the rectangle in the graph as specified by the two y-values.
            /// </summary>
            /// <param name="fromYValue">the lower y value</param>
            /// <param name="toYValue">the upper y value</param>
            /// <param name="color">the color</param>
            /// <returns>this Builder for chaining</returns>
            /// <exception cref="ArgumentException">if color is null or fromYValue is equal or greater than toYValue</exception>
            public Builder SetHorizontalFill(float fromYValue, float toYValue, Color color)
            {
                ValidationUtil.RequireNonNull(color);
                ValidationUtil.RequirePositive(toYValue - fromYValue, $"toYValue({toYValue}) cannot be less than or equal to fromYValue({fromYValue}).");

                HorizontalFill = Tuple.Create(fromYValue, toYValue, color);

                return this;
            }

            /// <summary>
            /// Returns a new  <see cref="GraphPlottable"/> instance from this Builder.
            /// </summary>
            /// <returns>new GraphPlottable instance</returns>
            public GraphPlottable Build()
            {
                ValidationUtil.RequirePositive(Lines.Count, "There must be at least one line given to plot.");
                return new GraphPlottable(this);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="builder">the builder instance</param>
        private GraphPlottable(Builder builder)
        {
            NumMarkersAxisX = builder.NumMarkersAxisX;
            NumMarkersAxisY = builder.NumMarkersAxisY;
            AxisX = builder.AxisX;
            AxisY = builder.AxisY;
            Grid = builder.Grid;
            NumVertical = builder.NumVertical;
            NumHorizontal = builder.NumHorizontal;
            AgainstMarkersX = builder.AgainstMarkersX;
            AgainstMarkersY = builder.AgainstMarkersY;
            Background = builder.Background;
            Border = builder.Border;
            legends = builder.Legends;
            horizontalLines = builder.HorizontalLines;
            verticalLines = builder.VerticalLines;
            HorizontalFill = builder.HorizontalFill;

            //
            // Calculates the axis limits if they are null.
            //
            if (builder.AxisLimitX == null)
            {
                var maxX = builder.Lines.Select(tup => tup.Item1.Count).Max();

                AxisLimitX = Tuple.Create(0, maxX);
            }
            else
            {
                AxisLimitX = builder.AxisLimitX;
            }

            if (builder.AxisLimitY == null)
            {
                var minValue = builder.Lines.Select(tup => tup.Item1.Min()).Min();
                var maxValue = builder.Lines.Select(tup => tup.Item1.Max()).Max();

                AxisLimitY = Tuple.Create(minValue, maxValue);
            }
            else
            {
                AxisLimitY = builder.AxisLimitY;
            }

            //
            // Scales the lines according to the axis limits.
            //
            var scaledX = builder.Lines.Select(l =>
            {
                var newPoints = l.Item1
                    .Skip(AxisLimitX.Item1)
                    .Take(AxisLimitX.Item2 - AxisLimitX.Item1)
                    .ToList();
                return new Tuple<List<float>, LineInfo, List<Tuple<int, Bitmap>>>(newPoints, l.Item2, l.Item3);
            }).ToList();

            var scaledY = scaledX.Select(l =>
            {
                var newPoints = l.Item1
                    .Select(v =>
                    {
                        if (v.CompareTo(AxisLimitY.Item1) < 0)
                        {
                            return AxisLimitY.Item1;
                        }

                        return v;
                    })
                    .Select(v =>
                    {
                        if (v.CompareTo(AxisLimitY.Item2) > 0)
                        {
                            return AxisLimitY.Item2;
                        }

                        return v;
                    })
                    .ToList();
                return new Tuple<List<float>, LineInfo, List<Tuple<int, Bitmap>>>(newPoints, l.Item2, l.Item3);
            }).ToList();

            lines = scaledY;
        }

        /// <inheritdoc cref="ICanvas"/>
        public void Plot(ICanvas canvas)
        {
            // Extra padding for the x and y markers.
            const float extraPadding = 0.025f;

            var size = canvas.GetCurrentDrawingSpace();

            var maxValue = AxisLimitY.Item2;
            // Avoids floating point errors.
            maxValue += Math.Abs(maxValue) * 0.01f;

            var minValue = AxisLimitY.Item1;
            // Avoids floating point errors.
            minValue -= Math.Abs(minValue) * 0.01f;

            var intervalValue = maxValue - minValue;
            var numPoints = AxisLimitX.Item2 - AxisLimitX.Item1;

            // The area that the border, grid and graph will be contained inside.
            var graphArea = PointPair.Full;

            //
            // Calculates and draws the markers.
            //
            canvas.SetFont(FontType.Helvetica, FontStyle.Regular, 8f);

            // Map of markers and their width.
            var markerWidthX = new Dictionary<string, float>();
            // The maximum height of the markers to update the graphArea and the markers position.
            var maxTextHeight = 0f;
            for (var i = 1f; i <= NumMarkersAxisX; i++)
            {
                var ratio = i / (NumMarkersAxisX + 1);
                //var marker = (ratio * numPoints).ToString("0", System.Globalization.CultureInfo.CurrentCulture);
                var marker = AxisX((int) (ratio * numPoints));

                var height = canvas.GetTextHeight(marker);
                var width = canvas.GetTextWidth(marker);

                maxTextHeight = Math.Max(maxTextHeight, height + extraPadding);
                markerWidthX[marker] = width;
            }

            foreach (var verticalLine in VerticalLines)
            {
                var height = canvas.GetTextHeight(verticalLine.Item3);

                maxTextHeight = Math.Max(maxTextHeight, height + extraPadding);
            }

            // Updates the graphArea to account for the maximum text height of the markers.
            graphArea = PointPair.NewInstance(graphArea.FromX, graphArea.FromY, graphArea.ToX, graphArea.ToY - maxTextHeight);

            // List of the x-axis markers x, y, center x and center y position to allow aligning the grid lines.
            var markersX = new List<PointPair>();
            var markerXIndex = 1f;
            foreach (var mw in markerWidthX)
            {
                var centerX = markerXIndex / (NumMarkersAxisX + 1);
                var centerY = 1 - maxTextHeight / 2;
                var x = centerX - mw.Value / 2;
                var y = 1f - extraPadding / 2;
                markersX.Add(PointPair.NewInstance(x, y, centerX, centerY));
                markerXIndex++;

                // Draws the marker.
                canvas.SetColor(Color.Black);
                canvas.WriteText(mw.Key, x, y);
            }

            // Map of markers and their height.
            var markerHeightY = new Dictionary<string, float>();
            // The maximum width of the markers to update the graphArea and the markers position.
            var maxTextWidth = 0f;
            for (var i = 1f; i <= NumMarkersAxisY; i++)
            {
                var ratio = i / (NumMarkersAxisY + 1);
                var value = ratio * intervalValue + minValue;
                var marker = AxisY(value);

                var height = canvas.GetTextHeight(marker);
                var width = canvas.GetTextWidth(marker);

                maxTextWidth = Math.Max(maxTextWidth, width + extraPadding);
                markerHeightY[marker] = height;
            }

            foreach (var horizontalLine in HorizontalLines)
            {
                var width = canvas.GetTextWidth(horizontalLine.Item3);

                maxTextWidth = Math.Max(maxTextWidth, width + extraPadding);
            }

            // Updates the graphArea to account for the maximum text width of the markers.
            graphArea = PointPair.NewInstance(graphArea.FromX + maxTextWidth, graphArea.FromY, graphArea.ToX, graphArea.ToY);

            // List of the y-axis markers x, y, center x and center y position to allow aligning the grid lines.
            var markersY = new List<PointPair>();
            var markerYIndex = 1f;
            foreach (var mw in markerHeightY)
            {
                var centerX = maxTextWidth / 2;
                var centerY = 1 - markerYIndex / (NumMarkersAxisY + 1);
                var x = 0 + extraPadding / 2;
                //var y = centerY - mw.Value / 2;
                var y = centerY;
                markersY.Add(PointPair.NewInstance(x, y, centerX, centerY));
                markerYIndex++;

                // Draws the marker.
                canvas.SetColor(Color.Black);
                canvas.WriteText(mw.Key, x, y);
            }

            //
            // Draws the background.
            //
            if (Background.HasValue)
            {
                canvas.SetColor(Background.Value);
                canvas.DrawRectangle(graphArea);
            }

            //
            // Draws the grid
            //
            if (Grid != null)
            {
                canvas.SetColor(Grid.Color);

                if (AgainstMarkersX || AgainstMarkersY)
                {
                    if (AgainstMarkersX)
                    {
                        
                        foreach (var marker in markersX)
                        {
                            var line = PointPair.NewInstance(
                                marker.ToX, 
                                marker.ToY - maxTextHeight / 2,
                                marker.ToX,
                                0f
                            );
                            canvas.DrawLine(line, Grid.Thickness, Grid.UnitsOn);
                        }
                        
                    }

                    if (AgainstMarkersY)
                    {
                        
                        foreach (var marker in markersY)
                        {
                            var line = PointPair.NewInstance(
                                marker.ToX + maxTextWidth / 2,
                                marker.ToY,
                                1f,
                                marker.ToY
                            );
                            canvas.DrawLine(line, Grid.Thickness, Grid.UnitsOn);
                        }
                        
                    }
                }
                else if ((NumVertical != 0 || NumHorizontal != 0) && NumHorizontal != -1)
                {
                    for (var i = 1; i <= NumVertical; i++)
                    {
                        var rawX = i / (NumVertical + 1f);
                        var x = rawX * graphArea.Width + graphArea.FromX;
                        var line = PointPair.NewInstance(x, graphArea.FromY, x, graphArea.ToY);
                        canvas.DrawLine(line, Grid.Thickness, Grid.UnitsOn);
                    }

                    for (var i = 1; i <= NumHorizontal; i++)
                    {
                        var rawY = i / (NumHorizontal + 1f);
                        var y = rawY * graphArea.Height + graphArea.FromY;
                        var line = PointPair.NewInstance(graphArea.FromX, y, graphArea.ToX, y);
                        canvas.DrawLine(line, Grid.Thickness, Grid.UnitsOn);
                    }
                }

                else if (NumVertical != 0 && NumHorizontal == -1)
                {
                    var incX = 1f / (NumVertical + 1);
                    var widthHeightRatio = (graphArea.Width / graphArea.Height) * (size.Width / size.Height);
                    var incY = incX * widthHeightRatio;

                    for (var inc = incX * graphArea.Width; inc.CompareTo(graphArea.Width) < 0; inc += incX * graphArea.Width)
                    {
                        var x = inc + graphArea.FromX;
                        var line = PointPair.NewInstance(x, graphArea.FromY, x, graphArea.ToY);
                        canvas.DrawLine(line, Grid.Thickness, Grid.UnitsOn);
                    }

                    for (var inc = incY * graphArea.Height; inc.CompareTo(graphArea.Height) < 0; inc += incY * graphArea.Height)
                    {
                        var y = graphArea.ToY - inc;
                        var line = PointPair.NewInstance(graphArea.FromX, y, graphArea.ToX, y);
                        canvas.DrawLine(line, Grid.Thickness, Grid.UnitsOn);
                    }
                }
            }

            //
            // Draws the vertical and horizontal lines
            //
            canvas.SetFont(FontType.Helvetica, FontStyle.Regular, 8f);

            if (HorizontalFill != null)
            {
                var fromYValue = HorizontalFill.Item1;
                var toYValue = HorizontalFill.Item2;
                var color = HorizontalFill.Item3;

                if (fromYValue.CompareTo(minValue) >= 0
                    && fromYValue.CompareTo(maxValue) <= 0
                    && toYValue.CompareTo(minValue) >= 0
                    && toYValue.CompareTo(maxValue) <= 0)
                {
                    var ratioFrom = 1 - (fromYValue - minValue) / intervalValue;
                    var ratioTo = 1 - (toYValue - minValue) / intervalValue;
                    var box = PointPair.NewInstance(
                        graphArea.FromX,
                        ratioFrom * graphArea.Height + graphArea.FromY,
                        graphArea.ToX,
                        ratioTo * graphArea.Height + graphArea.FromY
                    );
                    canvas.SetColor(color);
                    canvas.DrawRectangle(box);
                }
            }

            foreach (var horizontalLine in HorizontalLines)
            {
                var info = horizontalLine.Item1;
                var yValue = horizontalLine.Item2;
                var text = horizontalLine.Item3;

                if (yValue.CompareTo(minValue) < 0 || yValue.CompareTo(maxValue) > 0)
                {
                    continue;
                }

                var ratioY = 1 - (yValue - minValue) / intervalValue;
                var line = PointPair.NewInstance(
                    graphArea.FromX,
                    ratioY * graphArea.Height + graphArea.FromY,
                    graphArea.ToX,
                    ratioY * graphArea.Height + graphArea.FromY
                );

                var fromX = 0 + extraPadding / 2;
                var height = canvas.GetTextHeight(text);
                var fromY = line.FromY - height / 2;

                canvas.SetColor(Color.Black);
                canvas.WriteText(text, fromX, fromY + height * 0.85f);

                canvas.SetColor(info.Color);
                canvas.DrawLine(line, info.Thickness, info.UnitsOn);
            }

            foreach (var verticalLine in VerticalLines)
            {
                var info = verticalLine.Item1;
                var xValue = verticalLine.Item2;
                var text = verticalLine.Item3;

                var ratioX = 1f * xValue / (numPoints - 1);

                if (xValue < 0 || xValue >= numPoints)
                {
                    continue;
                }


                var line = PointPair.NewInstance(
                    ratioX * graphArea.Width + graphArea.FromX,
                    graphArea.FromY,
                    ratioX * graphArea.Width + graphArea.FromX,
                    graphArea.ToY
                );

                var fromY = 1 - extraPadding / 2;
                var width = canvas.GetTextWidth(text);
                var fromX = Math.Max(0, line.FromX - width / 2);


                canvas.SetColor(Color.Black);
                canvas.WriteText(text, fromX, fromY);

                canvas.SetColor(info.Color);
                canvas.DrawLine(line, info.Thickness, info.UnitsOn);
            }

            //
            // Draws the points.
            //

            foreach (var graphLine in Lines)
            {
                var points = graphLine.Item1;
                var lineInfo = graphLine.Item2;
                var lineMarkers = graphLine.Item3;
                canvas.SetColor(lineInfo.Color);

                for (var i = 1; i < points.Count; i++)
                {
                    var from = points.ElementAt(i - 1);
                    var to = points.ElementAt(i);
                    var fromRatioX = (i - 1f) / (numPoints - 1);
                    var toRatioX = i * 1f / (numPoints - 1);
                    var fromRatioY = 1 - (from - minValue) / intervalValue;
                    var toRatioY = 1 - (to - minValue) / intervalValue;

                    var line = PointPair.NewInstance(
                        fromRatioX * graphArea.Width + graphArea.FromX,
                        fromRatioY * graphArea.Height + graphArea.FromY,
                        toRatioX * graphArea.Width + graphArea.FromX,
                        toRatioY * graphArea.Height + graphArea.FromY
                    );
                    canvas.DrawLine(line, lineInfo.Thickness, lineInfo.UnitsOn);

                    if (i == 1)
                    {
                        foreach (var lineMarker in lineMarkers.Where(t => t.Item1 == 0))
                        {
                            var pngImage = lineMarker.Item2;
                            var height = canvas.GetImageHeight(pngImage);
                            var width = height * canvas.GetImageWidthHeightRatio(pngImage);

                            var fromX = Math.Max(0, line.FromX - width / 2);
                            var fromY = Math.Max(0, line.FromY - height * 1.1f);
                            var toX = Math.Min(1, line.FromX + width / 2);
                            var toY = Math.Max(0, line.FromY - height * 0.1f);

                            var imageBox = PointPair.NewInstance(fromX, fromY, toX, toY);

                            canvas.DrawImage(pngImage, imageBox);
                        }
                    }

                    foreach (var lineMarker in lineMarkers.Where(t => t.Item1 == i))
                    {
                        var canvasRatio = size.Width / size.Height;
                        var pngImage = lineMarker.Item2;
                        var height = canvas.GetImageHeight(pngImage);
                        var width = height * canvas.GetImageWidthHeightRatio(pngImage) / canvasRatio;

                        var fromX = Math.Max(0, line.ToX - width / 2);
                        var fromY = line.ToY - height - 0.025f;
                        if (fromY.CompareTo(0f) < 0)
                        {
                            fromY = line.ToY + height + 0.025f;
                        }
                        var toX   = Math.Min(1, line.ToX + width / 2);
                        var toY = fromY + height;

                        var imageBox = PointPair.NewInstance(fromX, fromY, toX, toY);

                        canvas.DrawImage(pngImage, imageBox);
                    }
                }
            }

            //
            // Draws the legend.
            //
            foreach (var legend in Legends)
            {
                var text = legend.Item1;
                var info = legend.Item2;
                var startX = legend.Item3;
                var startY = legend.Item4;
                canvas.SetColor(info.Color);
                canvas.SetFont(info.Type, info.Style, info.Size);
                // Don't move this above canvas.SetFont or the height won't be calculated correctly.
                var textHeight = canvas.GetTextHeight(text);
                try
                {
                    canvas.WriteText(text, startX, startY + textHeight * 0.85f);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            //
            // Draws the border.
            //
            if (Border != null)
            {
                canvas.SetColor(Border.Color);
                canvas.DrawRectangle(graphArea, Border.Thickness, Border.UnitsOn);
            }
        }
    }
}
