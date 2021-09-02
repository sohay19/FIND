using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��ġ]
 * player ������Ʈ
 * ������ �� �ִ� ��ü ������Ʈ
 * 
 * [���]
 * �ڽſ��� �ε��� ��ü�� ���̾ �ľ��Ͽ� bool �ݿ�
 * bool ���¸� Ȯ���Ͽ� ���� �ݿ�
 */

public class Detect : MonoBehaviour
{
    public bool istrans;
    //�߷��̵� Ȯ��
    public bool iscatched;
    //���� Ȯ��
    bool ispushed;
    //��ư Ȯ��
    bool istrack;
    //Track Ȯ��
    public bool isexit;
    //�ⱸ Ȯ��

    CharacterController other;
    //this�� �ε��� ������Ʈ
    Player_Move pmove;
    Props_Move omove;
    //�÷��̾�� ���������� ��ü�� Move ��ũ��Ʈ

    Spot sp;
    //�߷º�ȯ ����
    Teleport tp;
    //��ġ��ȯ ����
    Button bt;
    //��ư
    Track tr;
    //�ӷº�ȯ ����
    Item item;
    //������

    CollisionFlags flag;
    //this�� collision flag


    void Start()
    {
        if(GameObject.Find("Item") != null)
            item = GameObject.Find("Item").transform.GetComponent<Item>();
        //Ʃ�丮�� �������� ����

        if (transform.name == "Player")
        //this�� �÷��̾��� ��
        {
            pmove = transform.GetComponent<Player_Move>();
        }
        else
        {
            omove = transform.GetComponent<Props_Move>();
        //this�� ��ü�� ��
        }

        istrans = false;
        ispushed = false;
        iscatched = false;
        istrack = false;
        isexit = false;
        //bool �ʱ�ȭ
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
        //�÷��̾�� ��ü���Ŀ� ���� flag�޾ƿ���


        if (ispushed)
        //��ư �۵� ��
        {
            if (flag == CollisionFlags.None)
            {
                bt.ispush = false;
                bt = null;

                ispushed = false;
            }
        }
        else if (istrans)
        //��ȯ ��
        {
            if (sp != null)
            //�߷º�ȯ
            {
                if (sp.touchCheck == false)
                {
                    sp = null;

                    istrans = false;
                }
            }
            else if (tp != null)
            //��ġ��ȯ
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
        //�ⱸ
        {
            isexit = true;
        }
        else if(hit.gameObject.layer == LayerMask.NameToLayer("Item"))
        //������
        {
            if(transform.name == "Player")
            //�÷��̾��� ��쿡�� ����
            {
                AudioSource sound = transform.GetComponent<AudioSource>();
                sound.Play();

                item.GetPoint();

                item.RespawnItem(hit.transform.gameObject);
            }
        }


        if (hit.gameObject.layer != LayerMask.NameToLayer("Wall"))
        //���� �ƴѰ��
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("Spot"))
            //�߷º�ȯ ����
            {
                sp = hit.transform.GetComponent<Spot>();
                sp.charc = transform.GetComponent<CharacterController>();

                istrans = true;
                sp.touchCheck = true;

            }
            else if (hit.gameObject.layer == LayerMask.NameToLayer("Teleport"))
            //��ġ��ȯ ����
            {
                tp = hit.transform.GetComponent<Teleport>();
                tp.charc = transform.GetComponent<CharacterController>();

                istrans = true;
                tp.touchCheck = true;
            }
            else if (hit.gameObject.layer == LayerMask.NameToLayer("Button"))
            //��ư
            {
                if (flag == CollisionFlags.Below)
                {
                    bt = hit.transform.GetComponent<Button>();

                    bt.ispush = true;
                    ispushed = true;
                }
            }
            else if (hit.gameObject.layer == LayerMask.NameToLayer("Track"))
            //�ӷº�ȯ ����
            {
                if (transform.name == "Player")
                //�÷��̾��� ��쿡�� ����
                {
                    tr = hit.transform.GetComponent<Track>();

                    istrack = true;

                    tr.ChangeSpeed(pmove);
                }
            }
            else
            //�׿� ���� �ƴ� ���𰡿� �ε����� ��
            {
                if (istrans)
                //���º�ȯ ���̶�� -> ��ֹ��� �ε��� ��
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
                    //�߷� or ��ġ��ȯ �� �ش��ϴ� ���� �ε��� ��ü ����
                }
            }
        }
        else
        //���� ���
        {
            if (ispushed)
            //��ư �۵� ����
            {
                bt.ispush = false;
                bt = null;

                ispushed = false;
            }
            else if (istrack)
            //�ӷ� ��ȯ ����
            {
                tr.ReturnSpeed(pmove);
                tr = null;

                istrack = false;
            }
        }
    }
}
