using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [위치]
 * player 오브젝트
 * 움직일 수 있는 물체 오브젝트
 * 
 * [기능]
 * 자신에게 부딪힌 물체의 레이어를 파악하여 bool 반영
 * bool 상태를 확인하여 종료 반영
 */

public class Detect : MonoBehaviour
{
    public bool istrans;
    //중력이동 확인
    public bool iscatched;
    //잡힘 확인
    bool ispushed;
    //버튼 확인
    bool istrack;
    //Track 확인
    public bool isexit;
    //출구 확인

    CharacterController other;
    //this와 부딪힌 오브젝트
    Player_Move pmove;
    Props_Move omove;
    //플레이어와 움직여지는 물체의 Move 스크립트

    Spot sp;
    //중력변환 지점
    Teleport tp;
    //위치변환 지점
    Button bt;
    //버튼
    Track tr;
    //속력변환 지점
    Item item;
    //아이템

    CollisionFlags flag;
    //this의 collision flag


    void Start()
    {
        if(GameObject.Find("Item") != null)
            item = GameObject.Find("Item").transform.GetComponent<Item>();
        //튜토리얼엔 아이템이 없음

        if (transform.name == "Player")
        //this가 플레이어일 때
        {
            pmove = transform.GetComponent<Player_Move>();
        }
        else
        {
            omove = transform.GetComponent<Props_Move>();
        //this가 물체일 때
        }

        istrans = false;
        ispushed = false;
        iscatched = false;
        istrack = false;
        isexit = false;
        //bool 초기화
    }
    void Update()
    {
        if (transform.name == "Player")
        {
            flag = pmove.Flag;

        }
        else
        {
            flag = omove.Flag;
        }
        //플레이어냐 물체에냐에 따라 flag받아오기


        if (ispushed)
        //버튼 작동 중
        {
            if (flag == CollisionFlags.None)
            {
                bt.ispush = false;
                bt = null;

                ispushed = false;
            }
        }
        else if (istrans)
        //변환 중
        {
            if (sp != null)
            //중력변환
            {
                if (sp.touchCheck == false)
                {
                    sp = null;

                    istrans = false;
                }
            }
            else if (tp != null)
            //위치변환
            {
                if (tp.touchCheck == false)
                {
                    tp = null;

                    istrans = false;
                }
            }
        }
    }


    /* Collision */
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer == LayerMask.NameToLayer("Exit"))
        //출구
        {
            isexit = true;
        }
        else if(hit.gameObject.layer == LayerMask.NameToLayer("Item"))
        //아이템
        {
            if(transform.name == "Player")
            //플레이어일 경우에만 실행
            {
                AudioSource sound = transform.GetComponent<AudioSource>();
                sound.Play();

                item.GetPoint();

                item.RespawnItem(hit.transform.gameObject);
            }
        }


        if (hit.gameObject.layer != LayerMask.NameToLayer("Wall"))
        //벽이 아닌경우
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("Spot"))
            //중력변환 지점
            {
                sp = hit.transform.GetComponent<Spot>();
                sp.charc = transform.GetComponent<CharacterController>();

                istrans = true;
                sp.touchCheck = true;

            }
            else if (hit.gameObject.layer == LayerMask.NameToLayer("Teleport"))
            //위치변환 지점
            {
                tp = hit.transform.GetComponent<Teleport>();
                tp.charc = transform.GetComponent<CharacterController>();

                istrans = true;
                tp.touchCheck = true;
            }
            else if (hit.gameObject.layer == LayerMask.NameToLayer("Button"))
            //버튼
            {
                if (flag == CollisionFlags.Below)
                {
                    bt = hit.transform.GetComponent<Button>();

                    bt.ispush = true;
                    ispushed = true;
                }
            }
            else if (hit.gameObject.layer == LayerMask.NameToLayer("Track"))
            //속력변환 지점
            {
                if (transform.name == "Player")
                //플레이어일 경우에만 실행
                {
                    tr = hit.transform.GetComponent<Track>();

                    istrack = true;

                    tr.ChangeSpeed(pmove);
                }
            }
            else
            //그외 벽이 아닌 무언가에 부딪혔을 때
            {
                if (istrans)
                //상태변환 중이라면 -> 장애물에 부딪힌 것
                {
                    other = hit.transform.GetComponent<CharacterController>();

                    if (sp != null)
                    {
                        sp.othercharc = other;
                        sp.isobs = true;
                    }
                    else if (tp != null)
                    {
                        tp.othercharc = other;
                        tp.isobs = true;
                    }
                    //중력 or 위치변환 중 해당하는 곳에 부딪힌 물체 전달
                }
            }
        }
        else
        //벽인 경우
        {
            if (ispushed)
            //버튼 작동 종료
            {
                bt.ispush = false;
                bt = null;

                ispushed = false;
            }
            else if (istrack)
            //속력 변환 종료
            {
                tr.ReturnSpeed(pmove);
                tr = null;

                istrack = false;
            }
        }
    }
}
