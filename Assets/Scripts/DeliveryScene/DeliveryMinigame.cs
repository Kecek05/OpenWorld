using System;
using System.Collections;
using UnityEngine;

public class DeliveryMinigame : MonoBehaviour
{
    public static DeliveryMinigame Instance {  get; private set; }

    public event Action OnStartedMinigame;
    public event Action OnFinishedMinigame;

    private int hitCount = 0;
    private int missCount = 0;

    [SerializeField] private GameObject titleObj;
    [SerializeField] private float delayToStart;
    [SerializeField] private GameObject[] individualHits;

    private IEnumerator startMinigameCoroutine;
    private IEnumerator finishMinigameCoroutine;

    private int startRecieveMoney;
    private float recieveMoneyMultiply = 1;

    private MinigameDifficultySO minigameDifficultySO;

    [SerializeField] private MinigameDifficultySO debug;

    public enum HitInputs
    {
        A,
        S,
        D,
        F,
    }


    private void Awake()
    {
        Instance = this;
    }

    public void StartMinigame(int _startRecieveMoney, MinigameDifficultySO _minigameDifficultySO)
    {
        ResetMinigame(); // just for safety
        minigameDifficultySO = _minigameDifficultySO;
        //debug
        // minigameDifficultySO = debug;
        OnStartedMinigame?.Invoke(); //SFX
        startRecieveMoney = _startRecieveMoney;
        if(startMinigameCoroutine == null)
        {
            startMinigameCoroutine = StartMinigameCoroutine();
            StartCoroutine(startMinigameCoroutine);
        }
    }

    private IEnumerator StartMinigameCoroutine()
    {
        WitchInputs.Instance.ChangePLayerInputHitMinigame(true);
        titleObj.SetActive(true);
        yield return new WaitForSeconds(delayToStart);
        for (int i = 0; i < individualHits.Length; i++)
        {
            //turn all minigames on
            individualHits[i].gameObject.SetActive(true);
        }
    }

    private IEnumerator FinishedMinigame()
    {
        //wait a little 
        OnFinishedMinigame?.Invoke();
        yield return new WaitForSeconds(1f);
        //Add the money
        MoneyController.Instance.SetDayMoney(MoneyController.Instance.GetDayMoney() + CalculateMoneyAdd());
        Debug.Log("farmou" + ":" + MoneyController.Instance.GetTotalMoney());
        WitchInputs.Instance.ChangePLayerInputHitMinigame(false);
        titleObj.SetActive(false);
        for (int i = 0; i < individualHits.Length; i++)
        {
            //turn all minigames off
            individualHits[i].gameObject.SetActive(false);
        }

       ResetMinigame();

        
    }

    private void ResetMinigame()
    {

        hitCount = 0;
        missCount = 0;
        startRecieveMoney = 0;
        recieveMoneyMultiply = 1;
        startMinigameCoroutine = null;
        finishMinigameCoroutine = null;
    }

    private int CalculateMoneyAdd()
    {
        int totalMoney = startRecieveMoney * (int)recieveMoneyMultiply;
        return totalMoney;
    }


    public void Hitted(float multiplyAdd)
    {
       hitCount++;
       recieveMoneyMultiply += multiplyAdd;
       VerifyFinishedGame();
    }

    public void MissedHit()
    {
        missCount++;
        VerifyFinishedGame();
    }

    private void VerifyFinishedGame()
    {
        if ((hitCount + missCount) >= individualHits.Length)
        {
            if(finishMinigameCoroutine == null)
            {
                finishMinigameCoroutine = FinishedMinigame();
                StartCoroutine(finishMinigameCoroutine);
            }
        }
    }

    public MinigameDifficultySO GetMinigameDifficultySO() { return minigameDifficultySO; }
}
