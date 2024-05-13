using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : BaseEnemy
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float hitRange;

    private float patrolTimer;
    private float patrolMaxTimer;

    private float attackDelay = 0f ;
    [SerializeField] private float attackMaxDelay;
    private void Start()
    {
        state = State.IDLE;

    }

    private void Update()
    {
        UpdateState();
    }


    protected override void FollowState()
    {
        //Seguir o player, Se estiver no range de ataque, ataque!
        if(GetTarget() != null)
        {
            GetNavMesh().SetDestination(GetTarget().position);

            float distancia = Vector3.Distance(GetTarget().position, transform.position);

            if (distancia <= hitRange)
            {
                //Pode atirar no player
                state = State.ATTACK;
            }
        }
    }

    private void FixedUpdate()
    {
        if(state == State.ATTACK && attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
        }
    }


    protected override void AttackState()
    {
        if(attackDelay <= 0)
        {
            Vector3 spawnPos = shootingPoint.position;

            Instantiate(bullet, spawnPos, Quaternion.identity);


            int selfDamage = 1;
            SetHealth(GetHealth() - selfDamage);
            Debug.Log("HEALTH " + GetHealth());
            if(GetHealth() <= 0)
            {
                //robo morre
                Destroy(gameObject);
            }
            attackDelay = attackMaxDelay;
        }

    }

    protected override void IdleState()
    {
        //Fica parado
        

        if(CheckDistanceToPlayer())
        {
            SetTarget(PlayerOpenWorld.main.transform);
            //Seguir player
            state = State.FOLLOW;
        }
        else
        {
            SetTarget(null);
            //timer para chamar o PatrolState
            state = State.PATROL;
        }
    }

    protected override void PatrolState()
    {
        RandomPlacesToGO();
        state = State.IDLE;
    }

    private bool CheckDistanceToPlayer()
    {
        float distanciaToPlayer = Vector3.Distance(PlayerOpenWorld.main.transform.position, transform.position);
        if (distanciaToPlayer <= GetAggroRange())
        {
            return true;
        } else
        {
            return false;
        }
    }
}
