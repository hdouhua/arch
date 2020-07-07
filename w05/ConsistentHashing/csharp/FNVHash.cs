using System.Text;

namespace ConsistentHashing
{
    /// <summary>
    /// Fowler–Noll–Vo is a non-cryptographic hash function
    /// - please refer to https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
    /// </summary>
    public class FNVHash : IHash
    {
        private static readonly uint FnvPrime32 = 16777619;
        private static readonly uint FnvOffset32 = 2166136261;

        /// <summary>
        /// FNV-1a hash - 32bit
        /// </summary>
        /// <param name="toHash"></param>
        /// <returns></returns>
        public uint ToHash32(string toHash)
        {
            var bytes = Encoding.UTF8.GetBytes(toHash);

            // actual algorithm
            uint hash = FnvOffset32;
            foreach (var chunk in bytes)
            {
                hash ^= chunk;
                hash *= FnvPrime32;
            }
            return hash;
        }

        public override string ToString()
        {
            return "Fowler–Noll–Vo";
        }
    }
}