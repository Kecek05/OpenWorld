using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaulderonCounterVisual : MonoBehaviour
{


    [SerializeField] private CaulderonCounter caulderonCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        caulderonCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, CaulderonCounter.OnStateCHangedEventArgs e)
    {
        bool showVisual = e.state == CaulderonCounter.State.CookingIngredient || e.state == CaulderonCounter.State.CookedIngredient;
        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
