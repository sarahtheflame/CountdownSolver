using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownSolver.Core
{
    public class Combination
    {
        private readonly int[] orderedNumbersSet;
        private readonly int[] consecutiveIdenticalNumbersCounts;
        public int[] OrderedNumbers { get; private set; }

        public Combination(int size, int[] numbersSet)
            : this(numbersSet.Take(size).ToArray(), numbersSet)
        {
        }

        public Combination(int[] numbers, int[] numbersSet)
        {
            Debug.Assert(numbers.Length <= numbersSet.Length, "Combination size must be smaller than dimension");
            Debug.Assert(numbers.All(numbersSet.Contains), "Combination numbers must be part of numbers set");

            this.orderedNumbersSet = numbersSet.OrderBy(x => x).ToArray();
            this.OrderedNumbers = numbers.OrderBy(x => x).ToArray();

            int[] differences = this.orderedNumbersSet.Zip(this.orderedNumbersSet.Skip(1), (first, second) => second - first).ToArray();

            this.consecutiveIdenticalNumbersCounts = new int[this.Dimension];
            this.consecutiveIdenticalNumbersCounts[this.Dimension - 1] = 0;

            int consecutiveIdenticalNumbers = 0;
            for (int i = this.Dimension -2; i >= 0; --i)
            {
                if (differences[i] == 0)
                {
                    consecutiveIdenticalNumbers++;
                }
                else
                {
                    consecutiveIdenticalNumbers = 0;
                }

                this.consecutiveIdenticalNumbersCounts[i] = consecutiveIdenticalNumbers;
            }
        }

        public Combination(Combination combination)
        {
            this.consecutiveIdenticalNumbersCounts = combination.consecutiveIdenticalNumbersCounts;
            this.OrderedNumbers = combination.OrderedNumbers;
            this.orderedNumbersSet = combination.orderedNumbersSet;
        }

        public static Combination operator++ (Combination combination)
        {
            for (int i = combination.Size -1; i >= 0; --i)
            {
                int index = Array.IndexOf(combination.orderedNumbersSet, combination.OrderedNumbers[i]);
                int consecutive = combination.consecutiveIdenticalNumbersCounts[index];
                int nextIndex = index + consecutive + 1;
                int remainingPositionsToFill = combination.Size - i;
                int availableNumbers = combination.Dimension - nextIndex;

                if (availableNumbers >= remainingPositionsToFill)
                {
                    IEnumerable<int> combinationBeginning = combination.OrderedNumbers.Take(i);
                    IEnumerable<int> combinationEnding = combination.orderedNumbersSet.Skip(nextIndex).Take(remainingPositionsToFill);
                    combination.OrderedNumbers = combinationBeginning.Concat(combinationEnding).ToArray();
                    break;
                }
                else if (i == 0)
                {
                    combination.OrderedNumbers = combination.orderedNumbersSet.Take(combination.Size).ToArray();
                }

            }

            return combination;
        }

        public static bool operator ==(Combination combination1, Combination combination2)
        {
            return EqualityComparer<Combination>.Default.Equals(combination1, combination2);
        }

        public static bool operator !=(Combination combination1, Combination combination2)
        {
            return !(combination1 == combination2);
        }

        public int Size { get => this.OrderedNumbers.Length; }

        public int Dimension { get => this.orderedNumbersSet.Length; }

        public override bool Equals(object obj)
        {
            var combination = obj as Combination;
            return combination != null &&
                   Enumerable.SequenceEqual(orderedNumbersSet, combination.orderedNumbersSet) &&
                   Enumerable.SequenceEqual(OrderedNumbers, combination.OrderedNumbers);
        }

        public override int GetHashCode()
        {
            var hashCode = 1638634613;
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(orderedNumbersSet);
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(consecutiveIdenticalNumbersCounts);
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(OrderedNumbers);
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + Dimension.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return String.Join(",", this.OrderedNumbers);
        }
    }
}
