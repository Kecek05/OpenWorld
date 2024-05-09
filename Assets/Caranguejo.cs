using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caranguejo : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruir o caranguejo ao colidir com o jogador
        }
    }
}
