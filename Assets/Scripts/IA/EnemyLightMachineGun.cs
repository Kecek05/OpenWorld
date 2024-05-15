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

    [SerializeField] private float dieTime;
    private bool startDie;

    protected override IEnumerator AttackState()
    {
        startDie = true;
        while (state == State.ATTACK && GetTarget() != null)
        {

            Vector3 spawnPos = shootingPoint.position;

            Instantiate(bullet, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(attackDelay);

        }
    }

    private void FixedUpdate()
    {
        if (startDie)
        {
            if(dieTime > 0)
            {
                dieTime -= Time.deltaTime;
                if(dieTime <= 0)
                {
                    DestroySelf();
                }
            }

        }
    }
}
