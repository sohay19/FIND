using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * Main Camera 오브젝트
 * 
 * [기능]
 * 시야를 지정된 위치로
 */

public class Cam_Follow : MonoBehaviour
{
    public Transform target;
    //플레이어의 시야


    void Start()
    {

    }
    void Update()
    {
        transform.position = target.position;
        //메인카메라를 플레이어 시야 위치로
    }
}
