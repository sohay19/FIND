using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * 움직이는 물체 오브젝트
 * 
 * [기능]
 * 물체 중력적용
 * catch 시 마우스 포지션에 따른 이동
 */

public class Props_Move : MonoBehaviour
{
    Detect detect;
    CharacterController props_CC;
    Cam_Mouse getcamera;

    Vector3 dir;
    //목적지
    CollisionFlags flag;

    float gravity;


    /* 프로퍼티 */
    public CharacterController Props_CC
    {
        get
        {
            return props_CC;
        }
    }
    public float Gravity
    {
        get
        {
            return gravity;
        }
        set
        {
            gravity = value;
        }
    }
    public Vector3 Dir
    {
        get
        {
            return dir;
        }
        set
        {
            dir = value;
        }
    }
    public CollisionFlags Flag
    {
        get
        {
            return flag;
        }
    }
    //Cam_Mouse와 Detect에서 사용


    void Start()
    {
        props_CC = transform.GetComponent<CharacterController>();
        getcamera = Camera.main.GetComponent<Cam_Mouse>();
        detect = transform.GetComponent<Detect>();

        gravity = getcamera.Gravity;
    }
    void FixedUpdate()
    {
        if (!detect.istrans)
        //변환 중이 아닐 때만
        {
            if (!detect.iscatched)
            //catch가 아님
            { 
                ObjectMove();
                //중력만 적용
            }
            else
            //catch임
            {
                CatchMove();
                //마우스 포지션에 따라 이동
            }
        }
    }


    /* 함수 */
    void ObjectMove()
    //중력 적용
    {
        dir = transform.TransformDirection(Vector3.down);
        //로컬좌표계를 월드 좌표계로 변환

        flag = props_CC.Move(dir * gravity * Time.deltaTime);
    }
    void CatchMove()
    //마우스 포지션에 따라 이동
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(dir) - props_CC.transform.position;
        
        flag = props_CC.Move(pos);
    }
}
