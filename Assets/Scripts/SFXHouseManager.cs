using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHouseManager : MonoBehaviour
{
    public static SFXHouseManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    [SerializeField] private AudioSource sFXObject;

    [SerializeField] private float delayBetweenWalkingFootStepsSFX = 0.2f;

    [SerializeField] private float delayBetweenRunningFootStepsSFX = 0.15f;
    private IEnumerator playerWalkSFXCoroutine;
    private IEnumerator playerRunSFXCoroutine;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }


    private void Start()
    {
        DeliveryManager.Instance.OnRecipeWrong += Instance_OnRecipeWrong;
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        PlayerInHouse.InstancePlayerInHouse.OnPickedSomething += Player_OnPickedSomething;
        BasePlayer.OnPlayerWalking += WitchInputs_OnPlayerWalking;
        BasePlayer.OnPlayerRunning += WitchInputs_OnPlayerRunning;

    }

    private void Instance_OnRecipeWrong(object sender, System.EventArgs e)
    {
        Transform deliveryCounterPos = sender as Transform;
        PlayRandomSFXClip(GetAudioClipRefsSO().deliveryFail, deliveryCounterPos);
    }

    private void Instance_OnRecipeCompleted(object sender, DeliveryManager.OnRecipeCompletedEventArgs e)
    {
        Transform deliveryCounterPos = sender as Transform;
        PlayRandomSFXClip(GetAudioClipRefsSO().deliverySuccess, deliveryCounterPos);
    }

    private void WitchInputs_OnPlayerRunning()
    {
        if (playerRunSFXCoroutine == null)
        {
            //Isnt playing SFX
            playerRunSFXCoroutine = PlayerRunSFX();
            StartCoroutine(playerRunSFXCoroutine);
        }
    }

    private IEnumerator PlayerRunSFX()
    {
        PlayRandomSFXClip(audioClipRefsSO.footstep, BasePlayer.Instance.transform);
        yield return new WaitForSeconds(delayBetweenRunningFootStepsSFX);
        //Can play another SFX walking
        playerRunSFXCoroutine = null;
    }



    private void WitchInputs_OnPlayerWalking()
    {
        if(playerWalkSFXCoroutine == null)
        {
            //Isnt playing SFX
            playerWalkSFXCoroutine = PlayerMoveSFX();
            StartCoroutine(playerWalkSFXCoroutine);
        }
    }

    private IEnumerator PlayerMoveSFX()
    {
        PlayRandomSFXClip(audioClipRefsSO.footstep, BasePlayer.Instance.transform);
        yield return new WaitForSeconds(delayBetweenWalkingFootStepsSFX);
        //Can play another SFX walking
        playerWalkSFXCoroutine = null;
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlayRandomSFXClip(audioClipRefsSO.objectDrop, baseCounter.transform);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlayerInHouse player = sender as PlayerInHouse;
        PlayRandomSFXClip(audioClipRefsSO.objectPickup, player.transform);
    }

    public void PlayRandomSFXClip(AudioClip[] audioClips, Transform soundPos, float volume = 1f)
    {

        AudioSource audioSource = Instantiate(sFXObject, soundPos.position, Quaternion.identity);

        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }



    public AudioClipRefsSO GetAudioClipRefsSO() { return audioClipRefsSO; }
}
