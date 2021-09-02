using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * [위치]
 * UI캔버스의 Rocation 오브젝트
 * 
 * [기능]
 * 출구위치를 알려주는 좌표 Slider 조정
 * 출구와의 거리 표기
 */

public class Rocate : MonoBehaviour
{
    public Transform exit;
    //출구 위치

    GameObject player;
    //플레이어 오브젝트
    Slider slider;
    //좌표 Slider
    Text text;
    //거리 표기

    Vector3 dis;
    //y를 플레이어와 맞춘 출구위치
    Vector3 dir;
    //플레이어->출구(y값 제외)
    Vector3 look;
    //{플레이어가 바라보는방향}=>{플레이어->출구}


    void Start()
    {
        player = GameObject.Find("Player");
        slider = transform.GetComponent<Slider>();
        text = transform.GetComponentInChildren<Text>();
    }
    void Update()
    {
        dis = new Vector3(exit.position.x, player.transform.position.y, exit.position.z - 1.0f);
        //y를 플레이어와 맞춘 출구위치

        dir = new Vector3(dis.x, 0, dis.z) - new Vector3(player.transform.position.x, 0, player.transform.position.z);
        dir = transform.TransformDirection(dir.normalized);
        //월드좌표로 변환

        Vector3 pdir = transform.TransformDirection(player.transform.forward).normalized;
        //플레이어의 Z방향을 월드좌표로 변환

        look = dir - pdir;
        look = player.transform.InverseTransformDirection(look);
        //플레이어 로컬좌표로 변환


        if (look.x < 0)
        //출구가 플레이어의 왼쪽에 위치
        {
            if (look.z < -1.0f)
            //출구가 뒤쪽에 위치할때
            {
                slider.value = 0.0f;
                //Slider를 왼쪽 끝에 유지
            }
            else
            {
                slider.value = 0.5f - (Mathf.Abs(look.x) / 2);
                //x값이 -로 나오기 때문에 절대값
            }
        }
        else
        //출구가 플레이어의 오른쪽에 위치
        {
            if (look.z < -1.0f)
            //출구가 뒤쪽에 위치할때
            {
                slider.value = 1.0f;
                //Slider를 오른쪽 끝에 유지
            }
            else
            {
                slider.value = 0.5f + (look.x / 2);
            }
        }

        text.text = ((int)Vector3.Distance(player.transform.position, dis)).ToString() + " M";
        //출구와 플레이어 사이의 거리표기
    }
}
