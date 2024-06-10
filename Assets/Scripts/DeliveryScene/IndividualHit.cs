
using System;
using System.Collections;
using UnityEngine;

public class IndividualHit : MonoBehaviour
{
    public event EventHandler OnHitStarted;

    [SerializeField] private float spawnDelay;
    [SerializeField] private float downSpeed;
    [SerializeField] private float hitNumber;
    [SerializeField] private DeliveryMinigame.HitInputs hitInput;

    private float _hitCount = 0f;
    private bool inLoop = false;
    private bool hited = false;


    private IEnumerator hitLoopCoroutine;

    

    private void OnEnable()
    {
        hited = false;
        _hitCount = 0f;
        hitLoopCoroutine = HitLoop();
        StartCoroutine(hitLoopCoroutine);
    }


    private void Start()
    {
        switch(hitInput)
        {
            case DeliveryMinigame.HitInputs.Q:
                WitchInputs.Instance.OnHit1Performed += WitchInputs_OnHitPerformed;
                break;
            case DeliveryMinigame.HitInputs.W:
                WitchInputs.Instance.OnHit2Performed += WitchInputs_OnHitPerformed;
                break;
            case DeliveryMinigame.HitInputs.E:
                WitchInputs.Instance.OnHit3Performed += WitchInputs_OnHitPerformed;
                break;
            case DeliveryMinigame.HitInputs.R:
                WitchInputs.Instance.OnHit4Performed += WitchInputs_OnHitPerformed;
                break;

        }

    }

    private void WitchInputs_OnHitPerformed(object sender, System.EventArgs e)
    {
        if(inLoop)
        {
            hited = true;
            DeliveryMinigame.Instance.Hitted();
        }
    }

    private IEnumerator HitLoop()
    {
        yield return new WaitForSeconds(0.1f); // wait for the subscription
        OnHitStarted?.Invoke(this, EventArgs.Empty);

        while (!hited && _hitCount <= hitNumber)
        {
            //not clicked yet or can still click
            inLoop = true;

            _hitCount += Time.deltaTime;
            yield return null;
        }
        //Hitted
        inLoop = false;
        gameObject.SetActive(false);
    }

    
}
