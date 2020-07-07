namespace ConsistentHashing
{
    public interface IHash
    {
        /// <summary>
        /// Hash to UInt32
        /// </summary>
        /// <param name="toHash">string to hash</param>
        /// <returns></returns>
        public uint ToHash32(string toHash);
    }
}