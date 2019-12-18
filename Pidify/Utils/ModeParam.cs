namespace Pidify.Utils
{
    /// <summary>
    /// Parameter to flexibly request a mode to be used when creating a PDF.
    /// </summary>
    public enum ModeParam
    {
        /// <summary>
        /// No mode.
        /// </summary>
        None,

        /// <summary>
        /// Any area used on <see cref="IPlottable"/> should draw a rectangle around
        /// its responsible area.
        /// </summary>
        Boxed,

        /// <summary>
        /// Any <see cref="IPlottable"/> should plot necessary information
        /// to understand its layout or help in calibrating the plottable.
        /// </summary>
        Calibration,

        /// <summary>
        /// Both <see cref="Boxed"/> and <see cref="Calibration"/>.
        /// </summary>
        BoxedCalibration,
    }
}
