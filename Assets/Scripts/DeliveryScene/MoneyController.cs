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

    private int totalMoney;
    private int dayMoney;


    private void Awake()
    {
        Instance = this;
    }



    public int GetCurrentMoney() { return totalMoney; }

    public void ResetDayMoney()
    {
        dayMoney = 0;
    }

    public void SetCurrentMoney(int _currentMoney) 
    {
        dayMoney += (totalMoney - _currentMoney) * -1;
        totalMoney = _currentMoney;
        OnMoneyChanged?.Invoke(this, new OnMoneyChangedEventArgs { _currentMoney = totalMoney});

    }
}
