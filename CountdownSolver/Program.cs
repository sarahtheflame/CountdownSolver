using System;
using System.Collections.Generic;

namespace CountdownSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicSolver dynamic = new DynamicSolver();
            dynamic.Run(new List<int>(){ 1,2,3,4,5,6}, 720);
            Console.ReadLine();
        }
    }
}
