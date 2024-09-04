using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo
{
    internal class HashTableE<TKey, TValue>
    {
        private List<KeyValuePair<TKey, TValue>>[] buckets;
        private int capacity;

        public HashTableE(int capacity = 100)
        {
            this.capacity = capacity;
            buckets = new List<KeyValuePair<TKey, TValue>>[capacity];
        }

        private int GetBucketIndex(TKey key)
        { 
            int hashCode = key.GetHashCode();
            return Math.Abs(hashCode) % capacity;
        }

        public void Add(TKey key, TValue value)
        {
            int bucketIndex = GetBucketIndex(key);
            if (buckets[bucketIndex] == null)
            {
                buckets[bucketIndex] = [];
            }

            var bucket = buckets[bucketIndex];
            if (bucket.Any(kvp => kvp.Key.Equals(key)))
            {
                throw new ArgumentException("An element with the same key already exists.");
            }

            bucket.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int bucketIndex = GetBucketIndex(key);
            if (buckets[bucketIndex] != null)
            {
                var pair = buckets[bucketIndex].FirstOrDefault(kvp => kvp.Key.Equals(key));
                if (!pair.Equals(null)) 
                {
                    value = pair.Value;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public bool Remove(TKey key)
        {
            int bucketIndex = GetBucketIndex(key);
            if (buckets[bucketIndex] != null)
            {
                int index = buckets[bucketIndex].FindIndex(kvp => kvp.Key.Equals(key));
                if (index == -1)
                {
                    buckets[bucketIndex].RemoveAt(index);
                }
            }

            return false;
        }

       
    }
}
