
using TMPro;
using UnityEngine;

public class ItemCountUI : MonoBehaviour
{
    [SerializeField] private CountainerCounter countainerCounter;
    [SerializeField] private TextMeshProUGUI counterX;


    private void Start()
    {
        countainerCounter.OnChangedItemCount += CountainerCounter_OnChangedItemCount;
        Invoke(nameof(UpdateCounterTxt), 0.2f);

    }

    private void CountainerCounter_OnChangedItemCount(object sender, System.EventArgs e)
    {
        UpdateCounterTxt();
    }

    private void UpdateCounterTxt()
    {
        counterX.text = PlayerItems.Instance.GetCountWithItemType(countainerCounter.GetCounterType()).ToString();
    }
}
