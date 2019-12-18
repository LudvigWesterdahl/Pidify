using Pidify.Utils;
using System;
using System.Drawing;

namespace Pidify.Plottables
{
    /// <summary>
    /// Plots a signature line and a subtitle below.
    /// </summary>
    public sealed class SignaturePlottable : IPlottable
    {
        /// <summary>
        /// The width of the signature line.
        /// </summary>
        public float LineWidth { get; }

        /// <summary>
        /// The subtitle below the line.
        /// </summary>
        public string Subtitle { get; }

        /// <summary>
        /// The line info of the signature line.
        /// </summary>
        public LineInfo LineInfo { get; }

        /// <summary>
        /// The text info of the subtitle.
        /// </summary>
        public TextInfo TextInfo { get; }

        /// <summary>
        /// The start position of the signature line.
        /// </summary>
        public Tuple<float, float> StartAt { get; }

        /// <summary>
        /// Builder class to create <see cref="SignaturePlottable"/> instances.
        /// </summary>
        public sealed class Builder
        {
            /// <summary>
            /// The width of the signature line.
            /// </summary>
            public float LineWidth { get; }

            /// <summary>
            /// The subtitle below the line.
            /// </summary>
            public string Subtitle { get; }

            /// <summary>
            /// The line info of the signature line.
            /// </summary>
            public LineInfo LineInfo { get; private set; } = LineInfo.NewInstance(Color.FromArgb(66, 66, 66), 0.5f);

            /// <summary>
            /// The text info of the subtitle.
            /// </summary>
            public TextInfo TextInfo { get; private set; } = TextInfo.NewInstance(FontType.Helvetica, 8f);

            /// <summary>
            /// The start position of the signature line.
            /// </summary>
            public Tuple<float, float> StartAt { get; private set; } = Tuple.Create(0f, 0f);

            /// <summary>
            /// Private constructor.
            /// </summary>
            /// <param name="lineWidth">the width of the line</param>
            /// <param name="subtitle">the subtitle below</param>
            private Builder(float lineWidth, string subtitle)
            {
                LineWidth = lineWidth;
                Subtitle = subtitle;
            }

            /// <summary>
            /// Returns a new Builder instance.
            /// </summary>
            /// <param name="lineWidth">the width of the line</param>
            /// <param name="subtitle">the subtitle below</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewInstance(float lineWidth, string subtitle)
            {
                ValidationUtil.RequireBetween(lineWidth, 0, 1, $"lineWidth({lineWidth}) must be in the interval [0, 1]");
                ValidationUtil.RequirePositive(lineWidth, $"lineWidth({lineWidth}) must be greater than 0.");
                ValidationUtil.RequireNonNull(subtitle);

                return new Builder(lineWidth, subtitle);
            }

            /// <summary>
            /// Sets the line info of the signature line.
            /// <para>
            /// Defaults to a filled .5 thickness line with RGB(66, 66, 66).
            /// </para>
            /// </summary>
            /// <param name="lineInfo">the line info</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetLineInfo(LineInfo lineInfo)
            {
                LineInfo = ValidationUtil.RequireNonNull(lineInfo);

                return this;
            }

            /// <summary>
            /// Sets the text info of the subtitle.
            /// <para>
            /// Defaults to black Helvetica with font size 8.
            /// </para>
            /// </summary>
            /// <param name="textInfo">the text info</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetTextInfo(TextInfo textInfo)
            {
                TextInfo = ValidationUtil.RequireNonNull(textInfo);

                return this;
            }

            /// <summary>
            /// Sets the start position of the signature line by the upper left corner.
            /// <para>
            /// Defaults to 0, 0.
            /// </para>
            /// </summary>
            /// <param name="x">the x position of the upper left corner</param>
            /// <param name="y">the y position of the upper left corner</param>
            /// <returns></returns>
            public Builder SetStartAt(float x, float y)
            {
                ValidationUtil.RequireBetween(x, 0, 1, $"x({x}) must be in the interval [0, 1]");
                ValidationUtil.RequireBetween(y, 0, 1, $"y({y}) must be in the interval [0, 1]");

                StartAt = Tuple.Create(x, y);

                return this;
            }


            /// <summary>
            /// Creates a new <see cref="SignaturePlottable"/> from this Builder.
            /// </summary>
            /// <returns>new SignaturePlottable instance</returns>
            public SignaturePlottable Build()
            {
                return new SignaturePlottable(this);
            }

        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="builder">the Builder instance</param>
        private SignaturePlottable(Builder builder)
        {
            LineWidth = builder.LineWidth;
            Subtitle = builder.Subtitle;
            LineInfo = builder.LineInfo;
            TextInfo = builder.TextInfo;
            StartAt = builder.StartAt;

            ValidationUtil.RequireBetween(LineWidth + StartAt.Item1, 0, 1, $"LineWidth({LineWidth}) + StartAt.Item1({StartAt.Item1}) must be in the interval [0, 1]");
        }

        /// <inheritdoc cref="IPlottable"/>
        public void Plot(ICanvas canvas)
        {
            canvas.SetColor(LineInfo.Color);
            var line = PointPair.NewInstance(StartAt.Item1, StartAt.Item2, StartAt.Item1 + LineWidth, StartAt.Item2);
            canvas.DrawLine(line, LineInfo.Thickness, LineInfo.UnitsOn);

            canvas.SetColor(TextInfo.Color);
            canvas.SetFont(TextInfo.Type, TextInfo.Style, TextInfo.Size);
            var subtitleHeight = canvas.GetTextHeight(Subtitle);

            canvas.WriteText(Subtitle, StartAt.Item1, StartAt.Item2 + subtitleHeight * 0.9f);
        }
    }
}
