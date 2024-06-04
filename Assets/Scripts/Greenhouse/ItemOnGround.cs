using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour, IInteractable
{

    [SerializeField] private ItemOnGroundSO[] itemOnGroundSOArray;

    private ItemOnGroundSO selectedItemOnGroundSO;

    private Transform itemInGround;

    private int selectedItemClicksToCollect;

    private TesteItens item;

    

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
                case PlayerItens.ItensType.Carambola:
                   PlayerItens.Instance.SetCarambolaCount(PlayerItens.Instance.GetCarambolaCount() + 1);
                    
                   break;
                case PlayerItens.ItensType.Cogumelo:
                   PlayerItens.Instance.SetCogumeloCount(PlayerItens.Instance.GetCogumeloCount() + 1);
                   break;
                case PlayerItens.ItensType.Flor:
                   PlayerItens.Instance.SetFlorCount(PlayerItens.Instance.GetFlorCount() + 1);
                   break;
                case PlayerItens.ItensType.Lavanda:
                   PlayerItens.Instance.SetLavandaCount(PlayerItens.Instance.GetLavandaCount() + 1);
                   break;
                case PlayerItens.ItensType.Mandragora:
                   PlayerItens.Instance.SetMandragoraCount(PlayerItens.Instance.GetMandragoraCount() + 1);
                   break;
                case PlayerItens.ItensType.Samambaia:
                    PlayerItens.Instance.SetSamambaiaCount(PlayerItens.Instance.GetSamambaiaCount() + 1);
                    break;
            }

            Destroy(itemInGround.gameObject);
            Destroy(gameObject);

        }
    }
}
