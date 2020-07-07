using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsistentHashing
{
    class Program
    {
        const int ItemCount = 100_0000;
        const int NumberOfNodes = 10;
        const int NumberOfReplicas = 200;

        static void Main(string[] args)
        {
            var nodes = new List<Node>();
            for (int i = 1; i <= NumberOfNodes; i++)
            {
                nodes.Add(new Node($"10.1.1.{i}"));
            }

            var hashFuncs = new IHash[] { new MurmurHash3(4049661204), new MD5Hash(), new FNVHash() };
            var serverCluster = new ConsistentHash<Node>(hashFuncs[0], NumberOfReplicas, nodes);
            // serverCluster.Print();

            TestAddOrRemove(serverCluster);

            Statistics(nodes, serverCluster);
        }

        private static void TestAddOrRemove(ConsistentHash<Node> cluster)
        {
            var keys = new[] { "66778899", "hello world", "consistent hashing" };
            cluster.FindNode(keys);

            var newNode = new Node($"10.1.1.{NumberOfNodes}");
            cluster.RemoveFromRing(newNode);
            Console.WriteLine($"\nAfter removed a Node [{newNode}]:\n--------------------");
            cluster.FindNode(keys);

            cluster.AddToRing(newNode);
            Console.WriteLine($"\nAfter added a new Node [{newNode}]:\n--------------------");
            cluster.FindNode(keys);
        }

        private static void Statistics(IList<Node> nodes, ConsistentHash<Node> cluster)
        {
            var statDict = new Dictionary<String, int>();
            foreach (var n in nodes)
            {
                statDict.Add(n.IP, 0);
            }

            for (int i = 0; i < ItemCount; i++)
            {
                var node = cluster.GetNode($"key_{i}");
                statDict[node.IP] += 1;
            }

            var mean = statDict.Values.Average();
            var std = statDict.Values.StdDev();

            Console.WriteLine(@$"
Algorithm Statistics:

{cluster.ToString()}
--------------------
Total: {ItemCount:N0}
Mean: {mean:N0}
StdDev: {std:F2}
");

            foreach (var (k, v) in statDict)
            {
                Console.WriteLine("Node [{0}]: {1}", k, v);
            }
        }

    }

    static class Extensions
    {
        public static double StdDev(this IEnumerable<int> values)
        {
            if (values.Count() > 1)
            {
                double avg = values.Average();
                double sum = values.Sum(v => Math.Pow(v - avg, 2));
                //FIXME: use values.count instead of (values.count - 1) because the number of values is too small
                double denominator = values.Count();// values.Count() - 1;
                return Math.Sqrt(sum / denominator);
            }

            return -1;
        }

        public static void FindNode(this ConsistentHash<Node> cluster, String[] keys)
        {
            foreach (var key in keys)
            {
                cluster.FindNode(key);
            }
        }

        public static void FindNode(this ConsistentHash<Node> cluster, String key)
        {
            var found = cluster.GetNode(key);
            Console.WriteLine($"Key [{key}] - Hash [{cluster.GetHashKey(key)}], route to Node [{found.IP}]");
        }
    }
}