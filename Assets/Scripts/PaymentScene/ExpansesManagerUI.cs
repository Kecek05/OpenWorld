using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansesManagerUI : MonoBehaviour
{
    public static ExpansesManagerUI Instance { get; private set; }
    [SerializeField] private Transform container;
    [SerializeField] private Transform expanseTemplate;
    [SerializeField] private PaymentCostsSO paymentCostsSO;

    private void Awake()
    {
        expanseTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {

        for (int i = 0; i < RandomizeExpanseController.Instance.GetCurrentExpensesIndexList().Count; i++)
        {
            Transform expanseTransform = Instantiate(expanseTemplate, container);
            expanseTransform.SetSiblingIndex(3);
            expanseTransform.gameObject.SetActive(true);

            //passando os valores para os filhos
            expanseTransform.GetComponent<ExpanseSingleUI>().SetExpanseData(paymentCostsSO.expansesTxt[RandomizeExpanseController.Instance.GetCurrentExpensesIndexList()[i]], paymentCostsSO.cousts[RandomizeExpanseController.Instance.GetCurrentExpensesIndexList()[i]]);
            Debug.Log(RandomizeExpanseController.Instance.GetCurrentExpensesIndexList()[i]);
        }
    }
}
