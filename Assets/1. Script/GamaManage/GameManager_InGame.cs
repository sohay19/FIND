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
 * �� �̸� ���
 * �ɼ� �޴�
 * ���� �޴�
 * Tip �޴�
 */

public class GameManager_InGame : MonoBehaviour
{
    public static bool isoption;
    //�ɼ�Ȱ��ȭ ����

    public GameObject menu_main;
    public GameObject menu_tip;
    public GameObject menu_sound;
    public GameObject name_scene;
    //�� �޴��� ���̸�

    GameObject[] page;
    //����������
    Slider sound;
    //������� slider

    int curpage;
    //���� ������
    string sname;
    char first;
    //���� �� Ȯ��


    void Start()
    {
        Cursor.visible = false;
        //Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked;
        //Ŀ�� ��ġ

        sname = SceneManager.GetActiveScene().name;
        first = sname[0];
        //���� �� ����

        sound = menu_sound.transform.GetChild(2).GetComponent<Slider>();
        sound.value = Audio.music.volume;
        //������� ����

        page = new GameObject[menu_tip.transform.childCount];
        for (int i = 0; i < menu_tip.transform.childCount; i++)
        {
            page[i] = menu_tip.transform.GetChild(i).gameObject;
        }
        //���� ������

        isoption = false;
        //bool �ʱ�ȭ

        StartCoroutine(ShowName());
        //�� �̸� ���
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        //ESC ��ư Ŭ��
        {
            if (!isoption)
            //�ɼ� ����X -> �ɼ� �ѱ�
            {
                isoption = true;

                menu_main.SetActive(true);
                //�ɼ�_���θ޴� �ѱ�

                Cursor.visible = true;
                //Ŀ�� ����
                Cursor.lockState = CursorLockMode.Confined;
                //Ŀ�� ��ġ

                Time.timeScale = 0;
                //���� �Ͻ�����
            }
            else
            //�ɼ� ����O -> ���� �޴� �ݱ�
            {
                if (menu_sound.activeSelf)
                //�ɼ�_����޴� ����
                {
                    menu_sound.SetActive(false);
                    menu_main.SetActive(true);
                    //����޴� ���� ���θ޴� �ѱ�
                }
                else
                //�ɼ� ����
                {
                    Cursor.visible = false;
                    //Ŀ�� ����
                    Cursor.lockState = CursorLockMode.Locked;
                    //Ŀ�� ��ġ

                    menu_main.SetActive(false);

                    Time.timeScale = 1;
                    //���� ����

                    isoption = false;
                    //�ɼ� bool ����
                }
            }

        }
    }


    /* �ڷ�ƾ */
    IEnumerator ShowName()
    //�� ���� ���
    {
        Time.timeScale = 0;
        //��������

        name_scene.SetActive(true);

        yield return new WaitForSecondsRealtime(2.0f);

        name_scene.SetActive(false);

        Time.timeScale = 1;
        //���ӽ���

    }


    /* �Լ� */
    /* �ɼ� �޴� */
    public void OnClickTipButton()
    //tip �޴� �ѱ�
    {
        curpage = 1;
        //1�������� �ʱ�ȭ

        menu_main.SetActive(false);
        menu_tip.SetActive(true);
    }
    public void OnClickReStartButton()
    //���� �� �ٽý���
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
        //tutorial�� stage�� ù ������ audio�� ����ǹǷ� BGM������Ʈ �ı�


        Cursor.visible = false;
        //Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked;
        //Ŀ�� ��ġ

        Time.timeScale = 1;

        isoption = false;
    }
    public void OnClickMainButton()
    //�������� ���ư���
    {
        Audio.DestoryBGM();

        GameManager_Load.OtherLoadingScene("Main");

        Time.timeScale = 1;

        isoption = false;
    }
    public void OnClickEndButton()
    //����
    {
        isoption = false;

        Application.Quit();
        //���α׷� ����
    }
    public void OnClickSound()
    //���� �޴� �ѱ�
    {
        menu_main.SetActive(false);
        menu_sound.SetActive(true);
    }
    public void OnClickQuit()
    //�ɼǸ޴� ����
    {
        menu_main.SetActive(false);

        Cursor.visible = false;
        //Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked;
        //Ŀ�� ��ġ

        Time.timeScale = 1;

        isoption = false;
    }

    /* ���� �޴� */
    public void OnClickReMenu()
    //���� �޴��� ���ư���
    {
        menu_sound.SetActive(false);
        menu_main.SetActive(true);
    }
    public void OnClickBGMVolumeDown()
    //���� ���̱�
    {
        sound = menu_sound.transform.GetChild(2).GetComponent<Slider>();

        Audio.BGMVolumeDown(sound);
    }

    public void OnClickBGMVolumeUp()
    //���� �ѱ�
    {
        sound = menu_sound.transform.GetChild(2).GetComponent<Slider>();

        Audio.BGMVolumeUp(sound);
    }

    /* Tip �޴� */
    public void OnClickReMenu2()
    //���� �޴��� ���ư���
    {
        menu_tip.SetActive(false);
        menu_main.SetActive(true);
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
