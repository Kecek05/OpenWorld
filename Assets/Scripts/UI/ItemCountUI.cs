
using TMPro;
using UnityEngine;

public class ItemCountUI : MonoBehaviour
{
    [SerializeField] private CountainerCounter countainerCounter;
    [SerializeField] private TextMeshProUGUI counterX;

    [SerializeField] private PlayerItemsSO playerItemsSO;

    private void Start()
    {
        countainerCounter.OnChangedItemCount += CountainerCounter_OnChangedItemCount;
        UpdateCounterTxt();
    }

    private void CountainerCounter_OnChangedItemCount(object sender, System.EventArgs e)
    {
        UpdateCounterTxt();
    }

    private void UpdateCounterTxt()
    {
        counterX.text = playerItemsSO.GetCountWithItemType(countainerCounter.GetCounterType()).ToString();
    }
}
