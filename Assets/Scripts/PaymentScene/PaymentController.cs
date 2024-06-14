using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaymentController : MonoBehaviour
{
    public static MoneyController Instance;

    private int totalEconomy;
    private int expanseFixed;
    private int payoff;

    private bool paymentConcluded = false;
    [SerializeField] private Loader.Scene scene;

    [SerializeField] private TextMeshProUGUI[] expanseTxt;
    [SerializeField] private TextMeshProUGUI economyTxt;
    [SerializeField] private TextMeshProUGUI dayPaymentTxt;
    [SerializeField] private TextMeshProUGUI fixedExpanseTxt;
    [SerializeField] private TextMeshProUGUI payOffTxt;


    private void Start()
    {
        MoneyController.Instance.SetTotalMoney(MoneyController.Instance.GetTotalMoney() + MoneyController.Instance.GetDayMoney());
        totalEconomy = MoneyController.Instance.GetTotalMoney();
        economyTxt.text = totalEconomy.ToString(); // passa para o texto, evento aqui
        dayPaymentTxt.text = MoneyController.Instance.GetDayMoney().ToString(); // evento, pega o valor do dia e passar para o texto
        fixedExpanseTxt.text = expanseFixed.ToString(); // evento, só passa o valor fixo

    }


    public void OnButtonClick()
    {
       DoPayment(); // evento para quando aperta o botão
    }

    void DoPayment()
    {
        payoff = totalEconomy - expanseFixed; 
        payOffTxt.text = payoff.ToString(); // passa o evento
        paymentConcluded = true; // pagamento concluido
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
