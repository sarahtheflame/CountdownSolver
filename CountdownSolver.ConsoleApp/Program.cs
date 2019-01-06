using CountdownSolver.Core;
using System;

namespace CountdownSolver.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicSolver dynamic = new DynamicSolver();
            var bestSolution = dynamic.Run(new int[] { 1, 2, 5, 5, 6, 7 }, 720);
            var bestResult = bestSolution.Item1;
            var expressionForBestResult = bestSolution.Item2;

            Console.WriteLine($"Best result : {bestResult} = {expressionForBestResult}");
            Console.ReadLine();
        }
    }
}
