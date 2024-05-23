using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItens : MonoBehaviour
{
    public static PlayerItens main;

    private int carambolaCount;

    public enum ItensType
    {
        Carambola,

    }


    private void Awake()
    {
        main = this;
        DontDestroyOnLoad(main);
    }



    public void SetCarambolaCount(int _carambolaCount) { carambolaCount = _carambolaCount; }

    public int GetCarambolaCount() {  return carambolaCount; }
}
