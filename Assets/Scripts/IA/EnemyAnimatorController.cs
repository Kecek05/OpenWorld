using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAnimatorController : MonoBehaviour
{
    public Animator animator;

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
        Debug.Log("Speed set to: " + speed);
    }

    public void SetTurn(float turn)
    {
        animator.SetFloat("Turn", turn);
        Debug.Log("Turn set to: " + turn);
    }


}