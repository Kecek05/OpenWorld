
using UnityEngine;

public class ReSpawnVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem splashParticle;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;
    private void Start()
    {
        ReSpawnManager.OnWitchFallWater += ReSpawnManager_OnWitchFallWater;
    }

    private void ReSpawnManager_OnWitchFallWater()
    {
        if (SFXManager.Instance.GetAudioClipRefsSO().fallWater != null)
            SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().fallWater, Camera.main.transform);
        Vector3 spawnParticlePos = playerTransform.position + offset;
        Instantiate(splashParticle, spawnParticlePos, Quaternion.identity);
    }
}
