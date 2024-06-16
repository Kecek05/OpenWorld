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
        MoneyController.Instance.SetTotalMoney(MoneyController.Instance.GetTotalMoney() + MoneyController.Instance.GetDayMoney());
        totalEconomy = MoneyController.Instance.GetTotalMoney();
        payoff = totalEconomy;
        dayMoney = MoneyController.Instance.GetDayMoney();
        OnUiPaymentChanged?.Invoke(this, new OnUiPaymentChangedEventArgs { _totalEconomy = totalEconomy, _payoff = payoff, _dayMoney = dayMoney });
        paymentsConcluded = RandomizeExpanseController.Instance.GetExpensesCount();
    }

    public void DoPayment(int _expanseCost)
    {
        payoff -= _expanseCost;
        OnUiPaymentChanged?.Invoke(this, new OnUiPaymentChangedEventArgs { _totalEconomy = payoff, _payoff = payoff, _dayMoney = dayMoney });
        paymentsConcluded--;
        Debug.Log(paymentsConcluded + "quantidade de receitas");
    }

    public void PassDay()
    {
        if(paymentsConcluded <= 0)
        {
            MoneyController.Instance.SetTotalMoney(payoff);
            MoneyController.Instance.ResetDayMoney();
            Loader.Load(scene);
        }
        else 
        {
            Debug.Log("tadurodorme");
        }
    }

    
}
