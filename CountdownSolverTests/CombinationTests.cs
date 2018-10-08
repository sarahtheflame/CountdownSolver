using CountdownSolver;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountdownSolverTests
{
    public class CombinationTests
    {
        [Theory]
        [InlineData(new int[] { 1, 3, 5 }, new int[] { 1, 3, 6 }, new int[] { 1, 3, 5, 5, 6, 7 })]
        [InlineData(new int[] { 1, 3, 7 }, new int[] { 1, 5, 5 }, new int[] { 1, 3, 5, 5, 6, 7 })]
        [InlineData(new int[] { 1, 5, 5 }, new int[] { 1, 5, 6 }, new int[] { 1, 3, 5, 5, 6, 7 })]
        [InlineData(new int[] { 1, 5, 7 }, new int[] { 1, 6, 7 }, new int[] { 1, 3, 5, 5, 6, 7 })]
        [InlineData(new int[] { 1, 6, 7 }, new int[] { 3, 5, 5 }, new int[] { 1, 3, 5, 5, 6, 7 })]
        [InlineData(new int[] { 5, 6, 7 }, new int[] { 1, 3, 5 }, new int[] { 1, 3, 5, 5, 6, 7 })]
        [InlineData(new int[] { 2, 5 }, new int[] { 2, 7}, new int[] { 2, 5, 5, 7, 7 })]
        [InlineData(new int[] { 5, 7 }, new int[] { 7, 7 }, new int[] { 2, 5, 5, 7, 7 })]
        [InlineData(new int[] { 2, 2 }, new int[] { 2, 2 }, new int[] { 2, 2, 2, 2 })]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [InlineData(new int[] { 1 }, new int[] { 1 }, new int[] { 1 })]
        public void Increment(int[] numbers, int[] expectedNumbers, int[] numbersSet)
        {
            Combination combination = new Combination(numbers, numbersSet);
            Combination expectedCombination = new Combination(expectedNumbers, numbersSet);
            combination++;
            Assert.Equal(expectedCombination, combination);
        }
    }
}
