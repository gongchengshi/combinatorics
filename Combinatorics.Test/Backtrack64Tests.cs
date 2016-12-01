using System;
using System.Collections.Generic;
using Xunit;

namespace Combinatorics.Test
{
    public class Backtrack64Tests
    {
        [Fact]
        public void NoPruning()
        {
            var actualSolutions = new List<ulong>();
            var expectedSolutions = new List<ulong>()
            {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7
            };
            expectedSolutions.Sort();

            int n = 3;
            Backtrack64.Run(n, candidate =>
            {
                actualSolutions.Add(candidate);
                return false;
            });
            actualSolutions.Sort();

            Assert.Equal(Math.Pow(2, n), actualSolutions.Count);
            Assert.Equal(expectedSolutions, actualSolutions);
        }

        [Fact]
        public void PruneAfter3()
        {
            var actualSolutions = new List<ulong>();
            var expectedSolutions = new List<ulong>()
            {
                0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6
            };
            expectedSolutions.Sort();

            int n = 3;
            Backtrack64.Run(n, candidate =>
            {
                actualSolutions.Add(candidate);
                return candidate >= 3;
            });
            actualSolutions.Sort();

            Assert.Equal(expectedSolutions, actualSolutions);
        }
    }
}
