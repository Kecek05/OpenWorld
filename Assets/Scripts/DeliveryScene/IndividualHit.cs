
using System;
using System.Collections;
using UnityEngine;

public class IndividualHit : MonoBehaviour
{
    public event Action OnHitMissed;
    public event Action OnHitPerfect;
    public event Action OnHitGood;
    public event Action OnHitBad;


    public event Action<HitType> OnTextFeedback;

    public enum HitType
    {
        Perfect,
        Good,
        Bad,
    }

    [SerializeField] private IndividualMovingHit individualMovingHit;
    [SerializeField] private DeliveryMinigame.HitInputs hitInput;

    private float timeToHit;
    private float _hitTime = 0f;
    private float individualMultiplyAdd = 0f;
    private bool inMinigame = false;
    private bool hited = false;
    private IEnumerator hitLoopCoroutine;

    

    private void Awake()
    {
        ManageInput();
    }



    private void OnEnable()
    {
        DeliveryMinigame.Instance.OnFinishedMinigame += DeliveryMinigame_OnFinishedMinigame;
        if(hitLoopCoroutine == null)
        {
            hitLoopCoroutine = HitLoop();
            StartCoroutine(hitLoopCoroutine);
        }
    }

    private void OnDisable()
    {
        DeliveryMinigame.Instance.OnFinishedMinigame -= DeliveryMinigame_OnFinishedMinigame;
    }

    private void DeliveryMinigame_OnFinishedMinigame()
    {
        ResetHitIndividualMinigame();
    }

    private void ManageInput()
    {
        switch (hitInput)
        {
            case DeliveryMinigame.HitInputs.A:
                WitchInputs.Instance.OnHit1Performed += WitchInputs_OnHitPerformed;
                break;
            case DeliveryMinigame.HitInputs.S:
                WitchInputs.Instance.OnHit2Performed += WitchInputs_OnHitPerformed;
                break;
            case DeliveryMinigame.HitInputs.D:
                WitchInputs.Instance.OnHit3Performed += WitchInputs_OnHitPerformed;
                break;
            case DeliveryMinigame.HitInputs.F:
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
        //Random numbers based on difficulty of the recipe
        float randomSpawnDelay = UnityEngine.Random.Range(DeliveryMinigame.Instance.GetMinigameDifficultySO().minSpawnTime, DeliveryMinigame.Instance.GetMinigameDifficultySO().maxSpawnTime);
        timeToHit = UnityEngine.Random.Range(DeliveryMinigame.Instance.GetMinigameDifficultySO().minSpeed, DeliveryMinigame.Instance.GetMinigameDifficultySO().maxSpeed);
        Debug.Log(timeToHit + " time to hit");
        _hitTime = 0f;
        individualMovingHit.gameObject.SetActive(true);
        yield return new WaitForSeconds(randomSpawnDelay); // random spawn delay
        individualMovingHit.StartMoving();
        while (!hited && _hitTime <= timeToHit)
        {
            //not clicked yet or can still click
            inMinigame = true;

            _hitTime += Time.deltaTime;
            //yield return new WaitForSeconds(.5f);
            yield return null;
        }
        //Missed
        inMinigame = false;
        MissedHitIndividual();
    }

    private void MissedHitIndividual()
    {
        //Missed
        OnHitMissed?.Invoke(); // SFX
        DeliveryMinigame.Instance.MissedHit();
    }

    private void HittedIndividual()
    {
        if(hitLoopCoroutine != null) 
            StopCoroutine(hitLoopCoroutine); 

        //Hitted
        CalculateAccuracy(_hitTime);

        //Hit turn off the moving img
        individualMovingHit.StopMoving();
        individualMovingHit.gameObject.SetActive(false);

        DeliveryMinigame.Instance.Hitted(individualMultiplyAdd);
    }




    private void CalculateAccuracy(float timeClicked)
    {
        float accuracy = timeToHit - timeClicked;


        if(accuracy <= timeToHit / 25)
        {
            // Perfect click

            OnHitPerfect?.Invoke();
            individualMultiplyAdd = 0.25f;
            OnTextFeedback?.Invoke(HitType.Perfect);
        } else if( accuracy <= timeToHit / 10)
        {
            //Good click

            OnHitGood?.Invoke();
            individualMultiplyAdd = 0.15f;
            OnTextFeedback?.Invoke(HitType.Good);
        } else
        {
            //Bad click

            OnHitBad?.Invoke();
            individualMultiplyAdd = 0;
            OnTextFeedback?.Invoke(HitType.Bad);
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

    public float GetHitTime() { return _hitTime; }
    public float GetTimeToHit() { return timeToHit; }
    
}
