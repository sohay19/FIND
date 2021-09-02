using UnityEngine;

/*
 * [위치]
 * 버튼 동작에 의해 움직이는 오브젝트
 * 
 * [기능]
 * 버튼이 눌렸는지 확인하여 오브젝트를 움직임
 * 버튼이 떼어지면 제자리로 이동시킴
 * 작동될 경우 버튼위치 알림 소리를 중지
 * 작동이 멈출 경우 버튼위치 알림 소리를 재생
 */

public class Button_move : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    //버튼 2개
    public Transform starting;
    //버튼이 눌릴 경우 움직일 오브젝트의 트랜스폼
    public Transform destination;
    //작동 완료 시 트랜스폼

    Button bt1;
    Button bt2;
    //스크립트 컴포넌트
    AudioSource sound1;
    AudioSource sound2;
    //버튼위치 알림 소리

    Vector3 sdir_v;
    Quaternion sdir_q;
    //시작위치, 시작회전각
    Vector3 dir_v;
    Quaternion dir_q;
    //도착위치, 도착회전각


    void Start()
    {
        bt1 = button1.transform.GetChild(0).GetComponent<Button>();
        bt2 = button2.transform.GetChild(0).GetComponent<Button>();

        sdir_v = starting.transform.position;
        sdir_q = starting.transform.rotation;
        //시작점
        dir_v = destination.transform.position;
        dir_q = destination.transform.rotation;
        //도착점

        sound1 = bt1.transform.GetComponent<AudioSource>();
        sound2 = bt2.transform.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (bt1.ispush || bt2.ispush)
        //2개의 버튼 중 하나가 눌릴 경우
        {
            if(sound1.isPlaying && sound2.isPlaying)
            //두 버튼의 소리 모두 Off
            {
                sound1.Stop();
                sound2.Stop();
            }
            starting.transform.position = Vector3.MoveTowards(starting.transform.position, dir_v, 5.0f * Time.deltaTime);
            starting.transform.rotation = Quaternion.Slerp(starting.transform.rotation, dir_q, 5.0f * Time.deltaTime);
            //도착지점으로 오브젝트 움직임
        }
        else
        //2개의 버튼이 모두 눌리지 않을 경우
        {
            if (!sound1.isPlaying && !sound2.isPlaying)
            //두 버튼의 소리 모두 On
            {
                sound1.Play();
                sound2.Play();
            }
            starting.transform.position = Vector3.MoveTowards(starting.transform.position, sdir_v, 5.0f * Time.deltaTime);
            starting.transform.rotation = Quaternion.Slerp(starting.transform.rotation, sdir_q, 5.0f * Time.deltaTime);
            //시작지점으로 오브젝트 복귀
        }
    }
}
