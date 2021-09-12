using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * Main Camera ������Ʈ
 * 
 * [���]
 * ī�޶� �����ӿ� ���� �þ� ȸ��
 * ȸ�� ����
 */

public class Cam_Rotate : MonoBehaviour
{
    public float rotateSpeed;
    //ȸ���ӵ�

    Vector3 dir;
    //��������

    float sumX, sumY;
    float sumZ;
    //���� ȸ����


    /* ������Ƽ */
    public float RotateSpeed
    {
        set
        {
            rotateSpeed = value;
        }
        get
        {
            return rotateSpeed;
        }
    }
    //Cam, Player Rotate���� ���
    public float SumZ
    {
        set
        {
            sumZ = value;
        }
    }
    //Spot, Teleport ���


    void Start()
    {
        sumZ = 0;
    }
    void Update()
    {
        if (!GameManager_InGame.isoption)
        //�ɼǽ���X �� ��
        {
            float Y = Input.GetAxis("Mouse X");
            float X = Input.GetAxis("Mouse Y");
            //���콺 �Է��� �޾� ���� ����

            sumX += X * RotateSpeed * Time.deltaTime;
            sumY += Y * RotateSpeed * Time.deltaTime;
            //ȸ������ �����Ͽ� ����

            sumX = Mathf.Clamp(sumX, -80f, 80f);
            //���� ȸ���� ����


            if (sumZ < 178.0f)
            //�߷º�ȯ ��
            {
                dir = new Vector3(-sumX, sumY, sumZ);
            }
            else
            //�߷º�ȯ ��
            {
                dir = new Vector3(sumX, -sumY, sumZ);
            }

            transform.localEulerAngles = dir;
            //���� ���� ȸ��
        }
    }
}
