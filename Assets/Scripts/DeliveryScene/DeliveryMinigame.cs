using UnityEngine;

public class DeliveryMinigame : MonoBehaviour
{

    private void Start()
    {
        WitchInputs.Instance.OnHit1Performed += GameInput_OnHit1Performed;
        WitchInputs.Instance.OnHit2Performed += GameInput_OnHit2Performed;
        WitchInputs.Instance.OnHit3Performed += GameInput_OnHit3Performed;
        WitchInputs.Instance.OnHit4Performed += GameInput_OnHit4Performed;
    }

    private void OnEnable()
    {
        WitchInputs.Instance.ChangePLayerInputHitMinigame(true);
    }

    private void OnDisable()
    {
        WitchInputs.Instance.ChangePLayerInputHitMinigame(false);
    }

    private void GameInput_OnHit4Performed(object sender, System.EventArgs e)
    {

    }

    private void GameInput_OnHit3Performed(object sender, System.EventArgs e)
    {

    }

    private void GameInput_OnHit2Performed(object sender, System.EventArgs e)
    {

    }

    private void GameInput_OnHit1Performed(object sender, System.EventArgs e)
    {

    }
}
