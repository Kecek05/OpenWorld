using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaulderonCounterSound : MonoBehaviour
{

    [SerializeField] private CaulderonCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, CaulderonCounter.OnStateCHangedEventArgs e)
    {
        bool playSound = e.state == CaulderonCounter.State.CookingIngredient || e.state == CaulderonCounter.State.CookedIngredient;
        if (playSound)
        {
            audioSource.Play();
        } else
        {
            audioSource.Pause();
        }
    }
}
