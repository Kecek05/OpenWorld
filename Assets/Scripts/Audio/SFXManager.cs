using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }
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


    private void OnEnable()
    {
        //House
        if(DeliveryManager.Instance !=null)
        {
            DeliveryManager.Instance.OnRecipeWrong += Instance_OnRecipeWrong;
            DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        }
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        if(PlayerInHouse.InstancePlayerInHouse !=null) 
            PlayerInHouse.InstancePlayerInHouse.OnPickedSomething += Player_OnPickedSomething;

        //Geral
        BasePlayer.OnPlayerWalking += WitchInputs_OnPlayerWalking;
        BasePlayer.OnPlayerRunning += WitchInputs_OnPlayerRunning;

    }

    private void OnDisable()
    {
        //House
        if (DeliveryManager.Instance != null)
        {
            DeliveryManager.Instance.OnRecipeWrong -= Instance_OnRecipeWrong;
            DeliveryManager.Instance.OnRecipeCompleted -= Instance_OnRecipeCompleted;
        }
        BaseCounter.OnAnyObjectPlacedHere -= BaseCounter_OnAnyObjectPlacedHere;
        if (PlayerInHouse.InstancePlayerInHouse != null)
            PlayerInHouse.InstancePlayerInHouse.OnPickedSomething -= Player_OnPickedSomething;

        //Geral
        BasePlayer.OnPlayerWalking -= WitchInputs_OnPlayerWalking;
        BasePlayer.OnPlayerRunning -= WitchInputs_OnPlayerRunning;
    }

    private void Instance_OnRecipeWrong(object sender, System.EventArgs e)
    {
        BaseCounter deliveryCounterPos = sender as BaseCounter;
        if (GetAudioClipRefsSO().potionWrong != null && deliveryCounterPos != null)
        {
            PlayRandomSFXClip(GetAudioClipRefsSO().potionWrong, deliveryCounterPos.transform);
        }
    }

    private void Instance_OnRecipeCompleted(object sender, DeliveryManager.OnRecipeCompletedEventArgs e)
    {
        BaseCounter deliveryCounterPos = sender as BaseCounter;
        if(GetAudioClipRefsSO().potionSuccess != null && deliveryCounterPos != null)
        {
            PlayRandomSFXClip(GetAudioClipRefsSO().potionSuccess, deliveryCounterPos.transform);
        }
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
        if (GetAudioClipRefsSO().footstepOutSide != null)
        {

            PlayRandomSFXClip(GetAudioClipRefsSO().footstepOutSide, BasePlayer.Instance.transform);
            yield return new WaitForSeconds(delayBetweenRunningFootStepsSFX);
            //Can play another SFX walking
            playerRunSFXCoroutine = null;
        }
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
        if(PlayerInHouse.InstancePlayerInHouse != null)
        {
            // In house
            if (GetAudioClipRefsSO().footstepHouse != null)
            {
                PlayRandomSFXClip(GetAudioClipRefsSO().footstepHouse, BasePlayer.Instance.transform);
                yield return new WaitForSeconds(delayBetweenWalkingFootStepsSFX);
                //Can play another SFX walking
                playerWalkSFXCoroutine = null;

            }
        } else
        {
            //OutSide
            if (GetAudioClipRefsSO().footstepOutSide != null)
            {
                PlayRandomSFXClip(GetAudioClipRefsSO().footstepOutSide, BasePlayer.Instance.transform);
                yield return new WaitForSeconds(delayBetweenWalkingFootStepsSFX);
                //Can play another SFX walking
                playerWalkSFXCoroutine = null;

            }
        }
        
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        if (GetAudioClipRefsSO().kitchenObjDrop != null && baseCounter != null)
        {
            PlayRandomSFXClip(audioClipRefsSO.kitchenObjDrop, baseCounter.transform);

        }
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlayerInHouse player = sender as PlayerInHouse;
        if (GetAudioClipRefsSO().kitchenObjPickup != null && player != null)
        {
            PlayRandomSFXClip(audioClipRefsSO.kitchenObjPickup, player.transform);
        }
    }

    public void PlayRandomSFXClip(AudioClip[] audioClips, Transform soundPos, float volume = 1f)
    {
        if(audioClips != null)
        {

            //There is a clip to play
            AudioSource audioSource = Instantiate(sFXObject, soundPos.position, Quaternion.identity);

            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];

            audioSource.volume = volume;

            audioSource.Play();

            float clipLength = audioSource.clip.length;

            Destroy(audioSource.gameObject, clipLength);
        }

    }



    public AudioClipRefsSO GetAudioClipRefsSO() { return audioClipRefsSO; }
}
