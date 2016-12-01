using System;

namespace Combinatorics
{
    /// <summary>
    /// General backtracking algorithm for up to 2^64 possible solutions.
    /// Runtime complexity: O(n * 2^n) where 0>=n<=64 and assuming prune method is O(n)
    /// Space complexity: O(2^n+1)
    /// </summary>
    public class Backtrack64
    {
        private readonly int _n;
        private readonly Func<ulong, bool> _prune;

        /// <param name="n">
        /// Constrains the solution space to 2^n with n bits representing each candidate solution. 0>=n<=64
        /// </param>
        /// <param name="prune">
        /// Returns true if the solution tree should be pruned at the given candidate.
        /// Also gives the client an opportunity to process the candidate.
        /// </param>
        private Backtrack64(int n, Func<ulong, bool> prune)
        {
            if (n < 0 || n > sizeof(ulong) * 8)
            {
                throw new ArgumentOutOfRangeException(
                    $"{nameof(n)} must be between 0 and {sizeof(ulong) * 8} inclusively");
            }

            _n = n;
            _prune = prune;
        }

        public static void Run(int n, Func<ulong, bool> prune) => new Backtrack64(n, prune).Bt(0u, 0, true);

        // TODO: Don't check right branches just recurse.
        private void Bt(ulong candidate, int depth, bool leftBranch)
        {
            // Stop recursion if the branch should be pruned.
            // Only left branches are checked for pruning because
            // right branches have the same candidate as the parent.
            if (leftBranch && _prune(candidate)) { return; }

            // Stop recursion if the bottom of the tree has been reached.
            if (depth == _n) { return; }

            Bt(candidate | (1u << depth), depth + 1, true); // Left branch
            Bt(candidate, depth + 1, false); // Right branch
        }

        /// <summary>
        /// using static Backtrack64;
        /// </summary>
        public static void Backtrack(int n, Func<ulong, bool> prune) => Backtrack64.Run(n, prune);
    }
}