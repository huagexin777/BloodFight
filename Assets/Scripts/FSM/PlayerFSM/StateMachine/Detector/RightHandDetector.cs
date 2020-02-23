using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandDetector : MonoBehaviour
{
    #region 属性


    private CharacterControl baseCharacter;
    public CharacterControl BaseCharacter { get { return transform.root.GetComponent<CharacterControl>(); } }

    private SphereCollider sphereCollider;
    public SphereCollider SphereCollider { get { return GetComponent<SphereCollider>(); } }



    #endregion


    /// <summary>
    /// 开启检测
    /// </summary>
    public void OpenDetector()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, SphereCollider.radius);

        //跳过,自身trigger
        for (int i = 0; i < BaseCharacter.ragdollCollider.Count; i++)
        {
            for (int j = 0; j < colliders.Length; j++)
            {
                if (BaseCharacter.ragdollCollider[i] == colliders[i])
                {
                    continue;
                }
            }
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag(Tag.wall) && colliders[i] == SphereCollider)
            {
                continue;
            }
            //AttackInfoManager.Instance.Add(colliders[i]);
            
        }

    }



}
