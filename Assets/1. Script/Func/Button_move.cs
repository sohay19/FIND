using UnityEngine;

/*
 * [��ġ]
 * ��ư ���ۿ� ���� �����̴� ������Ʈ
 * 
 * [���]
 * ��ư�� ���ȴ��� Ȯ���Ͽ� ������Ʈ�� ������
 * ��ư�� �������� ���ڸ��� �̵���Ŵ
 * �۵��� ��� ��ư��ġ �˸� �Ҹ��� ����
 * �۵��� ���� ��� ��ư��ġ �˸� �Ҹ��� ���
 */

public class Button_move : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    //��ư 2��
    public Transform starting;
    //��ư�� ���� ��� ������ ������Ʈ�� Ʈ������
    public Transform destination;
    //�۵� �Ϸ� �� Ʈ������

    Button bt1;
    Button bt2;
    //��ũ��Ʈ ������Ʈ
    AudioSource sound1;
    AudioSource sound2;
    //��ư��ġ �˸� �Ҹ�

    Vector3 sdir_v;
    Quaternion sdir_q;
    //������ġ, ����ȸ����
    Vector3 dir_v;
    Quaternion dir_q;
    //������ġ, ����ȸ����


    void Start()
    {
        bt1 = button1.transform.GetChild(0).GetComponent<Button>();
        bt2 = button2.transform.GetChild(0).GetComponent<Button>();

        sdir_v = starting.transform.position;
        sdir_q = starting.transform.rotation;
        //������
        dir_v = destination.transform.position;
        dir_q = destination.transform.rotation;
        //������

        sound1 = bt1.transform.GetComponent<AudioSource>();
        sound2 = bt2.transform.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (bt1.ispush || bt2.ispush)
        //2���� ��ư �� �ϳ��� ���� ���
        {
            if(sound1.isPlaying && sound2.isPlaying)
            //�� ��ư�� �Ҹ� ��� Off
            {
                sound1.Stop();
                sound2.Stop();
            }
            starting.transform.position = Vector3.MoveTowards(starting.transform.position, dir_v, 5.0f * Time.deltaTime);
            starting.transform.rotation = Quaternion.Slerp(starting.transform.rotation, dir_q, 5.0f * Time.deltaTime);
            //������������ ������Ʈ ������
        }
        else
        //2���� ��ư�� ��� ������ ���� ���
        {
            if (!sound1.isPlaying && !sound2.isPlaying)
            //�� ��ư�� �Ҹ� ��� On
            {
                sound1.Play();
                sound2.Play();
            }
            starting.transform.position = Vector3.MoveTowards(starting.transform.position, sdir_v, 5.0f * Time.deltaTime);
            starting.transform.rotation = Quaternion.Slerp(starting.transform.rotation, sdir_q, 5.0f * Time.deltaTime);
            //������������ ������Ʈ ����
        }
    }
}
