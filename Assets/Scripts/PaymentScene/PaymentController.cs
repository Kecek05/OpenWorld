using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PaymentController : MonoBehaviour
{
    public static PaymentController Instance;
    public event EventHandler<OnUiPaymentChangedEventArgs> OnUiPaymentChanged;

    public class OnUiPaymentChangedEventArgs : EventArgs
    {
        public int _totalEconomy;
        public int _payoff;
        public int _dayMoney;
        public int _day;
    }

    private int totalEconomy;
    private int payoff;
    private int dayMoney;
    private int paymentsConcluded;
    private int dayCount = 0;
    private int countExpanses;

    [SerializeField] private Button fixedExpanseBtn;

    [SerializeField] private Loader.Scene scene;

    private IEnumerator changeSceneDelay;

    [SerializeField] private Button nextButon;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SavePlayer();
        LoadPlayer();
        dayCount++;
        // geting money of the day + economys of the player
        MoneyController.Instance.SetTotalMoney(MoneyController.Instance.GetTotalMoney() + MoneyController.Instance.GetDayMoney());
        totalEconomy = MoneyController.Instance.GetTotalMoney();

        payoff = totalEconomy;

        dayMoney = MoneyController.Instance.GetDayMoney();


        RandomizeExpanseController.Instance.OnNewExpansesList += Randomize_OnNewExpansesList;

        fixedExpanseBtn.Select();
        Invoke(nameof(DelayEvent), 0.1f);
    }

    private void DelayEvent()
    {
        OnUiPaymentChanged?.Invoke(this, new OnUiPaymentChangedEventArgs { _totalEconomy = totalEconomy, _payoff = payoff, _dayMoney = dayMoney, _day = dayCount });

    }

    private void Randomize_OnNewExpansesList()
    {
        paymentsConcluded = GetDayCounts(); // +1 adding fixedPayment
    }

    public void DoPayment(int _expanseCost)
    {
        payoff -= _expanseCost;
        OnUiPaymentChanged?.Invoke(this, new OnUiPaymentChangedEventArgs { _totalEconomy = payoff, _payoff = payoff, _dayMoney = dayMoney, _day = dayCount });
        paymentsConcluded--;
        nextButon.Select();
    }

    public void PassDay()
    {
        if (paymentsConcluded <= 0)
        {
            // player clicked all pay buttons
            if (payoff >= 0) // correct is payoff >= 0
            {
                // player have money to pay all expanses, next day 
              //  countExpanses = RandomizeExpanseController.Instance.GetExpensesCount();
                MoneyController.Instance.SetTotalMoney(payoff);
                MoneyController.Instance.ResetDayMoney();
                SavePlayer();

                //Reset the dontDestroy
                GameObject dontDestroyThisDay = GameObject.FindWithTag("DontDestroyThisDay");
                Destroy(dontDestroyThisDay);
                if (changeSceneDelay == null)
                {
                    changeSceneDelay = ChangeSceneDelay(Loader.Scene.GreenHouse);
                    StartCoroutine(changeSceneDelay);
                }
                
            }
            else
            {
                // player haven't money to pay all expanses, GameOver
                if (changeSceneDelay == null)
                {
                    changeSceneDelay = ChangeSceneDelay(Loader.Scene.GameOverScene);
                    StartCoroutine(changeSceneDelay);
                }
            }
        }
        else
        {
            // player did not click all the pay buttons 
        }
    }

    private IEnumerator ChangeSceneDelay(Loader.Scene _scene)
    {
        if(LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        if (WitchInputs.Instance != null)
            WitchInputs.Instance.ChangeActiveMap(Loader.Scene.GreenHouse);
        Loader.Load(_scene);
        changeSceneDelay = null;
    }


    public void BackToMenu()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        totalEconomy = data.economyPlayer;
        dayCount = data.dayCount;
        countExpanses = data.expansesCount;
    }

    public int GetTotalEconomy() { return totalEconomy; }
    public int GetDayCounts() { return dayCount; }
    public int GetExpansesCount() { return countExpanses; }
}
