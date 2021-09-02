using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * ������ track ������Ʈ
 * 
 * [���]
 * track�� �ӵ��� �÷��̾� �ӵ��� ����
 * track�� ����� ���� �ӵ��� ����
 */

public class Track : MonoBehaviour
{
    public float changeSpeed;
    //���� �� �ӵ�
    public float previousSpeed;
    //���� �÷��̾� �ӵ�

    Player_Move pm;


    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<Player_Move>();

        previousSpeed = pm.playerSpeed;
        //�÷��̾��� ���� �ӵ� ����
    }
    void Update()
    {
        
    }


    /* �Լ� */
    public void ChangeSpeed(Player_Move player)
    //Track�� �ӵ��� �÷��̾� �ӵ� ����
    {
        player.playerSpeed = changeSpeed;
    }
    public void ReturnSpeed(Player_Move player)
    //�÷��̾� ���� �ӵ� ����
    {
        player.playerSpeed = previousSpeed;
    }
}
