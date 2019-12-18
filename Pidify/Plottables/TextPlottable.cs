using Pidify.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace Pidify.Plottables
{
    /// <summary>
    /// Plots a text string.
    /// <para>
    /// See <see cref="Builder"/> for how the different placement arguments affect the location of the image.
    /// </para>
    /// <para>
    /// With <see cref="ModeParam.Calibration"/> or <see cref="ModeParam.BoxedCalibration"/> the original location
    /// is plotted with a red rectangle for both StartAt and CenterIn. Then a green rectangle is plotted where the text
    /// ends up with after gravity has been applied. Finally, black text color is used.
    /// </para>
    /// </summary>
    public sealed class TextPlottable : IPlottable
    {
        /// <summary>
        /// The text plotted.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the visual information.
        /// </summary>
        public TextInfo TextInfo { get; }

        /// <summary>
        /// Gets the start position, upper left corner, of the text, can be null.
        /// </summary>
        public Tuple<float, float> StartAt { get; }

        /// <summary>
        /// Gets the gravity.
        /// </summary>
        public int Gravity { get; }

        /// <summary>
        /// Gets the box the text is centered inside, can be null.
        /// </summary>
        public PointPair CenterIn { get; }

        /// <summary>
        /// Builder class to create <see cref="TextPlottable"/> instances.
        /// <para>
        /// There are three methods which controls the positioning of the text, <see cref="SetStartAt"/>, <see cref="SetGravity"/> and <see cref="SetCenterIn"/>.
        /// </para>
        /// <para>
        /// <see cref="SetStartAt"/> takes precedence over the others.
        /// </para>
        /// <para>
        /// <see cref="SetGravity"/> gravitates the text from the supplied x and y or on the box supplied to <see cref="SetCenterIn"/>, otherwise it gravitates the text
        /// from a centered position.
        /// </para>
        /// <para>
        /// <see cref="SetCenterIn"/> simply places the text vertically and horizontally center inside the provided box.
        /// </para>
        /// <para>
        /// Calling none of the positioning methods is the same as calling <see cref="SetStartAt"/> with 0, 0 as arguments.
        /// </para>
        /// </summary>
        public sealed class Builder
        {
            /// <summary>
            /// The text plotted.
            /// </summary>
            public string Text { get; }

            /// <summary>
            /// Gets the visual information.
            /// </summary>
            public TextInfo TextInfo { get; }

            /// <summary>
            /// Gets the start position, upper left corner, of the text, can be null.
            /// </summary>
            public Tuple<float, float> StartAt { get; private set; }

            /// <summary>
            /// Gets the gravity.
            /// </summary>
            public int Gravity { get; private set; } = (int)GravityType.None;

            /// <summary>
            /// Gets the box the text is centered inside, can be null.
            /// </summary>
            public PointPair CenterIn { get; private set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="text"></param>
            /// <param name="textInfo"></param>
            private Builder(string text, TextInfo textInfo)
            {
                Text = text;
                TextInfo = textInfo;
            }

            /// <summary>
            /// Returns a new Builder instance.
            /// </summary>
            /// <param name="text">the text</param>
            /// <param name="textInfo">the text information</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewInstance(string text, TextInfo textInfo)
            {
                ValidationUtil.RequireNonNull(text);
                ValidationUtil.RequireNonNull(textInfo);
                return new Builder(text, textInfo);
            }

            /// <summary>
            /// Returns a new Builder with Pidifys version of the form: "vMAJOR.MINOR.PATCH" where
            /// MAJOR = Major, MINOR = Minor and PATCH = Build.
            /// </summary>
            /// <param name="textInfo">the text information</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewPidifyVersionInstance(TextInfo textInfo)
            {
                return NewVersionInstance(typeof(TextPlottable).Assembly.GetName().Version, textInfo, "");
            }

            /// <summary>
            /// Returns a new Builder with Pidifys version of the form: "vMAJOR.MINOR.PATCH" where
            /// MAJOR = Major, MINOR = Minor and PATCH = Build.
            /// </summary>
            /// <param name="textInfo">the text information</param>
            /// <param name="prefix">the prefix added before the version</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewPidifyVersionInstance(TextInfo textInfo, string prefix)
            {
                return NewVersionInstance(typeof(TextPlottable).Assembly.GetName().Version, textInfo, prefix);
            }

            /// <summary>
            /// Returns a new Builder with the callings Assembly version of the form: "vMAJOR.MINOR.PATCH" where
            /// MAJOR = Major, MINOR = Minor and PATCH = Build.
            /// </summary>
            /// <param name="textInfo">the text information</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewCallingVersionInstance(TextInfo textInfo)
            {
                return NewVersionInstance(Assembly.GetCallingAssembly().GetName().Version, textInfo, "");
            }

            /// <summary>
            /// Returns a new Builder with the callings Assembly version of the form: "vMAJOR.MINOR.PATCH" where
            /// MAJOR = Major, MINOR = Minor and PATCH = Build.
            /// </summary>
            /// <param name="textInfo">the text information</param>
            /// <param name="prefix">the prefix added before the version</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewCallingVersionInstance(TextInfo textInfo, string prefix)
            {
                return NewVersionInstance(Assembly.GetCallingAssembly().GetName().Version, textInfo, prefix);
            }

            /// <summary>
            /// Returns a new Builder for the version of the form: "vMAJOR.MINOR.PATCH" where
            /// MAJOR = Major, MINOR = Minor and PATCH = Build.
            /// </summary>
            /// <param name="version">the version</param>
            /// <param name="textInfo">the text information</param>
            /// <param name="prefix">the prefix added before the version</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewVersionInstance(Version version, TextInfo textInfo, string prefix)
            {
                ValidationUtil.RequireNonNull(version);
                ValidationUtil.RequireNonNull(textInfo);
                ValidationUtil.RequireNonNull(prefix);

                var versionString = $"{prefix}v{version.Major}.{version.Minor}.{version.Build}";

                return new Builder(versionString, textInfo);
            }

            /// <summary>
            /// Starts the top left corner of the text at the specified coordinate.
            /// </summary>
            /// <param name="x">the x position</param>
            /// <param name="y">the y position</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetStartAt(float x, float y)
            {
                ValidationUtil.RequireBetween(x, 0f, 1f, $"x({x}) has to be in the interval [0, 1].");
                ValidationUtil.RequireBetween(y, 0f, 1f, $"y({y}) has to be in the interval [0, 1].");

                StartAt = new Tuple<float, float>(x, y);
                return this;
            }

            /// <summary>
            /// Gravitates the text according to the specified gravity strategy.
            /// </summary>
            /// <param name="gravityType">the gravity strategy</param>
            /// <param name="gravityTypes">combined with these gravities</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetGravity(GravityType gravityType, params GravityType[] gravityTypes)
            {
                Gravity = (int)gravityType;

                foreach (var g in gravityTypes)
                {
                    Gravity |= (int)g;
                }

                return this;
            }

            /// <summary>
            /// Centers the text inside the specified box.
            /// <para>
            /// Note: this might result in a <see cref="ArgumentException"/> in the <see cref="IPlottable.Plot(ICanvas)"/> method
            /// if the text cannot fit in the box.
            /// </para>
            /// </summary>
            /// <param name="box">the box</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetCenterIn(PointPair box)
            {
                CenterIn = ValidationUtil.RequireNonNull(box);

                return this;
            }

            /// <summary>
            /// Returns the text plottable from this Builder.
            /// </summary>
            /// <returns>new TextPlottable instance</returns>
            public TextPlottable Build()
            {
                return new TextPlottable(this);
            }
        }

        private TextPlottable(Builder builder)
        {
            Text = builder.Text;
            TextInfo = builder.TextInfo;
            StartAt = builder.StartAt;
            Gravity = builder.Gravity;
            CenterIn = builder.CenterIn;
        }

        private bool IsGravity(GravityType gravityType, params GravityType[] gravityTypes)
        {
            if (gravityType == GravityType.None)
            {
                return Gravity == (int) GravityType.None;
            }

            return (Gravity & (int) gravityType) == (int) gravityType
                   && gravityTypes.All(g => (Gravity & (int) g) == (int) g);
        }

        /// <inheritdoc cref="IPlottable"/>
        public void Plot(ICanvas canvas)
        {
            canvas.SetFont(TextInfo.Type, TextInfo.Style, TextInfo.Size);

            var textHeight = canvas.GetTextHeight(Text);
            var textWidth = canvas.GetTextWidth(Text);

            ValidationUtil.RequireBetween(textWidth, 0f, 1f, $"the text is to wide with a relative width of {textWidth}.");
            ValidationUtil.RequireBetween(textHeight, 0f, 1f, $"the text is to high with a relative height of {textHeight}.");

            var textBox = PointPair.NewInstance(0.5f - textWidth / 2, 0.5f - textHeight / 2, 0.5f + textWidth / 2, 0.5f + textHeight / 2);
            var x = 0.5f - textWidth / 2;
            var y = 0.5f - textHeight / 2;
            if (StartAt != null)
            {
                ValidationUtil.RequireBetween(StartAt.Item1 + textWidth, 0f, 1f, $"x({StartAt.Item1}) + textWidth({textWidth}) must be less than or equal to 1.");
                ValidationUtil.RequireBetween(StartAt.Item2 + textHeight, 0f, 1f, $"y({StartAt.Item2}) + textHeight({textHeight}) must be less than or equal to 1.");
                x = StartAt.Item1;
                y = StartAt.Item2;

                textBox = PointPair.NewInstance(x, y, x + textWidth, y + textHeight);

                if (canvas.GetModeParam() == ModeParam.Calibration 
                    || canvas.GetModeParam() == ModeParam.BoxedCalibration)
                {
                    canvas.SetColor(Colors.Red);
                    canvas.DrawRectangle(textBox);
                }
            }
            else if (CenterIn != null)
            {
                ValidationUtil.RequireNonNegative(CenterIn.Width - textWidth, $"CenterIn.Width({CenterIn.Width}) cannot be less than textWidth({textWidth}).");
                ValidationUtil.RequireNonNegative(CenterIn.Height - textHeight, $"CenterIn.Height({CenterIn.Height}) cannot be less than textHeight({textHeight}).");
                x = CenterIn.FromX + CenterIn.Width / 2 - textWidth / 2;
                y = CenterIn.FromY + CenterIn.Height / 2 - textHeight / 2;
                textBox = CenterIn;


                if (canvas.GetModeParam() == ModeParam.Calibration
                    || canvas.GetModeParam() == ModeParam.BoxedCalibration)
                {
                    canvas.SetColor(Colors.Red);
                    canvas.DrawRectangle(CenterIn);
                }
            }

            if (!IsGravity(GravityType.None))
            {
                if (IsGravity(GravityType.Top) && !IsGravity(GravityType.Bottom, GravityType.Top))
                {
                    textBox = PointPair.NewInstance(textBox.FromX, 0, textBox.ToX, textBox.Height);
                    y = textBox.FromY + textBox.Height / 2 - textHeight / 2;
                }

                if (IsGravity(GravityType.Bottom) && !IsGravity(GravityType.Bottom, GravityType.Top))
                {
                    textBox = PointPair.NewInstance(textBox.FromX, 1 - textBox.Height, textBox.ToX, 1f);
                    y = textBox.FromY + textBox.Height / 2 - textHeight / 2;
                }

                if (IsGravity(GravityType.Left) && !IsGravity(GravityType.Left, GravityType.Right))
                {
                    textBox = PointPair.NewInstance(0f, textBox.FromY, textBox.Width, textBox.ToY);
                    x = textBox.FromX + textBox.Width / 2 - textWidth / 2;
                }

                if (IsGravity(GravityType.Right) && !IsGravity(GravityType.Left, GravityType.Right))
                {
                    textBox = PointPair.NewInstance(1 - textBox.Width, textBox.FromY, 1f, textBox.ToY);
                    x = textBox.FromX + textBox.Width / 2 - textWidth / 2;
                }

                if (canvas.GetModeParam() == ModeParam.Calibration
                    || canvas.GetModeParam() == ModeParam.BoxedCalibration)
                {
                    canvas.SetColor(Colors.Green);
                    canvas.DrawRectangle(textBox);
                }
            }

            if (x.CompareTo(0f) < 0)
            {
                x = 0;
            }

            if (y.CompareTo(0f) < 0)
            {
                y = 0;
            }

            canvas.SetColor(TextInfo.Color);

            if (canvas.GetModeParam() == ModeParam.Calibration
                || canvas.GetModeParam() == ModeParam.BoxedCalibration)
            {
                canvas.SetColor(Color.Black);
            }

            canvas.SetFont(TextInfo.Type, TextInfo.Style, TextInfo.Size);
            canvas.WriteText(Text, x, y + textHeight * 0.85f);
        }
    }
}
