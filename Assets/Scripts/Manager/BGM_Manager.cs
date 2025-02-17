using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BGM
{
    public string name;
    public AudioClip clip;
}
public class BGM_Manager : MonoBehaviour
{
    static public BGM_Manager instance;

    public AudioSource audioSourceBgm;
    public float bgmVol=1;

    //public string[] playSoundName;

    public BGM[] bgmSounds;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            bgmVol = PlayerPrefs.GetFloat("MusicVol");
        }        
        audioSourceBgm.volume = bgmVol;

        //playSoundName = new string[audioSourceBgm];
    }
    public void PlayBGM(string _name)
    {
        //Debug.Log("여기까진왔나?");
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                if(audioSourceBgm.clip != bgmSounds[i].clip)
                {
                    audioSourceBgm.clip = bgmSounds[i].clip;
                    audioSourceBgm.Play();
                }
                //for (int j = 0; j < audioSourceEffects.Length; j++)
                //{
                //    if (!audioSourceEffects[j].isPlaying)
                //    {
                //playSoundName[i] = effectSounds[i].name;
                
                return;
                //    }
                // }
                // Debug.Log("모든 가용 오디오소스가 사용중입니다");
                // return;
            }
        }
        Debug.Log(_name + "사운드가 사운드매니져에 등록되지 않았습니다");
    }

    public void setBGMVol(float numb)
    {
        audioSourceBgm.volume = numb;
    }
}
