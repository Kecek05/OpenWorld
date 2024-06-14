using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaymentUI : MonoBehaviour
{
    public static PaymentUI instance;

    [SerializeField] private TextMeshProUGUI[] expanseTxt;
    [SerializeField] private TextMeshProUGUI economyTxt;
    [SerializeField] private TextMeshProUGUI dayPaymentTxt;
    [SerializeField] private TextMeshProUGUI fixedExpanseTxt;
    [SerializeField] private TextMeshProUGUI payOffTxt;


    private void SetStartPaymentTxt(int _economy, int _dayPayment, int _fixedExpanse)
    {
        economyTxt.text = _economy.ToString();
        dayPaymentTxt.text = _dayPayment.ToString();
        fixedExpanseTxt.text = _fixedExpanse.ToString();
    }



}
