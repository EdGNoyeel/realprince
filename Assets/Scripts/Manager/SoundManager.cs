using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Sound {
    public string name;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;
    bool soundStart = false;
    #region singleton
    void Awake()//객체 생성시 최초 실행
    {
        soundStart = false;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
            
        else
            Destroy(this.gameObject);
    }
    #endregion singleton

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;
    

    void Start()
    {
        float vol=1;
        playSoundName = new string[audioSourceEffects.Length];
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (PlayerPrefs.HasKey("MusicVol"))
            {
                vol = PlayerPrefs.GetFloat("EffectVol");
            }
            audioSourceEffects[i].volume = vol;
        }
    }

    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                //for (int j = 0; j < audioSourceEffects.Length; j++)
                //{
                //    if (!audioSourceEffects[j].isPlaying)
                //    {
                        playSoundName[i] = effectSounds[i].name;
                        audioSourceEffects[i].clip = effectSounds[i].clip;
                        audioSourceEffects[i].Play();                        
                        return;
                //    }
               // }
               // Debug.Log("모든 가용 오디오소스가 사용중입니다");
               // return;
            }
        }
        Debug.Log(_name + "사운드가 사운드매니져에 등록되지 않았습니다");
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
      
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
        Debug.Log("재생중인 " + _name + "사운드가 없습니다");
    }

    public void SetEffectVol(float numb)
    {
        if (soundStart)
        {
            for (int i = 0; i < audioSourceEffects.Length; i++)
            {
                audioSourceEffects[i].volume = numb;
            
                PlaySE("noMoney");
            }
            
        }
        soundStart = true;
    }
}
