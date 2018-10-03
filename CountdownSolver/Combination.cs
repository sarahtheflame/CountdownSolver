using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownSolver
{
    public class Combination
    {
        private readonly int[] allOrderedNumbers;
        public readonly int[] consecutiveIdenticalNumbersCounts;
        private int[] orderedNumbers;

        public Combination(int[] allNumbers, int[] numbers)
        {
            this.allOrderedNumbers = allNumbers.OrderBy(x => x).ToArray();
            this.orderedNumbers = numbers.OrderBy(x => x).ToArray();

            int[] differences = this.allOrderedNumbers.Zip(this.allOrderedNumbers.Skip(1), (first, second) => second - first).ToArray();

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

        /*public static Combination operator++ (Combination combination)
        {
            int[] 
        }*/

        public int Size()
        {
            return this.orderedNumbers.Length;
        }

        private int Dimension { get => this.allOrderedNumbers.Length; }
    }
}
