using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * [위치]
 * GameManager 오브젝트
 * 
 * [기능]
 * 설명 출력
 * 씬 전환
 */

public class GameManager_Tutorial : MonoBehaviour
{
    public GameObject ment;
    //설명 가장 상위 오브젝트

    Transform[] ments;
    //하위 설명 이미지

    string sname;
    int num;
    //현재 씬 정보
    int index = 1;
    //설명 페이지


    void Start()
    {
        sname = SceneManager.GetActiveScene().name;
        num = int.Parse(sname[sname.Length - 1].ToString());
        //현재 씬 정보

        ments = new Transform[ment.transform.childCount];
        ments = ment.transform.GetComponentsInChildren<Transform>(true);
        //설명 이미지
    }
    void Update()
    {
        if (GameManager_InGame.isoption)
        //옵션메뉴 실행 -> 설명 끄기
        {
            ment.SetActive(false);
        }
        else
        {
            ment.SetActive(true);
        }

        if (Input.GetButtonDown("Submit") && Time.timeScale != 0)
        //엔터 클릭 시 씬 전환
        {
            ++num;

            if (num == 5)
            //Tutorial이 4까지만 있기에 Stage로 넘김
            {
                GameManager_Load.StageLoadingScene("Stage_1");
            }
            else
            //다음 Tutorial로 전환
            {
                GameManager_Load.OtherLoadingScene("Tutorial_" + num);

            }
        }

        if (Time.timeScale != 0 && index < ments.Length)
        //일시정지가 아닐 경우 설명 출력
        {
            ShowMent();
        }
    }


    /* 함수 */
    void ShowMent()
    //설명 출력
    {
        if(!ments[index].gameObject.activeInHierarchy)
        //현재 설명이 active == false일 때 만
        {
            ments[index].gameObject.SetActive(true);

            StartCoroutine(Delay());
        }
    }


    /* 코루틴 */
    IEnumerator Delay()
    //3초간 설명 출력 후 끄고 index 증가
    {
        yield return new WaitForSeconds(3.0f);

        ments[index].gameObject.SetActive(false);

        index++;
    }
}
