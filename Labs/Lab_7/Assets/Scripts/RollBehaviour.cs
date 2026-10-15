using UnityEngine;


// A StateMachineBehaviour runs on an Animator state,
// not on a GameObject.
// This one is attached to the Stand to Roll state.
public class RollBehaviour : StateMachineBehaviour
{
    // Called when the Animator starts playing this state
    public override void OnStateEnter(Animator animator,
        AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Let the roll animation itself move the character forward
        animator.applyRootMotion = true;
    }


    // Called when the Animator finishes this state
    public override void OnStateExit(Animator animator,
        AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Hand movement back to the PlayerMovement script
        animator.applyRootMotion = false;
    }
}
