using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Setting : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    Slider effectVol;
    [SerializeField]
    Slider musicVol;
    public TextMeshProUGUI id;
    // Start is called before the first frame update
    void Start()
    {
        id.text = StatusManager.instance.email;
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            musicVol.value = PlayerPrefs.GetFloat("MusicVol");
        }
        if (PlayerPrefs.HasKey("EffectVol"))
        {
            effectVol.value = PlayerPrefs.GetFloat("EffectVol");
        }
        anim = GetComponent<Animator>();


        //effectVol.value = StatusManager.instance.effectVol;
        //musicVol.value = StatusManager.instance.musicVol;
    }

    // Update is called once per frame
    public void Open()
    {
        anim.SetTrigger("Open");
    }

    public void Close()
    {
        PlayerPrefs.SetFloat("MusicVol", musicVol.value);
        PlayerPrefs.SetFloat("EffectVol", effectVol.value);
        anim.SetTrigger("Close");
    }

    public void SetMusicVol(float numb)
    {
        PlayerPrefs.SetFloat("MusicVol", numb);
        GameObject.Find("BGM_Manager").GetComponent<BGM_Manager>().setBGMVol(numb);
        
    }
    public void SetEffectVol(float numb)
    {
        PlayerPrefs.SetFloat("EffectVol", numb);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SetEffectVol(numb);
        
    }
}
