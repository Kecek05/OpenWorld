
using UnityEngine;

public class ReSpawnVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem splashParticle;
    [SerializeField] private Vector3 offset;
    private Transform playerTransform;
    private void Start()
    {
        ReSpawnManager.OnWitchFallWater += ReSpawnManager_OnWitchFallWater;
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
            Debug.Log("Encontrou o transform do jogador");
        }
        else
        {
            Debug.LogWarning("Não achou o transform do jogador");
        }
    }

    private void ReSpawnManager_OnWitchFallWater()
    {
        if (playerTransform != null)
        {
            if (SFXManager.Instance.GetAudioClipRefsSO().fallWater != null)
            {
                SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().fallWater, Camera.main.transform);
            }
            Vector3 spawnParticlePos = playerTransform.position + offset;
            Instantiate(splashParticle, spawnParticlePos, Quaternion.identity);
        }
        else
        {
            Debug.Log("nois comeu bosta");
        }
    }
}
