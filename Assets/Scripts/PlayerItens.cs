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



    public void SetCarambolaCount(int _carambolaCount) { carambolaCount = _carambolaCount; }
    public int GetCarambolaCount() {  return carambolaCount; }


    public void SetCogumeloCount(int _cogumeloCount) { cogumeloCount = _cogumeloCount; }
    public int GetCogumeloCount() { return cogumeloCount; }


    public void SetFlorCount(int _florCount) {  florCount = _florCount; }
    public int GetFlorCount() {  return florCount; }


    public void SetLavandaCount(int _lavandaCount) { lavandaCount = _lavandaCount; }
    public int GetLavandaCount() {  return lavandaCount; }


    public void SetMandragoraCount(int _mangragoraCount) { mandragoraCount = _mangragoraCount; }
    public int GetMandragoraCount() {  return mandragoraCount; }


    public void SetSamambaiaCount(int _samambaiaCount) { samambaiaCount = _samambaiaCount; }
    public int GetSamambaiaCount() { return samambaiaCount; }

}
