using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * ������ teleport ������Ʈ
 * 
 * [���]
 * �� teleport�� ���� ��Ҵ��� Ȯ��
 * ���� ������Ʈ�� ���� teleport�� �̵� �� ȸ��
 * ���� teleport ���� �ѱ�
 */

public class Teleport : MonoBehaviour
{
    bool isend;
    //��ȯ�Ϸ� Ȯ��
    public bool touchCheck;
    //�ڷ���Ʈ�� ��Ҵ��� Ȯ��
    public bool isobs;
    //��ֹ����� Ȯ��

    public CharacterController charc;
    //��ġ��ȯ�� ������Ʈ
    public CharacterController othercharc;
    //��ֹ� ������Ʈ
    Cam_Rotate cam;
    //�þ� ȸ��
    Teleport_Point tp;
    Teleport[] tel;
    //��ġ��ȯ ����

    Vector3 dir;
    //������ ����
    Vector3 des;
    Quaternion qdes;
    //���� ���� �� ȸ����

    float restime;
    //�������ð�


    private void Awake()
    {
        touchCheck = false;
    }
    void Start()
    {
        tp = transform.GetComponentInParent<Teleport_Point>();
        cam = Camera.main.transform.GetComponent<Cam_Rotate>();

        tel = tp.tPoint;
        restime = tp.respawnTime;

        isend = false;
        isobs = false;
    }
    void FixedUpdate()
    {
        if (tel[0].touchCheck)
        //teleport 0�� ����
        {
            ChangePosition(tel[1]);
            //��ġ��ȯ �Լ� ����

            if(isend)
            //��ȯ �Ϸ� �� ����
            {
                tel[0].touchCheck = false;
                //teleport 0�� check ����
                isend = false;
                //��ġ��ȯ ��
            }
        }
        else if(tel[1].touchCheck)
        //teleport 1�� ����
        {
            ChangePosition(tel[0]);

            if (isend)
            {
                tel[1].touchCheck = false;

                isend = false;
            }
        }
    }


    /* �Լ� */
    void ChangePosition(Teleport movePoint)
    //��ġ ��ȯ �Լ�
    {
        if (charc != null)
        //ĳ���� ��Ʈ�ѷ��� �Ҵ�� ��츸 ����
        {
            if (!isobs)
            {
                des = movePoint.transform.position;
                qdes = movePoint.transform.localRotation;
                //���� ��ġ ����

                StartCoroutine(activePoint(movePoint));
                //���� point�� ���� �Ѵ� �ڷ�ƾ �Լ�

                charc.transform.position = des;
                charc.transform.localRotation = qdes;
                //������ point�� �̵�


                if (charc.name == "Player")
                {
                    cam.SumZ = qdes.eulerAngles.z;
                }
                //�÷��̾��� ��쿡�� ȸ���� ����

                if (Vector3.Distance(charc.transform.position, des) <= 0.5f)
                //�������� ���� �������� ���
                {
                    isend = true;
                    //�̵� ����
                    charc = null;
                    //���� ����
                }
            }
            else
            //���� ������ ��ֹ��� ���� ���
            {
                othercharc.Move(Vector3.right);
                //��ֹ� ġ���

                if (Vector3.Distance(dir, othercharc.transform.position) > 6.0f)
                //�������� ����� �־����� ���
                {
                    othercharc = null;
                    //���� ����
                    isobs = false;
                    //�浹 ���ϱ� ����
                }
            }
        }
    }


    /* �ڷ�ƾ */
    IEnumerator activePoint(Teleport point)
    //���� point ���� �ð� ���� �ѱ�
    {
        point.gameObject.SetActive(false);

        yield return new WaitForSeconds(restime);

        point.gameObject.SetActive(true);
    }
}
