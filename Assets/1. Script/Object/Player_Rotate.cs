using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * player 오브젝트
 * 
 * [기능]
 * 마우스 입력에 따른 플레이어 회전
 */

public class Player_Rotate : MonoBehaviour
{
    Cam_Rotate cam;
    //main 카메라

    Vector3 dir;
    //목적지


    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Cam_Rotate>();
    }

    void Update()
    {
        Vector3 camrot = cam.transform.localEulerAngles;
        //카메라 회전각

        dir = new Vector3(0, camrot.y, camrot.z);
        //좌우회전을 위한 y와 중력변환을 위한 z만 반영

        transform.localEulerAngles = dir;
    }
}
