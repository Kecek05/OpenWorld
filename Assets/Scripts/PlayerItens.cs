using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItens : MonoBehaviour
{
    public static PlayerItens Instance { get; private set; }

    private int carambolaCount;
    private int cogumeloCount;
    private int florCount;
    private int lavandaCount;
    private int mandragoraCount;
    private int samambaiaCount;

    [SerializeField] private int[] itensAvailableOnRound;

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
    }

    public bool TrySpawnItem(ItensType itemTypeToSpawn)
    {
        switch (itemTypeToSpawn)
        {
            case ItensType.Carambola:
                if (itensAvailableOnRound[0] > 0)
                {
                    itensAvailableOnRound[0]--;
                    return true;
                }
                break;
            case ItensType.Cogumelo:
                if (itensAvailableOnRound[1] > 0)
                {
                    itensAvailableOnRound[1]--;
                    return true;
                }
                break;
            case ItensType.Flor:
                if (itensAvailableOnRound[2] > 0)
                {
                    itensAvailableOnRound[2]--;
                    return true;
                }
                break;
            case ItensType.Lavanda:
                if (itensAvailableOnRound[3] > 0)
                {
                    itensAvailableOnRound[3]--;
                    return true;
                }
                break;
            case ItensType.Mandragora:
                if (itensAvailableOnRound[4] > 0)
                {
                    itensAvailableOnRound[4]--;
                    return true;
                }
                break;
            case ItensType.Samambaia:
                if (itensAvailableOnRound[5] > 0)
                {
                    itensAvailableOnRound[5]--;
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
                    itensAvailableOnRound[0]++;
                break;
            case ItensType.Cogumelo:
                    itensAvailableOnRound[1]++;
                break;
            case ItensType.Flor:
                    itensAvailableOnRound[2]++;
                break;
            case ItensType.Lavanda:
                    itensAvailableOnRound[3]++;
                break;
            case ItensType.Mandragora:
                    itensAvailableOnRound[4]++;
                break;
            case ItensType.Samambaia:
                    itensAvailableOnRound[5]++;
                break;
        }
    }

    public int GetCountWithItemType(ItensType itemType) // return the count with the type of the item
    {
        switch (itemType)
        {
            case ItensType.Carambola:
                return itensAvailableOnRound[0];
            case ItensType.Cogumelo:
                return itensAvailableOnRound[1];
            case ItensType.Flor:
                return itensAvailableOnRound[2];
            case ItensType.Lavanda:
                return itensAvailableOnRound[3];
            case ItensType.Mandragora:
                return itensAvailableOnRound[4];
            case ItensType.Samambaia:
                return itensAvailableOnRound[5];
        }
        return 0;
    }


    public void SetCarambolaCount(int _carambolaCount) { itensAvailableOnRound[0] = _carambolaCount; }

    public int GetCarambolaCount() {  return itensAvailableOnRound[0]; }


    public void SetCogumeloCount(int _cogumeloCount) { itensAvailableOnRound[1] = _cogumeloCount; }
    public int GetCogumeloCount() { return itensAvailableOnRound[1]; }


    public void SetFlorCount(int _florCount) { itensAvailableOnRound[2] = _florCount; }
    public int GetFlorCount() {  return itensAvailableOnRound[2]; }


    public void SetLavandaCount(int _lavandaCount) { itensAvailableOnRound[3] = _lavandaCount; }
    public int GetLavandaCount() {  return itensAvailableOnRound[3]; }


    public void SetMandragoraCount(int _mangragoraCount) { itensAvailableOnRound[4] = _mangragoraCount; }
    public int GetMandragoraCount() {  return itensAvailableOnRound[4]; }


    public void SetSamambaiaCount(int _samambaiaCount) { itensAvailableOnRound[5] = _samambaiaCount; }
    public int GetSamambaiaCount() { return itensAvailableOnRound[5]; }

}
