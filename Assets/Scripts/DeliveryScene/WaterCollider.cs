
using UnityEngine;

public class WaterCollider : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ReSpawnManager.instance.ReSpawn();
        }
    }
}
