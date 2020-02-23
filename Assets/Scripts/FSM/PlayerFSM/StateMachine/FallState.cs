using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "热血格斗/FallState", fileName = "Fall State")]
public class FallState : IAbstractStateInfo
{
    public float airMoveSpeed = 2;
    private OnGroundDetecter detector;

    public override void OnEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnEnter(animator, stateInfo, layerIndex);
        detector = Character.GetComponentInChildren<OnGroundDetecter>(true);
    }

    public override void OnUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnUpdate(animator, stateInfo, layerIndex);

        //匍匐
        if (VirtualInputManager.Instance.IsCrouch && detector.IsOnGround())
        {
            animator.SetBool(AnimParameterType.IsCrouch.ToString(), true);
        }

        //左移
        if (VirtualInputManager.Instance.IsLeft)
        {
            Character.transform.Translate(-Vector3.right * Time.deltaTime * airMoveSpeed, Space.World);
        }
        //左移
        if (VirtualInputManager.Instance.IsRight)
        {
            Character.transform.Translate(Vector3.right * Time.deltaTime * airMoveSpeed, Space.World);
        }
    }
    public override void OnExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnExit(animator, stateInfo, layerIndex);
    }
}
