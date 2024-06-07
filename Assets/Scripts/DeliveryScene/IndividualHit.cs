
using System.Collections;
using UnityEngine;

public class IndividualHit : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    [SerializeField] private float spawnDelay;
    [SerializeField] private float downSpeed;
    [SerializeField] private float hitNumber;

    private
        float _hitCount = 0f;

    private bool hited = false;

    private IEnumerator hitLoopCoroutine;

    private void OnEnable()
    {
        hited = false;
        hitLoopCoroutine = HitLoop();
        StartCoroutine(hitLoopCoroutine);
    }

    private IEnumerator HitLoop()
    {
        while(!hited || _hitCount <= hitNumber)
        {
            //not clicked yet or can still click


            _hitCount += Time.deltaTime;
            yield return null;
        }
        //Hitted
        parent.SetActive(false);
    }
}
