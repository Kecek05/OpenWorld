using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioMixerManager : MonoBehaviour
{
    public static AudioMixerManager instance;
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    private const string PLAYER_PREFS_MUSIC_VOLUME = "SoundEffectsVolume";
    private const string PLAYER_PREFS_MASTER_VOLUME = "SoundEffectsVolume";

    [SerializeField] private AudioMixer mainAudioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        float volumeMain = PlayerPrefs.GetFloat(PLAYER_PREFS_MASTER_VOLUME);
        SetMasterVolume(volumeMain);

        float volumeSFX = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME);
        SetSoundFXVolume(volumeSFX);

        float volumeMusic = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME);
        SetMusicVolume(volumeMain);

    }

    public void SetMasterVolume(float volume)
    {
        mainAudioMixer.SetFloat("MasterVolume", volume); 

        PlayerPrefs.SetFloat(PLAYER_PREFS_MASTER_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public void SetSoundFXVolume(float volume)
    {
        mainAudioMixer.SetFloat("SoundFXVolume", volume);

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        Debug.Log(PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME));
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        mainAudioMixer.SetFloat("MusicVolume", volume);

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
}
