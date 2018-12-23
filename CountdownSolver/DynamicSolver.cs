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
                CombinationCollection currentCombinations = new CombinationCollection(i, numbers);

                foreach (Combination combination in currentCombinations)
                {
                    var possibleOperandsList = SplitCombinationIntoOperandsList(combination);
                    possibleResultsFromCombination.Add(combination.ToString(), new Dictionary<int, List<string>>());

                    foreach (var operands in possibleOperandsList)
                    {
                        var leftOperand = operands.Item1;
                        var rightOperand = operands.Item2;
                        var possibleResultsOfLeftOperand = possibleResultsFromCombination[leftOperand.ToString()].ToList();
                        var possibleResultsOfRightOperand = possibleResultsFromCombination[rightOperand.ToString()].ToList();

                        foreach (var leftResult in possibleResultsOfLeftOperand)
                        {
                            int leftValue = leftResult.Key;
                            string leftExpression = leftResult.Value[0];
                            foreach (var rightResult in possibleResultsOfRightOperand)
                            {
                                List<int> possibleResults = new List<int>() { closestResult };
                                int rightValue = rightResult.Key;
                                string rightExpression = rightResult.Value[0];

                                int sum = leftValue + rightValue;
                                AddResultSafely(possibleResultsFromCombination[combination.ToString()], sum, BuildExpression(leftExpression, rightExpression, "+"));
                                possibleResults.Add(sum);

                                int product = leftValue * rightValue;
                                AddResultSafely(possibleResultsFromCombination[combination.ToString()], product, BuildExpression(leftExpression, rightExpression, "*"));
                                possibleResults.Add(product);

                                if (leftValue > rightValue)
                                {
                                    int difference = leftValue - rightValue;
                                    AddResultSafely(possibleResultsFromCombination[combination.ToString()], difference, BuildExpression(leftExpression, rightExpression, "-"));
                                    possibleResults.Add(difference);
                                }
                                else if (rightValue > leftValue)
                                {
                                    int difference = rightValue - leftValue;
                                    AddResultSafely(possibleResultsFromCombination[combination.ToString()], difference, BuildExpression(rightExpression, leftExpression, "-"));
                                    possibleResults.Add(difference);
                                }

                                if (leftValue % rightValue == 0)
                                {
                                    int quotient = leftValue / rightValue;
                                    AddResultSafely(possibleResultsFromCombination[combination.ToString()], quotient, BuildExpression(leftExpression, rightExpression, "/"));
                                    possibleResults.Add(quotient);
                                }
                                else if (rightValue % leftValue == 0)
                                {
                                    int quotient = rightValue / leftValue;
                                    AddResultSafely(possibleResultsFromCombination[combination.ToString()], quotient, BuildExpression(rightExpression, leftExpression, "/"));
                                    possibleResults.Add(quotient);
                                }

                                int newClosestResult = FindValueClosestToTarget(possibleResults, target);
                                if (newClosestResult != closestResult)
                                {
                                    closestResult = newClosestResult;
                                    closestCombination = combination;
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

            foreach (int value in values)
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

        private List<Tuple<Combination, Combination>> SplitCombinationIntoOperandsList(Combination combination)
        {
            var partialOperands = new List<Tuple<List<int>, List<int>>>();
            partialOperands.Add(Tuple.Create(new List<int> { combination.OrderedNumbers[0] }, new List<int>()));

            return SplitNumbersIntoBuckets(partialOperands, combination.OrderedNumbers.Skip(1).ToList());
        }

        private List<Tuple<Combination, Combination>> SplitNumbersIntoBuckets(List<Tuple<List<int>, List<int>>> partialOperandsList, List<int> remainingNumbers)
        {
            if (remainingNumbers.Count == 0)
            {
                return partialOperandsList
                    .Where(partialOperands => partialOperands.Item1.Count > 0 && partialOperands.Item2.Count > 0)
                    .Select(partialOperands =>
                        Tuple.Create(
                            new Combination(partialOperands.Item1.Count, partialOperands.Item1.ToArray()),
                            new Combination(partialOperands.Item2.Count, partialOperands.Item2.ToArray())
                        ))
                    .Distinct()
                    .ToList();
            }
            else
            {
                int number = remainingNumbers[0];
                var newPartialOperandsList = partialOperandsList.SelectMany(partialOperands =>
                    new List<Tuple<List<int>, List<int>>> {
                        Tuple.Create(
                            partialOperands.Item1.Concat(new List<int>() { number }).ToList(),
                            partialOperands.Item2
                        ),
                        Tuple.Create(
                            partialOperands.Item1,
                            partialOperands.Item2.Concat(new List<int>() { number }).ToList()
                        )
                    }
                ).ToList();
                return SplitNumbersIntoBuckets(newPartialOperandsList, remainingNumbers.Skip(1).ToList());
            }
        }

    }
}
