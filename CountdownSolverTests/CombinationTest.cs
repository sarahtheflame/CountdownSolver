using System;
using CountdownSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountdownSolverTests
{
    [TestClass]
    public class CombinationTest
    {
        [TestMethod]
        public void initialization()
        {
            Combination combination = new Combination(new int[] { 1, 5, 5, 5, 5, 6 }, new int[] { 1, 5, 5 });
            CollectionAssert.AreEqual(combination.consecutiveIdenticalNumbersCounts, new int[] { 0, 3, 2, 1, 0, 0 });

            Combination combination2 = new Combination(new int[] { 1, 2,3,4,4,6,6 }, new int[] { 1, 2, 3 });
            CollectionAssert.AreEqual(combination2.consecutiveIdenticalNumbersCounts, new int[] { 0,0,0,1,0,1,0 });
        }
    }
}
