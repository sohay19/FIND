using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * Main Camera ������Ʈ
 * 
 * [���]
 * �þ߸� ������ ��ġ��
 */

public class Cam_Follow : MonoBehaviour
{
    public Transform target;
    //�÷��̾��� �þ�


    void Start()
    {

    }
    void Update()
    {
        transform.position = target.position;
        //����ī�޶� �÷��̾� �þ� ��ġ��
    }
}
