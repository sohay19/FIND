using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * [위치]
 * GameManager 오브젝트
 * 
 * [기능]
 * Stage 시작
 * Tutorial 시작
 * 도움말 메뉴
 * 프로그램 종료
 */

public class GameManager_Main : MonoBehaviour
{
    public GameObject main;
    public GameObject help;

    GameObject[] page;
    //설명페이지

    int curpage;
    //현재 페이지


    void Start()
    {
        Cursor.visible = true;
        //커서 유무
        Cursor.lockState = CursorLockMode.Confined;
        //커서 위치

        page = new GameObject[help.transform.childCount];
        for(int i = 0; i < help.transform.childCount; i++)
        {
            page[i] = help.transform.GetChild(i).gameObject;
        }
        //설명 페이지
    }
    void Update()
    {

    }


    /* 함수 */
    /* 옵션 메뉴 */
    public void OnClickStoryButton()
    //Stage 실행
    {
        Audio.DestoryBGM();

        GameManager_Load.StageLoadingScene("Stage_1");
    }
    public void OnClickTutorialButton()
    //Tutorial 실행
    {
        Audio.DestoryBGM();

        GameManager_Load.OtherLoadingScene("Tutorial_1");
    }
    public void OnClickHelpButton()
    //도움말 메뉴 실행
    {
        curpage = 1;
        //1페이지로 초기화

        page[curpage].gameObject.SetActive(true);

        help.SetActive(true);
    }
    public void OnClickExitButton()
    //종료
    {
        Application.Quit();
        //프로그램 종료
    }

    /* 도움말 메뉴 */
    public void OnClickQuit()
    //메뉴 끄기
    {
        help.SetActive(false);
    }
    public void OnClickPre()
    //이전 페이지
    {
        if (curpage == 1)
            return;
        else
        {
            page[curpage].gameObject.SetActive(false);

            curpage--;

            page[curpage].gameObject.SetActive(true);
        }
    }
    public void OnClickNext()
    //다음 페이지
    {
        if (curpage == 5)
            return;
        else
        {
            page[curpage].gameObject.SetActive(false);

            curpage++;

            page[curpage].gameObject.SetActive(true);
        }
    }
}