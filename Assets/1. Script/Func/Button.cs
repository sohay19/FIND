using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * Button(하위 움직이는 Moving 오브젝트)
 * 
 * [기능]
 * 버튼이 눌리도록 작동
 * 눌렸는지 여부 확인
 * 전부 눌려 바닥에 닿았는지 확인
 * 전부 눌렸을 경우 이펙트 On
 */

public class Button : MonoBehaviour
{
    public GameObject effect;
    //눌렸을 시 작동 이펙트

    Vector3 sdir;
    //버튼의 원래 위치

    public bool ispush;
    //눌림여부
    bool isend;
    //전부 눌렸는지 확인여부


    void Start()
    {
        ispush = false;
        isend = false;
        
        sdir = transform.position;
        //눌리기전 위치 저장
    }
    void Update()
    {
        if (ispush && !isend)
        //눌림O & 전부 눌리지 않음
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
        else if(!ispush)
        //눌림X
        {
            if (transform.position != sdir)
            //버튼 원래 위치로 복귀
            {
                transform.position = Vector3.MoveTowards(transform.position, sdir, Time.deltaTime);
            }
        }
    }


    /* Collision */
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.name == "Plane")
        {
            isend = true;

            effect.SetActive(true);
            //이펙트 On
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.transform.name == "Plane")
        {
            isend = false;

            effect.SetActive(false);
            //이펙트 Off
        }
    }
}
