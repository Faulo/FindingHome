using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionEnabler : StateMachineBehaviour
{
    [SerializeField] GameObject body;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        body.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
