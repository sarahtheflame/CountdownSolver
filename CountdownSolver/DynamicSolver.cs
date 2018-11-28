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
        public void Run(int[] numbers, int target)
        {
            var possibleResultsFromCombination = new Dictionary<string, Dictionary<int, List<string>>>();

            foreach (int number in numbers)
            {
                possibleResultsFromCombination[number.ToString()] = new Dictionary<int, List<string>>() { { number, new List<string> { number.ToString() } } };
            }

            for (int i = 2; i < numbers.Length + 1; ++i)
            {
                int maximumSizeOfLeftOperand = i / 2;

                for (int j = maximumSizeOfLeftOperand; j > 0; j--)
                {
                    int sizeOfLeftOperand = j;
                    int sizeOfRightOperand = i - j;

                    CombinationCollection leftOperands = new CombinationCollection(sizeOfLeftOperand, numbers);
                    foreach (Combination leftOperand in leftOperands)
                    {
                        var rightOperandNumbersSet = numbers.Except(leftOperand.OrderedNumbers).ToArray();
                        CombinationCollection rightOperands = new CombinationCollection(sizeOfRightOperand, rightOperandNumbersSet);
                        foreach (Combination rightOperand in rightOperands)
                        {

                        }
                    }

                }
            } 
        }
        
    }
}
