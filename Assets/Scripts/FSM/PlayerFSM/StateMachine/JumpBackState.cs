using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "热血格斗/JumpBack", fileName = "JumpBack State")]
public class JumpBackState : IAbstractStateInfo
{
    [Range(100, 2000), Header("力:")] public float forceVolume = 500;
    public float moveSpeed = 3;
    private float timer = 0;
    private bool isJumpBack = true;

    public override void OnEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnEnter(animator, stateInfo, layerIndex);

        timer = 0;
        isJumpBack = true;
    }

    public override void OnUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnUpdate(animator, stateInfo, layerIndex);

        timer += Time.deltaTime;
        if (timer >= 0.3f && isJumpBack)
        {
            isJumpBack = false;
            //牛二公式 G=mg、F=ma
            //实际人物70kg
            Character.Rig.AddForce(Vector3.up * forceVolume, ForceMode.Acceleration);
        }

        //左移
        if (VirtualInputManager.Instance.IsLeft)
        {
            Character.transform.Translate(-Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }
        //左移
        if (VirtualInputManager.Instance.IsRight)
        {
            Character.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }
    }
    public override void OnExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnExit(animator, stateInfo, layerIndex);
    }
}
