using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ] 
 * item���� ���� ������Ʈ
 * 
 * [���]
 * item ���� -> point ���� + item �̹��� ��� + sound ���
 * item ������ -> 1�� �� ������
 * ��ų ���(15��) -> point ���� + item �̹��� ���� + ����Ʈ ���
 * �޼��� ��� -> ��ų ��� �޼���, ��ų ��� �Ұ� �޼���
 */

public class Item : MonoBehaviour
{
    public GameObject[] pimage;
    //item �̹���

    GameObject effect;
    //�ⱸ �˸� ����Ʈ

    int point;
    //point


    void Start()
    {
        point = 0;

        effect = GameObject.Find("Exit").transform.GetChild(1).gameObject;
        //Exit ������Ʈ�� �ִ� ����Ʈ ��������
    }
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        //spacebar Ŭ�� �� ��ų�ߵ�
        {
            if(point >= 5)
            //point�� 5�̻� �� ���
            {
                LosePoint();
                //��ų ����
            }
            else
            //point�� 5�̸��̶� ��ų ���� �Ұ�
            {
                StartCoroutine(MessageShow());
                //���� �޼��� ���
            }
        }
    }


    /* �Լ� */
    public void GetPoint()
    //item ȹ�濡 ���� point ����
    //Detect���� ���
    {
        if(point < 6)
        //point�� 6���� ���� ���
        {
            pimage[point].SetActive(true);
            //�̹����� �ϳ��� ���

            point++;
        }
        else
        //point�� 6�̻��� ���
        {
            point++;
            //point�� ����
        }
    }
    public void LosePoint()
    //��ų �ߵ�
    {
        int tmp = point - 5;
        //point�� 5��ŭ ���������� ���� point

        StartCoroutine(FindExit());
        //�ⱸ �˸� ����Ʈ ����
        StartCoroutine(ActiveMessage());
        //��ų �ߵ� �޼��� ���

        if (tmp < 6)
        //���� ����Ʈ�� 6���� ���� ���
        {
            while(point != tmp)
            //���������� �̹��� ����
            {
                point--;

                pimage[point].SetActive(false);
            }
        }
        else
        //point�� ����
        {
            point -= 5;
        }
    }
    public void RespawnItem(GameObject item)
    //item ������
    {
        StartCoroutine(Respawn(item));
        //item ������Ʈ ���� + ������
    }


    /* �ڷ�ƾ */
    IEnumerator Respawn(GameObject item)
    //item ���� + 1�� �� ������
    {
        item.SetActive(false);

        yield return new WaitForSecondsRealtime(60.0f);

        item.SetActive(true);
    }
    IEnumerator MessageShow()
    //��ų ���Ұ� �޼��� ���
    {
        pimage[6].SetActive(true);

        yield return new WaitForSeconds(3.0f);

        pimage[6].SetActive(false);
    }
    IEnumerator ActiveMessage()
    //��ų ���� �޼��� ���
    {
        pimage[7].SetActive(true);

        yield return new WaitForSeconds(3.0f);

        pimage[7].SetActive(false);
    }

    IEnumerator FindExit()
    //�ⱸ�˸� ����Ʈ 15�ʰ� ����
    {
        effect.SetActive(true);

        yield return new WaitForSecondsRealtime(15.0f);

        effect.SetActive(false);
    }
}
