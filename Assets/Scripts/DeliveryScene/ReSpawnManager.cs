
using System;
using System.Collections;
using UnityEngine;

public class ReSpawnManager : MonoBehaviour
{
    public static ReSpawnManager instance;
    [SerializeField] private Transform spawnPoint;
    public static event Action OnWitchFallWater;
    private GameObject playerObj;

    private IEnumerator respawnCoroutine;
    public void Awake()
    {
        if(instance == null)
            instance = this;

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerObj = player;
            Debug.Log("jogador encontrado.");
        }
        else
        {
            Debug.LogWarning("Jogador não encontrado");
        }
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
        if (playerObj != null)
        {
            playerObj.transform.position = spawnPoint.position;
        }
        else
        {
            Debug.LogWarning("playerObj foi destruído antes da reposição.");
        }
        if (WitchInputs.Instance != null)
            WitchInputs.Instance.ChangeMovement(true);
        respawnCoroutine = null;
    }
}
