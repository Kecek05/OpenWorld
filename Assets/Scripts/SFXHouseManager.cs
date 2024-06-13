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
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        PlayerInHouse.InstancePlayerInHouse.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;

        BasePlayer.OnPlayerWalking += WitchInputs_OnPlayerWalking;
        BasePlayer.OnPlayerRunning += WitchInputs_OnPlayerRunning;

    }


    private void WitchInputs_OnPlayerRunning()
    {
        if (playerRunSFXCoroutine == null)
        {
            //Isnt playing SFX
            playerRunSFXCoroutine = PlayerRunSFX();
            StartCoroutine(playerRunSFXCoroutine);
            Debug.Log("SFX RUN");
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
            Debug.Log("SFX MOVE");
        }
    }

    private IEnumerator PlayerMoveSFX()
    {
        PlayRandomSFXClip(audioClipRefsSO.footstep, BasePlayer.Instance.transform);
        yield return new WaitForSeconds(delayBetweenWalkingFootStepsSFX);
        //Can play another SFX walking
        playerWalkSFXCoroutine = null;
    }


    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;

        PlayRandomSFXClip(audioClipRefsSO.trash, trashCounter.transform);
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

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlayRandomSFXClip(audioClipRefsSO.chop, cuttingCounter.transform);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlayRandomSFXClip(audioClipRefsSO.deliveryFail, deliveryCounter.transform);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlayRandomSFXClip(audioClipRefsSO.deliverySuccess, deliveryCounter.transform);
    }


    public void PlayFootstepsSound(Vector3 position, float volume)
    {
   //     PlaySound(audioClipRefsSO.footstep, position, volume);
    }


    //new

    public void PlayRandomSFXClip(AudioClip[] audioClips, Transform soundPos, float volume = 1f)
    {

        AudioSource audioSource = Instantiate(sFXObject, soundPos.position, Quaternion.identity);

        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
