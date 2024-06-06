using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static PlayerItemsSO;

public class TransitionSaveValues : MonoBehaviour
{
    [SerializeField] PlayerItemsSO playerItemsSO;

    private void Start()
    {
        int[] itemCounts = playerItemsSO.GetItemsCollectedArray();

        for (int i = 0; i < itemCounts.Length; i++)
        {
           PlayerItemsSO.ItensType itemType = (PlayerItemsSO.ItensType)i;
           int itemCount = itemCounts[i];
        }
       
    }
}
