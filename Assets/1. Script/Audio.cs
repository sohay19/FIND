using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * [��ġ]
 * BGM ������Ʈ
 * 
 * [���]
 * BGM ���
 */

public class Audio : MonoBehaviour
{
    public static GameObject BGM;
    //bgm�� �ִ� ������Ʈ
    //Destroy�ϱ����� 
    public static AudioSource music;
    //bgm����


    private void Awake()
    {
        BGM = GameObject.Find("BGM");
        music = BGM.GetComponent<AudioSource>();


        if (music.isPlaying)
        //bgm�� ������̸� ����X
        {
            return;
        }
        else
        //bgm
        {
            music.Play();
            DontDestroyOnLoad(BGM);
        }
    }
    void Start()
    {
        
    }
    void Update()
    {

    }


    /* �Լ� */
    public static void BGMVolumeDown(Slider bgm_slider)
    //���� ���̱�
    {
        bgm_slider.value -= 0.1f;

        music.volume -= 0.1f;
    }
    public static void BGMVolumeUp(Slider bgm_slider)
    //���� Ű���
    {
        bgm_slider.value += 0.1f;

        music.volume += 0.1f;
    }
    public static void DestoryBGM()
    //BGM ������Ʈ �ı�
    {
        Destroy(BGM);
    }
}
