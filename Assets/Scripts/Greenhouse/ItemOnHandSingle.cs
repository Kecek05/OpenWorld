
using UnityEngine;
using UnityEngine.UI;

public class ItemOnHandSingle : MonoBehaviour
{
    [SerializeField] private Image itemSprite;


    public void SetItemSprite(Sprite _itemSprite)
    {
        itemSprite.sprite = _itemSprite;   
    }

}
