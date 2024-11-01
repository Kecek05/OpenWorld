using System;

using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public static MoneyController Instance;
    public event EventHandler<OnMoneyChangedEventArgs> OnMoneyChanged;

    public class OnMoneyChangedEventArgs
    {
        public int _totalMoney;
        public int _dayMoney;
    }

    private int totalMoney;
    private int dayMoney;


    private int dayCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTotalMoney(int _currentMoney) 
    {
        totalMoney = _currentMoney;
        OnMoneyChanged?.Invoke(this, new OnMoneyChangedEventArgs { _totalMoney = totalMoney});
    }

    public void SetDayMoney(int _dayMoney)
    {
        dayMoney = _dayMoney;
        OnMoneyChanged?.Invoke(this, new OnMoneyChangedEventArgs { _dayMoney = dayMoney });
    }

    public void ResetDayMoney()
    {
        dayMoney = 0;
    }

    public int GetDayMoney() { return dayMoney; }
    public int GetTotalMoney() { return totalMoney; }

    public int GetDayCount() {  return dayCount; }

    public void SetDayCount(int _dayCount) { dayCount = _dayCount; }
}
