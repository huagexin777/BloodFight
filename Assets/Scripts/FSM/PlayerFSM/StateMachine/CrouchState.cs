using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "热血格斗/Crouch", fileName = "Crouch State")]
public class CrouchState : IAbstractStateInfo
{
    private OnGroundDetecter detector;
    public override void OnEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnEnter(animator, stateInfo, layerIndex);
        detector = Character.GetComponentInChildren<OnGroundDetecter>();

        animator.SetBool(AnimParameterType.IsRun.ToString(), false);
        animator.SetBool(AnimParameterType.IsJump.ToString(), false);
    }
    public override void OnUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnUpdate(animator, stateInfo, layerIndex);

        //匍匐
        if (!VirtualInputManager.Instance.IsCrouch && detector.IsOnGround())
        {
            animator.SetBool(AnimParameterType.IsCrouch.ToString(), false);
        }
    }
    public override void OnExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnExit(animator, stateInfo, layerIndex);
    }
}
