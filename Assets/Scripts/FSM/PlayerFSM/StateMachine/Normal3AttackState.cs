using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[CreateAssetMenu(menuName = "热血格斗/Normal3Attack", fileName = "Normal3Attack State")]
public class Normal3AttackState : IAbstractStateInfo
{
    public AnimationCurve atkCurve;//攻击曲线
    [Range(0,0.5f)] public float duration = 0.3f;
    [Header("普攻-三连检测")]
    public float atk1_startTimer = 0.4f;
    public float atk1_endTimer = 0.55f;
    public float atk2_startTimer = 0.25f;
    public float atk2_endTimer = 0.35f;
    public float atk3_startTimer = 0.2f;
    public float atk3_endTimer = 0.35f;
    [Header("---------------攻击动作Info---------------")]
    public bool mustCollider;//是否是碰撞检测
    public string[] allNames;//出手部位名称
    public string raiseName;
    [Header("---------------伤害Info---------------")]
    public DamageInfo[] damageInfos;

    //private
    private AttackInfo attackInfo;
    private bool isAttack;


    public override void OnEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnEnter(animator, stateInfo, layerIndex);

        //添加位移
        Character.transform.DOMove(Character.transform.position + Character.transform.forward, duration)
            .SetEase(atkCurve);

        //每次，有进入状态.就创建一次  new AttackInfo()
        attackInfo = new AttackInfo();
        attackInfo.ResetInfo();
        isAttack = false;
    }
    public override void OnUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnUpdate(animator, stateInfo, layerIndex);

        //三连击动画
        Attack3Combo(animator, stateInfo);

        //三连击-attackInfo
        Attack3Combob_atkInfo(animator, stateInfo);
        //三连击退出-attackInfo
        Attack3Combob_end(animator, stateInfo);
    }
    public override void OnExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnExit(animator, stateInfo, layerIndex);


        //重置3连击普攻
        ResetAttack3Combob(animator);
        //重置基础运动
        ResetBasicMotion(animator);

    }

    /// <summary>
    /// 三连击
    /// </summary>
    void Attack3Combo(Animator animator, AnimatorStateInfo stateInfo)
    {
        if (VirtualInputManager.Instance.IsAttack)
        {
            //切换动画
            if (stateInfo.IsName("123combo_1"))
            {
                animator.SetInteger(AnimParameterType.NormalAtkCombo.ToString(), 2);
            }
            else if (stateInfo.IsName("123combo_2"))
            { 
                animator.SetInteger(AnimParameterType.NormalAtkCombo.ToString(), 3);
            }
            else if (stateInfo.IsName("123combo_3"))
            {

            }
        }
        else 
        {
            animator.SetInteger(AnimParameterType.NormalAtkCombo.ToString(), 0);
        }
    }

    /// <summary>
    /// 三连击-attackInfo
    /// </summary>
    void Attack3Combob_atkInfo(Animator animator, AnimatorStateInfo stateInfo)
    {
        //注入-攻击信息
        if (stateInfo.IsName("123combo_1") && isAttack == false)
        {
            //处于检测范围
            if ((stateInfo.normalizedTime >= atk1_startTimer && stateInfo.normalizedTime <= atk1_endTimer) && Character.GetComponentInChildren<OnGroundDetecter>().isOnFront)
            {
                isAttack = true;
                raiseName = allNames[0];
                //注册attackInfo
                attackInfo.damageInfo = damageInfos[0];
                attackInfo.RegisterInfo(Character,this);
                AttackInfoManager.Instance.Add(attackInfo);
            }
        }
        else if (stateInfo.IsName("123combo_2") && isAttack == false)
        {
            //处于检测范围
            if ((stateInfo.normalizedTime >= atk2_startTimer && stateInfo.normalizedTime <= atk2_endTimer) && Character.GetComponentInChildren<OnGroundDetecter>().isOnFront)
            {
                isAttack = true;
                raiseName = allNames[1];
                //注册attackInfo
                attackInfo.damageInfo = damageInfos[1];
                attackInfo.RegisterInfo(Character,this);
                AttackInfoManager.Instance.Add(attackInfo);
            }
        }
        else if (stateInfo.IsName("123combo_3") && isAttack == false)
        {
            //处于检测范围
            if ((stateInfo.normalizedTime >= atk3_startTimer && stateInfo.normalizedTime <= atk3_endTimer)&& Character.GetComponentInChildren<OnGroundDetecter>().isOnFront)
            {
                isAttack = true;
                raiseName = allNames[2];
                //注册attackInfo
                attackInfo.damageInfo = damageInfos[2];
                attackInfo.RegisterInfo(Character,this);
                AttackInfoManager.Instance.Add(attackInfo);
            }
        }
    }

    /// <summary>
    /// 三连击-attackInfo-结束
    /// </summary>
    void Attack3Combob_end(Animator animator, AnimatorStateInfo stateInfo)
    {
        //移除
        if (stateInfo.IsName("123combo_1"))
        {
            if (stateInfo.normalizedTime > atk1_endTimer)
            {
                AttackInfoManager.Instance.Remove(attackInfo);
            }
        }
        else if (stateInfo.IsName("123combo_2"))
        {

            if (stateInfo.normalizedTime > atk2_endTimer)
            {
                AttackInfoManager.Instance.Remove(attackInfo);
            }
        }
        else if (stateInfo.IsName("123combo_3"))
        {
            if (stateInfo.normalizedTime > atk3_endTimer)
            {
                AttackInfoManager.Instance.Remove(attackInfo);
            }
        }
    }


    /// <summary>
    /// 重置3连击普攻
    /// </summary>
    void ResetAttack3Combob(Animator animator)
    {
        animator.SetInteger(AnimParameterType.NormalAtkCombo.ToString(), 0);
        VirtualInputManager.Instance.AtkCombob = 0;
    }

    /// <summary>
    /// 重置基础运动
    /// </summary>
    void ResetBasicMotion(Animator animator)
    {
        animator.SetBool(AnimParameterType.IsJump.ToString(), false);
        animator.SetBool(AnimParameterType.IsRun.ToString(), false);
    }



}
