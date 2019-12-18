using Pidify.Utils;
using System.Drawing;

namespace Pidify
{
    /// <summary>
    /// A type that knows how to create PDF.
    /// </summary>
    public interface ICanvas
    {
        /// <summary>
        /// Returns the absolute drawing space in pixels.
        /// Returns the current drawing box.
        /// This information can be used to calculate some percentage values if needed.
        /// </summary>
        /// <returns>the drawing space</returns>
        RectangleF GetCurrentDrawingSpace();

        /// <summary>
        /// Returns the width of the specified text (0-1) with the set font.
        /// </summary>
        /// <param name="text">the text to measure</param>
        /// <returns>the width in percentage</returns>
        float GetTextWidth(string text);

        /// <summary>
        /// Returns the height of the specified text (0-1) with the set font.
        /// </summary>
        /// <param name="text">the text to measure</param>
        /// <returns>the height in percentage</returns>
        float GetTextHeight(string text);

        /// <summary>
        /// Returns the height of the image (0-1).
        /// </summary>
        /// <param name="pngImage">the image</param>
        /// <returns>the height</returns>
        float GetImageHeight(Bitmap pngImage);

        /// <summary>
        /// Returns the width height ratio of the specified image (width divided by height).
        /// </summary>
        /// <param name="imageFile">the file path to the image</param>
        /// <returns>the width height ratio of the image</returns>
        float GetImageWidthHeightRatio(string imageFile);

        /// <summary>
        /// Returns the width height ratio of the specified image (width divided by height).
        /// </summary>
        /// <param name="pngImage">the PNG image</param>
        /// <returns>the width height ratio of the image</returns>
        float GetImageWidthHeightRatio(Bitmap pngImage);

        /// <summary>
        /// Returns the width of the specified thickness (0-1) in the current drawing space.
        /// </summary>
        /// <param name="thickness">the thickness to convert</param>
        /// <returns>the thickness width</returns>
        float GetThicknessWidth(float thickness);

        /// <summary>
        /// Returns the height of the specified thickness (0-1) in the current drawing space.
        /// </summary>
        /// <param name="thickness">the thickness to convert</param>
        /// <returns>the thickness height</returns>
        float GetThicknessHeight(float thickness);

        /// <summary>
        /// Returns the requested ModeParam.
        /// </summary>
        /// <returns>the mode parameter</returns>
        ModeParam GetModeParam();

        /// <summary>
        /// Sets the color used when writing text and drawing lines.
        /// </summary>
        /// <param name="color">the color to use</param>
        /// <returns>this canvas for chaining</returns>
        ICanvas SetColor(Color color);

        /// <summary>
        /// Sets the font used when writing text.
        /// </summary>
        /// <param name="fontType">the type of font</param>
        /// <param name="fontStyle">the style of the font</param>
        /// <param name="fontSize">the font size</param>
        /// <returns>this canvas for chaining</returns>
        ICanvas SetFont(FontType fontType, FontStyle fontStyle, float fontSize);

        /// <summary>
        /// Writes text at the given position as the lower left corner.
        /// </summary>
        /// <param name="text">the text to write</param>
        /// <param name="x">the start x coordinate; lower left corner</param>
        /// <param name="y">the start y coordinate; lower left corner</param>
        /// <returns>this canvas for chaining</returns>
        ICanvas WriteText(string text, float x, float y);

        /// <summary>
        /// Draws a line between two points.
        /// </summary>
        /// <param name="line">the <see cref="PointPair"/> defining the line</param>
        /// <param name="thickness">the thickness of the line</param>
        /// <param name="unitsOn">percentage of line filled, rest is space (dash); 0-1</param>
        /// <returns>this canvas for chaining</returns>
        ICanvas DrawLine(PointPair line, float thickness, float unitsOn);

        /// <summary>
        /// Draws an image in the box specified by the arguments.
        /// </summary>
        /// <param name="imageFile">the image file path</param>
        /// <param name="box">the <see cref="PointPair"/> defining the area to draw the image</param>
        /// <returns>this canvas for chaining</returns>
        ICanvas DrawImage(string imageFile, PointPair box);

        /// <summary>
        /// Draws an PNG image in the box specified by the arguments.
        /// </summary>
        /// <param name="pngImage">the image in PNG format</param>
        /// <param name="box">the <see cref="PointPair"/> defining the area to draw the image</param>
        /// <returns>this canvas for chaining</returns>
        ICanvas DrawImage(Bitmap pngImage, PointPair box);

        /// <summary>
        /// Draws a filled rectangle in the specified rect.
        /// </summary>
        /// <param name="rect">the rectangle</param>
        /// <returns>this canvas for chaining</returns>
        ICanvas DrawRectangle(PointPair rect);

        /// <summary>
        /// Draws an rectangle border around the specified rect.
        /// </summary>
        /// <param name="rect">the rectangle</param>
        /// <param name="thickness">the thickness</param>
        /// <param name="unitsOn">percentage of line filled, rest is space (dash); 0-1</param>
        /// <returns>this canvas for chaining</returns>
        ICanvas DrawRectangle(PointPair rect, float thickness, float unitsOn);
    }
}
