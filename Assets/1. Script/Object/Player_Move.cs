using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * player ������Ʈ
 * 
 * [���]
 * Ű���� �Է¿� ���� �÷��̾� �̵�
 * �߷� ����
 */

public class Player_Move : MonoBehaviour
{
    public static bool ismove;
    //�����̴� ������ Ȯ��
    public float playerSpeed;
    public float gravity;
    //�ӷ�, �߷�
    
    Detect detect;
    //�÷��̾� Detect
    CharacterController p_cc;
    //�÷��̾� ��Ʈ�ѷ�

    Vector3 dir;
    //������
    CollisionFlags flag;
    
    float hor, ver;
    //Ű���� �Է�
    float v_speed;
    //�����ӷ�


    /* ������Ƽ */
    public CollisionFlags Flag
    {
        get
        {
            return flag;
        }
    }
    //Detect���� ���


    void Start()
    {
        p_cc = gameObject.GetComponent<CharacterController>();
        detect = gameObject.GetComponent<Detect>();
    }
    private void FixedUpdate()
    {
        if (!detect.istrans)
        //��ȯ ���� �ƴ� ����
        {
            Move();
        }
    }
    void Update()
    {
        /*
        if (Input.GetButtonDown("Jump"))
        {
            jump();
        }
        */
    }


    /* �Լ� */
    void Move()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        //Ű���� �Է�

        dir = new Vector3(hor, 0, ver);
        dir = dir.normalized;
        //�յ�,�翷��
        
        v_speed += -gravity * Time.deltaTime;
        //�߷�����

        dir.y = v_speed;
        //y�� ����
        dir = transform.TransformDirection(dir);
        //������ǥ�踦 ���� ��ǥ��� ��ȯ
        //CC�� ������ǥ �������� ������

        flag = p_cc.Move(dir * playerSpeed * Time.deltaTime);
    }


    /* ���� */
    /*void jump()
    {
        if (!isjump)
        {
            isjump = true;

            v_speed = jump_gravity;
        }
        else if (isjump && p_cc.collisionFlags != CollisionFlags.None)
        {
            v_speed = 0;

            isjump = false;
        }
    }
    */
}
