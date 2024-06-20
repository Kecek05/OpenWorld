
using UnityEngine;

public class ItemOnGroundSound : MonoBehaviour
{
    [SerializeField] private ItemOnGround itemOnGround;
    private void Start()
    {
        itemOnGround.OnItemClicked += ItemOnGround_OnItemClicked;
        itemOnGround.OnItemCollected += ItemOnGround_OnItemCollected;
    }

    private void ItemOnGround_OnItemCollected()
    {
        if (SFXManager.Instance.GetAudioClipRefsSO().completeCollect != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().completeCollect, transform);
    }

    private void ItemOnGround_OnItemClicked()
    {
        if (SFXManager.Instance.GetAudioClipRefsSO().interact != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().interact, transform);
    }
}
