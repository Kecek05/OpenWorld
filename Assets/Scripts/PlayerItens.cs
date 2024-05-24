using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItens : MonoBehaviour
{
    public static PlayerItens main;

    private int breadCount;
    private int meatPattyCount;
    private int cheeseCount;
    private int cabbageCount;
    private int carambolaCount;
    private int tomatoCount;
    private int cogumeloCount;
    private int florCount;
    private int lavandaCount;
    private int mandragoraCount;
    private int samambaiaCount;

    public enum ItensType
    {
        Bread,
        MeatPatty,
        Chesse,
        Cabbage,
        Carambola,
        Tomato,
        Cogumelo,
        Flor,
        Lavanda,
        Mandragora,
        Samambaia,
    }


    private void Awake()
    {
        main = this;
        DontDestroyOnLoad(main);
    }


    public void SetBreadCount(int _breadCount) { breadCount = _breadCount; }
    public int GetBreadCount() { return breadCount; }


    public void SetMeatyPattyCount(int _meatyPatty) { meatPattyCount = _meatyPatty; }
    public int GetMeatyPattyCount() { return meatPattyCount; }


    public void SetCheeseCount(int _cheeseCount) { cheeseCount = _cheeseCount; }
    public int GetCheeseCount() { return cheeseCount; }


    public void SetCabbageCount(int _cabbageCount) { cabbageCount = _cabbageCount; }
    public int GetCabbageCount() { return cabbageCount; }


    public void SetCarambolaCount(int _carambolaCount) { carambolaCount = _carambolaCount; }
    public int GetCarambolaCount() {  return carambolaCount; }


    public void SetTomatoCount(int _tomatoCount) { tomatoCount = _tomatoCount; }
    public int GetTomatoCount() {  return tomatoCount; }


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
