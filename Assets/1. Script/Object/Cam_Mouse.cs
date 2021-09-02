using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * Main Camera ������Ʈ
 * 
 * [���]
 * ũ�ν� ��� ��ȯ
 * ���콺�� �̿��� ��ü���
 */

public class Cam_Mouse : MonoBehaviour
{
    bool iscatch;
    //��ü catch���� Ȯ��

    public GameObject[] crosshair;
    //ũ�ν�����
    public float props_gravity;
    //��ü���� �߷�
    public float movespeed;
    //��ü �����̴� �ӵ�
    public float catchdistance;
    //���� �� �ִ� �Ÿ�

    Ray ray;
    RaycastHit hit;
    // Ray���� ����
    Vector3 mousePosition;
    //���콺 ��ġ
    CharacterController catch_CC;
    //���� ��ü�� ĳ���� �ݶ��̴�
    Props_Move catchProps;
    //���� ��ü�� ��ũ��Ʈ
    Detect detect;
    //���� ��ü�� Detect ������Ʈ

    float distance;
    //��ü�� �÷��̾��� �Ÿ�


    /* ������Ƽ */
    public float Gravity
    {
        get
        {
            return props_gravity;
        }
    }
    //��üMove���� ���


    void Start()
    {
        iscatch = false;
    }
    void Update()
    {
        mousePosition = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mousePosition);
        //���콺 �������� ����

        Physics.Raycast(ray, out hit, catchdistance, 1 << LayerMask.NameToLayer("Move"));
        //���̸� �̿��Ͽ� hit�� ����


        if (Time.timeScale != 0)
        //�Ͻ������� �ƴ� ����
        {
            if (!iscatch)
            //��ü���X
            {
                if (hit.transform != null)
                //���� ��ü�� ���� ��
                {
                    detect = hit.transform.GetComponent<Detect>();
                    //���� ��ü�� Detect ��������

                    if (!detect.istrans)
                    //��ü�� ��ȯ ���� �ƴ� ����
                    {
                        crosshair[0].SetActive(false);
                        //�Ϲ� ũ�ν���� ����
                        crosshair[1].SetActive(true);
                        //Not Hold ũ�ν���� �ѱ�

                        if (Input.GetMouseButtonDown(0))
                        //���콺 Ŭ�� ��
                        {
                            detect.iscatched = true;
                            iscatch = true;

                            distance = hit.distance;
                            //Z�Ÿ�

                            catchProps = hit.transform.GetComponent<Props_Move>();
                            //���� ������Ʈ�� Props_Move ����

                            catch_CC = catchProps.Props_CC;
                            catchProps.Gravity = 0.0f;
                            catchProps.Dir = new Vector3(mousePosition.x, mousePosition.y, distance);
                            //�߷��� 0���� �����, ��ü�� �̵��ؾ��ϴ� ��ġ = ���콺 ������
                            //ĳ���� ��Ʈ�ѷ��̹Ƿ� Move�� ���� �̵��ؾ���
                        }
                    }
                }
                else
                // ���� ��ü�� ���� ��
                {
                    crosshair[1].SetActive(false);
                    //Not Hold ũ�ν���� ����
                    crosshair[2].SetActive(false);
                    //Hold �˸� ũ�ν���� ����
                    crosshair[0].SetActive(true);
                    //�Ϲ� ũ�ν���� �ѱ�
                }
            }
            else
            //��ü���O
            {
                if (Input.GetMouseButton(0))
                {
                    crosshair[1].SetActive(false);
                    //Not Hold ũ�ν���� ����
                    crosshair[2].SetActive(true);
                    //Hold �˸� ũ�ν���� �ѱ�
                }
                else if (Input.GetMouseButtonUp(0))
                //���콺�� �� ��
                {
                    crosshair[2].SetActive(false);
                    //Hold �˸� ũ�ν���� ����
                    crosshair[0].SetActive(true);
                    //�Ϲ� ũ�ν���� �ѱ�

                    catchProps.Gravity = props_gravity;
                    //���� ��ü �߷����� ����

                    iscatch = false;
                    detect.iscatched = false;
                    //catch ���� bool ����
                }
            }
        }
        else
        //�ɼ��� ��������
        {
            crosshair[1].SetActive(false);
            //Not Hold ũ�ν���� ����
            crosshair[2].SetActive(false);
            //Hold �˸� ũ�ν���� ����
            crosshair[0].SetActive(false);
            //�Ϲ� ũ�ν���� ����
        }
    }
}
