using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "热血格斗/Jumping", fileName = "Jumping State")]
public class JumpingState : IAbstractStateInfo
{
    private OnGroundDetecter detector;

    public float airMoveSpeed = 4;
    public Vector3 offsetCharacter = new Vector3(0, 0.65f, 0);
    public float heightCharacter = 1.3f;


    [Range(100, 2000),Header("力:")] public float forceVolume = 700;
    [Header("作用力类型:")]public ForceMode forceMode = ForceMode.Impulse;

    public override void OnEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnEnter(animator, stateInfo, layerIndex);
        animator.SetBool(AnimParameterType.IsRun.ToString(), false);
        //由于,受到物理系统的影响.所以,得把 velocity = Vector3.zero
        Character.Rig.velocity = Vector3.zero;

        //牛二公式 G=mg、F=ma
        //实际人物70kg
        Character.Rig.AddForce(Vector3.up * forceVolume, forceMode);

        //设置,检测器 size
        detector = Character.GetComponentInChildren<OnGroundDetecter>(true);

        //设置,碰撞器 position、size
        Character.CC.center = offsetCharacter;
        Character.CC.height = heightCharacter;
    }

    public override void OnUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnUpdate(animator, stateInfo, layerIndex);

        //右移
        if (VirtualInputManager.Instance.IsRight)
        {
            Character.transform.Translate(Vector3.forward * airMoveSpeed * Time.deltaTime, Space.Self);
            Character.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        //左移
        if (VirtualInputManager.Instance.IsLeft)
        {
            Character.transform.Translate(Vector3.forward * airMoveSpeed * Time.deltaTime, Space.Self);
            Character.transform.localEulerAngles = new Vector3(0, -90, 0);
        }
    }
    public override void OnExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnExit(animator, stateInfo, layerIndex);
        
    }
}
