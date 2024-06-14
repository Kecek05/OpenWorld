
using UnityEngine;

public class TrashCounterSound : MonoBehaviour
{

    [SerializeField] private TrashCounter trashCounter;

    private void Start()
    {
        trashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().trash, trashCounter.transform);

    }
}
