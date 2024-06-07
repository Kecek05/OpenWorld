
using System;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerItemsSO : ScriptableObject
{

    public int[] itemsCollected;

    public enum ItensType
    {
        Carambola,
        Cogumelo,
        Flor,
        Lavanda,
        Mandragora,
        Samambaia,
    }

    public void ResetPlayerItems()
    {
        //itemsCollected = new int[6];
    }

    public bool TrySpawnItem(ItensType itemTypeToSpawn)
    {
        switch (itemTypeToSpawn)
        {
            case ItensType.Carambola:
                if (itemsCollected[0] > 0)
                {
                    itemsCollected[0]--;
                    return true;
                }
                break;
            case ItensType.Cogumelo:
                if (itemsCollected[1] > 0)
                {
                    itemsCollected[1]--;
                    return true;
                }
                break;
            case ItensType.Flor:
                if (itemsCollected[2] > 0)
                {
                    itemsCollected[2]--;
                    return true;
                }
                break;
            case ItensType.Lavanda:
                if (itemsCollected[3] > 0)
                {
                    itemsCollected[3]--;
                    return true;
                }
                break;
            case ItensType.Mandragora:
                if (itemsCollected[4] > 0)
                {
                    itemsCollected[4]--;
                    return true;
                }
                break;
            case ItensType.Samambaia:
                if (itemsCollected[5] > 0)
                {
                    itemsCollected[5]--;
                    return true;
                }
                break;
        }
        return false;
    }

    public void ReturnItemToCountainer(ItensType itemTypeToReturn)
    {
        switch (itemTypeToReturn)
        {
            case ItensType.Carambola:
                itemsCollected[0]++;
                break;
            case ItensType.Cogumelo:
                itemsCollected[1]++;
                break;
            case ItensType.Flor:
                itemsCollected[2]++;
                break;
            case ItensType.Lavanda:
                itemsCollected[3]++;
                break;
            case ItensType.Mandragora:
                itemsCollected[4]++;
                break;
            case ItensType.Samambaia:
                itemsCollected[5]++;
                break;
        }
    }

    public int GetCountWithItemType(ItensType itemType) // return the count with the type of the item
    {
        switch (itemType)
        {
            case ItensType.Carambola:
                return itemsCollected[0];
            case ItensType.Cogumelo:
                return itemsCollected[1];
            case ItensType.Flor:
                return itemsCollected[2];
            case ItensType.Lavanda:
                return itemsCollected[3];
            case ItensType.Mandragora:
                return itemsCollected[4];
            case ItensType.Samambaia:
                return itemsCollected[5];
        }
        return 0;
    }


    public void SetCarambolaCount(int _carambolaCount) { itemsCollected[0] = _carambolaCount; }

    public int GetCarambolaCount() { return itemsCollected[0]; }


    public void SetCogumeloCount(int _cogumeloCount) { itemsCollected[1] = _cogumeloCount; }
    public int GetCogumeloCount() { return itemsCollected[1]; }


    public void SetFlorCount(int _florCount) { itemsCollected[2] = _florCount; }
    public int GetFlorCount() { return itemsCollected[2]; }


    public void SetLavandaCount(int _lavandaCount) { itemsCollected[3] = _lavandaCount; }
    public int GetLavandaCount() { return itemsCollected[3]; }


    public void SetMandragoraCount(int _mangragoraCount) { itemsCollected[4] = _mangragoraCount; }
    public int GetMandragoraCount() { return itemsCollected[4]; }


    public void SetSamambaiaCount(int _samambaiaCount) { itemsCollected[5] = _samambaiaCount; }
    public int GetSamambaiaCount() { return itemsCollected[5]; }

    public int[] GetItemsCollectedArray() { return itemsCollected; }
}
