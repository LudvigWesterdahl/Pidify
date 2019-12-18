using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pidify.Utils;
using System;

namespace PidifyUnitTest.Utils
{
    [TestClass]
    public sealed class PointPairTest
    {
        public static bool AlmostEqual(float a, float b, float epsilon)
        {
            const float floatNormal = (1 << 23) * float.Epsilon;
            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if (a == b)
            {
                // Shortcut, handles infinities
                return true;
            }

            if (a == 0.0f || b == 0.0f || diff < floatNormal)
            {
                // a or b is zero, or both are extremely close to it.
                // relative error is less meaningful here
                return diff < (epsilon * floatNormal);
            }

            // use relative error
            return diff / Math.Min((absA + absB), float.MaxValue) < epsilon;
        }

        private void AlmostEqual(PointPair actual, PointPair expected)
        {
            Assert.IsTrue(AlmostEqual(actual.FromX, expected.FromX, 0.001f), $"actual.FromX({actual.FromX}) does not equal expected.FromX({expected.FromX})");
            Assert.IsTrue(AlmostEqual(actual.FromY, expected.FromY, 0.001f), $"actual.FromY({actual.FromY}) does not equal expected.FromY({expected.FromY})");
            Assert.IsTrue(AlmostEqual(actual.ToX, expected.ToX, 0.001f), $"actual.ToX({actual.ToX}) does not equal expected.ToX({expected.ToX})");
            Assert.IsTrue(AlmostEqual(actual.ToY, expected.ToY, 0.001f), $"actual.ToY({actual.ToY}) does not equal expected.ToY({expected.ToY})");
        }

        [TestMethod]
        public void Test_MaximizeInside_SmallerWidth()
        {
            var pointPair = PointPair.NewInstance(
                0.0f, 0.1f, 
                0.2f, 0.7f);

            var dest = PointPair.NewInstance(
                0.0f, 0.0f, 
                0.5f, 0.5f);

            var expected = PointPair.NewInstance(0.1666f, 0f, 0.3333f, 0.5f);
            var actual = pointPair.MaximizeInside(dest);

            AlmostEqual(actual, expected);
        }

        [TestMethod]
        public void Test_MaximizeInside_WiderWidth()
        {
            var pointPair = PointPair.NewInstance(
                0.0f, 0.1f,
                0.8f, 0.5f);

            var dest = PointPair.NewInstance(
                0.0f, 0.0f,
                0.4f, 1.0f);

            var expected = PointPair.NewInstance(0.0f, 0.4f, 0.4f, 0.6f);
            var actual = pointPair.MaximizeInside(dest);

            AlmostEqual(actual, expected);
        }


    }
}
