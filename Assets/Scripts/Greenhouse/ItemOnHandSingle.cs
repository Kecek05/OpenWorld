
using UnityEngine;

public class ItemOnHandSingle : MonoBehaviour
{
    [SerializeField] private Sprite itemSprite;


    public void SetItemSprite(Sprite _itemSprite)
    {
        itemSprite = _itemSprite;   
    }

}
