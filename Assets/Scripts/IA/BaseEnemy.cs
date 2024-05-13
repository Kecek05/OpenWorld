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

    protected enum State
    {
        IDLE,
        FOLLOW,
        PATROL,
        ATTACK
    }
    protected State state;


    protected void UpdateState()
    {
        switch (state)
        {
            case State.IDLE:
                IdleState();
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

    protected abstract void IdleState();
    
        // ficar parado
    protected abstract void FollowState();

    protected abstract void PatrolState();
    
        // andar com o personagem ate um lugar aleatorio
    

    protected abstract void AttackState();
    
        // se tiver um inimgo, chamar a sua funcao




    protected void RandomPlacesToGO()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }





    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        //Target in Aggro Range
    //        target = other.transform;
    //        state = State.FOLLOW;

           
    //    }
    //}


    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.LogWarning("SAIU DO RANGE");
    //        //Target not in Range
    //        target = null;
    //        state = State.IDLE;
    //    }
    //}


    //protected void CollidedWithPlayer(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        //Collidiu com o player

    //        // Instancia o item para ser dropado na posição calculada
    //        Instantiate(itemToDrop, transform.position, Quaternion.identity);
    //        Destroy(gameObject);
    //    }
    //}

    protected float GetAggroRange() { return aggroRange; }

    protected Transform GetTarget() { return target; }

    protected void SetTarget(Transform _target) { target = _target; }

    protected NavMeshAgent GetNavMesh() { return agent; }


    protected int GetHealth() { return Health; }

    protected void SetHealth(int _health) { Health = _health; }

}
