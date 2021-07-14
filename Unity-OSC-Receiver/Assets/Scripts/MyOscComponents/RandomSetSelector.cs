using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace MyOscComponents
{
    public class RandomSetSelector<T>
    {
        private T[] _items;
        private HashSet<T> _set;

        public int TotalCount => _items.Length;

        public void SetItems(IEnumerable<T> items)
        {
            _items = items.ToArray();
        }

        public IEnumerable<T> RandomSubset(int count)
        {
            if (count > _items.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count),
                    $"Only {_items.Length} items given, but {count} random subset expected!");
            }

            var list = _items.ToList();
            for (var i = 0; i < count; i++)
            {
                var randIdx = Random.Range(0, list.Count);
                var randItem = list[randIdx];

                list.RemoveAt(randIdx);
                yield return randItem;
            }
        }
    }
}