
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
        if(e.missed)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().potionWrong, transform);
    }

    private void PestleCounter_OnMoerRight(object sender, PestleCounter.OnMoerRightEventArgs e)
    {
        SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().potionSuccess, transform);
    }
}
