using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundDetecter : MonoBehaviour
{
    #region 属性

    private GameObject detectorSphere;
    public GameObject DetectorSphere
    {
        get
        {
            detectorSphere = Resources.Load<GameObject>("Detector/Sphere");
            return detectorSphere;
        }
    }


    private BoxCollider groundDetector;
    public BoxCollider GroundDetector
    {
        get
        {
            if (groundDetector == null)
            {
                BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].name == "GroundDetector")
                    {
                        groundDetector = colliders[i];
                    }
                }
            }
            return groundDetector;
        }
    }

    public bool isOnGround;
    public bool isOnFront;

    #endregion

    //检测球
    public List<GameObject> detectorSpheres_Bottom = new List<GameObject>();
    public List<GameObject> detectorSpheres_Front = new List<GameObject>();
    public float fron2offset = 0.2f;


    void Awake()
    {
        SpawnEdgeSphere();
    }

    void Update()
    {
        isOnGround = IsOnGround();
        isOnFront = IsOnFront();

    }

    /// <summary>
    /// 生成边缘球-用来检测用的.
    /// </summary>
    public void SpawnEdgeSphere()
    {
        //* Collider.bounds.center 是基于世界坐标轴 *

        //- 所以,生成的参数.都是世界坐标参数
        float bottom = GroundDetector.bounds.center.y - GroundDetector.bounds.extents.y;
        float top = GroundDetector.bounds.center.y + GroundDetector.bounds.extents.y;
        float front = GroundDetector.bounds.center.z + GroundDetector.bounds.extents.z;
        float back = GroundDetector.bounds.center.z - GroundDetector.bounds.extents.z;

        Debug.Log("bottom:" + bottom + "   top:" + top + "   front:" + front + "   back:" + back);


        //生成到底部
        Vector3 bottom2front = new Vector3(transform.position.x, bottom, front);
        Vector3 bottom2back = new Vector3(transform.position.x, bottom, back);
        //已sec全长,去生成6个检测球
        Vector3 norDir = (bottom2front - bottom2back).normalized;   //方向
        float sec = (bottom2front - bottom2back).magnitude;         //大小
        Transform bottomContainer = new GameObject("bottomContainer").transform;
        bottomContainer.SetParent(GroundDetector.transform,true); //保持,原有世界坐标轴.
        for (int i = 0; i <= 5; i++)
        {
            Vector3 tempDir = (sec / 5) * i * norDir;
            Vector3 pos = bottom2back + tempDir;
            GameObject sphere = Instantiate(DetectorSphere, pos, Quaternion.identity);
            sphere.transform.SetParent(bottomContainer, true);   //保持,原有世界坐标轴.
            detectorSpheres_Bottom.Add(sphere);
        }

        //生成到前面 
        Vector3 front2up = new Vector3(transform.position.x, top, front);
        Vector3 front2bottom = new Vector3(transform.position.x, bottom, front);
        //已sec全长,去生成6个检测球
        norDir = (front2up - front2bottom).normalized; //(方向:从下往上)
        sec = (front2up - front2bottom).magnitude;
        Transform frontContainer = new GameObject("frontContainer").transform;
        frontContainer.SetParent(GroundDetector.transform, true);
        for (int i = 0; i <= 5; i++)
        {
            Vector3 tempDir = (sec / 5) * i * norDir;
            Vector3 pos = front2bottom + tempDir;
            GameObject sphere = Instantiate(DetectorSphere, pos, Quaternion.identity);
            sphere.transform.SetParent(frontContainer, true);
            detectorSpheres_Front.Add(sphere);
        }

    }

    /// <summary>
    /// 检测地面
    /// </summary>
    public bool IsOnGround()
    {
        for (int i = 0; i < detectorSpheres_Bottom.Count; i++)
        {
            //由于.每个检测小球.raduis=0.025f
            bool isRayCast = Physics.Raycast(detectorSpheres_Bottom[i].transform.position, Vector3.down * 0.025f, 0.05f);
            if (isRayCast)
            {
                return true;
            }
            Debug.DrawRay(detectorSpheres_Bottom[i].transform.position, -transform.up * 0.1f, Color.red);
        }
        return false;
    }

    /// <summary>
    /// 检测前方
    /// </summary>
    public bool IsOnFront()
    {
        for (int i = 0; i < detectorSpheres_Front.Count; i++)
        {
            //由于.每个检测小球.raduis=0.025f
            bool isRayCast = Physics.Raycast(detectorSpheres_Front[i].transform.position, transform.forward * 0.7f, 1f);
            if (isRayCast)
            {
                return true;
            }
            Debug.DrawRay(detectorSpheres_Front[i].transform.position, transform.forward * 0.7f, Color.yellow);
        }
        return false;
    }

    /// <summary>
    /// 前方是否有敌人
    /// </summary>
    public bool IsOnFrontEnemy()
    {
        for (int i = 0; i < detectorSpheres_Front.Count; i++)
        {
            bool isRayCast = Physics.Raycast(detectorSpheres_Front[i].transform.position, transform.forward * 0.7f, 1f);
            if (isRayCast)
            {
                return true;
            }
        }
        return false;
    }


}
