using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeExpanseController : MonoBehaviour
{

    public static RandomizeExpanseController Instance { get; private set; }

    [SerializeField] private PaymentCostsSO paymentCostsSO;

    public event Action OnNewExpansesList;
    
    private List<int> currentExpensesIndex = new List<int>();


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Invoke(nameof(SetNewExpansesList),0.1f);
    }

    public void SetNewExpansesList()
    {
        List<int> availableExpensesIndex = new List<int>();

        for(int i = 0; i < paymentCostsSO.expansesTxt.Length; i++)
        {
            availableExpensesIndex.Add(i);
        }

        currentExpensesIndex.Clear();


        for (int i = 0; i < PaymentController.Instance.GetDayCounts(); i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableExpensesIndex.Count);
            currentExpensesIndex.Add(availableExpensesIndex[randomIndex]);
            availableExpensesIndex.Remove(randomIndex);
        }
        OnNewExpansesList?.Invoke();
    }

    public List<int> GetCurrentExpensesIndexList() { return currentExpensesIndex; }

}
