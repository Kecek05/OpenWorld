
using UnityEngine;

public class PestleCounterVisual : MonoBehaviour
{

    [SerializeField] private PestleCounter pestleCounter;
    [SerializeField] private Animator pestleMoedorAnim;
    [SerializeField] private Animator pestlePoteAnim;

    [SerializeField] private ParticleSystem pestleFinishedParticle;

    [SerializeField] private ParticleSystem pestleParticle;
    [SerializeField] private ParticleSystemRenderer pestlParticleRender;

    private const string MOER = "moer";
    private const string MISS = "miss";

    private void Start()
    {
        pestleCounter.OnMoerRight += PestleCounter_OnMoerRight;
        pestleCounter.OnHitMissed += PestleCounter_OnHitMissed;
        pestleCounter.OnHitFinished += PestleCounter_OnHitFinished;
    }

    private void PestleCounter_OnMoerRight(object sender, PestleCounter.OnMoerRightEventArgs e)
    {
        pestleMoedorAnim.SetTrigger(MOER);
        pestlParticleRender.material = e.particleMaterial;
        pestleParticle.Play();
    }

    private void PestleCounter_OnHitFinished(object sender, System.EventArgs e)
    {
        pestleFinishedParticle.Play();
    }

    private void PestleCounter_OnHitMissed(object sender, IHasHitBar.OnHitMissedEventArgs e)
    {
        if(e.missed)
            pestlePoteAnim.SetTrigger(MISS);
    }


}
