
using UnityEngine;

public class CuttingCounterSound : MonoBehaviour
{

    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, CuttingCounter.OnCutWithSOEventArgs e)
    {
        if(e.kitchenObjectSO.interactSFX != null)
            SFXManager.Instance.PlayRandomSFXClip(e.kitchenObjectSO.interactSFX, transform);
    }
}
