using System.Drawing;

namespace Pidify.Utils
{
    /// <summary>
    /// Container type for the visual properties of text.
    /// </summary>
    public sealed class TextInfo
    {
        /// <summary>
        /// The type of the font.
        /// </summary>
        public FontType Type { get; }

        /// <summary>
        /// The size of the font.
        /// </summary>
        public float Size { get; }

        /// <summary>
        /// The color.
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// The style of the font.
        /// </summary>
        public FontStyle Style { get; }

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="type">the type</param>
        /// <param name="size">the font size</param>
        /// <param name="color">the color</param>
        /// <param name="style">the style</param>
        private TextInfo(FontType type, float size, Color color, FontStyle style)
        {
            Type = type;
            Size = size;
            Color = color;
            Style = style;
        }

        /// <summary>
        /// Returns a new <see cref="TextInfo"/> instance.
        /// <para>
        /// Same as calling <see cref="NewInstance(FontType,float, System.Drawing.Color, FontStyle)"/> with color = BLACK and style = <see cref="FontStyle.Regular"/>.
        /// </para>
        /// </summary>
        /// <param name="type">the font type</param>
        /// <param name="size">the font size</param>
        /// <returns>a new TextInfo instance</returns>
        public static TextInfo NewInstance(FontType type, float size)
        {
            return NewInstance(type, size, Color.FromArgb(255, 0, 0, 0), FontStyle.Regular);
        }
        /// <summary>
        /// Returns a new <see cref="TextInfo"/> instance.
        /// <para>
        /// Same as calling <see cref="NewInstance(FontType,float, System.Drawing.Color, FontStyle)"/> with style = <see cref="FontStyle.Regular"/>.
        /// </para>
        /// </summary>
        /// <param name="type">the font type</param>
        /// <param name="size">the font size</param>
        /// <param name="color">the color of the text</param>
        /// <returns>a new TextInfo instance</returns>
        public static TextInfo NewInstance(FontType type, float size, Color color)
        {
            return NewInstance(type, size, color, FontStyle.Regular);
        }

        /// <summary>
        /// Returns a new <see cref="TextInfo"/> instance.
        /// </summary>
        /// <param name="type">the font type</param>
        /// <param name="size">the font size</param>
        /// <param name="color">the color of the text</param>
        /// <param name="style">the font style</param>
        /// <returns>a new TextInfo instance</returns>
        public static TextInfo NewInstance(FontType type, float size, Color color, FontStyle style)
        {
            ValidationUtil.RequireNonNull(color);

            return new TextInfo(type, size, color, style);
        }
    }
}
