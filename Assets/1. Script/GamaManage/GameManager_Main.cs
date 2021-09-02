using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * [��ġ]
 * GameManager ������Ʈ
 * 
 * [���]
 * Stage ����
 * Tutorial ����
 * ���� �޴�
 * ���α׷� ����
 */

public class GameManager_Main : MonoBehaviour
{
    public GameObject main;
    public GameObject help;

    GameObject[] page;
    //����������

    int curpage;
    //���� ������


    void Start()
    {
        Cursor.visible = true;
        //Ŀ�� ����
        Cursor.lockState = CursorLockMode.Confined;
        //Ŀ�� ��ġ

        page = new GameObject[help.transform.childCount];
        for(int i = 0; i < help.transform.childCount; i++)
        {
            page[i] = help.transform.GetChild(i).gameObject;
        }
        //���� ������
    }
    void Update()
    {

    }


    /* �Լ� */
    /* �ɼ� �޴� */
    public void OnClickStoryButton()
    //Stage ����
    {
        Audio.DestoryBGM();

        GameManager_Load.StageLoadingScene("Stage_1");
    }
    public void OnClickTutorialButton()
    //Tutorial ����
    {
        Audio.DestoryBGM();

        GameManager_Load.OtherLoadingScene("Tutorial_1");
    }
    public void OnClickHelpButton()
    //���� �޴� ����
    {
        curpage = 1;
        //1�������� �ʱ�ȭ

        page[curpage].gameObject.SetActive(true);

        help.SetActive(true);
    }
    public void OnClickExitButton()
    //����
    {
        Application.Quit();
        //���α׷� ����
    }

    /* ���� �޴� */
    public void OnClickQuit()
    //�޴� ����
    {
        help.SetActive(false);
    }
    public void OnClickPre()
    //���� ������
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
    //���� ������
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