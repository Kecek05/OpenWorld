
using System;
using System.Collections;
using UnityEngine;

public class IndividualHit : MonoBehaviour
{
    [SerializeField] private IndividualMovingHit individualMovingHit;
    [SerializeField] private float spawnIndividualDelay;
    [SerializeField] private float timeToHit;
    [SerializeField] private DeliveryMinigame.HitInputs hitInput;

    private float _hitTime = 0f;
    private bool inMinigame = false;
    private bool hited = false;
    private float individualMultiplyAdd = 0f;

    private IEnumerator hitLoopCoroutine;



    private void Awake()
    {
        ManageInput();
    }

    private void OnEnable()
    {

        if(hitLoopCoroutine == null)
        {
            hitLoopCoroutine = HitLoop();
            StartCoroutine(hitLoopCoroutine);
        }
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
            HittedIndividual();
        }
    }

    private IEnumerator HitLoop()
    {
        yield return new WaitForSeconds(spawnIndividualDelay); // random spawn delay
        _hitTime = 0f;
        individualMovingHit.StartMoving();
        while (!hited && _hitTime <= timeToHit)
        {
            //not clicked yet or can still click
            inMinigame = true;

            _hitTime += Time.deltaTime;
            yield return null;
        }
        //Missed
        inMinigame = false;
        MissedHitIndividual();
    }

    private void MissedHitIndividual()
    {
        //Missed
        Debug.Log("Missed");
        ResetHitIndividualMinigame();
        DeliveryMinigame.Instance.MissedHit();
    }

    private void HittedIndividual()
    {
        //Hitted
        CalculateAccuracy(_hitTime);

        ResetHitIndividualMinigame();
        DeliveryMinigame.Instance.Hitted(individualMultiplyAdd);
    }


    private void CalculateAccuracy(float timeClicked)
    {
        float accuracy = timeToHit - timeClicked;
        Debug.Log("Accuracy is " + accuracy + " Time Clicked is " + timeClicked);

        if(accuracy <= timeToHit / 10)
        {
            // Perfect click
            Debug.Log("Perfect");
            individualMultiplyAdd = 0.25f;
        } else if( accuracy <= timeToHit / 5)
        {
            //Good click
            Debug.Log("Good");
            individualMultiplyAdd = 0.15f;
        } else
        {
            //Bad click
            Debug.Log("Bad Click");
            individualMultiplyAdd = 0;
        }
    }

    
    private void ResetHitIndividualMinigame()
    {
        //Reset for the next
        hitLoopCoroutine = null;
        StopAllCoroutines();
        _hitTime = 0f;
        hited = false;
        inMinigame = false;
        individualMultiplyAdd = 0f;
        individualMovingHit.StopMoving();
    }

    public float GetTimeToHit() { return timeToHit; }
    
}
