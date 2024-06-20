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
        List<int> availableExpensesIndex = new List<int>();

        for(int i = 0; i < paymentCostsSO.expansesTxt.Length; i++)
        {
            availableExpensesIndex.Add(i);
        }

        expensesCount = UnityEngine.Random.Range(1, 4);
        currentExpensesIndex.Clear();
       
        for (int i = 0; i < expensesCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableExpensesIndex.Count);
            currentExpensesIndex.Add(availableExpensesIndex[randomIndex]);
            availableExpensesIndex.Remove(randomIndex);
        }
    }

    public int GetExpensesCount() { return expensesCount; }
    public List<int> GetCurrentExpensesIndexList() { return currentExpensesIndex; }

}
