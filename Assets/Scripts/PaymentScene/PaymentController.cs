using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaymentController : MonoBehaviour
{
    private int economys;
    private int DayPayment;
    private int fixedExpanse;
    private int[] expanses;
    private int payOff;

    private TextMeshPro[] expanseTxt;
    private TextMeshPro economyTxt;
    private TextMeshPro DayPaymentTxt;
    private TextMeshPro fixedExpanseTxt;
    private TextMeshPro payOffTxt;

    private void Start()
    {
        economys = MoneyController.Instance.GetCurrentMoney();
        string economyString = economys.ToString();
        economyTxt.text = economyString;

        Day
    }
}
