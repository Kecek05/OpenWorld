
using UnityEngine;

public class PestleCounterVisual : MonoBehaviour
{

    [SerializeField] private PestleCounter pestleCounter;
    [SerializeField] private Animator pestleMoedorAnim;
    [SerializeField] private Animator pestlePoteAnim;


    private const string MOER = "moer";
    private const string MISS = "miss";

    private void Start()
    {
        pestleCounter.OnMoerRight += PestleCounter_OnMoerRight;
        pestleCounter.OnMoerMiss += PestleCounter_OnMoerMiss;
    }

    private void PestleCounter_OnMoerMiss(object sender, System.EventArgs e)
    {
        pestlePoteAnim.SetTrigger(MISS);
    }

    private void PestleCounter_OnMoerRight(object sender, System.EventArgs e)
    {
        pestleMoedorAnim.SetTrigger(MOER);
    }
}
