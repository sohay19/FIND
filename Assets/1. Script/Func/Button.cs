using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * Button(���� �����̴� Moving ������Ʈ)
 * 
 * [���]
 * ��ư�� �������� �۵�
 * ���ȴ��� ���� Ȯ��
 * ���� ���� �ٴڿ� ��Ҵ��� Ȯ��
 * ���� ������ ��� ����Ʈ On
 */

public class Button : MonoBehaviour
{
    public GameObject effect;
    //������ �� �۵� ����Ʈ

    Vector3 sdir;
    //��ư�� ���� ��ġ

    public bool ispush;
    //��������
    bool isend;
    //���� ���ȴ��� Ȯ�ο���


    void Start()
    {
        ispush = false;
        isend = false;
        
        sdir = transform.position;
        //�������� ��ġ ����
    }
    void Update()
    {
        if (ispush && !isend)
        //����O & ���� ������ ����
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
        else if(!ispush)
        //����X
        {
            if (transform.position != sdir)
            //��ư ���� ��ġ�� ����
            {
                transform.position = Vector3.MoveTowards(transform.position, sdir, Time.deltaTime);
            }
        }
    }


    /* Collision */
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.name == "Plane")
        {
            isend = true;

            effect.SetActive(true);
            //����Ʈ On
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.transform.name == "Plane")
        {
            isend = false;

            effect.SetActive(false);
            //����Ʈ Off
        }
    }
}
