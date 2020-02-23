using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfoManager: Singleton<AttackInfoManager>
{
    /// <summary>
    /// 当前攻击信息
    /// </summary>
    public List<AttackInfo> currentAttckInfo = new List<AttackInfo>();


    public void Add(AttackInfo attack) 
    {
        currentAttckInfo.Add(attack);
    }

    public void Remove(AttackInfo attack)
    {
        currentAttckInfo.Remove(attack);
    }

    public void Clear() 
    {
        currentAttckInfo.Clear();
    }

}
