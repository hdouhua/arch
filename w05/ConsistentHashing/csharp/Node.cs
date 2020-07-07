using System;

namespace ConsistentHashing
{
    public class Node
    {
        public String IP { get; }

        public Node(String ipAddress)
        {
            this.IP = ipAddress;
        }

        public override string ToString()
        {
            return this.IP;
        }
    }
}