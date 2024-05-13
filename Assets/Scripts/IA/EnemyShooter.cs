using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : BaseEnemy
{
    [Header( "== Shooter ==")]
    [Space]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float shootRange;

    [Header("Attack Timer")]
    private float attackDelay = 0f ;
    [SerializeField] private float attackMaxDelay;

    protected override void Start()
    {
        attackDelay = attackMaxDelay;
        GetNavMesh().speed = GetSpeed();
       UpdateState(State.IDLE);
    }

    protected override void FixedUpdate()
    {
        switch (state)
        {
            case State.ATTACK:
                if(attackDelay > 0)
                    attackDelay -= Time.deltaTime;
                break;
            case State.WAITING:
                if (GetPatrolDelay() > 0)
                {
                    SetPatrolDelay(GetPatrolDelay() - Time.deltaTime);
                }
                else if (GetPatrolDelay() <= 0)
                {
                    SetPatrolDelay(GetPatrolDelayMax());
                    UpdateState(State.PATROL);
                }
                break;
        }

    }



    protected override void FollowState()
    {
        //Seguir o player, Se estiver no range de ataque, ataque!
        while (state == State.FOLLOW && GetTarget() != null)
        {

            GetNavMesh().SetDestination(GetTarget().position);

            float distancia = Vector3.Distance(GetTarget().position, transform.position);

            if (distancia <= shootRange)
            {
               // Pode atirar no player
                UpdateState(State.ATTACK);
            }
        }
        //out while
        UpdateState(State.IDLE);

    }




    protected override void AttackState()
    {
        if(attackDelay <= 0)
        {
            Vector3 spawnPos = shootingPoint.position;

            Instantiate(bullet, spawnPos, Quaternion.identity);


            int selfDamage = 1;
            SetHealth(GetHealth() - selfDamage);
            if(GetHealth() <= 0)
            {
                //robo morre
                Destroy(gameObject);
            }
            attackDelay = attackMaxDelay;
        }

    }




}
