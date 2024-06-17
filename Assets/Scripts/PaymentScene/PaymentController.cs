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
        public Button _paymentButton;
    }

    private int totalEconomy;
    private int payoff;
    private int dayMoney;
    private int paymentsConcluded;
    [SerializeField] private Loader.Scene scene;
    [SerializeField] private GameObject gameOverPanel;

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
        // geting money of the day + economys of the player
        MoneyController.Instance.SetTotalMoney(MoneyController.Instance.GetTotalMoney() + MoneyController.Instance.GetDayMoney()); 
        totalEconomy = MoneyController.Instance.GetTotalMoney();

        payoff = totalEconomy;

        dayMoney = MoneyController.Instance.GetDayMoney();

        OnUiPaymentChanged?.Invoke(this, new OnUiPaymentChangedEventArgs { _totalEconomy = totalEconomy, _payoff = payoff, _dayMoney = dayMoney });
        paymentsConcluded = RandomizeExpanseController.Instance.GetExpensesCount() + 1; // +1 adding fixedPayment
    }

    public void DoPayment(int _expanseCost)
    {
        payoff -= _expanseCost;
        OnUiPaymentChanged?.Invoke(this, new OnUiPaymentChangedEventArgs { _totalEconomy = payoff, _payoff = payoff, _dayMoney = dayMoney });
        paymentsConcluded--;
    }

    public void PassDay()
    {
        if(paymentsConcluded <= 0)
        {
            // player clicked all pay buttons
            if(payoff >= 0)
            {
                // player have money to pay all expanses, next day
                MoneyController.Instance.SetTotalMoney(payoff);
                MoneyController.Instance.ResetDayMoney();
                Loader.Load(scene);
            }
            else
            {
                // player haven't money to pay all expanses, GameOver
                gameOverPanel.SetActive(true);
            }
        }
        else 
        {
            // player did not click all the pay buttons 
        }
    }

    
}
