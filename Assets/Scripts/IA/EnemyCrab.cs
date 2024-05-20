using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrab : BaseEnemy
{
    protected override IEnumerator AttackState()
    {
        DestroySelf(); // Destruir o caranguejo ao colidir com o jogador
        yield break;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroySelf(); // Destruir o caranguejo ao colidir com o jogador
        }
    }
}
