using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLightMachineGun : BaseEnemy
{

    [Header("== LightShooter ==")]
    [Space]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootingPoint;


    [Header("Attack Timer")]
    [SerializeField] private float attackDelay;

    protected override IEnumerator AttackState()
    {
        throw new System.NotImplementedException();
    }
}
