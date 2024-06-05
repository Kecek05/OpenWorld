
using UnityEngine;

public class PlayerItens : MonoBehaviour
{
    public static PlayerItens Instance { get; private set; }

    private int[] itensCollected;

    public enum ItensType
    {
        Carambola,
        Cogumelo,
        Flor,
        Lavanda,
        Mandragora,
        Samambaia,
    }


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
        itensCollected = new int[6];
    }

    public bool TrySpawnItem(ItensType itemTypeToSpawn)
    {
        switch (itemTypeToSpawn)
        {
            case ItensType.Carambola:
                if (itensCollected[0] > 0)
                {
                    itensCollected[0]--;
                    return true;
                }
                break;
            case ItensType.Cogumelo:
                if (itensCollected[1] > 0)
                {
                    itensCollected[1]--;
                    return true;
                }
                break;
            case ItensType.Flor:
                if (itensCollected[2] > 0)
                {
                    itensCollected[2]--;
                    return true;
                }
                break;
            case ItensType.Lavanda:
                if (itensCollected[3] > 0)
                {
                    itensCollected[3]--;
                    return true;
                }
                break;
            case ItensType.Mandragora:
                if (itensCollected[4] > 0)
                {
                    itensCollected[4]--;
                    return true;
                }
                break;
            case ItensType.Samambaia:
                if (itensCollected[5] > 0)
                {
                    itensCollected[5]--;
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
                    itensCollected[0]++;
                break;
            case ItensType.Cogumelo:
                    itensCollected[1]++;
                break;
            case ItensType.Flor:
                    itensCollected[2]++;
                break;
            case ItensType.Lavanda:
                    itensCollected[3]++;
                break;
            case ItensType.Mandragora:
                    itensCollected[4]++;
                break;
            case ItensType.Samambaia:
                    itensCollected[5]++;
                break;
        }
    }

    public int GetCountWithItemType(ItensType itemType) // return the count with the type of the item
    {
        switch (itemType)
        {
            case ItensType.Carambola:
                return itensCollected[0];
            case ItensType.Cogumelo:
                return itensCollected[1];
            case ItensType.Flor:
                return itensCollected[2];
            case ItensType.Lavanda:
                return itensCollected[3];
            case ItensType.Mandragora:
                return itensCollected[4];
            case ItensType.Samambaia:
                return itensCollected[5];
        }
        return 0;
    }


    public void SetCarambolaCount(int _carambolaCount) { itensCollected[0] = _carambolaCount; }

    public int GetCarambolaCount() {  return itensCollected[0]; }


    public void SetCogumeloCount(int _cogumeloCount) { itensCollected[1] = _cogumeloCount; }
    public int GetCogumeloCount() { return itensCollected[1]; }


    public void SetFlorCount(int _florCount) { itensCollected[2] = _florCount; }
    public int GetFlorCount() {  return itensCollected[2]; }


    public void SetLavandaCount(int _lavandaCount) { itensCollected[3] = _lavandaCount; }
    public int GetLavandaCount() {  return itensCollected[3]; }


    public void SetMandragoraCount(int _mangragoraCount) { itensCollected[4] = _mangragoraCount; }
    public int GetMandragoraCount() {  return itensCollected[4]; }


    public void SetSamambaiaCount(int _samambaiaCount) { itensCollected[5] = _samambaiaCount; }
    public int GetSamambaiaCount() { return itensCollected[5]; }

}
