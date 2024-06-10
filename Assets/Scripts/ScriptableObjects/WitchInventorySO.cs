using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WitchInventorySO : ScriptableObject
{
    public event EventHandler OnDepositeItems;

    public event EventHandler<OnItemGrabEventArgs> OnItemGrab;



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
                case PlayerItems.ItensType.Carambola:
                    PlayerItems.Instance.SetCarambolaCount(PlayerItems.Instance.GetCarambolaCount() + 1);
                    break;
                case PlayerItems.ItensType.Cogumelo:
                    PlayerItems.Instance.SetCogumeloCount(PlayerItems.Instance.GetCogumeloCount() + 1);
                    break;
                case PlayerItems.ItensType.Flor:
                    PlayerItems.Instance.SetFlorCount(PlayerItems.Instance.GetFlorCount() + 1);
                    break;
                case PlayerItems.ItensType.Lavanda:
                    PlayerItems.Instance.SetLavandaCount(PlayerItems.Instance.GetLavandaCount() + 1);
                    break;
                case PlayerItems.ItensType.Mandragora:
                    PlayerItems.Instance.SetMandragoraCount(PlayerItems.Instance.GetMandragoraCount() + 1);
                    break;
                case PlayerItems.ItensType.Samambaia:
                    PlayerItems.Instance.SetSamambaiaCount(PlayerItems.Instance.GetSamambaiaCount() + 1);
                    break;
            }
        }
        inventoryList.Clear();
        OnDepositeItems?.Invoke(this, EventArgs.Empty);
    }

    public bool CanAddMoreItens() { return inventoryList.Count < listMaxLenght; }

    public bool ListNotEmpty() { return inventoryList.Count > 0;}
}
