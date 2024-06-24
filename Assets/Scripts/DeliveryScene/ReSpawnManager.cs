
using System;
using System.Collections;
using UnityEngine;

public class ReSpawnManager : MonoBehaviour
{
    public static ReSpawnManager instance;
    [SerializeField] private Transform spawnPoint;
    public static event Action OnWitchFallWater;
    [SerializeField] private GameObject playerObj;

    private IEnumerator respawnCoroutine;
    public void Awake()
    {
        if(instance == null)
            instance = this;
    }


    public void ReSpawn()
    {
        if(respawnCoroutine == null)
        {
            respawnCoroutine = ReSpawnDelay();
            StartCoroutine(respawnCoroutine);
        }
    }


    private IEnumerator ReSpawnDelay()
    {
        OnWitchFallWater?.Invoke();
        if(WitchInputs.Instance != null)
            WitchInputs.Instance.ChangeMovement(false);
        yield return new WaitForSeconds(0.5f);
        if(playerObj != null)
            playerObj.transform.position = spawnPoint.position;
        if (WitchInputs.Instance != null)
            WitchInputs.Instance.ChangeMovement(true);
        respawnCoroutine = null;
    }
}
