using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions {
    public static class IEnumerableExtensions {
        public static void ForAll<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (T item in source) {
                action(item);
            }
        }
    }
}