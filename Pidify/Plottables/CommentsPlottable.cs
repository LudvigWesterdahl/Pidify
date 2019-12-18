using Pidify.Utils;
using System;
using System.Drawing;

namespace Pidify.Plottables
{
    /// <summary>
    /// Plots a title and comment lines for humans to write on.
    /// </summary>
    public sealed class CommentsPlottable : IPlottable
    {
        /// <summary>
        /// The space between the lines.
        /// </summary>
        public float LineSpacing { get; }   
        
        /// <summary>
        /// The title above the lines to the left.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The text information of the title.
        /// </summary>
        public TextInfo TextInfo { get; }

        /// <summary>
        /// The number of lines.
        /// </summary>
        public int NumLines { get; }

        /// <summary>
        /// The width of the lines.
        /// </summary>
        public float LineWidth;

        /// <summary>
        /// The line information.
        /// </summary>
        public LineInfo LineInfo { get; }

        /// <summary>
        /// The start position of the title if exists, otherwise the first line.
        /// </summary>
        public Tuple<float, float> StartAt { get; }

        /// <summary>
        /// Builder class to create <see cref="CommentsPlottable"/> instances.
        /// </summary>
        public sealed class Builder
        {
            /// <summary>
            /// The space between the lines.
            /// </summary>
            public float LineSpacing { get; }

            /// <summary>
            /// The title above the lines to the left.
            /// </summary>
            public string Title { get; }

            /// <summary>
            /// The text information of the title.
            /// </summary>
            public TextInfo TextInfo { get; }

            /// <summary>
            /// The number of lines.
            /// </summary>
            public int NumLines { get; private set; } = -1;

            /// <summary>
            /// The width of the lines.
            /// </summary>
            public float LineWidth = 1f;

            /// <summary>
            /// The line information.
            /// </summary>
            public LineInfo LineInfo { get; private set; } = LineInfo.NewInstance(Color.FromArgb(66, 66, 66), .5f);

            /// <summary>
            /// The start position of the title if exists, otherwise the first line.
            /// </summary>
            public Tuple<float, float> StartAt { get; private set; } = Tuple.Create(0f, 0f);

            /// <summary>
            /// Private constructor.
            /// </summary>
            /// <param name="lineSpacing">the space between the lines</param>
            /// <param name="title">the title; if empty no title is used</param>
            /// <param name="textInfo">the font information of the title</param>
            private Builder(float lineSpacing, string title, TextInfo textInfo)
            {
                LineSpacing = lineSpacing;
                Title = title;
                TextInfo = textInfo;
            }

            /// <summary>
            /// Returns a new Builder instance without a title.
            /// </summary>
            /// <param name="lineSpacing">the space between the lines</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewInstance(float lineSpacing)
            {
                return new Builder(lineSpacing, string.Empty, TextInfo.NewInstance(FontType.Helvetica, 1));
            }

            /// <summary>
            /// Returns a new Builder instance with the title to the left of the comment lines. One lineSpacing will be
            /// added under the title before the first line.
            /// </summary>
            /// <param name="lineSpacing">the space between the lines</param>
            /// <param name="title">the title; if empty no title is used</param>
            /// <param name="textInfo">the font information of the title</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewInstance(float lineSpacing, string title, TextInfo textInfo)
            {
                ValidationUtil.RequireBetween(lineSpacing, 0, 1, $"lineSpacing({lineSpacing}) must be in the interval [0, 1]");
                ValidationUtil.RequirePositive(lineSpacing, $"lineSpacing({lineSpacing}) must be greater than 0.");

                ValidationUtil.RequireNonNull(title);
                ValidationUtil.RequireNonNull(textInfo);

                return new Builder(lineSpacing, title, textInfo);
            }

            /// <summary>
            /// Sets the number of lines. This will result in crash if they cannot fit. Give -1 to mean as many as possible.
            /// <para>
            /// Defaults to -1 and a .5 thickness line with RGB(66, 66, 66) with width of 1f.
            /// </para>
            /// </summary>
            /// <param name="numLines">the number of lines</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetLines(int numLines)
            {
                return SetLines(numLines, 1f, LineInfo);
            }

            /// <summary>
            /// Sets the number of lines. This will result in crash if they cannot fit. Give -1 to mean as many as possible.
            /// <para>
            /// Defaults to -1 and a .5 thickness line with RGB(66, 66, 66) with width of 1f.
            /// </para>
            /// </summary>
            /// <param name="numLines">the number of lines</param>
            /// <param name="lineWidth">the width of the lines</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetLines(int numLines, float lineWidth)
            {
                return SetLines(numLines, lineWidth, LineInfo);
            }

            /// <summary>
            /// Sets the number of lines. This will result in crash if they cannot fit. Give -1 to mean as many as possible.
            /// <para>
            /// Defaults to -1 and a .5 thickness line with RGB(66, 66, 66) with width of 1f.
            /// </para>
            /// </summary>
            /// <param name="numLines">the number of lines</param>
            /// <param name="lineWidth">the width of the lines</param>
            /// <param name="lineInfo">the line info</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetLines(int numLines, float lineWidth, LineInfo lineInfo)
            {
                if (numLines != -1)
                {
                    ValidationUtil.RequireNonNegative(numLines, $"numLines({numLines}) must be greater or equal to -1");
                }
                ValidationUtil.RequireBetween(lineWidth, 0, 1, $"lineWidth({lineWidth}) must be in the interval [0, 1]");
                ValidationUtil.RequireNonNull(lineInfo);

                NumLines = numLines;
                LineWidth = lineWidth;
                LineInfo = lineInfo;

                return this;
            }

            /// <summary>
            /// Sets the start position of the upper left corner of the title if it exists or the first line if no title.
            /// <para>
            /// Defaults to (0, 0).
            /// </para>
            /// </summary>
            /// <param name="x">the x position of the upper left corner</param>
            /// <param name="y">the y position of the upper left corner</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetStartAt(float x, float y)
            {
                ValidationUtil.RequireBetween(x, 0, 1, $"x({x}) must be in the interval [0, 1]");
                ValidationUtil.RequireBetween(y, 0, 1, $"y({y}) must be in the interval [0, 1]");

                StartAt = Tuple.Create(x, y);

                return this;
            }

            /// <summary>
            /// Returns a new <see cref="CommentsPlottable"/> from this Builder.
            /// </summary>
            /// <returns>new CommentsPlottable instance</returns>
            public CommentsPlottable Build()
            {
                return new CommentsPlottable(this);
            }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="builder">the Builder instance</param>
        private CommentsPlottable(Builder builder)
        {
            LineSpacing = builder.LineSpacing;
            Title = builder.Title;
            TextInfo = builder.TextInfo;
            NumLines = builder.NumLines;
            LineWidth = builder.LineWidth;
            LineInfo = builder.LineInfo;
            StartAt = builder.StartAt;

            ValidationUtil.RequireBetween(LineWidth + StartAt.Item1, 0, 1, $"LineWidth({LineWidth}) + StartAt.Item1({StartAt.Item1}) must be in the interval [0, 1]");
        }

        /// <inheritdoc cref="IPlottable"/>
        public void Plot(ICanvas canvas)
        {
            var startLineX = StartAt.Item1;
            var endLineX = StartAt.Item1 + LineWidth;
            var startLineY = StartAt.Item2;

            if (!string.Empty.Equals(Title))
            {
                canvas.SetColor(TextInfo.Color);
                canvas.SetFont(TextInfo.Type, TextInfo.Style, TextInfo.Size);
                var titleHeight = canvas.GetTextHeight(Title);
                canvas.WriteText(Title, startLineX, startLineY + titleHeight * 0.85f);
                startLineY += titleHeight * 0.85f + LineSpacing;
            }

            canvas.SetColor(LineInfo.Color);

            var stopAtY = 1f;
            if (NumLines != -1)
            {
                stopAtY = startLineY + NumLines * LineSpacing;
            }

            for (var y = startLineY; y.CompareTo(stopAtY) <= 0; y += LineSpacing)
            {
                var line = PointPair.NewInstance(startLineX, y, endLineX, y);
                canvas.DrawLine(line, LineInfo.Thickness, LineInfo.UnitsOn);
            }
        }
    }
}
