using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounterVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem trashParticle;

    private void Start()
    {
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        trashParticle.Play();
    }
}
