using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * 각각의 track 오브젝트
 * 
 * [기능]
 * track의 속도로 플레이어 속도를 변경
 * track을 벗어나면 원래 속도로 변경
 */

public class Track : MonoBehaviour
{
    public float changeSpeed;
    //변경 될 속도
    public float previousSpeed;
    //원래 플레이어 속도

    Player_Move pm;


    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<Player_Move>();

        previousSpeed = pm.playerSpeed;
        //플레이어의 원래 속도 저장
    }
    void Update()
    {
        
    }


    /* 함수 */
    public void ChangeSpeed(Player_Move player)
    //Track의 속도로 플레이어 속도 변경
    {
        player.playerSpeed = changeSpeed;
    }
    public void ReturnSpeed(Player_Move player)
    //플레이어 원래 속도 변경
    {
        player.playerSpeed = previousSpeed;
    }
}
