using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "热血格斗/JumpStart", fileName = "JumpStart State")]
public class JumpStartState : IAbstractStateInfo
{
    //public float airMoveSpeed = 4;


    public override void OnEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(AnimParameterType.IsJump.ToString(), false);
        Character.Rig.velocity = Vector3.zero;
    }

    public override void OnUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnUpdate(animator, stateInfo, layerIndex);


        //主角可以后空翻
        if (VirtualInputManager.Instance.IsLeft && Character.transform.localEulerAngles.y == 90)
        {
            animator.SetBool(AnimParameterType.IsJumpBack.ToString(),true);
        }
        else if (VirtualInputManager.Instance.IsRight && Character.transform.localEulerAngles.y == 270)
        {
            animator.SetBool(AnimParameterType.IsJumpBack.ToString(), true);
        }
        else
        {
            animator.SetBool(AnimParameterType.IsJumpBack.ToString(), false);
        }
    }
    public override void OnExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnExit(animator, stateInfo, layerIndex);

        animator.SetBool(AnimParameterType.IsJumpBack.ToString(), false);
    }
}
