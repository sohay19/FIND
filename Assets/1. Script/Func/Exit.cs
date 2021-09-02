using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * [위치]
 * 출구 오브젝트
 * 
 * [기능]
 * 출구에 Player가 닿을 경우 씬 전환
 */

public class Exit : MonoBehaviour
{
    ParticleSystem exit;
    //자식오브젝트
    Detect player;
    //플레이어의 Detect

    string sname;
    char first;
    int num;
    //현재 씬 확인용


    void Start()
    {
        exit = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        player = GameObject.Find("Player").GetComponent<Detect>();

        sname = SceneManager.GetActiveScene().name;
        //현재 실행 중인 씬
        first = sname[0];
        //씬 이름의 첫 글자
        num = int.Parse(sname[sname.Length - 1].ToString());
        //씬 이름의 마지막 숫자
    }
    void Update()
    {
        if (Time.timeScale != 0 && !exit.isPlaying)
        //TimeScale이 1일 때 실행
        {
            exit.Play();
        }

        if(player.isexit)
        //플레이어가 출구에 닿은 경우
        {
            if(first == 'T')
            //현재 씬이 Tutorial일 경우
            {
                ++num;
                //숫자증가

                if (num == 5)
                //Tutorial이 4까지만 있기에 Stage로 넘김
                {
                    GameManager_Load.StageLoadingScene("Stage_1");
                }
                else
                //다음 Tutorial로 전환
                {
                    GameManager_Load.OtherLoadingScene("Tutorial_" + num);

                }
            }
            else if(first == 'S')
            //현재 씬이 Stage일 경우
            {
                GameManager_Load.OtherLoadingScene("Main");

                /*
                ++num;

                Load.StageLoadingScene("Stage_" + num);
                */

                //현재 Stage가 1개이므로 Main씬으로 이동
            }
        }
    }
}
