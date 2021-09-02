using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * [��ġ]
 * UIĵ������ Rocation ������Ʈ
 * 
 * [���]
 * �ⱸ��ġ�� �˷��ִ� ��ǥ Slider ����
 * �ⱸ���� �Ÿ� ǥ��
 */

public class Rocate : MonoBehaviour
{
    public Transform exit;
    //�ⱸ ��ġ

    GameObject player;
    //�÷��̾� ������Ʈ
    Slider slider;
    //��ǥ Slider
    Text text;
    //�Ÿ� ǥ��

    Vector3 dis;
    //y�� �÷��̾�� ���� �ⱸ��ġ
    Vector3 dir;
    //�÷��̾�->�ⱸ(y�� ����)
    Vector3 look;
    //{�÷��̾ �ٶ󺸴¹���}=>{�÷��̾�->�ⱸ}


    void Start()
    {
        player = GameObject.Find("Player");
        slider = transform.GetComponent<Slider>();
        text = transform.GetComponentInChildren<Text>();
    }
    void Update()
    {
        dis = new Vector3(exit.position.x, player.transform.position.y, exit.position.z - 1.0f);
        //y�� �÷��̾�� ���� �ⱸ��ġ

        dir = new Vector3(dis.x, 0, dis.z) - new Vector3(player.transform.position.x, 0, player.transform.position.z);
        dir = transform.TransformDirection(dir.normalized);
        //������ǥ�� ��ȯ

        Vector3 pdir = transform.TransformDirection(player.transform.forward).normalized;
        //�÷��̾��� Z������ ������ǥ�� ��ȯ

        look = dir - pdir;
        look = player.transform.InverseTransformDirection(look);
        //�÷��̾� ������ǥ�� ��ȯ


        if (look.x < 0)
        //�ⱸ�� �÷��̾��� ���ʿ� ��ġ
        {
            if (look.z < -1.0f)
            //�ⱸ�� ���ʿ� ��ġ�Ҷ�
            {
                slider.value = 0.0f;
                //Slider�� ���� ���� ����
            }
            else
            {
                slider.value = 0.5f - (Mathf.Abs(look.x) / 2);
                //x���� -�� ������ ������ ���밪
            }
        }
        else
        //�ⱸ�� �÷��̾��� �����ʿ� ��ġ
        {
            if (look.z < -1.0f)
            //�ⱸ�� ���ʿ� ��ġ�Ҷ�
            {
                slider.value = 1.0f;
                //Slider�� ������ ���� ����
            }
            else
            {
                slider.value = 0.5f + (look.x / 2);
            }
        }

        text.text = ((int)Vector3.Distance(player.transform.position, dis)).ToString() + " M";
        //�ⱸ�� �÷��̾� ������ �Ÿ�ǥ��
    }
}
