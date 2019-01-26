using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions {
    public static class GameObjectExtensions {
        public static bool HasComponentEnabled<T>(this GameObject gameObject) where T : MonoBehaviour {
            return gameObject.GetComponents<T>()
                .Where(component => component.enabled)
                .Any();
        }
    }
}