using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaymentController : MonoBehaviour
{
    private int economys;
    private int dayPayment;
    private int expanseFixed;
    private int[] expanses;
    private int payOff;

    [SerializeField] private TextMeshProUGUI[] expanseTxt;
    [SerializeField] private TextMeshProUGUI economyTxt;
    [SerializeField] private TextMeshProUGUI dayPaymentTxt;
    [SerializeField] private TextMeshProUGUI fixedExpanseTxt;
    [SerializeField] private TextMeshProUGUI payOffTxt;

    private bool isActionOne = true;
    [SerializeField] private bool paymentConcluded = false;
    [SerializeField] private Loader.Scene scene;


    private void Start()
    {
        economys = MoneyController.Instance.GetTotalMoney();
        string economyString = economys.ToString();
        economyTxt.text = economyString;

        dayPayment = MoneyController.Instance.GetDayMoney();
        string dayPaymentString = dayPayment.ToString();
        dayPaymentTxt.text = dayPaymentString;

        expanseFixed = 50;
        string fixedExpanse = expanseFixed.ToString();
        fixedExpanseTxt.text = fixedExpanse;
    }

    public void OnButtonClick()
    {
        if (isActionOne)
        {
            PerformActionOne();
            Debug.Log("apertou");
        }
        else
        {
            PerformActionTwo();
            Debug.Log("soltou");
        }
        isActionOne = !isActionOne;
    }

    void PerformActionOne()
    {
        payOff = economys - expanseFixed;
        paymentConcluded = true;
        string payOffstring = payOff.ToString();
        payOffTxt.text = payOffstring;
        MoneyController.Instance.SetTotalMoney(payOff);
    }

    void PerformActionTwo()
    {
        payOff = 0;
        string payOffstring = payOff.ToString();
        payOffTxt.text = payOffstring;
        paymentConcluded = false;   
    }

    public void PassDay()
    {
        if(paymentConcluded == true)
        {
            MoneyController.Instance.ResetDayMoney();
            Loader.Load(scene);
        }
        else 
        {
            Debug.Log("tadurodorme");
        }
    }
}
