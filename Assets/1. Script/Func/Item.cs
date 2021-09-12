using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치] 
 * item들의 상위 오브젝트
 * 
 * [기능]
 * item 습득 -> point 증가 + item 이미지 출력 + sound 재생
 * item 리스폰 -> 1분 후 리스폰
 * 스킬 사용(15초) -> point 감소 + item 이미지 삭제 + 이펙트 재생
 * 메세지 출력 -> 스킬 사용 메세지, 스킬 사용 불가 메세지
 */

public class Item : MonoBehaviour
{
    public GameObject[] pimage;
    //item 이미지

    GameObject effect;
    //출구 알림 이펙트

    int point;
    //point


    void Start()
    {
        point = 0;

        effect = GameObject.Find("Exit").transform.GetChild(1).gameObject;
        //Exit 오브젝트에 있는 이펙트 가져오기
    }
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        //spacebar 클릭 시 스킬발동
        {
            if(point >= 5)
            //point가 5이상 일 경우
            {
                LosePoint();
                //스킬 실행
            }
            else
            //point가 5미만이라 스킬 실행 불가
            {
                StartCoroutine(MessageShow());
                //실패 메세지 출력
            }
        }
    }


    /* 함수 */
    public void GetPoint()
    //item 획득에 따른 point 증가
    //Detect에서 사용
    {
        if(point < 6)
        //point가 6보다 작을 경우
        {
            pimage[point].SetActive(true);
            //이미지를 하나씩 출력

            point++;
        }
        else
        //point가 6이상일 경우
        {
            point++;
            //point만 증가
        }
    }
    public void LosePoint()
    //스킬 발동
    {
        int tmp = point - 5;
        //point를 5만큼 소진했을때 남은 point

        StartCoroutine(FindExit());
        //출구 알림 이펙트 실행
        StartCoroutine(ActiveMessage());
        //스킬 발동 메세지 출력

        if (tmp < 6)
        //남은 포인트가 6보다 작을 경우
        {
            while(point != tmp)
            //순차적으로 이미지 삭제
            {
                point--;

                pimage[point].SetActive(false);
            }
        }
        else
        //point만 차감
        {
            point -= 5;
        }
    }
    public void RespawnItem(GameObject item)
    //item 리스폰
    {
        StartCoroutine(Respawn(item));
        //item 오브젝트 끄기 + 리스폰
    }


    /* 코루틴 */
    IEnumerator Respawn(GameObject item)
    //item 끄기 + 1분 후 리스폰
    {
        item.SetActive(false);

        yield return new WaitForSecondsRealtime(60.0f);

        item.SetActive(true);
    }
    IEnumerator MessageShow()
    //스킬 사용불가 메세지 출력
    {
        pimage[6].SetActive(true);

        yield return new WaitForSeconds(3.0f);

        pimage[6].SetActive(false);
    }
    IEnumerator ActiveMessage()
    //스킬 실행 메세지 출력
    {
        pimage[7].SetActive(true);

        yield return new WaitForSeconds(3.0f);

        pimage[7].SetActive(false);
    }

    IEnumerator FindExit()
    //출구알림 이펙트 15초간 실행
    {
        effect.SetActive(true);

        yield return new WaitForSecondsRealtime(15.0f);

        effect.SetActive(false);
    }
}
