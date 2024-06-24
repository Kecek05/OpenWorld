using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaymentUI : MonoBehaviour
{
    public static PaymentUI instance;

    [SerializeField] private TextMeshProUGUI economyTxt;
    [SerializeField] private TextMeshProUGUI dayPaymentTxt;
    [SerializeField] private TextMeshProUGUI payOffTxt;
    [SerializeField] private TextMeshProUGUI dayCountTxt;

    private void OnEnable()
    {
        PaymentController.Instance.OnUiPaymentChanged += PaymentController_OnUiPaymentChanged;   
    }

    private void PaymentController_OnUiPaymentChanged(object sender, PaymentController.OnUiPaymentChangedEventArgs e)
    {
        economyTxt.text = e._totalEconomy.ToString();
        dayPaymentTxt.text = e._dayMoney.ToString();
        payOffTxt.text = e._payoff.ToString();
        dayCountTxt.text = e._day.ToString();
    }

}
