using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * telpoint가 모인 최상위 오브젝트
 * 
 * [기능]
 * 1set의 teleport들을 관리
 * 리스폰시간을 관리
 */

public class Teleport_Point : MonoBehaviour
{
    public Teleport[] tPoint;
    //텔레포트 개별 영역

    public float respawnTime;
    //반대편 포인트가 리스폰 되는 시간


    void Start()
    {

    }
    void Update()
    {
        
    }
}