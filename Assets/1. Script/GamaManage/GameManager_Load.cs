using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * [��ġ]
 * Loading ���� GameManager
 * 
 * [���]
 * �񵿱� ���ε�
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
        //Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked;
        //Ŀ�� ��ġ

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
    //�ٸ� ������ ȣ���ϴ� �Լ�
    {
        scenename = name;
        isStage = true;

        SceneManager.LoadScene("Loading");
    }

    public static void OtherLoadingScene(string name)
    //�ٸ� ������ ȣ���ϴ� �Լ�
    {
        scenename = name;
        isStage = false;

        SceneManager.LoadScene("Loading");
    }

    IEnumerator OtherLoading()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scenename);
        //�񵿱� �������� ���� �ε�

        ao.allowSceneActivation = false;
        //�ε�Ǵ� ���� ����� ȭ�鿡 ������ �ʰ� �Ѵ�

        ment[0].gameObject.SetActive(false);
        ment[1].gameObject.SetActive(false);
        ment[2].gameObject.SetActive(false);
        ment[3].gameObject.SetActive(true);

        ment[3].wrapMode = WrapMode.Loop;


        while (!ao.isDone)
        //�ε��� �Ϸ�� ������ �ݺ��ؼ� ���� ��ҵ��� �ε��ϰ� ��������� ȭ�鿡 ǥ���Ѵ�
        {
            yield return null;
            //���� �������� �� ������ ��ٸ���

            curruentTime += Time.deltaTime;
            //�ð��� �帧 üũ

            
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
        //�񵿱� �������� ���� �ε�

        ao.allowSceneActivation = false;
        //�ε�Ǵ� ���� ����� ȭ�鿡 ������ �ʰ� �Ѵ�


        while (!ao.isDone)
        //�ε��� �Ϸ�� ������ �ݺ��ؼ� ���� ��ҵ��� �ε��ϰ� ��������� ȭ�鿡 ǥ���Ѵ�
        {
            yield return null;
            //���� �������� �� ������ ��ٸ���

            curruentTime += Time.deltaTime;
            //�ð��� �帧 üũ


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
