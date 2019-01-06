using CountdownSolver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CountdownSolverTests
{
    public class DynamicSolverTests
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 5, 5, 6, 7 }, 720, 720, "(((2)*((5)+(7)))*(5))*(6)")]
        [InlineData(new int[] { 1, 2, 5, 5, 6, 7 }, 3999, 3150, "(((((1)+(2))*(5))*(5))*(6))*(7)")]
        [InlineData(new int[] { 3, 4, 5, 5, 7, 9 }, 709, 709, "((((4)*(5))*(5))*(7))+(9)")]
        [InlineData(new int[] { 1, 3, 7, 20, 23, 72 }, 757, 757, "(((1)+(7))*((23)+(72)))-(3)")]
        [InlineData(new int[] { 1, 3, 21, 25, 75, 100 }, 758, 758, "((((25)*(100))-(1))/(3))-(75)")]
        [InlineData(new int[] { 4, 6, 8, 8, 10, 100 }, 959, 959, "(((100)-(4))*(10))-((8)/(8))")]
        [InlineData(new int[] { 2, 5, 8, 9, 20, 25 }, 1246, 1246, "((((5)*(20))*(25))-(8))/(2)")]
        [InlineData(new int[] { 2, 5, 6, 7, 50, 75 }, 1277, 1277, "(2)+((((50)/(5))+(7))*(75))")]
        [InlineData(new int[] { 4, 5, 9, 12, 25, 100 }, 1319, 1319, "(((100)-(4))*((5)+(9)))-(25)")]
        [InlineData(new int[] { 2, 4, 5, 6, 9, 75 }, 1484, 1484, "((((4)*(75))-(2))*(5))-(6)")]
        [InlineData(new int[] { 3, 4, 7, 10, 25, 75 }, 1543, 1543, "(((75)-((3)+(10)))*(25))-(7)")]
        public void Run(int[] numbers, int target, int expectedBestResult, string expectedExpression)
        {
            DynamicSolver dynamicSolver = new DynamicSolver();
            Tuple<int, string> bestSolution = dynamicSolver.Run(numbers, target);
            var bestResult = bestSolution.Item1;
            var expressionForBestResult = bestSolution.Item2;

            Assert.Equal(bestResult, expectedBestResult);
            Assert.Equal(expressionForBestResult, expectedExpression);
        }
    }
}
