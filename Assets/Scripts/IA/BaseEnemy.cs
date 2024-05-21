using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float attackRange;

    private float timeCheckDistanceToPlayer;
    private float maxTimeCheckDistanceToPlayer = 0.3f;

    [SerializeField] private Vector3 offsetFollow;
    [SerializeField] private float patrolDelay;
    public Rigidbody rbPlayer;

    protected IEnumerator currentCoroutine;
    protected enum State
    {
        IDLE,
        FOLLOW,
        PATROL,
        ATTACK
    }
    protected State state;

    protected EnemyAnimatorController enemyAnimationController;

    protected void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        rbPlayer = player.GetComponent<Rigidbody>();
        if(rbPlayer == null )
            rbPlayer = player.transform.parent.GetComponent<Rigidbody>();
        timeCheckDistanceToPlayer = maxTimeCheckDistanceToPlayer;
        agent.speed = speed;
        //  UpdateState(State.IDLE);
        StartCoroutine(IdleState());
    }

    protected virtual void Awake()
    {
        enemyAnimationController = GetComponent<EnemyAnimatorController>();
    }

    protected void Update()
    {


        //Delay to check
        if(timeCheckDistanceToPlayer > 0)
        {
            timeCheckDistanceToPlayer -= Time.deltaTime;
        } else if(timeCheckDistanceToPlayer <= 0)
        {
            timeCheckDistanceToPlayer = maxTimeCheckDistanceToPlayer;
            CheckDistanceToPlayer();
        }


    }
    protected void UpdateState(State newState)
    {
        if(newState != state)
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            state = newState;
            switch (state)
            {
                case State.IDLE:
                    currentCoroutine = IdleState();
                    break;
                case State.FOLLOW:
                    currentCoroutine = FollowState();
                    break;
                case State.PATROL:
                    currentCoroutine = PatrolState();
                    break;
                case State.ATTACK:
                    currentCoroutine = AttackState();
                    break;
            }
            StartCoroutine(currentCoroutine);
        }

    }

    protected IEnumerator IdleState()
    {
        //stay in the place
        while (state == State.IDLE && GetTarget() == null)
        {
            
                yield return new WaitForSeconds(1f);
                UpdateState(State.PATROL);
            
        }

    }

    protected IEnumerator PatrolState()
    {
        while (state == State.PATROL && GetTarget() == null)
        {
            if (GetTarget() != null)
                break;
            //Player not In range
            RandomPlacesToGO();
            if(enemyAnimationController != null)
            {
                enemyAnimationController.SetSpeed(agent.velocity.magnitude);
                enemyAnimationController.SetTurn(Vector3.Dot(agent.velocity.normalized, transform.forward));

            }
            yield return new WaitForSeconds(patrolDelay);
            UpdateState(State.IDLE);





        }
    }



    protected IEnumerator FollowState()
    {
        //Seguir o player, Se estiver no range de ataque, ataque!
        while (state == State.FOLLOW && GetTarget() != null)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 result = GetTarget().transform.position + offsetFollow;

            if(enemyAnimationController != null)
            {
                enemyAnimationController.SetSpeed(agent.velocity.magnitude);
                enemyAnimationController.SetTurn(Vector3.Dot(agent.velocity.normalized, transform.forward));

            }

            GetNavMesh().SetDestination(result);

            float distanciaToPlayer = Vector3.Distance(PlayerOpenWorld.main.transform.position, transform.position);

            if (distanciaToPlayer <= attackRange)
            {
                UpdateState(State.ATTACK);
                break;
            }


        }


    }



    protected abstract IEnumerator AttackState();

    private void RandomPlacesToGO()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }

    private void CheckDistanceToPlayer()
    {
        float distanciaToPlayer = Vector3.Distance(PlayerOpenWorld.main.transform.position, transform.position);
        if (distanciaToPlayer <= aggroRange)
        {
            SetTarget(PlayerOpenWorld.main.transform);
            UpdateState(State.FOLLOW);
        }
        else
        {
            SetTarget(null);
        }
    }

    protected void DestroySelf()
    {
        print("spawn Coin");
        Instantiate(itemToDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected float GetAggroRange() { return aggroRange; }

    protected Transform GetTarget() { return target; }

    protected void SetTarget(Transform _target) { target = _target; }

    protected NavMeshAgent GetNavMesh() { return agent; }


    protected int GetHealth() { return Health; }

    protected void SetHealth(int _health) { Health = _health; }

    protected float GetSpeed() { return speed; }
}
