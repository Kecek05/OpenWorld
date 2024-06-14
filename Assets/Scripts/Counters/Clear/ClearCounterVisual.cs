
using UnityEngine;

public class ClearCounterVisual : MonoBehaviour
{

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private ParticleSystem itemMovedparticle;

    private void Start()
    {
        clearCounter.OnItemMoved += ClearCounter_OnItemMoved;
    }

    private void ClearCounter_OnItemMoved()
    {
        itemMovedparticle.Play();
    }
}
