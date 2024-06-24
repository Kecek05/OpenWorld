using UnityEngine.Audio;
using UnityEngine;

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
        else
        {
            Destroy(gameObject);
        }

        mainAudioMixer.SetFloat("SoundFXVolume", PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME));

        mainAudioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME));

        mainAudioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat(PLAYER_PREFS_MASTER_VOLUME));
    }

    public void SetMasterVolume(float volume)
    {
        mainAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f); // math to compensate the volume curve

        PlayerPrefs.SetFloat(PLAYER_PREFS_MASTER_VOLUME, Mathf.Log10(volume) * 20f);
        PlayerPrefs.Save();
    }

    public void SetSoundFXVolume(float volume)
    {
        mainAudioMixer.SetFloat("SoundFXVolume", Mathf.Log10(volume) * 20f);

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, Mathf.Log10(volume) * 20f);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        mainAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, Mathf.Log10(volume) * 20f);
        PlayerPrefs.Save();
    }
}
