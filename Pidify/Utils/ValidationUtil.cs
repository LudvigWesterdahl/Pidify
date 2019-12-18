using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Pidify.Utils
{
    /// <summary>
    /// Static helper class for validation of arguments and variables.
    /// </summary>
    public static class ValidationUtil
    {
        /// <summary>
        /// Returns true if the given argument is null.
        /// </summary>
        /// <param name="obj">object to test</param>
        /// <returns>true if obj == null, false otherwise</returns>
        public static bool IsNull(object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Returns true if the given argument is not null.
        /// </summary>
        /// <param name="obj">object to test</param>
        /// <returns>true if obj != null, false otherwise</returns>
        public static bool NonNull(object obj)
        {
            return obj != null;
        }

        /// <summary>
        /// Returns true if val is in the interval specified by from and to.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static bool IsBetween(double val, double from, double to)
        {
            if (val.CompareTo(from) < 0 || val.CompareTo(to) > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true if val is in the interval specified by from and to but not on the edge.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static bool IsStrictBetween(double val, double from, double to)
        {
            if (val.CompareTo(from) <= 0 || val.CompareTo(to) >= 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the given file does not exist.
        /// </summary>
        /// <param name="file">file to test</param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <exception cref="ArgumentException">if the file does not exist</exception>
        public static void RequireFileExist(string file, [CallerMemberName] string methodName = "N/A", [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (!FileUtil.FileExist(file))
            {
                ThrowArgumentException(file + " doesn't exist", methodName, fileName, lineNumber);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the given file does not exist or is not an image file.
        /// </summary>
        /// <param name="file">file to test</param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <exception cref="ArgumentException">if the file does not exist</exception>
        public static void RequireImageFile(string file, [CallerMemberName] string methodName = "N/A", [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (!FileUtil.FileExist(file))
            {
                ThrowArgumentException(file + " doesn't exist", methodName, fileName, lineNumber);
            }

            if (!FileUtil.IsImageFile(file))
            {
                ThrowArgumentException(file + " doesn't exist", methodName, fileName, lineNumber);
            }
        }

        /// <summary>
        /// Returns the object if it is not null or throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="obj">object to test</param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>obj</returns>
        /// <exception cref="ArgumentException">if obj == null</exception>
        public static T RequireNonNull<T>(T obj, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (obj == null)
            {
                ThrowArgumentException("Object cannot be null", methodName, fileName, lineNumber);
            }

            return obj;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the value is false.
        /// </summary>
        /// <param name="val">value to test</param>
        /// <param name="message">exception message</param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <exception cref="ArgumentException">if val == false</exception>
        public static void RequireTrue(bool val, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (!val)
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }
        }

        /// <summary>
        /// Returns the given value if it is greater than or equal to zero (<code>val >= 0</code>)
        /// or throws <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        /// <exception cref="ArgumentException">if val &lt; 0</exception>
        public static int RequireNonNegative(int val, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (val < 0)
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }
            return val;
        }

        /// <summary>
        /// Returns the given value if it is greater than or equal to zero (<code>val >= 0</code>)
        /// or throws <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        /// <exception cref="ArgumentException">if val &lt; 0</exception>
        public static double RequireNonNegative(double val, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (val.CompareTo(0.0) < 0)
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }
            return val;
        }

        /// <summary>
        /// Returns the given value if it is greater than zero (<code>val > 0</code>)
        /// or throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        /// <exception cref="ArgumentException">if val &lt;= 0</exception>
        public static int RequirePositive(int val, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (val <= 0)
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }
            return val;
        }

        /// <summary>
        /// Returns the given value if it is greater than zero (<code>val > 0</code>)
        /// or throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        public static double RequirePositive(double val, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (val.CompareTo(0.0) <= 0)
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }

            return val;
        }

        /// <summary>
        /// Returns the given value if it is greater than zero (<code>val > 0</code>)
        /// or throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        public static float RequirePositive(float val, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (val.CompareTo(0f) <= 0)
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }

            return val;
        }

        /// <summary>
        /// Returns the given value if it is greater or equal than zero (<code>val >= 0</code>)
        /// or throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        public static float RequireNonNegative(float val, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (val.CompareTo(0f) < 0)
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }

            return val;
        }

        /// <summary>
        /// Returns the given value if it is in the specified interval
        /// or throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        public static int RequireBetween(int val, int from, int to, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (!IsBetween(val, from, to))
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }

            return val;
        }

        /// <summary>
        /// Returns the given value if it is in the specified interval but not on the edge
        /// or throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        public static int RequireStrictBetween(int val, int from, int to, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (!IsBetween(val, from, to))
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }

            return val;
        }

        /// <summary>
        /// Returns the given value if it is in the specified interval
        /// or throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        public static double RequireBetween(double val, double from, double to, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (!IsBetween(val, from, to))
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }

            return val;
        }

        /// <summary>
        /// Returns the given value if it is in the specified interval
        /// or throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        public static float RequireBetween(float val, float from, float to, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (!IsBetween(val, from, to))
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }

            return val;
        }

        /// <summary>
        /// Returns the given value if it is in the specified interval but not on the edge
        /// or throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns>val</returns>
        public static double RequireStrictBetween(double val, double from, double to, object message, [CallerMemberName] string methodName = "N/A",
            [CallerFilePath] string fileName = "N/A", [CallerLineNumber] int lineNumber = -1)
        {
            if (!IsStrictBetween(val, from, to))
            {
                ThrowArgumentException(message, methodName, fileName, lineNumber);
            }

            return val;
        }

        /// <summary>
        /// Throws an argument exception.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="methodName">the name of the method received through caller info attribute</param>
        /// <param name="fileName">the filename received through caller info attribute</param>
        /// <param name="lineNumber">the line number as received through caller info attribute</param>
        /// <exception cref="ArgumentException">always</exception>
        private static void ThrowArgumentException(object message, string methodName, string fileName, int lineNumber)
        {
            throw new ArgumentException(
                $"[{methodName}() at line: {lineNumber} in file {Path.GetFileName(fileName)}] {message}");
        }
    }
}
