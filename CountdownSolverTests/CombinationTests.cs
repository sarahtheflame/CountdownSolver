using CountdownSolver;
using CountdownSolver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void Iterate()
        {
            int[] numbersSet = new int[] { 1, 2, 5, 5, 6, 7 };
            CombinationCollection collection = new CombinationCollection(5, numbersSet);
            var combinations = collection.ToList();

            Assert.Equal(5, combinations.Count);
            Assert.Equal(new Combination(new int[] { 1, 2, 5, 5, 6 }, numbersSet), combinations[0]);
            Assert.Equal(new Combination(new int[] { 1, 2, 5, 5, 7 }, numbersSet), combinations[1]);
            Assert.Equal(new Combination(new int[] { 1, 2, 5, 6, 7 }, numbersSet), combinations[2]);
            Assert.Equal(new Combination(new int[] { 1, 5, 5, 6, 7 }, numbersSet), combinations[3]);
            Assert.Equal(new Combination(new int[] { 2, 5, 5, 6, 7 }, numbersSet), combinations[4]);
        }
    }
}
