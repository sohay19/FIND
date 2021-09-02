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
 * 씬 이름 출력
 * 옵션 메뉴
 * 사운드 메뉴
 * Tip 메뉴
 */

public class GameManager_InGame : MonoBehaviour
{
    public static bool isoption;
    //옵션활성화 여부

    public GameObject menu_main;
    public GameObject menu_tip;
    public GameObject menu_sound;
    public GameObject name_scene;
    //각 메뉴와 씬이름

    GameObject[] page;
    //설명페이지
    Slider sound;
    //배경음악 slider

    int curpage;
    //현재 페이지
    string sname;
    char first;
    //현재 씬 확인


    void Start()
    {
        Cursor.visible = false;
        //커서 유무
        Cursor.lockState = CursorLockMode.Locked;
        //커서 위치

        sname = SceneManager.GetActiveScene().name;
        first = sname[0];
        //현재 씬 정보

        sound = menu_sound.transform.GetChild(2).GetComponent<Slider>();
        sound.value = Audio.music.volume;
        //배경음악 정보

        page = new GameObject[menu_tip.transform.childCount];
        for (int i = 0; i < menu_tip.transform.childCount; i++)
        {
            page[i] = menu_tip.transform.GetChild(i).gameObject;
        }
        //설명 페이지

        isoption = false;
        //bool 초기화

        StartCoroutine(ShowName());
        //씬 이름 출력
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        //ESC 버튼 클릭
        {
            if (!isoption)
            //옵션 실행X -> 옵션 켜기
            {
                isoption = true;

                menu_main.SetActive(true);
                //옵션_메인메뉴 켜기

                Cursor.visible = true;
                //커서 유무
                Cursor.lockState = CursorLockMode.Confined;
                //커서 위치

                Time.timeScale = 0;
                //게임 일시정지
            }
            else
            //옵션 실행O -> 기존 메뉴 닫기
            {
                if (menu_sound.activeSelf)
                //옵션_사운드메뉴 켜짐
                {
                    menu_sound.SetActive(false);
                    menu_main.SetActive(true);
                    //사운드메뉴 끄고 메인메뉴 켜기
                }
                else
                //옵션 끄기
                {
                    Cursor.visible = false;
                    //커서 유무
                    Cursor.lockState = CursorLockMode.Locked;
                    //커서 위치

                    menu_main.SetActive(false);

                    Time.timeScale = 1;
                    //게임 실행

                    isoption = false;
                    //옵션 bool 변경
                }
            }

        }
    }


    /* 코루틴 */
    IEnumerator ShowName()
    //씬 네임 출력
    {
        Time.timeScale = 0;
        //게임정지

        name_scene.SetActive(true);

        yield return new WaitForSecondsRealtime(2.0f);

        name_scene.SetActive(false);

        Time.timeScale = 1;
        //게임실행

    }


    /* 함수 */
    /* 옵션 메뉴 */
    public void OnClickTipButton()
    //tip 메뉴 켜기
    {
        curpage = 1;
        //1페이지로 초기화

        menu_main.SetActive(false);
        menu_tip.SetActive(true);
    }
    public void OnClickReStartButton()
    //현재 씬 다시시작
    {
        if (first == 'S')
        {
            if (sname == "Stage_1")
            {
                Audio.DestoryBGM();
            }

            GameManager_Load.StageLoadingScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            if (sname == "Tutorial_1")
            {
                Audio.DestoryBGM();
            }

            GameManager_Load.OtherLoadingScene(SceneManager.GetActiveScene().name);
        }
        //tutorial과 stage의 첫 씬들은 audio가 실행되므로 BGM오브젝트 파괴


        Cursor.visible = false;
        //커서 유무
        Cursor.lockState = CursorLockMode.Locked;
        //커서 위치

        Time.timeScale = 1;

        isoption = false;
    }
    public void OnClickMainButton()
    //메인으로 돌아가기
    {
        Audio.DestoryBGM();

        GameManager_Load.OtherLoadingScene("Main");

        Time.timeScale = 1;

        isoption = false;
    }
    public void OnClickEndButton()
    //종료
    {
        isoption = false;

        Application.Quit();
        //프로그램 종료
    }
    public void OnClickSound()
    //사운드 메뉴 켜기
    {
        menu_main.SetActive(false);
        menu_sound.SetActive(true);
    }
    public void OnClickQuit()
    //옵션메뉴 끄기
    {
        menu_main.SetActive(false);

        Cursor.visible = false;
        //커서 유무
        Cursor.lockState = CursorLockMode.Locked;
        //커서 위치

        Time.timeScale = 1;

        isoption = false;
    }

    /* 사운드 메뉴 */
    public void OnClickReMenu()
    //이전 메뉴로 돌아가기
    {
        menu_sound.SetActive(false);
        menu_main.SetActive(true);
    }
    public void OnClickBGMVolumeDown()
    //볼륨 줄이기
    {
        sound = menu_sound.transform.GetChild(2).GetComponent<Slider>();

        Audio.BGMVolumeDown(sound);
    }

    public void OnClickBGMVolumeUp()
    //볼륨 켜기
    {
        sound = menu_sound.transform.GetChild(2).GetComponent<Slider>();

        Audio.BGMVolumeUp(sound);
    }

    /* Tip 메뉴 */
    public void OnClickReMenu2()
    //이전 메뉴로 돌아가기
    {
        menu_tip.SetActive(false);
        menu_main.SetActive(true);
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
