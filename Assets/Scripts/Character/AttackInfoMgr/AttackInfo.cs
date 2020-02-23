using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackInfo
{
    //--------------攻击动作Info-------------------
    public CharacterControl attacker;//攻击者
    public Normal3AttackState attack3State;//三连击State
    public bool mustCollider;//是否是碰撞检测
    /// <summary>
    /// 出手部位名称
    /// </summary>
    public string raiseName;//出手部位名称
    public bool isRegisted;//是否注册
    public bool isFinished;//是否结束



    //--------------伤害Info-------------------
    public DamageInfo damageInfo;

    public void ResetInfo()
    {
        isRegisted = false;
        isFinished = false;
    }

    public void RegisterInfo(CharacterControl characterControl, Normal3AttackState attack)
    {
        //动作Info
        attacker = characterControl;
        raiseName = attack.raiseName;
        mustCollider = attack.mustCollider;
        attack3State = attack;
        isRegisted = true;
        isFinished = true;

    }

}

[System.Serializable]
public class DamageInfo 
{
    public int def_Damage;

}
