using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
{

    [SerializeField] private GameObject itemToDrop;
    private Transform target;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private int Health;
    [SerializeField] private float speed;
    [SerializeField] private float aggroRange;
    [SerializeField] private float attackRange;

    [SerializeField] private Vector3 offsetFollow;
    [SerializeField] private float patrolDelay;

    protected IEnumerator currentCoroutine;

    private int comboCount;

    [Header("== Shooter ==")]
    [Space]
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform shootingPoint2;
    [SerializeField] private Transform spawnPoint;


    [Header("Attack Timer")]
    [SerializeField] private float attackDelay;




    [SerializeField] private Rigidbody rb;

    protected enum State
    {
        IDLE,
        FOLLOW,
        PATROL,
        ATTACK1,
        ATTACK2,
        ATTACK3,
        ATTACK4,
        SUMMON,

    }
    protected State state;

    protected EnemyAnimatorController enemyAnimationController;

    protected void Start()
    {

        agent.speed = speed;
          UpdateState(State.IDLE);
        //currentCoroutine = IdleState();
        //StartCoroutine(currentCoroutine);
    }

    protected virtual void Awake()
    {
        enemyAnimationController = GetComponent<EnemyAnimatorController>();
    }

    protected void Update()
    {

        float distanciaToPlayer = Vector3.Distance(PlayerOpenWorld.main.transform.position, transform.position);
        if (distanciaToPlayer <= aggroRange && target == null)
        {
            SetTarget(PlayerOpenWorld.main.transform);
            //if(state == State.IDLE || state == State.PATROL) // so muda de state se nao estiver atacando
            UpdateState(State.FOLLOW);
        }
        else if (distanciaToPlayer > aggroRange)
        {
            SetTarget(null);
        }


        Debug.Log(state);
    }
    protected void UpdateState(State newState)
    {
        if (newState != state)
        {
 
            if(currentCoroutine != null)
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
                case State.ATTACK1:
                    currentCoroutine = AttackState1();
                    break;
                case State.ATTACK2:
                    currentCoroutine = AttackState2();
                    break;
                case State.ATTACK3:
                    currentCoroutine = AttackState3();
                    break;
                case State.ATTACK4:
                    currentCoroutine = AttackState4();
                    break;
                case State.SUMMON:
                    currentCoroutine = SpawnState();
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
        //andar aleatoriamente
        while (state == State.PATROL && GetTarget() == null)
        {
            if (GetTarget() != null)
                break;
            //Player not In range
            RandomPlacesToGO();
            enemyAnimationController.SetSpeed(agent.velocity.magnitude);
            enemyAnimationController.SetTurn(Vector3.Dot(agent.velocity.normalized, transform.forward));
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

            enemyAnimationController.SetSpeed(agent.velocity.magnitude);
            enemyAnimationController.SetTurn(Vector3.Dot(agent.velocity.normalized, transform.forward));

            GetNavMesh().SetDestination(result);

            float distanciaToPlayer = Vector3.Distance(PlayerOpenWorld.main.transform.position, transform.position);

            if (distanciaToPlayer <= attackRange)
            {
                UpdateState(State.ATTACK1);
                break;
            }
           

        }


    }

    private void RandomPlacesToGO()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
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










    protected IEnumerator AttackState1()
    {
        float startTime = Time.time;

        while (state == State.ATTACK1 && GetTarget() != null)
        {
            yield return new WaitForSeconds(0.1f);

            Vector3 spawnPos = shootingPoint.position;

            Instantiate(bullet, spawnPos, Quaternion.identity);


            //int selfDamage = 1;

            //SetHealth(GetHealth() - selfDamage);
            //if (GetHealth() <= 0)
            //{
            //    //robo morre
            //    DestroySelf();
            //}


            if(Time.time - startTime > 5f)
                UpdateState(State.ATTACK2);


        }



    }



    protected IEnumerator AttackState2()
    {

        //dash
        rb.velocity = GetDashDirection() * 15f;


        yield return new WaitForSeconds(attackDelay);
        rb.velocity = Vector3.zero;
        UpdateState(State.ATTACK3);
    }

    protected IEnumerator AttackState3()
    {
        yield return new WaitForSeconds(attackDelay);

        UpdateState(State.SUMMON);
    }



    protected IEnumerator SpawnState()
    {
        Vector3 spawnPos = spawnPoint.position;
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            //instance all enemies
            Instantiate(enemiesToSpawn[i], spawnPos, Quaternion.identity);
        }



        yield return new WaitForSeconds(attackDelay);

        UpdateState(State.ATTACK4);
    }


    protected IEnumerator AttackState4()
    {
        //ultimo ataque, morre
        DestroySelf();
        yield return null;
    }


    private Vector3 GetDashDirection()
    {
        // Lógica para determinar a direção do dash
        // Pode ser em direção ao jogador, em uma direção aleatória, etc.
        // Aqui está um exemplo simples de dash em direção ao jogador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 direction = (transform.position - player.transform.position).normalized;
            return direction;
        }
        return Vector3.forward; // Direção padrão se o jogador não for encontrado
    }

}
