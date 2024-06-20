using UnityEngine;

public class DeliveryMinigameSound : MonoBehaviour
{
    [SerializeField] private DeliveryMinigame deliveryMinigame;

    private void Start()
    {
        deliveryMinigame.OnStartedMinigame += DeliveryMinigame_OnStartedMinigame;
        deliveryMinigame.OnFinishedMinigame += DeliveryMinigame_OnFinishedMinigame;
    }

    private void DeliveryMinigame_OnFinishedMinigame()
    {
        if (SFXManager.Instance.GetAudioClipRefsSO().finishedMinigame != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().finishedMinigame, transform);
    }

    private void DeliveryMinigame_OnStartedMinigame()
    {
        if (SFXManager.Instance.GetAudioClipRefsSO().startMinigameRingBell != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().startMinigameRingBell, transform);
    }
}
