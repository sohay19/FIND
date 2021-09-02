using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * 각각의 teleport 오브젝트
 * 
 * [기능]
 * 각 teleport에 무언가 닿았는지 확인
 * 닿은 오브젝트를 도착 teleport로 이동 및 회전
 * 도착 teleport 껐다 켜기
 */

public class Teleport : MonoBehaviour
{
    bool isend;
    //변환완료 확인
    public bool touchCheck;
    //텔레포트에 닿았는지 확인
    public bool isobs;
    //장애물여부 확인

    public CharacterController charc;
    //위치변환될 오브젝트
    public CharacterController othercharc;
    //장애물 오브젝트
    Cam_Rotate cam;
    //시야 회전
    Teleport_Point tp;
    Teleport[] tel;
    //위치변환 지점

    Vector3 dir;
    //움직일 방향
    Vector3 des;
    Quaternion qdes;
    //도착 지점 및 회전각

    float restime;
    //리스폰시간


    private void Awake()
    {
        touchCheck = false;
    }
    void Start()
    {
        tp = transform.GetComponentInParent<Teleport_Point>();
        cam = Camera.main.transform.GetComponent<Cam_Rotate>();

        tel = tp.tPoint;
        restime = tp.respawnTime;

        isend = false;
        isobs = false;
    }
    void FixedUpdate()
    {
        if (tel[0].touchCheck)
        //teleport 0에 닿음
        {
            ChangePosition(tel[1]);
            //위치변환 함수 실행

            if(isend)
            //변환 완료 시 실행
            {
                tel[0].touchCheck = false;
                //teleport 0의 check 변경
                isend = false;
                //위치변환 끝
            }
        }
        else if(tel[1].touchCheck)
        //teleport 1에 닿음
        {
            ChangePosition(tel[0]);

            if (isend)
            {
                tel[1].touchCheck = false;

                isend = false;
            }
        }
    }


    /* 함수 */
    void ChangePosition(Teleport movePoint)
    //위치 변환 함수
    {
        if (charc != null)
        //캐릭터 컨트롤러가 할당될 경우만 실행
        {
            if (!isobs)
            {
                des = movePoint.transform.position;
                qdes = movePoint.transform.localRotation;
                //도착 위치 세팅

                StartCoroutine(activePoint(movePoint));
                //도착 point를 껐다 켜는 코루틴 함수

                charc.transform.position = des;
                charc.transform.localRotation = qdes;
                //목적지 point로 이동


                if (charc.name == "Player")
                {
                    cam.SumZ = qdes.eulerAngles.z;
                }
                //플레이어일 경우에는 회전값 전달

                if (Vector3.Distance(charc.transform.position, des) <= 0.5f)
                //목적지에 거의 도착했을 경우
                {
                    isend = true;
                    //이동 성공
                    charc = null;
                    //연결 끊기
                }
            }
            else
            //도착 지점에 장애물이 있을 경우
            {
                othercharc.Move(Vector3.right);
                //장애물 치우기

                if (Vector3.Distance(dir, othercharc.transform.position) > 6.0f)
                //목적지와 충분히 멀어졌을 경우
                {
                    othercharc = null;
                    //연결 끊기
                    isobs = false;
                    //충돌 피하기 성공
                }
            }
        }
    }


    /* 코루틴 */
    IEnumerator activePoint(Teleport point)
    //도착 point 일정 시간 껐다 켜기
    {
        point.gameObject.SetActive(false);

        yield return new WaitForSeconds(restime);

        point.gameObject.SetActive(true);
    }
}
