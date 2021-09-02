using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * [��ġ]
 * �ⱸ ������Ʈ
 * 
 * [���]
 * �ⱸ�� Player�� ���� ��� �� ��ȯ
 */

public class Exit : MonoBehaviour
{
    ParticleSystem exit;
    //�ڽĿ�����Ʈ
    Detect player;
    //�÷��̾��� Detect

    string sname;
    char first;
    int num;
    //���� �� Ȯ�ο�


    void Start()
    {
        exit = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        player = GameObject.Find("Player").GetComponent<Detect>();

        sname = SceneManager.GetActiveScene().name;
        //���� ���� ���� ��
        first = sname[0];
        //�� �̸��� ù ����
        num = int.Parse(sname[sname.Length - 1].ToString());
        //�� �̸��� ������ ����
    }
    void Update()
    {
        if (Time.timeScale != 0 && !exit.isPlaying)
        //TimeScale�� 1�� �� ����
        {
            exit.Play();
        }

        if(player.isexit)
        //�÷��̾ �ⱸ�� ���� ���
        {
            if(first == 'T')
            //���� ���� Tutorial�� ���
            {
                ++num;
                //��������

                if (num == 5)
                //Tutorial�� 4������ �ֱ⿡ Stage�� �ѱ�
                {
                    GameManager_Load.StageLoadingScene("Stage_1");
                }
                else
                //���� Tutorial�� ��ȯ
                {
                    GameManager_Load.OtherLoadingScene("Tutorial_" + num);

                }
            }
            else if(first == 'S')
            //���� ���� Stage�� ���
            {
                GameManager_Load.OtherLoadingScene("Main");

                /*
                ++num;

                Load.StageLoadingScene("Stage_" + num);
                */

                //���� Stage�� 1���̹Ƿ� Main������ �̵�
            }
        }
    }
}
