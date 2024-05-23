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
        Debug.Log(selectedItemClicksToCollect);
        if(selectedItemClicksToCollect <= 0 )
        {
            switch(selectedItemOnGroundSO.itemType)
            {
                case PlayerItens.ItensType.Carambola:
                    PlayerItens.main.SetCarambolaCount(PlayerItens.main.GetCarambolaCount() + 1);
                    break;
            }


            Destroy(itemInGround.gameObject);
            Destroy(gameObject);

        }
    }
}
