using System;
using System.Collections.Generic;
using UnityEngine;

public class WitchInventory : MonoBehaviour
{
    public event EventHandler OnDepositeItems;

    public event EventHandler<OnItemGrabEventArgs> OnItemGrab;

    public class OnItemGrabEventArgs
    {
        public Sprite itemSprite;
    }

    public static WitchInventory Instance { get; private set; }

    private List<ItemOnGroundSO> inventoryList = new List<ItemOnGroundSO>();

    private int listMaxLenght = 4;


    private void Awake()
    {
        Instance = this;
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
                case PlayerItens.ItensType.Carambola:
                    PlayerItens.Instance.SetCarambolaCount(PlayerItens.Instance.GetCarambolaCount() + 1);
                    break;
                case PlayerItens.ItensType.Cogumelo:
                    PlayerItens.Instance.SetCogumeloCount(PlayerItens.Instance.GetCogumeloCount() + 1);
                    break;
                case PlayerItens.ItensType.Flor:
                    PlayerItens.Instance.SetFlorCount(PlayerItens.Instance.GetFlorCount() + 1);
                    break;
                case PlayerItens.ItensType.Lavanda:
                    PlayerItens.Instance.SetLavandaCount(PlayerItens.Instance.GetLavandaCount() + 1);
                    break;
                case PlayerItens.ItensType.Mandragora:
                    PlayerItens.Instance.SetMandragoraCount(PlayerItens.Instance.GetMandragoraCount() + 1);
                    break;
                case PlayerItens.ItensType.Samambaia:
                    PlayerItens.Instance.SetSamambaiaCount(PlayerItens.Instance.GetSamambaiaCount() + 1);
                    break;
            }
        }
        inventoryList.Clear();
        OnDepositeItems?.Invoke(this, EventArgs.Empty);
    }

    public bool CanAddMoreItens() { return inventoryList.Count < listMaxLenght; }

    public bool ListNotEmpty() { return inventoryList.Count > 0;}
}
