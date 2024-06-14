using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeExpanseController : MonoBehaviour
{

    public static RandomizeExpanseController Instance { get; private set; }

    [SerializeField] private PaymentCostsSO paymentCostsSO; 

    private List<int> currentExpensesIndex = new List<int>();

    private int expensesCount;


    private void Awake()
    {
        Instance = this;
        SetNewExpansesList();
    }

    public void SetNewExpansesList()
    {
        expensesCount = UnityEngine.Random.Range(1, 4);
        currentExpensesIndex.Clear();

        for (int i = 0; i < expensesCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, paymentCostsSO.expansesTxt.Length);
            currentExpensesIndex.Add(randomIndex);
        }
    }

    public List<int> GetCurrentExpensesIndex() { return currentExpensesIndex; }

}
