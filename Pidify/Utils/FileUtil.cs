using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Pidify.Utils
{
    /// <summary>
    /// Utility class for file management.
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// The string ".pdf".
        /// </summary>
        public const string PdfExtension = ".pdf";

        /// <summary>
        /// Returns true if the given file exists.
        /// </summary>
        /// <param name="file">file to check</param>
        /// <returns>true if exists, false otherwise</returns>
        public static bool FileExist(string file)
        {
            try
            {
                return new FileInfo(file).Exists;

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Ensures that the file has the extension ext. If it doesn't, then this function returns
        /// a new filename with the extension. Note that it does not change the actual filename.
        /// </summary>
        /// <param name="file">the file to check</param>
        /// <param name="ext">the extension</param>
        /// <returns>file or new filepath with extension ext</returns>
        public static string EnsureExtension(string file, string ext)
        {
            if (string.IsNullOrEmpty(file) || string.IsNullOrEmpty(ext))
            {
                return null;
            }

            return file + (Path.GetExtension(file) == ext ? string.Empty : ext);
        }

        /// <summary>
        /// Returns true if the file has any of the provided extensions.
        /// </summary>
        /// <param name="file">the file</param>
        /// <param name="extension">extension</param>
        /// <param name="otherExtensions">extensions</param>
        /// <returns>true if file has any of the extensions, false otherwise</returns>
        public static bool HasAnyExtension(string file, string extension, params string[] otherExtensions)
        {
            if (string.IsNullOrEmpty(file))
            {
                return false;
            }

            if (!Path.HasExtension(file))
            {
                return false;
            }

            var fileExt = Path.GetExtension(file);

            if (fileExt.Equals(extension))
            {
                return true;
            }

            return otherExtensions != null && otherExtensions.Any(ext => fileExt.Equals(ext));
        }

        /// <summary>
        /// Returns if a file can be read or not.
        /// Note that it doesn't guarantee that the file can be read too after this call.
        /// </summary>
        /// <param name="file">the file</param>
        /// <returns>true if file can be read, false otherwise</returns>
        public static bool CanReadFile(string file)
        {
            if (!FileExist(file))
            {
                return false;
            }

            try
            {
                using (new StreamReader(file))
                {
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the given file no matter the extension
        /// is an image.
        /// </summary>
        /// <param name="filename">file to test</param>
        /// <returns>true if the content of the file is an image, false otherwise</returns>
        public static bool IsImageFile(string filename)
        {
            if (!FileExist(filename))
            {
                return false;
            }

            try
            {
                var image = Image.FromFile(filename);
                var _ = image.RawFormat; // Don't remove, this will sometimes throw exception.
                var graphics = Graphics.FromImage(image);

                image.Dispose();
                graphics.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a new filepath if the given file while maintaining the extension.
        /// <para>
        /// For example: given that file is C:/Desktop/TEST.png and that that file already exists.
        /// </para>
        /// <para>
        /// Then this function will return C:/Desktop/TEST-1.png. And if the function is called again
        /// with C:/Desktop/TEST.png it will return C:/Desktop/TEST-2.png.
        /// </para>
        /// </summary>
        /// <param name="file">the file</param>
        /// <returns>file or an incremented filename</returns>
        public static string IncrementFilenameIfExists(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                return null;
            }

            var processedFilename = file;

            if (!string.IsNullOrEmpty(Path.GetExtension(processedFilename)))
            {
                var tmp = file;
                var ext = Path.GetExtension(file);
                for (var i = 1; FileExist(processedFilename); i++)
                {
                    processedFilename = tmp.Substring(0, tmp.LastIndexOf(ext, StringComparison.Ordinal)) + "-" + i + ext;
                }
            }
            else
            {
                var tmp = file;
                for (var i = 1; FileExist(processedFilename); i++)
                {
                    processedFilename = tmp + "-" + i;
                }
            }
            return processedFilename;
        }

        /// <summary>
        /// Returns the absolute path from the current directory.
        /// </summary>
        /// <param name="path">the path</param>
        /// <returns>absolute path</returns>
        public static string RelativePath(string path)
        {
            return Path.Combine(Environment.CurrentDirectory, path);
        }
    }
}
