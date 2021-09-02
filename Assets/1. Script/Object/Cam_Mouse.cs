using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * Main Camera 오브젝트
 * 
 * [기능]
 * 크로스 헤어 변환
 * 마우스를 이용한 물체잡기
 */

public class Cam_Mouse : MonoBehaviour
{
    bool iscatch;
    //물체 catch여부 확인

    public GameObject[] crosshair;
    //크로스헤어들
    public float props_gravity;
    //물체들의 중력
    public float movespeed;
    //물체 움직이는 속도
    public float catchdistance;
    //잡을 수 있는 거리

    Ray ray;
    RaycastHit hit;
    // Ray관련 변수
    Vector3 mousePosition;
    //마우스 위치
    CharacterController catch_CC;
    //닿은 물체의 캐릭터 콜라이더
    Props_Move catchProps;
    //닿은 물체의 스크립트
    Detect detect;
    //닿은 물체의 Detect 컴포넌트

    float distance;
    //물체와 플레이어의 거리


    /* 프로퍼티 */
    public float Gravity
    {
        get
        {
            return props_gravity;
        }
    }
    //물체Move에서 사용


    void Start()
    {
        iscatch = false;
    }
    void Update()
    {
        mousePosition = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mousePosition);
        //마우스 포지션을 대입

        Physics.Raycast(ray, out hit, catchdistance, 1 << LayerMask.NameToLayer("Move"));
        //레이를 이용하여 hit에 저장


        if (Time.timeScale != 0)
        //일시정지가 아닐 때만
        {
            if (!iscatch)
            //물체잡기X
            {
                if (hit.transform != null)
                //닿은 물체가 있을 때
                {
                    detect = hit.transform.GetComponent<Detect>();
                    //닿은 물체의 Detect 가져오기

                    if (!detect.istrans)
                    //물체가 변환 중이 아닐 때만
                    {
                        crosshair[0].SetActive(false);
                        //일반 크로스헤어 끄기
                        crosshair[1].SetActive(true);
                        //Not Hold 크로스헤어 켜기

                        if (Input.GetMouseButtonDown(0))
                        //마우스 클릭 시
                        {
                            detect.iscatched = true;
                            iscatch = true;

                            distance = hit.distance;
                            //Z거리

                            catchProps = hit.transform.GetComponent<Props_Move>();
                            //닿은 오브젝트의 Props_Move 저장

                            catch_CC = catchProps.Props_CC;
                            catchProps.Gravity = 0.0f;
                            catchProps.Dir = new Vector3(mousePosition.x, mousePosition.y, distance);
                            //중력을 0으로 만들고, 물체가 이동해야하는 위치 = 마우스 포지션
                            //캐릭터 컨트롤러이므로 Move를 통해 이동해야함
                        }
                    }
                }
                else
                // 닿은 물체가 없을 때
                {
                    crosshair[1].SetActive(false);
                    //Not Hold 크로스헤어 끄기
                    crosshair[2].SetActive(false);
                    //Hold 알림 크로스헤어 끄기
                    crosshair[0].SetActive(true);
                    //일반 크로스헤어 켜기
                }
            }
            else
            //물체잡기O
            {
                if (Input.GetMouseButton(0))
                {
                    crosshair[1].SetActive(false);
                    //Not Hold 크로스헤어 끄기
                    crosshair[2].SetActive(true);
                    //Hold 알림 크로스헤어 켜기
                }
                else if (Input.GetMouseButtonUp(0))
                //마우스를 뗄 때
                {
                    crosshair[2].SetActive(false);
                    //Hold 알림 크로스헤어 끄기
                    crosshair[0].SetActive(true);
                    //일반 크로스헤어 켜기

                    catchProps.Gravity = props_gravity;
                    //원래 물체 중력으로 변경

                    iscatch = false;
                    detect.iscatched = false;
                    //catch 관련 bool 변경
                }
            }
        }
        else
        //옵션이 켜졌을때
        {
            crosshair[1].SetActive(false);
            //Not Hold 크로스헤어 끄기
            crosshair[2].SetActive(false);
            //Hold 알림 크로스헤어 끄기
            crosshair[0].SetActive(false);
            //일반 크로스헤어 끄기
        }
    }
}
