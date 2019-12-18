using System.Drawing;

namespace Pidify.Utils
{
    /// <summary>
    /// Utility class for Color management.
    /// </summary>
    public static class Colors
    {
        /// <summary>
        /// RGB: 244, 67, 54
        /// </summary>
        public static Color Red = Color.FromArgb(244, 67, 54);

        /// <summary>
        /// RGB: 233, 30, 99
        /// </summary>
        public static Color Pink = Color.FromArgb(233, 30, 99);

        /// <summary>
        /// RGB: 156, 39, 176
        /// </summary>
        public static Color Purple = Color.FromArgb(156, 39, 176);

        /// <summary>
        /// RGB: 103, 58, 183
        /// </summary>
        public static Color DeepPurple = Color.FromArgb(103, 58, 183);

        /// <summary>
        /// RGB: 63, 81, 181
        /// </summary>
        public static Color Indigo = Color.FromArgb(63, 81, 181);

        /// <summary>
        /// RGB: 33, 150, 243
        /// </summary>
        public static Color Blue = Color.FromArgb(33, 150, 243);

        /// <summary>
        /// RGB: 3, 169, 244
        /// </summary>
        public static Color LightBlue = Color.FromArgb(3, 169, 244);

        /// <summary>
        /// RGB: 0, 188, 212
        /// </summary>
        public static Color Cyan = Color.FromArgb(0, 188, 212);

        /// <summary>
        /// RGB: 0, 150, 136
        /// </summary>
        public static Color Teal = Color.FromArgb(0, 150, 136);

        /// <summary>
        /// RGB: 76, 175, 80
        /// </summary>
        public static Color Green = Color.FromArgb(76, 175, 80);

        /// <summary>
        /// RGB: 139, 195, 74
        /// </summary>
        public static Color LightGreen = Color.FromArgb(139, 195, 74);

        /// <summary>
        /// RGB: 205, 220, 57
        /// </summary>
        public static Color Lime = Color.FromArgb(205, 220, 57);

        /// <summary>
        /// RGB: 255, 235, 59
        /// </summary>
        public static Color Yellow = Color.FromArgb(255, 235, 59);

        /// <summary>
        /// RGB: 255, 193, 7
        /// </summary>
        public static Color Amber = Color.FromArgb(255, 193, 7);

        /// <summary>
        /// RGB: 255, 152, 0
        /// </summary>
        public static Color Orange = Color.FromArgb(255, 152, 0);

        /// <summary>
        /// RGB: 255, 87, 34
        /// </summary>
        public static Color DeepOrange = Color.FromArgb(255, 87, 34);

        /// <summary>
        /// RGB: 121, 85, 72
        /// </summary>
        public static Color Brown = Color.FromArgb(121, 85, 72);

        /// <summary>
        /// RGB: 158, 158, 158
        /// </summary>
        public static Color Grey = Color.FromArgb(158, 158, 158);

        /// <summary>
        /// RGB: 96, 125, 139
        /// </summary>
        public static Color BlueGrey = Color.FromArgb(96, 125, 139);
        
        /// <summary>
        /// RGB: 66, 66, 66
        /// </summary>
        public static Color DarkGrey = Color.FromArgb(66, 66, 66);

        /// <summary>
        /// Returns a color with the applied alpha value on a white background.
        /// </summary>
        /// <param name="alpha">the alpha channel, 0 to 1</param>
        /// <param name="color">the color</param>
        /// <returns>alpha applied on color</returns>
        public static Color ApplyAlpha(float alpha, Color color)
        {
            return ApplyAlpha(alpha, color, Color.FromArgb(255, 255, 255));
        }

        /// <summary>
        /// Returns a color with the applied alpha value on the background.
        /// </summary>
        /// <param name="alpha">the alpha channel, 0 to 1</param>
        /// <param name="color">the color</param>
        /// <param name="background">the background</param>
        /// <returns>alpha applied on color</returns>
        public static Color ApplyAlpha(float alpha, Color color, Color background)
        {
            ValidationUtil.RequireBetween(alpha, 0, 1, $"alpha({alpha}) must be in interval [0, 1]");
            ValidationUtil.RequireNonNull(color);
            ValidationUtil.RequireNonNull(background);

            var red = alpha * color.R + (1 - alpha) * background.R;
            var green = alpha * color.G + (1 - alpha) * background.G;
            var blue = alpha * color.B + (1 - alpha) * background.B;

            return Color.FromArgb((int)(red + .5), (int)(green + .5), (int)(blue + .5));
        }
    }
}
