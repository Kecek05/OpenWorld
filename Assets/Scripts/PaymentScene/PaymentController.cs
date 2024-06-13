using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaymentController : MonoBehaviour
{
    private int totalEconomy;
    private int expanseFixed = 50;
    //private int[] expanses;

    [SerializeField] private TextMeshProUGUI[] expanseTxt;
    [SerializeField] private TextMeshProUGUI economyTxt;
    [SerializeField] private TextMeshProUGUI dayPaymentTxt;
    [SerializeField] private TextMeshProUGUI fixedExpanseTxt;
    [SerializeField] private TextMeshProUGUI payOffTxt;

    [SerializeField] private bool paymentConcluded = false;
    [SerializeField] private Loader.Scene scene;
    private int payoff;

    private void Start()
    {
        MoneyController.Instance.SetTotalMoney(MoneyController.Instance.GetTotalMoney() + MoneyController.Instance.GetDayMoney());
        totalEconomy = MoneyController.Instance.GetTotalMoney();
        economyTxt.text = totalEconomy.ToString();
        dayPaymentTxt.text = MoneyController.Instance.GetDayMoney().ToString();
        fixedExpanseTxt.text = expanseFixed.ToString();
        
    }


    public void OnButtonClick()
    {
       DoPayment();
    }

    void DoPayment()
    {
        payoff = totalEconomy - expanseFixed;
        payOffTxt.text = payoff.ToString();
        paymentConcluded = true;
    }

   
    public void PassDay()
    {
        if(paymentConcluded == true)
        {
            MoneyController.Instance.SetTotalMoney(payoff);
            Debug.Log("quando sai da cena" + MoneyController.Instance.GetTotalMoney());
            MoneyController.Instance.ResetDayMoney();
            Loader.Load(scene);
        }
        else 
        {
            Debug.Log("tadurodorme");
        }
    }
}
