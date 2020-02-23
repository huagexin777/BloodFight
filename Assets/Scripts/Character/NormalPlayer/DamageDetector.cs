using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    #region 属性

    private CharacterControl characterControl;
    public CharacterControl CharacterControl { get { return transform.root.GetComponent<CharacterControl>(); } }

    private List<AttackInfo> AttackInfos { get { return AttackInfoManager.Instance.currentAttckInfo; } }

    private DamageCalculate DamageCalculate { get { return GetComponentInChildren<DamageCalculate>(); } }

    #endregion


    void Start()
    {
        
    }

    void Update()
    {
        Damage2Die();
    }


    /// <summary>
    /// 处理伤害
    /// </summary>
    void Damage2Die() 
    {
        for (int i = 0; i < AttackInfos.Count; i++)
        {
            if (AttackInfos[i].attacker == this.CharacterControl)
            {
                continue;
            }
            DamageCalculate.DamageCal(AttackInfos[i].damageInfo.def_Damage);
        }    
    }

}
