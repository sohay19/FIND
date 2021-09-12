using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * Main Camera 오브젝트
 * 
 * [기능]
 * 카메라 움직임에 따라 시야 회전
 * 회전 제한
 */

public class Cam_Rotate : MonoBehaviour
{
    public float rotateSpeed;
    //회전속도

    Vector3 dir;
    //목적방향

    float sumX, sumY;
    float sumZ;
    //누적 회전값


    /* 프로퍼티 */
    public float RotateSpeed
    {
        set
        {
            rotateSpeed = value;
        }
        get
        {
            return rotateSpeed;
        }
    }
    //Cam, Player Rotate에서 사용
    public float SumZ
    {
        set
        {
            sumZ = value;
        }
    }
    //Spot, Teleport 사용


    void Start()
    {
        sumZ = 0;
    }
    void Update()
    {
        if (!GameManager_InGame.isoption)
        //옵션실행X 때 만
        {
            float Y = Input.GetAxis("Mouse X");
            float X = Input.GetAxis("Mouse Y");
            //마우스 입력을 받아 값을 저장

            sumX += X * RotateSpeed * Time.deltaTime;
            sumY += Y * RotateSpeed * Time.deltaTime;
            //회전값을 누적하여 저장

            sumX = Mathf.Clamp(sumX, -80f, 80f);
            //상하 회전값 제한


            if (sumZ < 178.0f)
            //중력변환 전
            {
                dir = new Vector3(-sumX, sumY, sumZ);
            }
            else
            //중력변환 후
            {
                dir = new Vector3(sumX, -sumY, sumZ);
            }

            transform.localEulerAngles = dir;
            //값에 따라 회전
        }
    }
}
