using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInput : MonoBehaviour
{
    public KeyCode leftDir = KeyCode.A;
    public KeyCode rightDir = KeyCode.D;
    public KeyCode attack = KeyCode.J;
    public KeyCode jump = KeyCode.K;
    public KeyCode crouch = KeyCode.L;
    void Start()
    {
        
    }

    void Update()
    {
        //左移
        if (Input.GetKey(leftDir))
        {
            VirtualInputManager.Instance.IsLeft = true;
        }
        else if (Input.GetKeyUp(leftDir))
        {
            VirtualInputManager.Instance.IsLeft = false;
        }
        //右移
        if (Input.GetKey(rightDir))
        {
            VirtualInputManager.Instance.IsRight = true;
        }
        else if (Input.GetKeyUp(rightDir))
        {
            VirtualInputManager.Instance.IsRight = false;
        }
        //攻击
        if (Input.GetKeyDown(attack))
        {
            StartCoroutine(NorAtkKey());
        }
        IEnumerator NorAtkKey() 
        {
            VirtualInputManager.Instance.IsAttack = true;
            yield return new WaitForSeconds(0.1f);
            VirtualInputManager.Instance.IsAttack = false;
        }

        //跳跃
        if (Input.GetKeyDown(jump))
        {
            StartCoroutine(JumpKey());
        }
        IEnumerator JumpKey()
        {
            VirtualInputManager.Instance.IsJump = true;
            yield return new WaitForSeconds(0.1f);
            VirtualInputManager.Instance.IsJump = false;
        }

        //匍匐
        if (Input.GetKey(crouch))
        {
            VirtualInputManager.Instance.IsCrouch = true;
        }
        else if (Input.GetKeyUp(crouch))
        {
            VirtualInputManager.Instance.IsCrouch = false;
        }
    }
}
