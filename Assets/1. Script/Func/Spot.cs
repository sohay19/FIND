using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * ������ spot ������Ʈ
 * 
 * [���]
 * �� spot�� ���� ��Ҵ��� Ȯ��
 * ���� ������Ʈ�� ���� spot���� �̵� �� ȸ��
 * ���� spot ���� �ѱ�
 */

public class Spot : MonoBehaviour
{
    bool turn;
    //��ġ��ȯ Ȯ��
    public bool touchCheck;
    //���̿� ��Ҵ��� Ȯ��
    public bool isobs;
    //��ֹ����� Ȯ��

    public CharacterController charc;
    //�߷º�ȯ�� ������Ʈ
    public CharacterController othercharc;
    //��ֹ� ������Ʈ
    Cam_Rotate cam;
    //�þ� ȸ��
    Spot_Point sp;
    Spot[] spot;
    //�߷º�ȯ ����

    Vector3 dir;
    //������ ����
    Vector3 des;
    Quaternion qdes;
    //���� ���� �� ȸ����

    float[] num;
    //�ð� �� �ӵ� ���� ����


    private void Awake()
    {
        touchCheck = false;
    }
    void Start()
    {
        sp = transform.GetComponentInParent<Spot_Point>();
        cam = Camera.main.transform.GetComponent<Cam_Rotate>();

        spot = sp.sPoint;
        num = sp.times;

        turn = false;
        isobs = false;
    }
    private void FixedUpdate()
    {
        if (spot[0].touchCheck)
        //spot 0�� ����
        {
            ChangeGravity(spot[1]);
            //�߷º�ȯ �Լ� ����

            if (turn)
            //��ȯ �Ϸ� �� ����
            {
                spot[0].touchCheck = false;
                //spot 0�� check ����
                turn = false;
                //�߷º�ȯ ��
            }
        }
        else if (spot[1].touchCheck)
        //spot 1�� ����
        {
            ChangeGravity(spot[0]);

            if (turn)
            {
                spot[1].touchCheck = false;

                turn = false;
            }
        }
    }


    /* �Լ� */
    void ChangeGravity(Spot moveSpot)
    //�߷� ��ȯ �Լ�
    {
        if (charc != null)
        //ĳ���� ��Ʈ�ѷ��� �Ҵ�� ��츸 ����
        {
            if (!isobs)
            //���� ������ ��ֹ��� ���� ��
            {
                des = moveSpot.transform.position;
                qdes = moveSpot.transform.localRotation;
                //���� ��ġ ����

                StartCoroutine(activeSpot(moveSpot));
                //���� spot�� ���� �Ѵ� �ڷ�ƾ �Լ�

                dir = des - charc.transform.position;
                //������������ ���� ����

                charc.Move(dir * num[2] * Time.deltaTime);
                charc.transform.localRotation = Quaternion.Slerp(charc.transform.localRotation, qdes, num[1] * Time.deltaTime);
                //������ spot �̵�


                if (charc.name == "Player")
                {
                    cam.SumZ = qdes.eulerAngles.z;
                }
                //�÷��̾��� ��쿡�� ȸ���� ����

                if (Vector3.Distance(charc.transform.position, des) <= 2.5f)
                //�������� ���� �������� ���
                {
                    charc.transform.localEulerAngles = qdes.eulerAngles;
                    //ȸ�� ���� ����

                    turn = true;
                    //ȸ�� ����
                    charc = null;
                    //���� ����
                }
            }
            else
            //���� ������ ��ֹ��� ���� ���
            {
                othercharc.Move(Vector3.right * num[2]);
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
    IEnumerator activeSpot(Spot spot)
    //���� spot ���� �ð� ���� �ѱ�
    {
        spot.gameObject.SetActive(false);

        yield return new WaitForSeconds(num[0]);

        spot.gameObject.SetActive(true);
    }
}
