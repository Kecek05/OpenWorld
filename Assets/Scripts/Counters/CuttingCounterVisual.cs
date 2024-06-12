using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "cut";

    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private ParticleSystem finishedCuttingParticle;
    [SerializeField] private ParticleSystem cutParticle;
    [SerializeField] private ParticleSystemRenderer cutParticleRender;

    [SerializeField] private Animator animator;

    private void Start()
    {
        cuttingCounter.OnCutParticle += CuttingCounter_OnCutParticle;
        cuttingCounter.OnCutFinished += CuttingCounter_OnCutFinished;
    }

    private void CuttingCounter_OnCutParticle(object sender, CuttingCounter.OnCutParticleEventArgs e)
    {
        animator.SetTrigger(CUT);
        cutParticleRender.material = e.particleMaterial;
        cutParticle.Play();
    }

    private void CuttingCounter_OnCutFinished(object sender, System.EventArgs e)
    {
        finishedCuttingParticle.Play();
    }
}
