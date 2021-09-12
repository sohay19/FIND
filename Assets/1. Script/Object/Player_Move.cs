using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * player 오브젝트
 * 
 * [기능]
 * 키보드 입력에 따른 플레이어 이동
 * 중력 적용
 */

public class Player_Move : MonoBehaviour
{
    public static bool ismove;
    //움직이는 중인지 확인
    public float playerSpeed;
    public float gravity;
    //속력, 중력
    
    Detect detect;
    //플레이어 Detect
    CharacterController p_cc;
    //플레이어 컨트롤러

    Vector3 dir;
    //목적지
    CollisionFlags flag;
    
    float hor, ver;
    //키보드 입력
    float v_speed;
    //수직속력


    /* 프로퍼티 */
    public CollisionFlags Flag
    {
        get
        {
            return flag;
        }
    }
    //Detect에서 사용


    void Start()
    {
        p_cc = gameObject.GetComponent<CharacterController>();
        detect = gameObject.GetComponent<Detect>();
    }
    private void FixedUpdate()
    {
        if (!detect.istrans)
        //변환 중이 아닐 때만
        {
            Move();
        }
    }
    void Update()
    {
        /*
        if (Input.GetButtonDown("Jump"))
        {
            jump();
        }
        */
    }


    /* 함수 */
    void Move()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        //키보드 입력

        dir = new Vector3(hor, 0, ver);
        dir = dir.normalized;
        //앞뒤,양옆만
        
        v_speed += -gravity * Time.deltaTime;
        //중력적용

        dir.y = v_speed;
        //y축 적용
        dir = transform.TransformDirection(dir);
        //로컬좌표계를 월드 좌표계로 변환
        //CC는 월드좌표 기준으로 움직임

        flag = p_cc.Move(dir * playerSpeed * Time.deltaTime);
    }


    /* 삭제 */
    /*void jump()
    {
        if (!isjump)
        {
            isjump = true;

            v_speed = jump_gravity;
        }
        else if (isjump && p_cc.collisionFlags != CollisionFlags.None)
        {
            v_speed = 0;

            isjump = false;
        }
    }
    */
}
