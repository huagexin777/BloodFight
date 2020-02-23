using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "热血格斗/Jump2Landing", fileName = "Jump2Landing State")]
public class Jump2LandingState : IAbstractStateInfo
{
    private OnGroundDetecter detector;
    public Vector3 offsetCharacter = new Vector3(0, 1f, 0);
    public float heightCharacter = 2f;

    public override void OnEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnEnter(animator, stateInfo, layerIndex);
        detector = Character.GetComponentInChildren<OnGroundDetecter>(true);
        //detector.GroundDetector.transform.localPosition = Vector3.zero;
        detector.transform.localScale = Vector3.one;

        Character.CC.center = offsetCharacter;
        Character.CC.height = heightCharacter;
    }

    public override void OnUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnUpdate(animator, stateInfo, layerIndex);

        //匍匐
        if (VirtualInputManager.Instance.IsCrouch && detector.IsOnGround())
        {
            animator.SetBool(AnimParameterType.IsCrouch.ToString(), true);
        }
    }
    public override void OnExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnExit(animator, stateInfo, layerIndex);
    }
}
