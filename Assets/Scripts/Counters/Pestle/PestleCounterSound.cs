
using UnityEngine;

public class PestleCounterSound : MonoBehaviour
{
    [SerializeField] private PestleCounter pestleCounter;


    private void Start()
    {
        pestleCounter.OnMoerRight += PestleCounter_OnMoerRight;
        pestleCounter.OnHitMissed += PestleCounter_OnHitMissed;
    }

    private void PestleCounter_OnHitMissed(object sender, IHasHitBar.OnHitMissedEventArgs e)
    {
        if(e.missed && SFXManager.Instance.GetAudioClipRefsSO().pestleFail != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().pestleFail, transform);
    }

    private void PestleCounter_OnMoerRight(object sender, PestleCounter.OnMoerRightEventArgs e)
    {
        if(SFXManager.Instance.GetAudioClipRefsSO() != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().pestleSuccess, transform);
    }
}
