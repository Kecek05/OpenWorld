using UnityEngine.Audio;
using UnityEngine;

public class AudioMixerManager : MonoBehaviour
{

    [SerializeField] private AudioMixer mainAudioMixer;


    public void SetMasterVolume(float volume)
    {
        mainAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f); // math to compensate the volume curve
    }

    public void SetSoundFXVolume(float volume)
    {
        mainAudioMixer.SetFloat("SoundFXVolume", Mathf.Log10(volume) * 20f);
    }

    public void SetMusicVolume(float volume)
    {
        mainAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
    }
}
