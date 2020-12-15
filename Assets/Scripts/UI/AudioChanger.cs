using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioChanger : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public bool isMusic;
    public bool isSFX;

    private void Start()
    {
        if(isMusic)
            slider.value = AudioManager.Instance.musicVolume;
        else if (isSFX)
            slider.value = AudioManager.Instance.sfxVolume;
    }

    public void ChangeSFX()
    {
        AudioManager.Instance.sfxVolume = slider.value;
    }
    
    public void ChangeMusic()
    {
        AudioManager.Instance.musicVolume = slider.value;
    }
}
