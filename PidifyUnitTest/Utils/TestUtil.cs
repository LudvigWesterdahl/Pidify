using System;
using System.IO;

namespace PidifyUnitTest.Utils
{
    public static class TestUtil
    {
        public static string ResourcesPath(string file)
        {
            return Path.Combine(RelativePath("Resources"), file);
        }

        public static string RelativePath(string path)
        {
            return Path.Combine(Environment.CurrentDirectory, path);
        }
    }
}
