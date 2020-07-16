using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{
    [SerializeField] public Slider mainVolumeSlider;
    [SerializeField] public AudioMixer mainMixer;
    [SerializeField] public Slider musicVolumeSlider;
    [SerializeField] public AudioMixer musicMixer;

    [SerializeField] public Slider soundFXVolumeSlider;
    [SerializeField] public AudioMixer sfxMixer;

    public void Start ()
    {

        bool result = mainMixer.GetFloat ("Master", out var temp);

        if (result)
        {
            mainVolumeSlider.value = temp;
        }

        bool result2 = musicMixer.GetFloat ("MusicMaster", out var temp2);

        if (result2)
        {
            musicVolumeSlider.value = temp2;
        }

        bool result3 = sfxMixer.GetFloat ("FXMaster", out var temp3);

        if (result3)
        {
            soundFXVolumeSlider.value = temp3;
        }

    }

    public void SetMainVol ()
    {
        mainMixer.SetFloat ("Master", mainVolumeSlider.value);
    }

    public void SetMusicVol ()
    {
        musicMixer.SetFloat ("MusicMaster", musicVolumeSlider.value);
    }

    public void SetSFXVol ()
    {
        sfxMixer.SetFloat ("FXMaster", soundFXVolumeSlider.value);
    }

}