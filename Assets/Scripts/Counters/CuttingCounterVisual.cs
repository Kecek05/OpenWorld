using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "cut";

    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private ParticleSystem finishedCuttingParticle;
    [SerializeField] private ParticleSystem pickupItem;

    [SerializeField] private Animator animator;

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
        cuttingCounter.OnCutFinished += CuttingCounter_OnCutFinished;
    }



    private void CuttingCounter_OnCutFinished(object sender, System.EventArgs e)
    {
        finishedCuttingParticle.Play();
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
