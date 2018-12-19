using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownSolver
{
    public class DynamicSolver
    {
        public Tuple<int, string> Run(int[] numbers, int target)
        {
            // Key: combination ordered and joined into a string separated by a comma
            // Value: A dictionary of possible results (key) and a list of expressions to obtain it (value)
            var possibleResultsFromCombination = new Dictionary<string, Dictionary<int, List<string>>>();
            Combination closestCombination = null;
            int closestResult = int.MinValue;

            foreach (int number in numbers)
            {
                Combination combination = new Combination(new int[] { number }, numbers);
                possibleResultsFromCombination[combination.ToString()] = new Dictionary<int, List<string>>() { { number, new List<string> { number.ToString() } } };
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
                            var mergedCombination = new Combination(leftOperand.OrderedNumbers.Union(rightOperand.OrderedNumbers).ToArray(), numbers);
                            possibleResultsFromCombination.Add(mergedCombination.ToString(), new Dictionary<int, List<string>>());

                            var possibleResultsOfLeftOperand = possibleResultsFromCombination[leftOperand.ToString()].ToList();
                            var possibleResultsOfRightOperand = possibleResultsFromCombination[rightOperand.ToString()].ToList();

                            foreach (var leftResult in possibleResultsOfLeftOperand)
                            {
                                int leftValue = leftResult.Key;
                                string leftExpression = leftResult.Value[0];
                                foreach(var rightResult in possibleResultsOfRightOperand)
                                {
                                    List<int> possibleResults = new List<int>() { closestResult };
                                    int rightValue = rightResult.Key;
                                    string rightExpression = rightResult.Value[0];

                                    int sum = leftValue + rightValue;
                                    AddResultSafely(possibleResultsFromCombination[mergedCombination.ToString()], sum, BuildExpression(leftExpression, rightExpression, "+"));
                                    possibleResults.Add(sum);

                                    int product = leftValue * rightValue;
                                    AddResultSafely(possibleResultsFromCombination[mergedCombination.ToString()], product, BuildExpression(leftExpression, rightExpression, "*"));
                                    possibleResults.Add(product);

                                    if (leftValue > rightValue)
                                    {
                                        int difference = leftValue - rightValue;
                                        AddResultSafely(possibleResultsFromCombination[mergedCombination.ToString()], difference, BuildExpression(leftExpression, rightExpression, "-"));
                                        possibleResults.Add(difference);
                                    }
                                    else if (rightValue > leftValue)
                                    {
                                        int difference = rightValue - leftValue;
                                        AddResultSafely(possibleResultsFromCombination[mergedCombination.ToString()], difference, BuildExpression(rightExpression, leftExpression, "-"));
                                        possibleResults.Add(difference);
                                    }

                                    if (leftValue % rightValue == 0)
                                    {
                                        int quotient = leftValue / rightValue;
                                        AddResultSafely(possibleResultsFromCombination[mergedCombination.ToString()], quotient, BuildExpression(leftExpression, rightExpression, "/"));
                                        possibleResults.Add(quotient);
                                    }
                                    else if (rightValue % leftValue == 0)
                                    {
                                        int quotient = rightValue / leftValue;
                                        AddResultSafely(possibleResultsFromCombination[mergedCombination.ToString()], quotient, BuildExpression(rightExpression, leftExpression, "/"));
                                        possibleResults.Add(quotient);
                                    }

                                    int newClosestResult = FindValueClosestToTarget(possibleResults, target);
                                    if (newClosestResult != closestResult)
                                    {
                                        closestResult = newClosestResult;
                                        closestCombination = mergedCombination;
                                    }

                                    if (closestResult == target)
                                    {
                                        return Tuple.Create(target, possibleResultsFromCombination[closestCombination.ToString()][target][0]);
                                    }
                                }
                            }

                        }
                    }

                }
            }
            return Tuple.Create(closestResult, possibleResultsFromCombination[closestCombination.ToString()][closestResult][0]);
        }

        private void AddResultSafely(Dictionary<int, List<string>> possibleResults, int result, string expression)
        {
            if (!possibleResults.ContainsKey(result))
            {
                possibleResults.Add(result, new List<string>());
            }

            possibleResults[result].Add(expression);
        }

        private string BuildExpression(string leftExpression, string rightExpression, string operatorSign)
        {
            return $"({leftExpression}){operatorSign}({rightExpression})";
        }

        private int FindValueClosestToTarget(List<int> values, int target)
        {
            int closestValueToTarget = int.MinValue;

            foreach(int value in values)
            {
                int differenceBetweenClosestAndTarget = Math.Abs(target - closestValueToTarget);
                int differenceBetweenCurrentAndTarget = Math.Abs(target - value);
                if (differenceBetweenCurrentAndTarget < differenceBetweenClosestAndTarget)
                {
                    closestValueToTarget = value;
                }
            }

            return closestValueToTarget;
        }
        
    }
}
