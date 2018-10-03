using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownSolver
{
    public class DynamicSolver
    {
        public void Run(List<int> numbers, int target)
        {
            int[] orderedNumbers = numbers.OrderBy(x => x).ToArray();

            List <Tuple<List<int>, List<int>>> size2Operands = GetSize2Operands(orderedNumbers);
            List<Tuple<List<int>, List<int>>> size3Operands = new List<Tuple<List<int>, List<int>>>();
            List<Tuple<List<int>, List<int>>> size4Operands = new List<Tuple<List<int>, List<int>>>();
            List<Tuple<List<int>, List<int>>> size5Operands = new List<Tuple<List<int>, List<int>>>();
            List<Tuple<List<int>, List<int>>> size6Operands = new List<Tuple<List<int>, List<int>>>();

            Dictionary<string, SortedSet<int>> operationResults = new Dictionary<string, SortedSet<int>>();


            Console.WriteLine(JsonConvert.SerializeObject(size2Operands));
        }

        private List<Tuple<List<int>, List<int>>> GetSize2Operands(int[] orderedNumbers)
        {
            List<Tuple<List<int>, List<int>>> size2Operands = new List<Tuple<List<int>, List<int>>>();

            for (int i = 0; i < orderedNumbers.Length - 1; ++i)
            {
                List<int> leftOperand = new List<int>() { orderedNumbers[i] };
                for (int j = i + 1; j < orderedNumbers.Length; ++j)
                {
                    List<int> rightOperand = new List<int>() { orderedNumbers[j] };
                    Tuple<List<int>, List<int>> operands = Tuple.Create(leftOperand, rightOperand);
                    size2Operands.Add(operands);
                }

            }

            return size2Operands;
        }

        /*private List<Tuple<List<int>, List<int>>> GetSize3Operands(int[] orderedNumbers)
        {
            List<Tuple<List<int>, List<int>>> size3Operands = new List<Tuple<List<int>, List<int>>>();

            List<Tuple<List<int>, List<int>>> size2Operands = GetSize2Operands(orderedNumbers);
        }*/
    }
}
