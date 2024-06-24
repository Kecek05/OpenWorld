using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderMusic;



    private void OnEnable()
    {
        if (AudioMixerManager.instance != null)
        {
            sliderMaster.onValueChanged.AddListener(value => { AudioMixerManager.instance.SetMasterVolume(value); });
            sliderSFX.onValueChanged.AddListener(value => { AudioMixerManager.instance.SetSoundFXVolume(value); });
            sliderMusic.onValueChanged.AddListener(value => { AudioMixerManager.instance.SetMusicVolume(value); });
        }
    }
}
