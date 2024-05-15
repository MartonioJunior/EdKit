using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class Position_Tests
    {
        [SetUp]
        public void CreateTestContext() {}
        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Mocking
    public partial class Position_Tests
    {
        public static Position Mock
        {
            get => new Position(new Vector3(1, 2, 3), Vector3.one);
        }
    }
    #endregion

    #region Test Cases
    public partial class Position_Tests
    {
        public static IEnumerable Value_UseCases()
        {
            yield return new object[]{new Vector3(1,2,3), 1, 2, 3};
        }
        [TestCaseSource(nameof(Value_UseCases))]
        public void Value_ReturnsPositionForStructure(Vector3 vector, float x, float y, float z)
        {
            var position = new Position(vector);

            Assert.AreEqual(x, position.Value.x);
            Assert.AreEqual(y, position.Value.y);
            Assert.AreEqual(z, position.Value.z);
        }

        public static IEnumerable Deadzone_UseCases()
        {
            yield return new object[]{new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0.05f, 0.05f, 0.05f)};
        }
        [TestCaseSource(nameof(Deadzone_UseCases))]
        public void Deadzone_ReturnsBoundsForPositionReference(Vector3 vector, Vector3 deadzoneResult)
        {
            var position = new Position(Vector3.zero, vector);

            Assert.AreEqual(deadzoneResult, position.Deadzone);
        }

        public static IEnumerable Initializer_Vector3_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Initializer_Vector3_UseCases))]
        public void Initializer_Vector3_CreatesNewPositionWithDefaultDeadzone()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Initializer_Vector3_Vector3_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Initializer_Vector3_Vector3_UseCases))]
        public void Initializer_Vector3_Vector3_CreatesNewPositionWithValueAndDeadzone()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Contains_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Contains_UseCases))]
        public void Contains_ChecksWhenValueIsInsidePositionBounds()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable PrecisionFor_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(PrecisionFor_UseCases))]
        public void PrecisionFor_ReturnsProximityOfPointToCenterOfBounds()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable XFor_UseCases()
        {
            yield return new object[]{new Position(new Vector3(1, 2, 3), Vector3.one), new Vector3(0.5f, 1.5f, 2.75f), Axis.X.Center};
            yield return new object[]{new Position(new Vector3(1, 2, 3), Vector3.one), new Vector3(3, 2, 1), Axis.X.Right};
            yield return new object[]{new Position(new Vector3(1, 2, 3), Vector3.one), new Vector3(-1, 2, 3), Axis.X.Left};
        }
        [TestCaseSource(nameof(XFor_UseCases))]
        public void XFor_ReturnsPositionInAxisXForValue(Position position, Vector3 relativePosition, Axis.X expected)
        {
            Assert.AreEqual(expected, position.XFor(relativePosition));
        }

        public static IEnumerable YFor_UseCases()
        {
            yield return new object[]{new Position(new Vector3(1, 2, 3), Vector3.one), new Vector3(0.5f, 1.75f, 2.75f), Axis.Y.Neutral};
            yield return new object[]{new Position(new Vector3(1, 2, 3), Vector3.one), new Vector3(1, 4, 1), Axis.Y.Above};
            yield return new object[]{new Position(new Vector3(1, 2, 3), Vector3.one), new Vector3(1, 0, 3), Axis.Y.Below};
        }
        [TestCaseSource(nameof(YFor_UseCases))]
        public void YFor_ReturnsPositionInAxisYForValue(Position position, Vector3 relativePosition, Axis.Y expected)
        {
            Assert.AreEqual(expected, position.YFor(relativePosition));
        }

        public static IEnumerable ZFor_UseCases()
        {
            yield return new object[]{new Position(new Vector3(1, 2, 3), Vector3.one), new Vector3(0.5f, 1.5f, 2.75f), Axis.Z.Body};
            yield return new object[]{new Position(new Vector3(1, 2, 3), Vector3.one), new Vector3(1, 2, 4), Axis.Z.Front};
            yield return new object[]{new Position(new Vector3(1, 2, 3), Vector3.one), new Vector3(1, 2, 0), Axis.Z.Back};
        }
        [TestCaseSource(nameof(ZFor_UseCases))]
        public void ZFor_ReturnsPositionInAxisZForValue(Position position, Vector3 relativePosition, Axis.Z expected)
        {
            Assert.AreEqual(expected, position.ZFor(relativePosition));
        }
    }
    #endregion
}