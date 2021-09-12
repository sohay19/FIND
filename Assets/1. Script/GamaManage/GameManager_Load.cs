using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * [위치]
 * Loading 씬의 GameManager
 * 
 * [기능]
 * 비동기 씬로딩
 * 
 */

public class GameManager_Load : MonoBehaviour
{
    public Animation[] ment;
    //
    public Slider loadingbar;

    public static string scenename;
    public static bool isStage;

    float curruentTime = 0f;


    void Start()
    {
        Cursor.visible = false;
        //커서 유무
        Cursor.lockState = CursorLockMode.Locked;
        //커서 위치

        if(isStage)
        {
            StartCoroutine(StageLoading());

        }
        else
        {
            StartCoroutine(OtherLoading());
        }

    }

    void Update()
    {

    }

    public static void StageLoadingScene(string name)
    //다른 씬에서 호출하는 함수
    {
        scenename = name;
        isStage = true;

        SceneManager.LoadScene("Loading");
    }

    public static void OtherLoadingScene(string name)
    //다른 씬에서 호출하는 함수
    {
        scenename = name;
        isStage = false;

        SceneManager.LoadScene("Loading");
    }

    IEnumerator OtherLoading()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scenename);
        //비동기 형식으로 씬을 로드

        ao.allowSceneActivation = false;
        //로드되는 씬의 모습이 화면에 보이지 않게 한다

        ment[0].gameObject.SetActive(false);
        ment[1].gameObject.SetActive(false);
        ment[2].gameObject.SetActive(false);
        ment[3].gameObject.SetActive(true);

        ment[3].wrapMode = WrapMode.Loop;


        while (!ao.isDone)
        //로딩이 완료될 때까지 반복해서 씬의 요소들을 로드하고 진행과정을 화면에 표시한다
        {
            yield return null;
            //다음 프레임이 될 때까지 기다린다

            curruentTime += Time.deltaTime;
            //시간의 흐름 체크

            
            if (loadingbar.value >= 1.0f)
            {
                ao.allowSceneActivation = true;
            }

            loadingbar.value = Mathf.SmoothStep(loadingbar.value, 1, curruentTime);
        }
    }

    IEnumerator StageLoading()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scenename);
        //비동기 형식으로 씬을 로드

        ao.allowSceneActivation = false;
        //로드되는 씬의 모습이 화면에 보이지 않게 한다


        while (!ao.isDone)
        //로딩이 완료될 때까지 반복해서 씬의 요소들을 로드하고 진행과정을 화면에 표시한다
        {
            yield return null;
            //다음 프레임이 될 때까지 기다린다

            curruentTime += Time.deltaTime;
            //시간의 흐름 체크


            if (loadingbar.value >= 0.99f)
            {
                loadingbar.value = 1.0f;

                 ao.allowSceneActivation = true;
            }
            else if (loadingbar.value >= 0.9f)
            {
                ment[2].gameObject.SetActive(false);
                ment[3].gameObject.SetActive(true);
            }
            else
            {
                if (loadingbar.value <= 0.2f)
                {
                    ment[0].gameObject.SetActive(true);
                }
                else if (loadingbar.value <= 0.5f)
                {
                    ment[0].gameObject.SetActive(false);
                    ment[1].gameObject.SetActive(true);
                }
                else
                {
                    ment[1].gameObject.SetActive(false);
                    ment[2].gameObject.SetActive(true);
                }
            }

            loadingbar.value = Mathf.SmoothStep(loadingbar.value, 1, curruentTime / 5000);
        }
    }
}
