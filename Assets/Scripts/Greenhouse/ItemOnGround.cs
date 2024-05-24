using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour, IInteractable
{

    [SerializeField] private ItemOnGroundSO[] itemOnGroundSOArray;

    private ItemOnGroundSO selectedItemOnGroundSO;

    private Transform itemInGround;

    private int selectedItemClicksToCollect;

    private void Start()
    {
        int randomItemOnGround = Random.Range(0, itemOnGroundSOArray.Length);
        selectedItemOnGroundSO = itemOnGroundSOArray[randomItemOnGround];
        
        selectedItemClicksToCollect = selectedItemOnGroundSO.clicksToCollect;

        itemInGround = Instantiate(selectedItemOnGroundSO.prefab, transform.position, Quaternion.identity);
    }


    public void Interact()
    {
        selectedItemClicksToCollect--;

        if(selectedItemClicksToCollect <= 0 )
        {
            switch(selectedItemOnGroundSO.itemType)
            {
                case PlayerItens.ItensType.Bread:
                   PlayerItens.main.SetBreadCount(PlayerItens.main.GetBreadCount() + 1);
                   break;
                case PlayerItens.ItensType.MeatPatty:
                   PlayerItens.main.SetMeatyPattyCount(PlayerItens.main.GetMeatyPattyCount() + 1);
                   break;
                case PlayerItens.ItensType.Chesse:
                   PlayerItens.main.SetCheeseCount(PlayerItens.main.GetCheeseCount() + 1);
                   break;
                case PlayerItens.ItensType.Cabbage:
                   PlayerItens.main.SetCabbageCount(PlayerItens.main.GetCabbageCount() + 1);
                   break;
                case PlayerItens.ItensType.Carambola:
                   PlayerItens.main.SetCarambolaCount(PlayerItens.main.GetCarambolaCount() + 1);
                   break;
                case PlayerItens.ItensType.Tomato:
                   PlayerItens.main.SetTomatoCount(PlayerItens.main.GetTomatoCount() + 1);
                   break;
                case PlayerItens.ItensType.Cogumelo:
                   PlayerItens.main.SetCogumeloCount(PlayerItens.main.GetCogumeloCount() + 1);
                   break;
                case PlayerItens.ItensType.Flor:
                   PlayerItens.main.SetFlorCount(PlayerItens.main.GetFlorCount() + 1);
                   break;
                case PlayerItens.ItensType.Lavanda:
                   PlayerItens.main.SetLavandaCount(PlayerItens.main.GetLavandaCount() + 1);
                   break;
                case PlayerItens.ItensType.Mandragora:
                   PlayerItens.main.SetMandragoraCount(PlayerItens.main.GetMandragoraCount() + 1);
                   break;
                case PlayerItens.ItensType.Samambaia:
                    PlayerItens.main.SetSamambaiaCount(PlayerItens.main.GetSamambaiaCount() + 1);
                    break;
            }

            Destroy(itemInGround.gameObject);
            Destroy(gameObject);

        }
    }
}
