using System;

using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public static MoneyController Instance;

    public event EventHandler<OnMoneyChangedEventArgs> OnMoneyChanged;

    public class OnMoneyChangedEventArgs
    {
        public int _currentMoney;
    }

    private int currentMoney;


    private void Awake()
    {
        Instance = this;
    }



    public int GetCurrentMoney() { return currentMoney; }

    public void SetCurrentMoney(int _currentMoney) 
    { 
        currentMoney = _currentMoney;
        OnMoneyChanged?.Invoke(this, new OnMoneyChangedEventArgs { _currentMoney = currentMoney});
    }
}
