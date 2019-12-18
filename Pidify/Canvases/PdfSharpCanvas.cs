using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Pidify.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Pidify.Canvases
{
    /// <summary>
    /// Implementation of <see cref="ICanvasConfig"/> and <see cref="ICanvas"/> using the PdfSharp library.
    /// </summary>
    public sealed class PdfSharpCanvas : ICanvasConfig, ICanvas
    {
        /*
         * ================================================================================
         * ==============================       FIELDS       ==============================
         * ================================================================================
         */
        private readonly PdfDocument pdfDocument;

        private readonly Dictionary<int, PdfPage> pages = new Dictionary<int, PdfPage>();

        private readonly Dictionary<int, XGraphics> graphics = new Dictionary<int, XGraphics>();

        /*
         * ================================================================================
         * ==============================       STATE       ===============================
         * ================================================================================
         */

        private XGraphics currentGfx;

        private PdfPage currentPage;

        private RectangleF currentDrawingSpace = new RectangleF(0, 0, 0, 0);

        private FontType currentFontType = FontType.Helvetica;

        private FontStyle currentFontStyle = FontStyle.Regular;

        private double currentFontSize = 12;

        private Color currentColor = Color.FromArgb(255, 0, 0, 0);

        private ModeParam modeParam = ModeParam.None;
    
        /// <summary>
        /// Constructor
        /// </summary>
        public PdfSharpCanvas()
        {
            pdfDocument = new PdfDocument();
            pdfDocument.Options.ColorMode = PdfColorMode.Undefined;
        }

        /*
         * ================================================================================
         * ==========================       Helper Methods       ==========================
         * ================================================================================
         */
        #region HelperMethods

        private XGraphics GetGraphics(int pageId)
        {
            if (!graphics.ContainsKey(pageId))
            {
                graphics.Add(pageId, XGraphics.FromPdfPage(currentPage));
            }

            graphics.TryGetValue(pageId, out var graphic);

            return ValidationUtil.RequireNonNull(graphic);
        }

        private PdfPage GetPage(int pageId)
        {
            pages.TryGetValue(pageId, out var page);

            return ValidationUtil.RequireNonNull(page);
        }

        private XFont GetFont()
        {
            XFontStyle style;
            switch (currentFontStyle)
            {
                case FontStyle.Regular:
                    style = XFontStyle.Regular;
                    break;

                case FontStyle.Bold:
                    style = XFontStyle.Bold;
                    break;

                case FontStyle.Italic:
                    style = XFontStyle.Italic;
                    break;

                case FontStyle.Underline:
                    style = XFontStyle.Underline;
                    break;

                case FontStyle.Strikeout:
                    style = XFontStyle.Strikeout;
                    break;

                default:
                    // Should not execute.
                    throw new ArgumentException();
            }

            XFont font;
            switch (currentFontType)
            {
                case FontType.Arial:
                    font = new XFont("Arial", currentFontSize, style);
                    break;
                case FontType.Helvetica:
                    font = new XFont("Helvetica", currentFontSize, style);

                    break;
                case FontType.Times:
                    font = new XFont("Times", currentFontSize, style);

                    break;
                case FontType.Verdana:
                    font = new XFont("Verdana", currentFontSize, style);
                    break;

                default:
                    // Should not execute.
                    throw new ArgumentException();
            }

            return font;
        }

        private XColor GetColor()
        {
            return XColor.FromArgb(currentColor.A, currentColor.R, currentColor.G, currentColor.B);
        }

        private XBrush GetBrush()
        {
            return new XSolidBrush(GetColor());
        }

        private XPen GetPen(double width)
        {
            return new XPen(GetColor(), width);
        }

        #endregion
        /*
         * ================================================================================
         * ==========================       ICanvasConfig       ===========================
         * ================================================================================
         */
        #region ICanvasConfig

        /// <inheritdoc cref="ICanvasConfig"/>
        public bool AddPage(int pageId)
        {
            if (pages.ContainsKey(pageId))
            {
                return false;
            }

            var page = pdfDocument.AddPage();
            pages.Add(pageId, page);

            return true;
        }

        /// <inheritdoc cref="ICanvasConfig"/>
        public List<int> GetPagesIds()
        {
            return pages.Select(kvp => kvp.Key).ToList();
        }

        /// <inheritdoc cref="ICanvasConfig"/>
        public ICanvas UsePage(int pageId)
        {
            return UsePageArea(PointPair.NewInstance(0, 0, 1, 1), pageId);
        }

        /// <inheritdoc cref="ICanvasConfig"/>
        public ICanvas UsePageArea(PointPair box, int pageId)
        {
            currentPage = GetPage(pageId);

            // Calculates the new sizes.

            const float widthRatio = 0.94f;
            const float heightRatio = 0.96f;
            var pageWidth = currentPage.Width.Point * widthRatio;
            var pageHeight = currentPage.Height.Point * heightRatio;
            var moveX = (float)(currentPage.Width.Point - pageWidth) / 2;
            var moveY = (float)(currentPage.Height.Point - pageHeight) / 2;

            var newX = (float)(pageWidth * box.FromX);
            var newY = (float)(pageHeight * box.FromY);
            var newWidth = (float)(pageWidth * box.Width);
            var newHeight = (float)(pageHeight * box.Height);


            // Sets the current drawing state.
            currentDrawingSpace = new RectangleF(newX + moveX, newY + moveY, newWidth, newHeight);
            
            // Updates the drawing matrix to start at new coordinates.
            currentGfx = GetGraphics(pageId);

            return this;
        }

        /// <inheritdoc cref="ICanvasConfig"/>
        public ICanvasConfig RequestMode(ModeParam mode)
        {
            modeParam = mode;

            return this;
        }

        /// <inheritdoc cref="ICanvasConfig"/>
        public bool End(string file)
        {
            try
            {
                pdfDocument.Save(file);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
        /*
         * ================================================================================
         * =============================       ICanvas       ==============================
         * ================================================================================
         */
        #region ICanvas

        /// <inheritdoc cref="ICanvas"/>
        public RectangleF GetCurrentDrawingSpace()
        {
            return currentDrawingSpace;
        }

        /// <inheritdoc cref="ICanvas"/>
        public float GetTextWidth(string text)
        {
            var absoluteWidth = (float)currentGfx.MeasureString(text, GetFont()).Width;

            return absoluteWidth / currentDrawingSpace.Width;
        }

        /// <inheritdoc cref="ICanvas"/>
        public float GetTextHeight(string text)
        {
            var absoluteHeight = (float)currentGfx.MeasureString(text, GetFont()).Height;

            return absoluteHeight / currentDrawingSpace.Height;
        }

        /// <inheritdoc cref="ICanvas"/>
        public float GetImageHeight(Bitmap pngImage)
        {
            using (var stream = new MemoryStream())
            {
                pngImage.Save(stream, ImageFormat.Png);
                var image = XImage.FromStream(stream);
                var absoluteHeight = (float)image.PointHeight;

                return absoluteHeight / currentDrawingSpace.Height;
            }
        }

        /// <inheritdoc cref="ICanvas"/>
        public float GetImageWidthHeightRatio(string imageFile)
        {
            var image = XImage.FromFile(imageFile);
            return (float) (image.PointWidth / image.PointHeight);
        }

        /// <inheritdoc cref="ICanvas"/>
        public float GetImageWidthHeightRatio(Bitmap pngImage)
        {
            using (var stream = new MemoryStream())
            {
                pngImage.Save(stream, ImageFormat.Png);
                var image = XImage.FromStream(stream);
                return (float)(image.PointWidth / image.PointHeight);
            }
        }

        /// <inheritdoc cref="ICanvas"/>
        public float GetThicknessWidth(float thickness)
        {
            return thickness / currentDrawingSpace.Width;
        }

        /// <inheritdoc cref="ICanvas"/>
        public float GetThicknessHeight(float thickness)
        {
            return thickness / currentDrawingSpace.Height;
        }

        /// <inheritdoc cref="ICanvas"/>
        public ModeParam GetModeParam()
        {
            return modeParam;
        }

        /// <inheritdoc cref="ICanvas"/>
        public ICanvas SetColor(Color color)
        {
            currentColor = ValidationUtil.RequireNonNull(color);

            return this;
        }

        /// <inheritdoc cref="ICanvas"/>
        public ICanvas SetFont(FontType fontType, FontStyle fontStyle, float fontSize)
        {
            currentFontType = fontType;
            currentFontStyle = fontStyle;
            currentFontSize = ValidationUtil.RequirePositive(fontSize, $"fontSize({fontSize}) has to be positive.");
            return this;
        }

        /// <inheritdoc cref="ICanvas"/>
        public ICanvas WriteText(string text, float x, float y)
        {
            ValidationUtil.RequireNonNull(text);
            ValidationUtil.RequireBetween(x, 0, 1, $"x({x}) must be in range [0, 1].");
            ValidationUtil.RequireBetween(y, 0, 1, $"y({y}) must be in range [0, 1].");

            var textWidth = GetTextWidth(text);
            var textHeight = GetTextHeight(text);

            var textRect = new RectangleF(
                (currentDrawingSpace.Width * x) + currentDrawingSpace.X, 
                (currentDrawingSpace.Height * y) + currentDrawingSpace.Y, 
                textWidth * currentDrawingSpace.Width, 
                textHeight * currentDrawingSpace.Height);


            /*
            var f = GetFont();
            if (!currentDrawingSpace.Contains(textRect))
            {
                throw new ArgumentException("The text does not fit in the current drawing space.");
            }
            */

            //currentGfx.DrawString(text, GetFont(), GetBrush(), currentDrawingSpace.Width * x, currentDrawingSpace.Height * y);
            currentGfx.DrawString(text, GetFont(), GetBrush(), new XPoint(textRect.X, textRect.Y));
            return this;
        }

        private Tuple<float, float> BindPoint(float x, float y, float diff, RectangleF inside)
        {
            var fromX = inside.X;
            var fromY = inside.Y;
            var toX = inside.X + inside.Width;
            var toY = inside.Y + inside.Height;

            //ValidationUtil.RequireNonNegative(x - fromX, "x needs to be inside the specified rectangle.");
            if ((x - fromX).CompareTo(0.0f) < 0)
            {
                x = fromX;
            }
            //ValidationUtil.RequireNonNegative(y - fromY, "y needs to be inside the specified rectangle");
            if ((y - fromY).CompareTo(0.0f) < 0)
            {
                y = fromY;
            }
            //ValidationUtil.RequireNonNegative(toX - x, "x needs to be inside the specified rectangle.");
            if ((toX - x).CompareTo(0.0f) < 0)
            {
                x = toX;
            }
            //ValidationUtil.RequireNonNegative(toY - y, "y needs to be inside the specified rectangle");
            if ((toY - y).CompareTo(0.0f) < 0)
            {
                y = toY;
            }

            var newX = x;
            if ((x - diff).CompareTo(fromX) < 0)
            {
                newX += (fromX - (x - diff));
            }

            if ((x + diff).CompareTo(toX) > 0)
            {
                newX -= (x + diff - toX);
            }

            var newY = y;
            if ((y - diff).CompareTo(fromY) < 0)
            {
                newY += (fromY - (y - diff));
            }

            if ((y + diff).CompareTo(toY) > 0)
            {
                newY -= (y + diff - toY);
            }

            return new Tuple<float, float>(newX, newY);
        }

        /// <inheritdoc cref="ICanvas"/>
        public ICanvas DrawLine(PointPair line, float thickness, float unitsOn)
        {
            ValidationUtil.RequireNonNull(line);
            ValidationUtil.RequirePositive(thickness, $"thickness({thickness}) must be positive (>0).");
            ValidationUtil.RequireBetween(unitsOn, 0, 1, $"unitsOn({unitsOn}) must be in the range [0, 1].");

            // Draws the line with the pen.
            var pen = GetPen(thickness);

            if (unitsOn.CompareTo(1f) == 0)
            {
                pen.DashStyle = XDashStyle.Solid;
            }
            else
            {
                pen.DashStyle = XDashStyle.Dash;
                var lineLength = line.ConvertLineLength(currentDrawingSpace);
                var numUnits = lineLength / thickness;
                var numOnUnits = unitsOn * numUnits;
                var numOffUnits = numUnits - numOnUnits;
                //pen.DashPattern = new double[] {numOnUnits, numOffUnits};
                pen.DashPattern = new double[] { Math.Max(1, numOnUnits / numOffUnits), Math.Max(1, numOffUnits / numOnUnits) };
            }

            var fromX = line.ConvertFromX(currentDrawingSpace);
            var fromY = line.ConvertFromY(currentDrawingSpace);
            var toX = line.ConvertToX(currentDrawingSpace);
            var toY = line.ConvertToY(currentDrawingSpace);

            var from = BindPoint(
                fromX, 
                fromY,
                thickness / 2, 
                currentDrawingSpace);

            var to = BindPoint(
                toX,
                toY,
                thickness / 2,
                currentDrawingSpace);


            //currentGfx.DrawLine(pen, fromX, fromY, toX, toY);
            currentGfx.DrawLine(pen, from.Item1, from.Item2, to.Item1, to.Item2);

            return this;
        }

        /// <inheritdoc cref="ICanvas"/>
        public ICanvas DrawImage(string imageFile, PointPair box)
        {
            // Validates the arguments.
            ValidationUtil.RequireFileExist(imageFile);
            if (!FileUtil.IsImageFile(imageFile))
            {
                throw new ArgumentException($"imageFile({imageFile}) is not an image.");
            }

            ValidationUtil.RequireNonNull(box);

            // Calculates the dimensions.
            var rect = box.ToRectangle(currentDrawingSpace);
            var image = XImage.FromFile(imageFile);
            currentGfx.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
            return this;
        }

        /// <inheritdoc cref="ICanvas"/>
        public ICanvas DrawImage(Bitmap pngImage, PointPair box)
        {
            // Validates the arguments.
            ValidationUtil.RequireNonNull(pngImage);
            ValidationUtil.RequireNonNull(box);

            using (var stream = new MemoryStream())
            {
                pngImage.Save(stream, ImageFormat.Png);
                // Calculates the dimensions.
                var rect = box.ToRectangle(currentDrawingSpace);
                var image = XImage.FromStream(stream);
                currentGfx.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
                return this;
            }
        }

        /// <inheritdoc cref="ICanvas"/>
        public ICanvas DrawRectangle(PointPair rect)
        {
            ValidationUtil.RequireNonNull(rect);

            var r = rect.ToRectangle(currentDrawingSpace);

            currentGfx.DrawRectangle(GetBrush(), r.X, r.Y, r.Width, r.Height);

            return this;
        }

        /// <inheritdoc cref="ICanvas"/>
        public ICanvas DrawRectangle(PointPair rect, float thickness, float unitsOn)
        {
            ValidationUtil.RequireNonNull(rect);

            var r = rect.ToRectangle(currentDrawingSpace);

            var pen = GetPen(thickness);

            if (unitsOn.CompareTo(1f) == 0)
            {
                pen.DashStyle = XDashStyle.Solid;
            }
            else
            {
                pen.DashStyle = XDashStyle.Dash;
                //var lineLength = rect.ConvertLineLength(currentDrawingSpace);
                var lineLength = r.Width * 2 + r.Height * 2;
                var numUnits = lineLength / thickness;
                var numOnUnits = unitsOn * numUnits;
                var numOffUnits = numUnits - numOnUnits;
                pen.DashPattern = new double[] { Math.Max(1, numOnUnits / numOffUnits), Math.Max(1, numOffUnits / numOnUnits) };
            }

            var from = BindPoint(r.X, r.Y, thickness / 2, currentDrawingSpace);
            var to = BindPoint(r.X + r.Width, r.Y + r.Height, thickness / 2, currentDrawingSpace);

            //currentGfx.DrawRectangle(pen, r.X, r.Y, r.Width, r.Height);
            currentGfx.DrawRectangle(pen, from.Item1, from.Item2, to.Item1 - from.Item1, to.Item2 - from.Item2);

            return this;
        }

        #endregion
    }
}
