using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * player ������Ʈ
 * 
 * [���]
 * ���콺 �Է¿� ���� �÷��̾� ȸ��
 */

public class Player_Rotate : MonoBehaviour
{
    Cam_Rotate cam;
    //main ī�޶�

    Vector3 dir;
    //������


    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Cam_Rotate>();
    }

    void Update()
    {
        Vector3 camrot = cam.transform.localEulerAngles;
        //ī�޶� ȸ����

        dir = new Vector3(0, camrot.y, camrot.z);
        //�¿�ȸ���� ���� y�� �߷º�ȯ�� ���� z�� �ݿ�

        transform.localEulerAngles = dir;
    }
}
