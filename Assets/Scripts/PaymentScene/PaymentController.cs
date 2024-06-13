using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaymentController : MonoBehaviour
{
    private int totalEconomy;
    private int expanseFixed;
    //private int[] expanses;

    [SerializeField] private TextMeshProUGUI[] expanseTxt;
    [SerializeField] private TextMeshProUGUI economyTxt;
    [SerializeField] private TextMeshProUGUI dayPaymentTxt;
    [SerializeField] private TextMeshProUGUI fixedExpanseTxt;
    [SerializeField] private TextMeshProUGUI payOffTxt;

    [SerializeField] private bool paymentConcluded = false;
    [SerializeField] private Loader.Scene scene;


    private void Start()
    {
        Debug.Log("iniciou");
        totalEconomy = MoneyController.Instance.GetTotalMoney() + MoneyController.Instance.GetDayMoney();
        economyTxt.text = totalEconomy.ToString();

        dayPaymentTxt.text = MoneyController.Instance.GetDayMoney().ToString();

        expanseFixed = 50;
        fixedExpanseTxt.text = expanseFixed.ToString();
    }

    public void OnButtonClick()
    {
       DoPayment();
    }

    void DoPayment()
    {
        totalEconomy -= expanseFixed;
        payOffTxt.text = totalEconomy.ToString();
        paymentConcluded = true;
    }

   
    public void PassDay()
    {
        if(paymentConcluded == true)
        {
            MoneyController.Instance.SetTotalMoney(totalEconomy);
            MoneyController.Instance.ResetDayMoney();
            Loader.Load(scene);
        }
        else 
        {
            Debug.Log("tadurodorme");
        }
    }
}
