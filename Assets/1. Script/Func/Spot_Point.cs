using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * spot이 모인 최상위 오브젝트
 * 
 * [기능]
 * 1set의 spot들을 관리
 * 리스폰시간, 회전속도, 이동속도를 관리
 */

public class Spot_Point : MonoBehaviour
{
    public Spot[] sPoint;
    //spot 개별 영역

    public float respawnTime;
    //반대편 spot이 리스폰 되는 시간
    public float turnSpeed;
    //회전 속도
    public float spotTransSpeed;
    //다른 spot으로 이동하는 속도


    /* 프로퍼티 */
    public float[] times
    {
        get
        {
            float[] num = { respawnTime, turnSpeed, spotTransSpeed };

            return num;
        }
    }
    //Spot에서 사용


    void Start()
    {
        
    }
    void Update()
    {

    }
}
