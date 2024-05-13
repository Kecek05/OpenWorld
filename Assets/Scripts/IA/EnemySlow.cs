using Cinemachine.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlow : BaseEnemy
{
    [SerializeField] private GameObject gfx;
    [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private float slowDuration;

    private bool startSlow;


    protected override IEnumerator AttackState()
    {

        startSlow = true;
        gfx.SetActive(false);
        yield break;
    }
    private void FixedUpdate()
    {
        if(startSlow)
        {
            rbPlayer.constraints = RigidbodyConstraints.FreezePosition;
            slowDuration -= Time.deltaTime;

            if(slowDuration <= 0)
            {
                rbPlayer.constraints = RigidbodyConstraints.None;
                rbPlayer.constraints = RigidbodyConstraints.FreezeRotation;
                DestroySelf(); 
            }

        }
    }



}
