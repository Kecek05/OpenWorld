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
}
