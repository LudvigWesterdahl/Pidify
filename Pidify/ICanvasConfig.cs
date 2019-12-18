using Pidify.Utils;
using System.Collections.Generic;

namespace Pidify
{
    /// <summary>
    /// A type that knows how to configure a <see cref="ICanvas"/> for creating PDFs.
    /// </summary>
    public interface ICanvasConfig
    {
        /// <summary>
        /// Adds a new page with the specified ID at the end of the document.
        /// </summary>
        /// <param name="pageId">the id of the page</param>
        /// <returns>true if created, false if already exists.</returns>
        bool AddPage(int pageId);

        /// <summary>
        /// Returns all page IDs.
        /// </summary>
        /// <returns>all page IDs</returns>
        List<int> GetPagesIds();

        /// <summary>
        /// Returns a <see cref="ICanvas"/> which draws on the full page.
        /// </summary>
        /// <param name="pageId">the id of the page</param>
        /// <returns>canvas to draw on</returns>
        ICanvas UsePage(int pageId);

        /// <summary>
        /// Returns a <see cref="ICanvas"/> which only draws inside the specified area on the page.
        /// </summary>
        /// <param name="box">the area on the page</param>
        /// <param name="pageId">the id of the page</param>
        /// <returns>canvas to draw on</returns>
        ICanvas UsePageArea(PointPair box, int pageId);

        /// <summary>
        /// Requests a mode for this ICanvasConfig and thus its ICanvas that it produces.
        /// <para>
        /// Note that it is up to the <see cref="IPlottable"/> to support this by calling
        /// the function <see cref="ICanvas.GetModeParam"/> and act accordingly.
        /// The <see cref="Area"/> class support <see cref="ModeParam.Boxed"/>.
        /// </para>
        /// </summary>
        /// <param name="mode">the mode requested</param>
        /// <returns>this ICanvasConfig</returns>
        ICanvasConfig RequestMode(ModeParam mode);
        
        /// <summary>
        /// Creates the PDF with the specified filepath.
        /// </summary>
        /// <param name="file">the path and name of the PDF</param>
        /// <returns>true if successful, false otherwise</returns>
        bool End(string file);
    }
}
