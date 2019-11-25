using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions {
    public static class RandomExtensions {
        private static readonly Random Random = new Random();

        public static T RandomElement<T>(this T[] enumerable) {
            if (enumerable == null || enumerable.Length == 0) {
                return default;
            } else {
                return enumerable.ElementAtOrDefault(Random.Next(enumerable.Length));
            }
        }

        public static T RandomElement<T>(this IEnumerable<T> enumerable) {
            if (enumerable == null || enumerable.Count() == 0) {
                return default;
            } else {
                return enumerable.Skip(Random.Next(enumerable.Count())).FirstOrDefault();
            }
        }
    }
}