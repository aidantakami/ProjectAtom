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