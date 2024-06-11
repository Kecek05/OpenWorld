
using System;
using System.Collections;
using UnityEngine;

public class IndividualHit : MonoBehaviour
{
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
            Debug.Log("Performed hit");
            hited = true;
            HittedIndividual();
        }
    }

    private IEnumerator HitLoop()
    {
        //Random numbers based on difficulty of the recipe
        float randomSpawnDelay = UnityEngine.Random.Range(DeliveryMinigame.Instance.GetMinigameDifficultySO().minSpawnTime, DeliveryMinigame.Instance.GetMinigameDifficultySO().maxSpawnTime);
        timeToHit = UnityEngine.Random.Range(DeliveryMinigame.Instance.GetMinigameDifficultySO().minSpeed, DeliveryMinigame.Instance.GetMinigameDifficultySO().maxSpeed);
        _hitTime = 0f;
        individualMovingHit.gameObject.SetActive(true);
        Debug.Log(DeliveryMinigame.Instance.GetMinigameDifficultySO().difficultyName);
        yield return new WaitForSeconds(randomSpawnDelay); // random spawn delay
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
        DeliveryMinigame.Instance.MissedHit();
    }

    private void HittedIndividual()
    {
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
        Debug.Log("Accuracy is " + accuracy + " Time Clicked is " + timeClicked);

        if(accuracy <= timeToHit / 10)
        {
            // Perfect click
            OnTextFeedback?.Invoke(HitType.Perfect);

            Debug.Log("Perfect");
            individualMultiplyAdd = 0.25f;
        } else if( accuracy <= timeToHit / 5)
        {
            //Good click
            OnTextFeedback?.Invoke(HitType.Good);
            Debug.Log("Good");
            individualMultiplyAdd = 0.15f;
        } else
        {
            //Bad click
            OnTextFeedback?.Invoke(HitType.Bad);
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
