using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WitchInventorySO : ScriptableObject
{
    public event EventHandler OnDepositeItems;

    public event EventHandler<OnItemGrabEventArgs> OnItemGrab;

    [SerializeField] private PlayerItemsSO playerItemsSO;

    public class OnItemGrabEventArgs
    {
        public Sprite itemSprite;
    }

    private List<ItemOnGroundSO> inventoryList = new List<ItemOnGroundSO>();

    private int listMaxLenght = 4;

    public void ResetInventoryList()
    {
        inventoryList.Clear();
    }

    public void AddItemToInventoryList(ItemOnGroundSO itemToAdd)
    {
        inventoryList.Add(itemToAdd);
        OnItemGrab?.Invoke(this, new OnItemGrabEventArgs
        {
            itemSprite = itemToAdd.itemSprite,
        });
    }

    public void DepositeItemOnBox()
    {
        foreach(ItemOnGroundSO itemInListSO in inventoryList)
        {
            switch (itemInListSO.itemType)
            {
                case PlayerItemsSO.ItensType.Carambola:
                    playerItemsSO.SetCarambolaCount(playerItemsSO.GetCarambolaCount() + 1);
                    break;
                case PlayerItemsSO.ItensType.Cogumelo:
                    playerItemsSO.SetCogumeloCount(playerItemsSO.GetCogumeloCount() + 1);
                    break;
                case PlayerItemsSO.ItensType.Flor:
                    playerItemsSO.SetFlorCount(playerItemsSO.GetFlorCount() + 1);
                    break;
                case PlayerItemsSO.ItensType.Lavanda:
                    playerItemsSO.SetLavandaCount(playerItemsSO.GetLavandaCount() + 1);
                    break;
                case PlayerItemsSO.ItensType.Mandragora:
                    playerItemsSO.SetMandragoraCount(playerItemsSO.GetMandragoraCount() + 1);
                    break;
                case PlayerItemsSO.ItensType.Samambaia:
                    playerItemsSO.SetSamambaiaCount(playerItemsSO.GetSamambaiaCount() + 1);
                    break;
            }
        }
        inventoryList.Clear();
        OnDepositeItems?.Invoke(this, EventArgs.Empty);
    }

    public bool CanAddMoreItens() { return inventoryList.Count < listMaxLenght; }

    public bool ListNotEmpty() { return inventoryList.Count > 0;}
}
