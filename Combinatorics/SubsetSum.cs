using System;
using System.Collections.Generic;
using System.Linq;
using static Combinatorics.Backtrack64;

namespace Combinatorics
{
    public static class SubsetSum
    {
        class Solution : List<int>, IEquatable<Solution>
        {
            public Solution(IEnumerable<int> values) : base(values)
            {
                Sort();
            }

            public bool Equals(Solution other)
            {
                // TODO: does SequenceEqual first check if count is equal?
                return this.SequenceEqual(other);
            }

            public override int GetHashCode()
            {
                int hashcode = 0;

                foreach (int x in this)
                {
                    hashcode ^= x;
                }

                return hashcode;
            }

            public override string ToString() => string.Join(", ", this);
        }

        public static IEnumerable<List<int>> Run(IList<int> values, long sum)
        {
            if(values.Count > sizeof(ulong) * 8)
            {
                throw new ArgumentException(
                    $"{nameof(values)} may only contain up to {sizeof(ulong) * 8} items.");
            }

            if(sum <= 0)
            {
                throw new ArgumentException($"{nameof(sum)} must be greater than 0.");
            }

            if(values.Any(x => x < 0))
            {
                if(sum <= 0)
                {
                    throw new ArgumentException($"{nameof(SubsetSum)} only works with positive values.");
                }
            }

            var solutions = new HashSet<Solution>();

            Backtrack(
                values.Count,
                candidate =>
                {
                    var candidateSum = candidate.AllSetBits(values.Count).Sum(i => values[i]);

                    if (candidateSum == sum)
                    {
                        solutions.Add(
                            new Solution(
                                candidate.AllSetBits(values.Count).Select(i => values[i])));
                    }

                    return candidateSum >= sum;
                });

            return solutions;
        }

        private static IEnumerable<int> AllSetBits(this ulong bits, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if ((bits & (1u << i)) != 0)
                {
                    yield return i;
                }
            }
        }
    }
}
