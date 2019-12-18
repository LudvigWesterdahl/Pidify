using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pidify.Utils;
using static PidifyUnitTest.Utils.TestUtil;

namespace PidifyUnitTest.Utils
{
    [TestClass]
    public class FileUtilTest
    {
        [TestMethod]
        public void Test_FileExist_Success()
        {
            Assert.IsTrue(FileUtil.FileExist(ResourcesPath("test-text-file.txt")));
        }

        [TestMethod]
        public void Test_FileExist_Null()
        {
            Assert.IsFalse(FileUtil.FileExist(null));
        }

        [TestMethod]
        public void Test_FileExist_Empty()
        {
            Assert.IsFalse(FileUtil.FileExist(string.Empty));
        }

        [TestMethod]
        public void Test_FileExist_NoExtension()
        {
            Assert.IsFalse(FileUtil.FileExist(ResourcesPath("test-text-file")));
        }

        [TestMethod]
        public void Test_EnsureExtension_NoExtension()
        {
            var filename = "my-pdf-file";
            var ext = ".pdf";

            var expected = "my-pdf-file.pdf";
            var actual = FileUtil.EnsureExtension(filename, ext);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_EnsureExtension_HasExtension()
        {
            var filename = "my-pdf-file.pdf";
            var ext = ".pdf";

            var expected = filename;
            var actual = FileUtil.EnsureExtension(filename, ext);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_EnsureExtension_PathWithExtension()
        {
            var filename = ResourcesPath("test-monkey-jpg-file.jpg");
            var ext = ".jpg";

            var expected = filename;
            var actual = FileUtil.EnsureExtension(filename, ext);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_EnsureExtension_Empty()
        {
            var filename = "";
            var ext = ".pdf";

            string expected = null;
            var actual = FileUtil.EnsureExtension(filename, ext);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_EnsureExtension_Null()
        {
            string filename = null;
            string ext = ".pdf";

            string expected = null;
            var actual = FileUtil.EnsureExtension(filename, ext);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_HasAnyExtension_Success()
        {
            string filename = "test.png";
            string ext = ".png";
            string[] exts = {".jpg", ".gif", ".pdf"};

            var actual = FileUtil.HasAnyExtension(filename, ext, exts);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Test_HasAnyExtension_NotInList()
        {
            string filename = "test.png";
            string ext = ".jpg";
            string[] exts = { ".jpg", ".gif", ".pdf" };

            var actual = FileUtil.HasAnyExtension(filename, ext, exts);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Test_HasAnyExtension_NoDot()
        {
            string filename = "test.png";
            string ext = "png";
            string[] exts = { "jpg", "gif", "pdf" };

            var actual = FileUtil.HasAnyExtension(filename, ext, exts);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Test_HasAnyExtension_NullExtensionIsInList()
        {
            string filename = "test.png";
            string ext = null;
            string[] exts = { ".png", ".gif", ".pdf" };

            var actual = FileUtil.HasAnyExtension(filename, ext, exts);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Test_HasAnyExtension_Null()
        {
            string filename = "test.png";
            string ext = null;
            string[] exts = null;

            var actual = FileUtil.HasAnyExtension(filename, ext, exts);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Test_HasAnyExtension_NullExtensions()
        {
            string filename = "test.png";
            string ext = ".png";
            string[] exts = null;

            var actual = FileUtil.HasAnyExtension(filename, ext, exts);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Test_CanReadFile_1()
        {
            string file = ResourcesPath("test-text-file.txt");

            var actual = FileUtil.CanReadFile(file);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Test_CanReadFile_2()
        {
            string file = ResourcesPath("test-monkey-jpg-file.jpg");

            var actual = FileUtil.CanReadFile(file);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Test_CanReadFile_3()
        {
            string file = ResourcesPath("test-monkey-png-file.png");

            var actual = FileUtil.CanReadFile(file);

            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void Test_CanReadFile_NoFile()
        {
            string file = "this-file-doesnt-exist";

            var actual = FileUtil.CanReadFile(file);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Test_CanReadFile_Null()
        {
            string file = null;

            var actual = FileUtil.CanReadFile(file);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Test_CanReadFile_Empty()
        {
            string file = string.Empty;

            var actual = FileUtil.CanReadFile(file);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Test_IsImageFile_1()
        {
            string file = ResourcesPath("test-monkey-jpg-file.jpg");

            var actual = FileUtil.IsImageFile(file);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Test_IsImageFile_2()
        {
            string file = ResourcesPath("test-monkey-png-file.png");

            var actual = FileUtil.IsImageFile(file);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Test_IsImageFile_TextFile()
        {
            string file = ResourcesPath("test-text-file.txt");

            var actual = FileUtil.IsImageFile(file);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Test_IsImageFile_ContentIsImage()
        {
            string file = ResourcesPath("test-monkey-png-file.pdf");

            var actual = FileUtil.IsImageFile(file);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Test_IsImageFile_ContentIsText()
        {
            string file = ResourcesPath("test-text-file.png");

            var actual = FileUtil.IsImageFile(file);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Test_IsImageFile_Null()
        {
            string file = null;

            var actual = FileUtil.IsImageFile(file);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Test_IncrementFilenameIfExists_Increments()
        {
            string file = ResourcesPath("test-text-file.txt");

            var expected = ResourcesPath("test-text-file-1.txt");
            var actual = FileUtil.IncrementFilenameIfExists(file);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IncrementFilenameIfExists_NoExist()
        {
            string file = ResourcesPath("file-no-exist.txt");

            var expected = file;
            var actual = FileUtil.IncrementFilenameIfExists(file);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IncrementFilenameIfExists_Empty()
        {
            string file = null;

            string expected = null;
            var actual = FileUtil.IncrementFilenameIfExists(file);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IncrementFilenameIfExists_Null()
        {
            string file = string.Empty;

            string expected = null;
            var actual = FileUtil.IncrementFilenameIfExists(file);

            Assert.AreEqual(expected, actual);
        }

    }
}
