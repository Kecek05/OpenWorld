
using UnityEngine;

public class DepositeItemBoxSound : MonoBehaviour
{
    [SerializeField] private DepositeItemBox depositeItemBox;

    private void Start()
    {
        depositeItemBox.OnItemStashed += DepositeItemBox_OnItemStashed;
    }

    private void DepositeItemBox_OnItemStashed()
    {
        if(SFXManager.Instance.GetAudioClipRefsSO().interact != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().stashItem, transform);
    }
}
