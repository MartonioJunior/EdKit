using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class AffordanceEffect_Tests
    {
        [SetUp]
        public void CreateTestContext() {}
        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Methods
    public partial class AffordanceEffect_Tests
    {
        public static IEnumerable Initializer_UseCases()
        {
            yield return new object[]{ 0.2f, 0.4f, 0.2f, 0.4f };
            yield return new object[]{ -4.0f, 0.4f, -1.0f, 0.4f };
            yield return new object[]{ 3.6f, 0.3f, 1.0f, 0.3f };
            yield return new object[]{ 0.2f, 4.0f, 0.2f, 1.0f };
            yield return new object[]{ 0.6f, -8.0f, 0.6f, 0.0f };
        }
        [TestCaseSource(nameof(Initializer_UseCases))]
        public void Initializer_CreatesNewAffordanceEffectFromValues(float alignment, float scale, float expectedAlignment, float expectedScale)
        {
            var effect = new AffordanceEffect(alignment, scale, 1);

            Assert.AreEqual(expectedAlignment, effect.Alignment);
            Assert.AreEqual(expectedScale, effect.Scale);
        }
    }
    #endregion
}