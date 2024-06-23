
using UnityEngine;
using UnityEngine.UI;

public class SelectedPotionUI : MonoBehaviour
{
    [SerializeField] private Image potionImage;
    [SerializeField] private SpriteRenderer potionImageMinimap;

    [SerializeField] private DeliverySpot thisDeliverySpot;

    private void OnEnable()
    {
        thisDeliverySpot.OnPotionSet += ThisDeliverySpot_OnPotionSet;
    }

    private void ThisDeliverySpot_OnPotionSet(object sender, DeliverySpot.OnPotionSetEventArgs e)
    {
        potionImage.sprite = e.potionSprite;
        potionImageMinimap.sprite = e.potionSprite;
    }
}
