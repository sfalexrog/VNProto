using System;
using System.Collections.Generic;
using Random = System.Random;

namespace Utils.Collections.Generic {

    public static class ListExtension {

        private static Random random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count; i > 0; i--)
            {
                int index = i - 1;
                int newIndex = random.Next(index + 1);
                T value = list[newIndex];
                list[newIndex] = list[index];
                list[index] = value;
            }
        }
        
        public static T Random<T>(this T[] items)
        {
            if (items == null)
            {
                return default(T);
            }
            if (items.Length == 0)
            {
                return default(T);
            }
            var index = UnityEngine.Random.Range(0, items.Length);
            return items[index];
        }

        public static T Random<T>(this IList<T> items)
        {
            if (items == null)
            {
                return default(T);
            }
            if (items.Count == 0)
            {
                return default(T);
            }
            var index = UnityEngine.Random.Range(0, items.Count);
            return items[index];
        }

        public static T RemoveRandom<T>(this IList<T> items)
        {
            if (items == null)
            {
                return default(T);
            }
            if (items.Count == 0)
            {
                return default(T);
            }
            var index = UnityEngine.Random.Range(0, items.Count);
            var result = items[index];
            items.RemoveAt(index);
            return result;
        }

        public static T Nearest<T>(this IEnumerable<T> list, Func<T, float> ditanceGetter)
        {
            var result = default(T);
            var nearestDistance = float.MaxValue;
            foreach (var item in list)
            {
                var distance = ditanceGetter(item);
                if (distance < nearestDistance)
                {
                    result = item;
                    nearestDistance = distance;
                }
            }
            return result;
        }
    }

}
