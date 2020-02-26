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

        //BoxCollider GroundDetector = GetComponentInChildren<BoxCollider>();

        ////- 生成到局部坐标轴 float参数
        //float bottom = GroundDetector.bounds.center.y - GroundDetector.bounds.extents.y;
        //float top = GroundDetector.bounds.center.y + GroundDetector.bounds.extents.y;
        //float front = GroundDetector.bounds.center.z + GroundDetector.bounds.extents.z;
        //float back = GroundDetector.bounds.center.z - GroundDetector.bounds.extents.z;
        //Debug.Log("bottom:" + bottom + "   top:" + top + "   front:" + front + "   back:" + back);



        ////生成到底部
        //Vector3 bottom2front = new Vector3(0, bottom, front); Debug.LogError("bottom2front:" + bottom2front); //bottom2front = transform.TransformPoint(bottom2front);
        //Vector3 bottom2back = new Vector3(0, bottom, back); Debug.LogError("bottom2back:" + bottom2back); //bottom2back = transform.TransformPoint(bottom2back);

        //GameObject DetectorSphere = new GameObject("sphere");
        //DetectorSphere.transform.position = Vector3.zero;
        //Transform b2f = Instantiate(DetectorSphere, bottom2front, Quaternion.identity).transform;
        //Transform b2b = Instantiate(DetectorSphere, bottom2back, Quaternion.identity).transform;

        //b2f.SetParent(transform, false);
        //b2b.SetParent(transform, false);
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
