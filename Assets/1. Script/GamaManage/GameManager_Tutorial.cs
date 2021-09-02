using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * [��ġ]
 * GameManager ������Ʈ
 * 
 * [���]
 * ���� ���
 * �� ��ȯ
 */

public class GameManager_Tutorial : MonoBehaviour
{
    public GameObject ment;
    //���� ���� ���� ������Ʈ

    Transform[] ments;
    //���� ���� �̹���

    string sname;
    int num;
    //���� �� ����
    int index = 1;
    //���� ������


    void Start()
    {
        sname = SceneManager.GetActiveScene().name;
        num = int.Parse(sname[sname.Length - 1].ToString());
        //���� �� ����

        ments = new Transform[ment.transform.childCount];
        ments = ment.transform.GetComponentsInChildren<Transform>(true);
        //���� �̹���
    }
    void Update()
    {
        if (GameManager_InGame.isoption)
        //�ɼǸ޴� ���� -> ���� ����
        {
            ment.SetActive(false);
        }
        else
        {
            ment.SetActive(true);
        }

        if (Input.GetButtonDown("Submit") && Time.timeScale != 0)
        //���� Ŭ�� �� �� ��ȯ
        {
            ++num;

            if (num == 5)
            //Tutorial�� 4������ �ֱ⿡ Stage�� �ѱ�
            {
                GameManager_Load.StageLoadingScene("Stage_1");
            }
            else
            //���� Tutorial�� ��ȯ
            {
                GameManager_Load.OtherLoadingScene("Tutorial_" + num);

            }
        }

        if (Time.timeScale != 0 && index < ments.Length)
        //�Ͻ������� �ƴ� ��� ���� ���
        {
            ShowMent();
        }
    }


    /* �Լ� */
    void ShowMent()
    //���� ���
    {
        if(!ments[index].gameObject.activeInHierarchy)
        //���� ������ active == false�� �� ��
        {
            ments[index].gameObject.SetActive(true);

            StartCoroutine(Delay());
        }
    }


    /* �ڷ�ƾ */
    IEnumerator Delay()
    //3�ʰ� ���� ��� �� ���� index ����
    {
        yield return new WaitForSeconds(3.0f);

        ments[index].gameObject.SetActive(false);

        index++;
    }
}
