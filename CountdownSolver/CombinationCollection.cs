using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownSolver
{
    public class CombinationCollection : IEnumerable<Combination>, IEnumerator<Combination>
    {
        private Combination combination;
        private Combination initialCombination;

        object IEnumerator.Current => Current;

        public Combination Current
        {
            get
            {
                return new Combination(this.combination);
            }
            private set
            {
                this.combination = value;
            }
        }

        public CombinationCollection(int size, int[] numbersSet)
        {
            this.initialCombination = new Combination(size, numbersSet);
        }


        public IEnumerator<Combination> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (this.combination == null)
            {
                this.combination = new Combination(this.initialCombination);
                return true;
            }
            this.combination++;
            return !this.combination.Equals(this.initialCombination);
        }

        public void Reset()
        {
            this.combination = this.initialCombination;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
        }
    }
}
