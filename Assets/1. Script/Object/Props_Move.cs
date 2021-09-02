using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * �����̴� ��ü ������Ʈ
 * 
 * [���]
 * ��ü �߷�����
 * catch �� ���콺 �����ǿ� ���� �̵�
 */

public class Props_Move : MonoBehaviour
{
    Detect detect;
    CharacterController props_CC;
    Cam_Mouse getcamera;

    Vector3 dir;
    //������
    CollisionFlags flag;

    float gravity;


    /* ������Ƽ */
    public CharacterController Props_CC
    {
        get
        {
            return props_CC;
        }
    }
    public float Gravity
    {
        get
        {
            return gravity;
        }
        set
        {
            gravity = value;
        }
    }
    public Vector3 Dir
    {
        get
        {
            return dir;
        }
        set
        {
            dir = value;
        }
    }
    public CollisionFlags Flag
    {
        get
        {
            return flag;
        }
    }
    //Cam_Mouse�� Detect���� ���


    void Start()
    {
        props_CC = transform.GetComponent<CharacterController>();
        getcamera = Camera.main.GetComponent<Cam_Mouse>();
        detect = transform.GetComponent<Detect>();

        gravity = getcamera.Gravity;
    }
    void FixedUpdate()
    {
        if (!detect.istrans)
        //��ȯ ���� �ƴ� ����
        {
            if (!detect.iscatched)
            //catch�� �ƴ�
            { 
                ObjectMove();
                //�߷¸� ����
            }
            else
            //catch��
            {
                CatchMove();
                //���콺 �����ǿ� ���� �̵�
            }
        }
    }


    /* �Լ� */
    void ObjectMove()
    //�߷� ����
    {
        dir = transform.TransformDirection(Vector3.down);
        //������ǥ�踦 ���� ��ǥ��� ��ȯ

        flag = props_CC.Move(dir * gravity * Time.deltaTime);
    }
    void CatchMove()
    //���콺 �����ǿ� ���� �̵�
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(dir) - props_CC.transform.position;
        
        flag = props_CC.Move(pos);
    }
}
