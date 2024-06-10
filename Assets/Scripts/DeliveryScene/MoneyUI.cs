
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI moneyTxt;

    private void Start()
    {
        MoneyController.Instance.OnMoneyChanged += MoneyController_OnMoneyChanged;
    }

    private void MoneyController_OnMoneyChanged(object sender, MoneyController.OnMoneyChangedEventArgs e)
    {
        moneyTxt.text = e._currentMoney.ToString();
    }
}
