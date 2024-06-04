using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteItens
{
    public enum ItemType
    {
        Carambola,
        Cogumelo,
        Flor,
        Lavanda,
        Mandragora,
        Samambaia,
    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Carambola:    return ItemAssetsInventory.Instance.carambola;
            case ItemType.Cogumelo:     return ItemAssetsInventory.Instance.cogumelo;
            case ItemType.Flor:         return ItemAssetsInventory.Instance.flor;
            case ItemType.Lavanda:      return ItemAssetsInventory.Instance.lavanda;
            case ItemType.Mandragora:   return ItemAssetsInventory.Instance.mandragora;
            case ItemType.Samambaia:    return ItemAssetsInventory.Instance.samambaia;
        }

    }
}

