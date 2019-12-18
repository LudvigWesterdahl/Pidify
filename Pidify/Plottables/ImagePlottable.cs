using Pidify.Utils;
using System;
using System.Drawing;
using System.Linq;

namespace Pidify.Plottables
{
    /// <summary>
    /// Plots an image.
    /// <para>
    /// With <see cref="ModeParam.Calibration"/> or <see cref="ModeParam.BoxedCalibration"/> the original location
    /// is plotted with a red rectangle for the imageBox applied through <see cref="Builder.SetScale(ScaleType, PointPair)"/>.
    /// Then a green rectangle is plotted where the imageBox ends up with after gravity has been applied.
    /// Finally, the image is not plotted, but instead an orange box with a cross shows where the
    /// image would be placed.
    /// </para>
    /// </summary>
    public sealed class ImagePlottable : IPlottable
    {
        /// <summary>
        /// Gets the path to the image, can be null.
        /// </summary>
        public string ImageFile { get; }

        /// <summary>
        /// Gets the PNG image as a bitmap, can be null.
        /// </summary>
        public Bitmap PngImage { get; }

        /// <summary>
        /// Gets the scale type.
        /// </summary>
        public ScaleType Scale { get; }

        /// <summary>
        /// Gets the box where the image is drawn.
        /// </summary>
        public PointPair ImageBox { get; }

        /// <summary>
        /// Gets the gravity applied.
        /// </summary>
        public int Gravity { get; }

        /// <summary>
        /// Builder class to create <see cref="ImagePlottable"/> instances.
        /// </summary>
        public sealed class Builder
        {
            /// <summary>
            /// Gets the path to the image, can be null.
            /// </summary>
            public string ImageFile { get; }

            /// <summary>
            /// Gets the PNG image as a bitmap, can be null.
            /// </summary>
            public Bitmap PngImage { get; }

            /// <summary>
            /// Gets the scale type.
            /// </summary>
            public ScaleType Scale { get; private set; } = ScaleType.Fill;

            /// <summary>
            /// Gets the box where the image is drawn.
            /// </summary>
            public PointPair ImageBox { get; private set; } = PointPair.NewInstance(0, 0, 1, 1);

            /// <summary>
            /// Gets the gravity applied.
            /// </summary>
            public int Gravity { get; private set; } = (int) GravityType.None;

            /// <summary>
            /// Private constructor
            /// </summary>
            /// <param name="imageFile">the path to the image</param>
            private Builder(string imageFile)
            {
                ImageFile = imageFile;
            }

            /// <summary>
            /// Private constructor
            /// </summary>
            /// <param name="pngImage">the PNG as bitmap</param>
            private Builder(Bitmap pngImage)
            {
                PngImage = pngImage;
            }

            /// <summary>
            /// Creates a new Builder instance with the image at the given path.
            /// </summary>
            /// <param name="imageFile">the path to the image</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewInstance(string imageFile)
            {
                ValidationUtil.RequireImageFile(imageFile);

                return new Builder(imageFile);
            }

            /// <summary>
            /// Creates a new Builder instance with the PNG as a bitmap.
            /// </summary>
            /// <param name="pngImage">the PNG as a bitmap</param>
            /// <returns>new Builder instance</returns>
            public static Builder NewInstance(Bitmap pngImage)
            {
                ValidationUtil.RequireNonNull(pngImage);

                return new Builder(pngImage);
            }

            /// <summary>
            /// Sets the scale of the image inside the canvas. This is the same as calling <see cref="SetScale(ScaleType,PointPair)"/>
            /// with a maximum <see cref="PointPair"/>.
            /// <para>
            /// Defaults to <see cref="ScaleType.Fill"/> with maximum <see cref="PointPair"/>.
            /// </para>
            /// </summary>
            /// <param name="scale">the scale type to apply</param>
            /// <returns></returns>
            public Builder SetScale(ScaleType scale)
            {
                return SetScale(scale, PointPair.NewInstance(0, 0, 1, 1));
            }

            /// <summary>
            /// Sets the scale of the image inside the specified image box.
            /// <para>
            /// Defaults to <see cref="ScaleType.Fill"/> with maximum <see cref="PointPair"/>.
            /// </para>
            /// </summary>
            /// <param name="scale">the scale type to apply</param>
            /// <param name="imageBox">the box to apply scale typ inside</param>
            /// <returns></returns>
            public Builder SetScale(ScaleType scale, PointPair imageBox)
            {
                Scale = scale;
                ImageBox = ValidationUtil.RequireNonNull(imageBox);
                return this;
            }

            /// <summary>
            /// Sets gravity of the image on the canvas it is plotted on.
            /// <para>
            /// Defaults to <see cref="GravityType.None"/>.
            /// </para>
            /// </summary>
            /// <param name="gravityType">first gravity</param>
            /// <param name="gravityTypes">rest of gravity types</param>
            /// <returns>this Builder for chaining</returns>
            public Builder SetGravity(GravityType gravityType, params GravityType[] gravityTypes)
            {
                Gravity = (int) gravityType;

                foreach (var g in gravityTypes)
                {
                    Gravity |= (int) g;
                }

                return this;
            }

            /// <summary>
            /// Returns a new <see cref="ImagePlottable"/> from this Builder instance.
            /// </summary>
            /// <returns>new ImagePlottable</returns>
            public ImagePlottable Build()
            {
                return new ImagePlottable(this);
            }
        }

        private ImagePlottable(Builder builder)
        {
            ImageFile = builder.ImageFile;
            PngImage = builder.PngImage;
            Scale = builder.Scale;
            ImageBox = builder.ImageBox;
            Gravity = builder.Gravity;
        }

        private float WidthHeightRatio(ICanvas canvas)
        {
            if (ImageFile != null)
            {
                return canvas.GetImageWidthHeightRatio(ImageFile);
            }

            return canvas.GetImageWidthHeightRatio(PngImage);
        }

        private void DrawImage(ICanvas canvas, PointPair box)
        {
            if (ImageFile != null)
            {
                canvas.DrawImage(ImageFile, box);
                return;
            }

            canvas.DrawImage(PngImage, box);
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
            if (canvas.GetModeParam() == ModeParam.Calibration
                || canvas.GetModeParam() == ModeParam.BoxedCalibration)
            {
                canvas.SetColor(Colors.Red);
                canvas.DrawRectangle(ImageBox);
            }

            var moveX = 0f;
            var moveY = 0f;
            if ((Gravity & (int)GravityType.Top) == 1 && !IsGravity(GravityType.Top, GravityType.Bottom))
            {
                // Moves the box up.
                moveY -= ImageBox.FromY;
            }

            if ((Gravity & (int)GravityType.Bottom) == (int)GravityType.Bottom && !IsGravity(GravityType.Top, GravityType.Bottom))
            {
                // Moves the box down.
                moveY += 1 - ImageBox.ToY;
            }

            if ((Gravity & (int)GravityType.Left) == (int)GravityType.Left && !IsGravity(GravityType.Left, GravityType.Right))
            {
                // Moves the box to the left.
                moveX -= ImageBox.FromX;
            }

            if ((Gravity & (int)GravityType.Right) == (int)GravityType.Right && !IsGravity(GravityType.Left, GravityType.Right))
            {
                // moves the box to the right.
                moveX += 1 - ImageBox.ToX;
            }

            // Moves the image box.
            var gravitatedImageBox = ImageBox.Move(moveX, moveY);

            if (canvas.GetModeParam() == ModeParam.Calibration
                || canvas.GetModeParam() == ModeParam.BoxedCalibration)
            {
                canvas.SetColor(Colors.Green);
                canvas.DrawRectangle(gravitatedImageBox);
            }

            // Field for the image width-height ratio.
            var imageRatio = WidthHeightRatio(canvas);
            var canvasRatio = canvas.GetCurrentDrawingSpace().Width / canvas.GetCurrentDrawingSpace().Height;
            var ratios = canvasRatio / imageRatio;

            // The container that will be gravitated and that the image is contained inside.
            //var container = PointPair.NewInstance(0, 0, Math.Min(1, 1 * imageRatio), Math.Min(1, 1 / imageRatio)).MaximizeInside(PointPair.NewInstance(0, 0, 1, 1));
            var container = ratios < 1
                ? PointPair.NewInstance(0, 0, 1, ratios).MaximizeInside(PointPair.NewInstance(0, 0, 1, 1))
                : PointPair.NewInstance(0, 0, 1 / ratios, 1).MaximizeInside(PointPair.NewInstance(0, 0, 1, 1));

            // Field for the fitted image box.
            PointPair imageBox;

            switch (Scale)
            {
                case ScaleType.Fill:
                    // Use provided ImageBox.
                    imageBox = gravitatedImageBox;
                    break;

                case ScaleType.FitCenter:
                    // Use the default centered imageMaximized.
                    imageBox = container.MaximizeInside(gravitatedImageBox);
                    break;

                case ScaleType.FitStart:
                    imageBox = container.MaximizeInside(gravitatedImageBox);
                    if (ratios < 1)
                    {
                        // Moves the image box to the top (because the image is wider than the drawing space).
                        imageBox = imageBox.Move(0, gravitatedImageBox.FromY - imageBox.FromY);
                    }
                    else
                    {
                        // Moves the image box to the left.
                        imageBox = imageBox.Move(gravitatedImageBox.FromX - imageBox.FromX, 0);
                    }

                    break;

                case ScaleType.FitEnd:
                    imageBox = container.MaximizeInside(gravitatedImageBox);

                    if (ratios < 1)
                    {
                        // Moves the image box to the bottom (because the image is wider than the drawing space).
                        imageBox = imageBox.Move(0, imageBox.FromY - gravitatedImageBox.FromY);
                    }
                    else
                    {
                        // Moves the image box to the right.
                        imageBox = imageBox.Move(imageBox.FromX - gravitatedImageBox.FromX, 0);
                    }

                    break;

                default:
                    throw new NotImplementedException();
            }

            if (canvas.GetModeParam() == ModeParam.Calibration
                || canvas.GetModeParam() == ModeParam.BoxedCalibration)
            {
                canvas.SetColor(Colors.Orange);
                canvas.DrawRectangle(imageBox, 1, 1);
                canvas.DrawLine(PointPair.NewInstance(imageBox.FromX, imageBox.FromY, imageBox.ToX, imageBox.ToY), 1, 1);
                canvas.DrawLine(PointPair.NewInstance(imageBox.FromX, imageBox.ToY, imageBox.ToX, imageBox.FromY), 1, 1);
            }
            else
            {
                // Draws the image.
                DrawImage(canvas, imageBox);
            }
        }
    }
}