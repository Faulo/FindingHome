using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions {
    public static class AnimatorExtensions {
        public static IEnumerator PlayAndWait(this Animator animator, string state) {
            animator.Play(state);
            do {
                yield return new WaitForFixedUpdate();
            } while (animator.GetCurrentAnimatorStateInfo(0).IsName(state));
        }
    }
}