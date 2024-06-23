
using System.Collections;
using UnityEngine;

public class SharkWalk : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolSpeed = 2.5f;
    [SerializeField] private float rotationSpeed = 4.5f;

    private int currentPointIndex = 0;
    private Transform targetPoint;
    private bool isPatrolling = true;



    private IEnumerator walkCoroutine;


    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            targetPoint = patrolPoints[currentPointIndex];
        }

        if (walkCoroutine == null)
        {
            walkCoroutine = Walk();
            StartCoroutine(walkCoroutine);
        }
            
    }

    private IEnumerator Walk()
    {
        while(true)
        {
            if (isPatrolling && patrolPoints.Length > 0)
            {
                Patrol();
            }
            yield return null;
        }
    }


    private void Patrol()
    {
        // Move
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);

        // Rotate
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        // Finished?
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Atualizar o índice do ponto de patrulha atual
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            targetPoint = patrolPoints[currentPointIndex];
        }
    }
}

