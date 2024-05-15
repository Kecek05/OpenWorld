using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooter : BaseEnemy
{
    [Header( "== Shooter ==")]
    [Space]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootingPoint;


    [Header("Attack Timer")]
    [SerializeField] private float attackDelay;


    protected override IEnumerator AttackState()
    {
        while (state == State.ATTACK && GetTarget() != null)
        {

            Vector3 spawnPos = shootingPoint.position;

            Instantiate(bullet, spawnPos, Quaternion.identity);


            int selfDamage = 1;

            SetHealth(GetHealth() - selfDamage);
            if (GetHealth() <= 0)
            {
                //robo morre
                DestroySelf();
            }
            
            yield return new WaitForSeconds(attackDelay);
           // UpdateState(State.FOLLOW); // volta pro follow para seguir o player

        }


    }




}
