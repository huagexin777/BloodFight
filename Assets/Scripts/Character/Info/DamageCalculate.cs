using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculate : MonoBehaviour
{
    public RuntimeAnimatorController dieAnim;

    #region 属性

    private PlayerProperty PlayerProperty { get { return GetComponentInChildren<PlayerProperty>(); } }

    private CharacterControl CharacterControl { get { return transform.root.GetComponent<CharacterControl>(); } }

    #endregion

    /// <summary>
    /// 伤害计算
    /// </summary>
    public void DamageCal(int damage) 
    {
        int resoult = damage - PlayerProperty.Phy_defense;
        PlayerProperty.Hp -= resoult;

        if (PlayerProperty.Hp <= 0)
        {
            //触发死亡动画
            CharacterControl.Anim.runtimeAnimatorController = dieAnim;
        }
    }

}
