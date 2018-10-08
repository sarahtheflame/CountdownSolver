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

        object IEnumerator.Current => Current;

        public Combination Current => combination;

        public CombinationCollection(int size, int[] numbersSet)
            : this(new Combination(size, numbersSet))
        {
        }

        public CombinationCollection(Combination startCombination)
        {
            this.combination = startCombination;
        }

        public IEnumerator<Combination> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
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
