using Xunit;
using Xunit.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Combinatorics.Tests
{
    public class SubsetSumTests
    {
        private readonly ITestOutputHelper _output;

        public SubsetSumTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact()]
        public void RunTest()
        {
            var values = new List<int>
            {
                8, 8, 8, 8, 8, 8, 8, 8,
                10, 10, 10, 10, 10, 10,
                12, 12, 12, 12, 12,
                16, 16, 16, 16,
                20, 20, 20
            };

            var sum = 68;

            var actual = SubsetSum.Run(values, sum);

            var expectedCount = 32;
            Assert.Equal(expectedCount, actual.Count());

            _output.WriteLine($"Count: {actual.Count()}");
            foreach (var solution in actual)
            {
                _output.WriteLine(string.Join(" ", solution));
            }
        }
    }
}