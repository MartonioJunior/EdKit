using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class AffordanceData_Tests
    {
        [SetUp]
        public void CreateTestContext() {}
        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Methods (IAffordance)
    public partial class AffordanceData_Tests
    {
        public static IEnumerable Audio_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Audio_UseCases))]
        public void Audio_ReturnsDefinedAudioClip()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Color_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Color_UseCases))]
        public void Color_ReturnsColorForAffordance()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Material_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Material_UseCases))]
        public void Material_ReturnsMaterialForAffordance()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Text_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Text_UseCases))]
        public void Text_ReturnsDisplayStringForAffordance()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Run_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Run_UseCases))]
        public void Run_InvokesActionUnityEvent()
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion


}