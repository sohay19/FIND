using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * spot�� ���� �ֻ��� ������Ʈ
 * 
 * [���]
 * 1set�� spot���� ����
 * �������ð�, ȸ���ӵ�, �̵��ӵ��� ����
 */

public class Spot_Point : MonoBehaviour
{
    public Spot[] sPoint;
    //spot ���� ����

    public float respawnTime;
    //�ݴ��� spot�� ������ �Ǵ� �ð�
    public float turnSpeed;
    //ȸ�� �ӵ�
    public float spotTransSpeed;
    //�ٸ� spot���� �̵��ϴ� �ӵ�


    /* ������Ƽ */
    public float[] times
    {
        get
        {
            float[] num = { respawnTime, turnSpeed, spotTransSpeed };

            return num;
        }
    }
    //Spot���� ���


    void Start()
    {
        
    }
    void Update()
    {

    }
}
