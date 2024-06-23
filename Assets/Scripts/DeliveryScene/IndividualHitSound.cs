
using UnityEngine;

public class IndividualHitSound : MonoBehaviour
{


    [SerializeField] private IndividualHit individualHit;


    private void Start()
    {
        individualHit.OnHitPerfect += IndividualHit_OnHitPerfect;
        individualHit.OnHitGood += IndividualHit_OnHitGood;
        individualHit.OnHitBad += IndividualHit_OnHitBad;
        individualHit.OnHitMissed += IndividualHit_OnHitMissed;
    }

    private void IndividualHit_OnHitPerfect()
    {
        if (SFXManager.Instance.GetAudioClipRefsSO().knockDoorPerfect != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().knockDoorPerfect, Camera.main.transform);
    }


    private void IndividualHit_OnHitGood()
    {
        if (SFXManager.Instance.GetAudioClipRefsSO().knockDoorGood != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().knockDoorGood, Camera.main.transform);
    }

    private void IndividualHit_OnHitBad()
    {
        if (SFXManager.Instance.GetAudioClipRefsSO().badClick != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().badClick, Camera.main.transform);
    }

    private void IndividualHit_OnHitMissed()
    {
        if (SFXManager.Instance.GetAudioClipRefsSO().missedClick != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().missedClick, Camera.main.transform);
    }
}
