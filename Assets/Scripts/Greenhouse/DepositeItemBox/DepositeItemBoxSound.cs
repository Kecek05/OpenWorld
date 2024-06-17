
using UnityEngine;

public class DepositeItemBoxSound : MonoBehaviour
{
    [SerializeField] private DepositeItemBox depositeItemBox;
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Start()
    {
        depositeItemBox.OnItemStashed += DepositeItemBox_OnItemStashed;
    }

    private void DepositeItemBox_OnItemStashed()
    {
        if(audioClipRefsSO.stashItemInBox != null)
            SFXManager.Instance.PlayRandomSFXClip(audioClipRefsSO.stashItemInBox, transform);
    }
}
