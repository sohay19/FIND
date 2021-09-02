using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * 각각의 spot 오브젝트
 * 
 * [기능]
 * 각 spot에 무언가 닿았는지 확인
 * 닿은 오브젝트를 도착 spot으로 이동 및 회전
 * 도착 spot 껐다 켜기
 */

public class Spot : MonoBehaviour
{
    bool turn;
    //위치변환 확인
    public bool touchCheck;
    //스팟에 닿았는지 확인
    public bool isobs;
    //장애물여부 확인

    public CharacterController charc;
    //중력변환될 오브젝트
    public CharacterController othercharc;
    //장애물 오브젝트
    Cam_Rotate cam;
    //시야 회전
    Spot_Point sp;
    Spot[] spot;
    //중력변환 지점

    Vector3 dir;
    //움직일 방향
    Vector3 des;
    Quaternion qdes;
    //도착 지점 및 회전각

    float[] num;
    //시간 및 속도 관련 변수


    private void Awake()
    {
        touchCheck = false;
    }
    void Start()
    {
        sp = transform.GetComponentInParent<Spot_Point>();
        cam = Camera.main.transform.GetComponent<Cam_Rotate>();

        spot = sp.sPoint;
        num = sp.times;

        turn = false;
        isobs = false;
    }
    private void FixedUpdate()
    {
        if (spot[0].touchCheck)
        //spot 0에 닿음
        {
            ChangeGravity(spot[1]);
            //중력변환 함수 실행

            if (turn)
            //변환 완료 시 실행
            {
                spot[0].touchCheck = false;
                //spot 0의 check 변경
                turn = false;
                //중력변환 끝
            }
        }
        else if (spot[1].touchCheck)
        //spot 1에 닿음
        {
            ChangeGravity(spot[0]);

            if (turn)
            {
                spot[1].touchCheck = false;

                turn = false;
            }
        }
    }


    /* 함수 */
    void ChangeGravity(Spot moveSpot)
    //중력 변환 함수
    {
        if (charc != null)
        //캐릭터 컨트롤러가 할당될 경우만 실행
        {
            if (!isobs)
            //도착 지점에 장애물이 없을 때
            {
                des = moveSpot.transform.position;
                qdes = moveSpot.transform.localRotation;
                //도착 위치 세팅

                StartCoroutine(activeSpot(moveSpot));
                //도착 spot을 껐다 켜는 코루틴 함수

                dir = des - charc.transform.position;
                //도착지점으로 가는 벡터

                charc.Move(dir * num[2] * Time.deltaTime);
                charc.transform.localRotation = Quaternion.Slerp(charc.transform.localRotation, qdes, num[1] * Time.deltaTime);
                //목적지 spot 이동


                if (charc.name == "Player")
                {
                    cam.SumZ = qdes.eulerAngles.z;
                }
                //플레이어일 경우에는 회전값 전달

                if (Vector3.Distance(charc.transform.position, des) <= 2.5f)
                //목적지에 거의 도착했을 경우
                {
                    charc.transform.localEulerAngles = qdes.eulerAngles;
                    //회전 강제 적용

                    turn = true;
                    //회전 성공
                    charc = null;
                    //연결 끊기
                }
            }
            else
            //도착 지점에 장애물이 있을 경우
            {
                othercharc.Move(Vector3.right * num[2]);
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
    IEnumerator activeSpot(Spot spot)
    //도착 spot 일정 시간 껐다 켜기
    {
        spot.gameObject.SetActive(false);

        yield return new WaitForSeconds(num[0]);

        spot.gameObject.SetActive(true);
    }
}
