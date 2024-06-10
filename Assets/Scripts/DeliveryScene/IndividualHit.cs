
using System;
using System.Collections;
using UnityEngine;

public class IndividualHit : MonoBehaviour
{
    public event EventHandler OnHitStarted;

    [SerializeField] private float spawnIndividualDelay;
    [SerializeField] private float timeToHit;
    [SerializeField] private DeliveryMinigame.HitInputs hitInput;

    private float _hitTime = 0f;
    private bool inMinigame = false;
    private bool hited = false;


    private IEnumerator hitLoopCoroutine;


    private void Awake()
    {
        ManageInput();
    }

    private void OnEnable()
    {
        hited = false;
        _hitTime = 0f;
        hitLoopCoroutine = HitLoop();
        StartCoroutine(hitLoopCoroutine);
    }


    private void ManageInput()
    {
        switch (hitInput)
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
        if(inMinigame)
        {
            hited = true;
            DeliveryMinigame.Instance.Hitted();
        }
    }

    private IEnumerator HitLoop()
    {
        yield return new WaitForSeconds(0.1f); // wait for the subscription
        OnHitStarted?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(spawnIndividualDelay); // random spawn delay
        while (!hited && _hitTime <= timeToHit)
        {
            //not clicked yet or can still click
            inMinigame = true;

            _hitTime += Time.deltaTime;
            yield return null;
        }
        //Hitted
        inMinigame = false;
        DeliveryMinigame.Instance.Hitted();
        gameObject.SetActive(false);
    }

    public float GetTimeToHit() { return timeToHit; }
    
}
