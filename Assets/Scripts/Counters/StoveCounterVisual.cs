using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{


    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;   
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateCHangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.CookingIngredient || e.state == StoveCounter.State.CookedIngredient;
        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
