using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsistentHashing
{
    public class ConsistentHash<T> where T : class
    {
        private readonly IHash _hashFunc;
        private readonly SortedDictionary<uint, T> _hashRing;
        // to accelerate key search in SortedDictionary
        private uint[] _sortedKeys = null;
        private readonly int _numberOfReplicas;

        public ConsistentHash(IHash hashFunc, int numberOfReplicas, List<T> nodes)
        {
            this._hashFunc = hashFunc;
            this._numberOfReplicas = numberOfReplicas;

            this._hashRing = new SortedDictionary<uint, T>();

            if (nodes != null)
            {
                foreach (var s in nodes)
                {
                    this.AddToRing(s, false);
                }

                this._sortedKeys = this._hashRing.Keys.ToArray();
            }
        }

        public void AddToRing(T node, bool updateKeys = true)
        {
            for (int i = 0; i < _numberOfReplicas; i++)
            {
                uint hash = this._hashFunc.ToHash32(CombineId(node.ToString(), i));
                this._hashRing.Add(hash, node);
            }

            if (updateKeys)
            {
                this._sortedKeys = this._hashRing.Keys.ToArray();
            }
        }

        public void RemoveFromRing(T node)
        {
            for (int i = 0; i < _numberOfReplicas; i++)
            {
                uint hash = this._hashFunc.ToHash32(CombineId(node.ToString(), i));
                this._hashRing.Remove(hash);
            }

            this._sortedKeys = this._hashRing.Keys.ToArray();
        }

        public T GetNode(String key)
        {
            if (this._hashRing.Count == 0)
            {
                return null;
            }

            uint hash = this._hashFunc.ToHash32(key);

            // too slow ...
            // if (!this._hashRing.ContainsKey(hash))
            // {
            //     var tail = this._hashRing.Keys.FirstOrDefault(x => x >= hash);
            //     hash = tail == 0 ? this._hashRing.Keys.First() : tail;
            // }
            // switch to bisect
            uint first = BisectLeft(this._sortedKeys, hash);

            return this._hashRing[this._sortedKeys[first]];
        }

        public override string ToString()
        {
            int count = this._hashRing.Count() / this._numberOfReplicas;
            return $"Hash-Func: {this._hashFunc.ToString()}\tNodes: {count:N0}\t Replicas: {this._numberOfReplicas:N0}";
        }

        private String CombineId(string id, int index)
        {
            return $"{id}:{index}";
        }

        /// <summary>
        /// get the index of first item that >= val
        /// </summary>
        private uint BisectLeft(uint[] arr, uint val)
        {
            int begin = 0;
            int end = arr.Length - 1;

            if (arr[end] < val || arr[0] > val)
            {
                return 0;
            }

            int mid;
            while (end - begin > 1)
            {
                mid = (end + begin) / 2;
                if (arr[mid] >= val)
                {
                    end = mid;
                }
                else
                {
                    begin = mid;
                }
            }

            if (arr[begin] > val || arr[end] < val)
            {
                throw new Exception("should not happen");
            }

            return (uint)end;
        }

        #region Additional

        internal uint GetHashKey(String key)
        {
            return this._hashFunc.ToHash32(key);
        }

        internal void Print()
        {
            foreach (var (k, v) in _hashRing)
            {
                Console.WriteLine("{0}: {1}", k, v.ToString());
            }
        }

        #endregion

    }
}