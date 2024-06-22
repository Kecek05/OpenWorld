using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpanseSingleUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI expanseTxt;
    [SerializeField] private TextMeshProUGUI expansePrice;
    [SerializeField] private Button payButton;
    [SerializeField] private int expanseSingleCost;
    [SerializeField] private GameObject iconCheckMark;

    public void SetExpanseData(string expanseName, int expanseCost)
    {
        expanseSingleCost = expanseCost;
        expanseTxt.text = expanseName;
        expansePrice.text = expanseSingleCost.ToString();
    }

    public void DoSinglePayment()
    {
        PaymentController.Instance.DoPayment(expanseSingleCost);
        iconCheckMark.SetActive(true);
        payButton.interactable = false;
    }

}
