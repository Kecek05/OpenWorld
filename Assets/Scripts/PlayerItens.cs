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

    [SerializeField] private int[] itensCount;

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
                if (itensCount[0] > 0)
                {
                    itensCount[0]--;
                    return true;
                }
                break;
            case ItensType.Cogumelo:
                if (itensCount[1] > 0)
                {
                    itensCount[1]--;
                    return true;
                }
                break;
            case ItensType.Flor:
                if (itensCount[2] > 0)
                {
                    itensCount[2]--;
                    return true;
                }
                break;
            case ItensType.Lavanda:
                if (itensCount[3] > 0)
                {
                    itensCount[3]--;
                    return true;
                }
                break;
            case ItensType.Mandragora:
                if (itensCount[4] > 0)
                {
                    itensCount[4]--;
                    return true;
                }
                break;
            case ItensType.Samambaia:
                if (itensCount[5] > 0)
                {
                    itensCount[5]--;
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
                    itensCount[0]++;
                break;
            case ItensType.Cogumelo:
                    itensCount[1]++;
                break;
            case ItensType.Flor:
                    itensCount[2]++;
                break;
            case ItensType.Lavanda:
                    itensCount[3]++;
                break;
            case ItensType.Mandragora:
                    itensCount[4]++;
                break;
            case ItensType.Samambaia:
                    itensCount[5]++;
                break;
        }
    }

    public int GetCountWithItemType(ItensType itemType) // return the count with the type of the item
    {
        switch (itemType)
        {
            case ItensType.Carambola:
                return itensCount[0];
            case ItensType.Cogumelo:
                return itensCount[1];
            case ItensType.Flor:
                return itensCount[2];
            case ItensType.Lavanda:
                return itensCount[3];
            case ItensType.Mandragora:
                return itensCount[4];
            case ItensType.Samambaia:
                return itensCount[5];
        }
        return 0;
    }


    public void SetCarambolaCount(int _carambolaCount) { itensCount[0] = _carambolaCount; }

    public int GetCarambolaCount() {  return itensCount[0]; }


    public void SetCogumeloCount(int _cogumeloCount) { itensCount[1] = _cogumeloCount; }
    public int GetCogumeloCount() { return itensCount[1]; }


    public void SetFlorCount(int _florCount) { itensCount[2] = _florCount; }
    public int GetFlorCount() {  return itensCount[2]; }


    public void SetLavandaCount(int _lavandaCount) { itensCount[3] = _lavandaCount; }
    public int GetLavandaCount() {  return itensCount[3]; }


    public void SetMandragoraCount(int _mangragoraCount) { itensCount[4] = _mangragoraCount; }
    public int GetMandragoraCount() {  return itensCount[4]; }


    public void SetSamambaiaCount(int _samambaiaCount) { itensCount[5] = _samambaiaCount; }
    public int GetSamambaiaCount() { return itensCount[5]; }

}
