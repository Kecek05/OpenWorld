
using UnityEngine;

public class ItemOnGroundSound : MonoBehaviour
{
    [SerializeField] private ItemOnGround itemOnGround;
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private void Start()
    {
        itemOnGround.OnItemClicked += ItemOnGround_OnItemClicked;
        itemOnGround.OnItemCollected += ItemOnGround_OnItemCollected;
    }

    private void ItemOnGround_OnItemCollected()
    {
        if(audioClipRefsSO.completeCollect != null)
            SFXManager.Instance.PlayRandomSFXClip(audioClipRefsSO.completeCollect, transform);
    }

    private void ItemOnGround_OnItemClicked()
    {
        if (audioClipRefsSO.clickCollect != null)
            SFXManager.Instance.PlayRandomSFXClip(audioClipRefsSO.clickCollect, transform);
    }
}
