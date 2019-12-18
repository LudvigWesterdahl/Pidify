namespace Pidify.Utils
{
    /// <summary>
    /// Defines different type of scaling strategies inside a box.
    /// </summary>
    public enum ScaleType
    {
        /// <summary>
        /// Fills with no regard to the width-height ratio.
        /// </summary>
        Fill,

        /// <summary>
        /// Maximizes the width or height while respecting the width-height ratio either vertical or horizontal center.
        /// </summary>
        FitCenter,

        /// <summary>
        /// Maximizes the width or height while respecting the width-height ratio either top or left.
        /// </summary>
        FitStart,

        /// <summary>
        /// Maximizes the width or height while respecting the width-height ratio either bottom or right.
        /// </summary>
        FitEnd,
    }
}
