using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pidify.Utils;
using System;
using static PidifyUnitTest.Utils.TestUtil;


namespace PidifyUnitTest.Utils
{
    [TestClass]
    public class ValidationUtilTest
    {
        [TestMethod]
        public void Test_IsBetween_PositiveSmall()
        {
            double val = 0.02;
            double from = 0.01;
            double to = 0.05;

            var expected = true;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_Positive()
        {
            double val = 18;
            double from = 10;
            double to = 20;

            var expected = true;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_NegativeSmall()
        {
            double val = -0.02;
            double from = -0.05;
            double to = -0.01;

            var expected = true;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_Negative()
        {
            double val = -20;
            double from = -50;
            double to = -10;

            var expected = true;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_PositiveNegative()
        {
            double val = 2;
            double from = -0.01;
            double to = 2.01;

            var expected = true;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_OutsideLeft1()
        {
            double val = -2;
            double from = 3.01;
            double to = 5.2;

            var expected = false;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_OutsideLeft2()
        {
            double val = -10;
            double from = -5.2;
            double to = -1;

            var expected = false;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_OutsideRight1()
        {
            double val = 3;
            double from = 0.01;
            double to = 2.01;

            var expected = false;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_OutsideRight2()
        {
            double val = 0.02;
            double from = -10;
            double to = -0.01;

            var expected = false;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_AllSame()
        {
            double val = -10;
            double from = -10;
            double to = -10;

            var expected = true;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_BadArguments1()
        {
            double val = 0;
            double from = 1;
            double to = -5;

            var expected = false;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_BadArguments2()
        {
            double val = -10;
            double from = -10;
            double to = -10.000001;

            var expected = false;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsBetween_BadArguments3()
        {
            double val = 0;
            double from = 10;
            double to = -1;

            var expected = false;
            var actual = ValidationUtil.IsBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_PositiveSmall()
        {
            double val = 0.02;
            double from = 0.01;
            double to = 0.05;

            var expected = true;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_Positive()
        {
            double val = 18;
            double from = 10;
            double to = 20;

            var expected = true;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_NegativeSmall()
        {
            double val = -0.02;
            double from = -0.05;
            double to = -0.01;

            var expected = true;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_Negative()
        {
            double val = -20;
            double from = -50;
            double to = -10;

            var expected = true;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_PositiveNegative()
        {
            double val = 2;
            double from = -0.01;
            double to = 2.01;

            var expected = true;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_OutsideLeft1()
        {
            double val = -2;
            double from = 3.01;
            double to = 5.2;

            var expected = false;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_OutsideLeft2()
        {
            double val = -10;
            double from = -5.2;
            double to = -1;

            var expected = false;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_OutsideRight1()
        {
            double val = 3;
            double from = 0.01;
            double to = 2.01;

            var expected = false;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_OutsideRight2()
        {
            double val = 0.02;
            double from = -10;
            double to = -0.01;

            var expected = false;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_AllSame()
        {
            double val = -10;
            double from = -10;
            double to = -10;

            var expected = false;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_BadArguments1()
        {
            double val = 0;
            double from = 1;
            double to = -5;

            var expected = false;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_BadArguments2()
        {
            double val = -10;
            double from = -10;
            double to = -10.000001;

            var expected = false;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_BadArguments3()
        {
            double val = 0;
            double from = 10;
            double to = -1;

            var expected = false;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_EdgeLeft()
        {
            double val = 5.3948;
            double from = 0.01;
            double to = 5.3948;

            var expected = false;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsStrictBetween_EdgeRight()
        {
            double val = 0.01;
            double from = 0.01;
            double to = 5;

            var expected = false;
            var actual = ValidationUtil.IsStrictBetween(val, from, to);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_RequireFileExist_Exist()
        {
            string file = ResourcesPath("test-text-file.txt");

            ValidationUtil.RequireFileExist(file);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequireFileExist_NoExist()
        {
            string file = ResourcesPath("no-exist-file");

            ValidationUtil.RequireFileExist(file);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequireFileExist_Null()
        {
            string file = null;

            ValidationUtil.RequireFileExist(file);
        }

        [TestMethod]
        public void Test_RequireNonNull_NotNull()
        {
            object val = new object();

            ValidationUtil.RequireNonNull(val);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequireNonNull_Null()
        {
            object val = null;

            ValidationUtil.RequireNonNull(val);
        }

        [TestMethod]
        public void Test_RequireTrue_True()
        {
            var val = true;

            ValidationUtil.RequireTrue(val, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequireTrue_False()
        {
            var val = false;

            ValidationUtil.RequireTrue(val, "");
        }

        [TestMethod]
        public void Test_RequireNonNegative_1()
        {
            int val = 1;

            ValidationUtil.RequireNonNegative(val, "");
        }

        [TestMethod]
        public void Test_RequireNonNegative_2()
        {
            int val = 0;

            ValidationUtil.RequireNonNegative(val, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequireNonNegative_3()
        {
            int val = -1;

            ValidationUtil.RequireNonNegative(val, "");
        }

        [TestMethod]
        public void Test_RequirePositive_Int_1()
        {
            int val = 1;

            ValidationUtil.RequirePositive(val, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequirePositive_Int_2()
        {
            int val = 0;

            ValidationUtil.RequirePositive(val, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequirePositive_Int_3()
        {
            int val = -1;

            ValidationUtil.RequirePositive(val, "");
        }

        [TestMethod]
        public void Test_RequirePositive_Double_1()
        {
            double val = 1;

            ValidationUtil.RequirePositive(val, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequirePositive_Double_2()
        {
            double val = 0;

            ValidationUtil.RequirePositive(val, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequirePositive_Double_3()
        {
            double val = -1;

            ValidationUtil.RequirePositive(val, "");
        }

        [TestMethod]
        public void Test_RequireBetween_Inside()
        {
            double val = 1;
            double from = -2;
            double to = 5;

            ValidationUtil.RequireBetween(val, from, to, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequireBetween_Outside()
        {
            double val = 2;
            double from = -0.01;
            double to = 1.99;

            ValidationUtil.RequireBetween(val, from, to, "");
        }

        [TestMethod]
        public void Test_RequireStrictBetween_Inside()
        {
            double val = 2;
            double from = -0.01;
            double to = 2.01;

            ValidationUtil.RequireStrictBetween(val, from, to, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequireStrictBetween_Outside()
        {
            double val = 5;
            double from = 2;
            double to = 2.01;

            ValidationUtil.RequireStrictBetween(val, from, to, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException was not thrown.")]
        public void Test_RequireStrictBetween_Edge()
        {
            double val = 100;
            double from = 2;
            double to = 100;

            ValidationUtil.RequireStrictBetween(val, from, to, "");
        }
    }
}
