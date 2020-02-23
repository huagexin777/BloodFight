using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "热血格斗/GroundDetecter", fileName = "GroundDetecter")]
public class GroundDetecter : IAbstractStateInfo
{
    private OnGroundDetecter detector;

    public override void OnEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnEnter(animator, stateInfo, layerIndex);
        detector = Character.GetComponentInChildren<OnGroundDetecter>(true);
    }
    public override void OnUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnUpdate(animator, stateInfo, layerIndex);

        bool isOnGround = detector.IsOnGround();
        if (isOnGround)
        {
            animator.SetBool(AnimParameterType.IsFall.ToString(), false);
            animator.SetBool(AnimParameterType.IsCanJump.ToString(), true);
        }
        else
        {
            animator.SetBool(AnimParameterType.IsCanJump.ToString(), false);
            animator.SetBool(AnimParameterType.IsFall.ToString(), true);
        }
    }
    public override void OnExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnExit(animator, stateInfo, layerIndex);
    }
}
