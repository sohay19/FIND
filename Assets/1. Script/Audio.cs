using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * [위치]
 * BGM 오브젝트
 * 
 * [기능]
 * BGM 재생
 */

public class Audio : MonoBehaviour
{
    public static GameObject BGM;
    //bgm이 있는 오브젝트
    //Destroy하기위해 
    public static AudioSource music;
    //bgm파일


    private void Awake()
    {
        BGM = GameObject.Find("BGM");
        music = BGM.GetComponent<AudioSource>();


        if (music.isPlaying)
        //bgm이 재생중이면 실행X
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


    /* 함수 */
    public static void BGMVolumeDown(Slider bgm_slider)
    //볼륨 줄이기
    {
        bgm_slider.value -= 0.1f;

        music.volume -= 0.1f;
    }
    public static void BGMVolumeUp(Slider bgm_slider)
    //볼륨 키우기
    {
        bgm_slider.value += 0.1f;

        music.volume += 0.1f;
    }
    public static void DestoryBGM()
    //BGM 오브젝트 파괴
    {
        Destroy(BGM);
    }
}
