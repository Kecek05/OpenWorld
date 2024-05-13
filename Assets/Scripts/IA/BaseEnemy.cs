using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour
{


    [SerializeField] private GameObject itemToDrop;
    private Transform target;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private int Health;
    [SerializeField] private float speed;
    [SerializeField] private float aggroRange;

    [Header("Timers")]
    private float patrolDelay;
    [SerializeField] private float patrolMaxDelay;
    protected enum State
    {
        IDLE,
        FOLLOW,
        WAITING,
        PATROL,
        ATTACK
    }
    protected State state;

    protected virtual void Start()
    {
        agent.speed = speed;
        patrolDelay = patrolMaxDelay;
        UpdateState(State.IDLE);
    }

    protected virtual void FixedUpdate()
    {
        switch (state)
        {
            case State.WAITING:
                if (patrolDelay > 0)
                {
                    patrolDelay -= Time.deltaTime;
                } else if (patrolDelay <= 0)
                {
                    patrolDelay = patrolMaxDelay;
                    UpdateState(State.PATROL);
                }
                break;
        }

    }


    protected void UpdateState(State newState)
    {

            state = newState;
            switch (state)
            {
                case State.IDLE:
                    IdleState();
                    break;
                case State.WAITING:
                    WaitingState();
                    break;
                case State.FOLLOW:
                    FollowState();
                    break;
                case State.PATROL:
                    PatrolState();
                    break;
                case State.ATTACK:
                    AttackState();
                    break;
            }

        
        Debug.Log(state);
    }

    protected virtual void IdleState()
    {
        //Fica parado
        if (CheckDistanceToPlayer())
        {
            print("IN RANGEE");
            SetTarget(PlayerOpenWorld.main.transform);
            //Seguir player
            UpdateState(State.FOLLOW);
        }
        else
        {
            SetTarget(null);
            //timer para chamar o PatrolState
            UpdateState(State.WAITING);
        }

    }

    protected virtual void FollowState()
    {
        //Seguir o player, Se estiver no range de ataque, ataque!
        while (state == State.FOLLOW && GetTarget() != null)
        {
            GetNavMesh().SetDestination(GetTarget().position);

            float distancia = Vector3.Distance(GetTarget().position, transform.position);
        } 
        //out while
        UpdateState(State.IDLE);

    }

    protected virtual void PatrolState()
    {
        print("aptrol");
        if(CheckDistanceToPlayer())
        {
            SetTarget(PlayerOpenWorld.main.transform);
            UpdateState(State.FOLLOW);

        }
        else
        {
            RandomPlacesToGO();
            UpdateState(State.WAITING);

        }



    }

    protected virtual void WaitingState()
    {
        if (CheckDistanceToPlayer())
        {
            SetTarget(PlayerOpenWorld.main.transform);

            UpdateState(State.FOLLOW);
        }
    }

    protected abstract void AttackState();
    

    private void RandomPlacesToGO()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }

    private bool CheckDistanceToPlayer()
    {
        float distanciaToPlayer = Vector3.Distance(PlayerOpenWorld.main.transform.position, transform.position);
        if (distanciaToPlayer <= aggroRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    protected float GetAggroRange() { return aggroRange; }

    protected Transform GetTarget() { return target; }

    protected void SetTarget(Transform _target) { target = _target; }

    protected NavMeshAgent GetNavMesh() { return agent; }


    protected int GetHealth() { return Health; }

    protected void SetHealth(int _health) { Health = _health; }


    protected float GetPatrolDelay() { return patrolDelay; }

    protected void SetPatrolDelay(float _patrolDelay) {  patrolDelay = _patrolDelay; }

    protected float GetPatrolDelayMax() { return patrolMaxDelay; }

    protected float GetSpeed() { return speed; }
}
