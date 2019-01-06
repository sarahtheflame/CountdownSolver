using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using CountdownSolver.Lambda;

namespace CountdownSolver.Lambda.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestSolverCountdownFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var result = function.FunctionHandler(new List<int> { 1, 2, 5, 5, 6, 7, 720 }, context);

            Assert.Equal("Best result : 720 = (((2)*((5)+(7)))*(5))*(6)", result);
        }
    }
}
