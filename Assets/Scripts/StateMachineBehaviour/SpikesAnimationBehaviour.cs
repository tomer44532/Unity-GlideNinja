using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesAnimationBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player player = animator.gameObject.GetComponent<ColoredSpikes>().Player;
        if (stateInfo.IsName("spikes 2") || stateInfo.IsName("spikes 2 0"))
        {
            animator.gameObject.GetComponent<ColoredSpikes>().CurrentCollider = 1;
        }
        else if (stateInfo.IsName("spikes 3"))
        {
            animator.gameObject.GetComponent<ColoredSpikes>().CurrentCollider = 2;

        }
        else
        {
            animator.gameObject.GetComponent<ColoredSpikes>().CurrentCollider = 0;
        }
        Debug.Log("animator " + animator.gameObject.GetComponent<ColoredSpikes>().CurrentCollider);

        animator.gameObject.GetComponent<ColoredSpikes>().changeColliders();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
