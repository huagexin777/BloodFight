using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画参数类型
/// </summary>
public enum AnimParameterType 
{ 
    IsRun,
    IsJump,
    IsJumpBack,//后空翻
    IsCanJump,
    IsCrouch,
    IsFall,
    NormalAtkCombo,
    ForceTransition,//强制动画跳转
}

public class CharacterControl : MonoBehaviour
{
    [HideInInspector] public Rigidbody Rig;
    [HideInInspector] public CapsuleCollider CC;
    [HideInInspector] public Animator Anim;

    #region 测试

    public bool isEnemy = false;

    [Header("测试")]
    public List<int> cecececececececece;

    #endregion


    #region Anim状态机-参数

    public List<Collider> ragdollCollider = new List<Collider>();//布娃娃collider

    //攻击

    #endregion

    #region 检测

    [HideInInspector] public RightHandDetector rightHandDetector;//右手检测

    #endregion


    private void Awake()
    {
        Rig = GetComponentInChildren<Rigidbody>();
        CC = GetComponentInChildren<CapsuleCollider>();
        Anim = GetComponentInChildren<Animator>();

        rightHandDetector = GetComponentInChildren<RightHandDetector>(true);

        SetRagdoll();
    }

    void Start()
    {
      
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TurnOnRagdoll();
        }

        if (isEnemy)
        {
            Anim.enabled = false;
        }
    }

    #region 快速切换材质

    [Header("快速切换-材质")]
    public Material targetMaterail;
    /// <summary>
    /// 改变材质
    /// </summary>
    public void ChangeMaterails()
    {
        if (targetMaterail == null) { Debug.LogError("要切换的材质不存在!请先拖动到Inspector面板."); return; }
        SkinnedMeshRenderer[] render = GetComponentsInChildren<SkinnedMeshRenderer>(true);
        for (int i = 0; i < render.Length; i++)
        {
            render[i].material = targetMaterail;
        }
    }

    #endregion

    #region 布娃娃系统

    /// <summary>
    /// 开启布娃娃
    /// </summary>
    public void TurnOnRagdoll() 
    {
        Anim.enabled = false;
        CC.enabled = false;

        for (int i = 0; i < ragdollCollider.Count; i++)
        {
            ragdollCollider[i].isTrigger = false;
        }
    }

    /// <summary>
    /// 设置布娃娃
    /// </summary>
    public void SetRagdoll()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>(true);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != this.gameObject && colliders[i].transform.name != "GroundDetector" && !colliders[i].CompareTag(Tag.edgeDetectorSphere))
            {
                colliders[i].isTrigger = true;
                ragdollCollider.Add(colliders[i]);
            }
        }
    }

    #endregion

}
