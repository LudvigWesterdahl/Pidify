namespace Pidify.Utils
{
    /// <summary>
    /// Defines different type of gravity strategies for a box inside a parent box that can be combined using the OR operator.
    /// </summary>
    public enum GravityType
    {
        /// <summary>
        /// No gravity.
        /// </summary>
        None = 0,

        /// <summary>
        /// Gravities to the top.
        /// </summary>
        Top = 1,

        /// <summary>
        /// Gravities to the bottom.
        /// </summary>
        Bottom = 2,

        /// <summary>
        /// Gravities to the left.
        /// </summary>
        Left = 4,

        /// <summary>
        /// Gravities to the right.
        /// </summary>
        Right = 8
    }
}
