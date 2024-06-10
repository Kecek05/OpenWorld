using System.Collections;
using UnityEngine;

public class DeliveryMinigame : MonoBehaviour
{
    public static DeliveryMinigame Instance {  get; private set; }

    private int hitCount = 0;
    private int missCount = 0;

    [SerializeField] private GameObject titleObj;
    [SerializeField] private float delayToStart;
    [SerializeField] private GameObject[] individualHits;

    private IEnumerator startMinigameCoroutine;

    private int startRecieveMoney;
    private float recieveMoneyMultiply = 1;
    

    public enum HitInputs
    {
        Q,
        W,
        E,
        R,
    }


    private void Awake()
    {
        Instance = this;
    }

    public void StartMinigame(int _startRecieveMoney)
    {
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
        hitCount = 0;

        for (int i = 0; i < individualHits.Length; i++)
        {
            //turn all minigames on
            individualHits[i].gameObject.SetActive(true);
        }
    }

    private void FinishedMinigame()
    {
        //Add the money
        MoneyController.Instance.SetCurrentMoney(MoneyController.Instance.GetCurrentMoney() + CalculateMoneyAdd());

        WitchInputs.Instance.ChangePLayerInputHitMinigame(false);
        titleObj.SetActive(false);
        for (int i = 0; i < individualHits.Length; i++)
        {
            //turn all minigames off
            individualHits[i].gameObject.SetActive(false);
        }
        startRecieveMoney = 0;
        recieveMoneyMultiply = 1;
        startMinigameCoroutine = null;

        
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
        if ((hitCount + missCount) >= 1)
        {
            FinishedMinigame();
        }
    }
}
