using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "热血格斗/Idle", fileName = "Idle State")]
public class IdleState : IAbstractStateInfo
{
    private OnGroundDetecter detector;
    private bool isJump =false;


    public override void OnEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnEnter(animator, stateInfo, layerIndex);
        detector = Character.GetComponentInChildren<OnGroundDetecter>();
        isJump = false;
    }

    public override void OnUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnUpdate(animator, stateInfo, layerIndex);
      

        //三连击(起手)
        Attack3Combo(animator);
        //基础运动
        BasicMotionControl(animator);
 
    }
    public override void OnExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnExit(animator, stateInfo, layerIndex);

        animator.SetBool(AnimParameterType.IsRun.ToString(), false);
    }

    /// <summary>
    /// (基础)运动控制
    /// </summary>
    void BasicMotionControl(Animator animator) 
    {
        //跳跃
        if (VirtualInputManager.Instance.IsJump && detector.IsOnGround() && !isJump)
        {
            animator.SetBool(AnimParameterType.IsJump.ToString(), true);
            isJump = true;
            return;
        }
        //匍匐
        if (VirtualInputManager.Instance.IsCrouch && detector.IsOnGround())
        {
            animator.SetBool(AnimParameterType.IsCrouch.ToString(), true);
        }

        //左右同时按下.角色不移动.
        if (VirtualInputManager.Instance.IsRight && VirtualInputManager.Instance.IsLeft)
        {
            animator.SetBool(AnimParameterType.IsRun.ToString(), false);
            return;
        }
        if (!VirtualInputManager.Instance.IsLeft && !VirtualInputManager.Instance.IsRight)
        {
            animator.SetBool(AnimParameterType.IsRun.ToString(), false);
            return;
        }

        //右移
        if (VirtualInputManager.Instance.IsRight && !VirtualInputManager.Instance.IsLeft)
        {
            animator.SetBool(AnimParameterType.IsRun.ToString(), true);
        }
        //左移
        else if (VirtualInputManager.Instance.IsLeft && !VirtualInputManager.Instance.IsRight)
        {
            animator.SetBool(AnimParameterType.IsRun.ToString(), true);
        }
    }

    /// <summary>
    /// 三连击(起手)
    /// </summary>
    void Attack3Combo(Animator animator) 
    {
        if (VirtualInputManager.Instance.IsAttack)
        {
            animator.SetInteger(AnimParameterType.NormalAtkCombo.ToString(), 1);
        }
    }

  

}
