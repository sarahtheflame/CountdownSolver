using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using CountdownSolver.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CountdownSolver.Lambda
{
    public class Function
    {
        
        /// <summary>
        /// Functions that instanciate a countdown solver and run it on the specified problem
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(List<int> inputNumbers, ILambdaContext context)
        {
            if (inputNumbers.Count < 2)
            {
                return "Error : numbers and a target must be provided.";
            }

            int[] numbersForArithmeticOperations = inputNumbers.Take(inputNumbers.Count - 1).ToArray();
            int target = inputNumbers.Last();

            DynamicSolver solver = new DynamicSolver();
            var bestSolution = solver.Run(numbersForArithmeticOperations, target);
            var bestResult = bestSolution.Item1;
            var expressionForBestResult = bestSolution.Item2;

            return $"Best result : {bestResult} = {expressionForBestResult}";
        }
    }
}
