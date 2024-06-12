
using UnityEngine;

public class PestleCounterVisual : MonoBehaviour
{

    [SerializeField] private PestleCounter pestleCounter;
    [SerializeField] private Animator pestleAnim;

    private const string MOER = "moer";

    private void Start()
    {
        pestleCounter.OnMoerRight += PestleCounter_OnMoerRight;
    }

    private void PestleCounter_OnMoerRight(object sender, System.EventArgs e)
    {
        pestleAnim.SetTrigger(MOER);
    }
}
