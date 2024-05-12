using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public int maxShots = 3;
    private int shotsFired = 0;

    public void Shoot()
    {
        if(shotsFired < maxShots)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null )
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }           
            shotsFired++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a colisão não envolve o próprio atirador
        if (collision.gameObject != gameObject)
        {
            // Destruir a bala
            Destroy(collision.gameObject);
        }

    }

}
