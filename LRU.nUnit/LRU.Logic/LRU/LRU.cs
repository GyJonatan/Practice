using System;
using System.Collections.Generic;

namespace Lru.Logic
{
    public class LRU
    {
        public const int DEFAULT_LIMIT = 10;
        public List<object> Recent { get; private set; }
        public int ListLimit { get; private set; }
        public LRU(int limit = DEFAULT_LIMIT)
        {
            if (limit <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(limit), "Must be positive");
            }

            this.Recent = new List<object>();
            this.ListLimit = limit;
        }

        public void Add(object subject) //SUT = Subject Under Test
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject), "Can't add null");
            }

            if (Recent.Contains(subject))
            {
                Recent.Remove(subject);
            }

            Recent.Insert(0, subject);

            if (Recent.Count > ListLimit)
            {
                Recent.RemoveAt(Recent.Count - 1);
            }
        }
    }
}
