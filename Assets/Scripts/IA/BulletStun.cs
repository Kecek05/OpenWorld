using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStun : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject gfx;
    [SerializeField] private float bulletSpeed;

    private Rigidbody rbPlayer;
    [SerializeField] private float slowDuration;

    private bool startSlow;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        rbPlayer = player.transform.parent.GetComponent<Rigidbody>();

        Vector3 direcao = (PlayerOpenWorld.main.GetAggroPoint().position - transform.position).normalized;

        rb.velocity = direcao * bulletSpeed;

        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startSlow = true;
            gfx.SetActive(false);
        }
    }


    private void FixedUpdate()
    {
        if (startSlow)
        {
            rbPlayer.constraints = RigidbodyConstraints.FreezePosition;
            slowDuration -= Time.deltaTime;

            if (slowDuration <= 0)
            {
                rbPlayer.constraints = RigidbodyConstraints.None;
                rbPlayer.constraints = RigidbodyConstraints.FreezeRotation;
                Destroy(gameObject);
            }

        }
    }

}
