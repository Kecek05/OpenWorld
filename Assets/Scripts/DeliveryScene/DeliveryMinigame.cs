using UnityEngine;

public class DeliveryMinigame : MonoBehaviour
{

    [SerializeField] private GameInput gameInput;

    private void Start()
    {
        gameInput.OnHit1Performed += GameInput_OnHit1Performed;
        gameInput.OnHit2Performed += GameInput_OnHit2Performed;
        gameInput.OnHit3Performed += GameInput_OnHit3Performed;
        gameInput.OnHit4Performed += GameInput_OnHit4Performed;
    }

    private void OnEnable()
    {
        gameInput.ChangePLayerInputHitMinigame(true);
    }

    private void OnDisable()
    {
        gameInput.ChangePLayerInputHitMinigame(false);
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
