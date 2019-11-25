using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions {
    public static class Vector3Extensions {
        public static float ManhattanDistance(this Vector3 a, Vector3 b) {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
        }
    }
}