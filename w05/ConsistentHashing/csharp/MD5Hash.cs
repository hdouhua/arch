using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsistentHashing
{
    public class MD5Hash : IHash
    {
        public uint ToHash32(string toHash)
        {
            using MD5 md5 = MD5.Create();
            var hashed = md5.ComputeHash(Encoding.UTF8.GetBytes(toHash));
            return BitConverter.ToUInt32(hashed, 0);
        }

        public override string ToString()
        {
            return "MD5";
        }
    }
}