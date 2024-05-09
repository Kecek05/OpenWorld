using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    public float freezeDuration = 3f; // tempo congelado
    private bool isFrozen = false;
    private float freezeTimer = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FreezePlayer();
        }
    }

    private void FreezePlayer()
    {
        isFrozen = true;
        freezeTimer = freezeDuration;
        Debug.Log("congelo");
        Destroy(gameObject);

    }

    private void Update()
    {
        if (isFrozen)
        {
            // contagem regressiva do tempo de congelamento
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                isFrozen = false;
            }
        }
    }
}