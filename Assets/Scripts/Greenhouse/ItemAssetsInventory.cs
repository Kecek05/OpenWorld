using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssetsInventory : MonoBehaviour
{
    public static ItemAssetsInventory Instance { get; private set; }

    


    private void Awake()
    {
        Instance = this;
    }

    public Sprite carambola;
    public Sprite flor;
    public Sprite lavanda;
    public Sprite mandragora;
    public Sprite samambaia;
    public Sprite cogumelo;

}

    



