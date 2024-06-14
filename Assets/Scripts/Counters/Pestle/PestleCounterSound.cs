
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
            SFXHouseManager.Instance.PlayRandomSFXClip(SFXHouseManager.Instance.GetAudioClipRefsSO().deliveryFail, transform);
    }

    private void PestleCounter_OnMoerRight(object sender, PestleCounter.OnMoerRightEventArgs e)
    {
        SFXHouseManager.Instance.PlayRandomSFXClip(SFXHouseManager.Instance.GetAudioClipRefsSO().deliverySuccess, transform);
    }
}
