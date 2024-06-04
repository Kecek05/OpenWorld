using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchInventory
{
    private List<TesteItens> itemList;
    
    public WitchInventory()
    {
        itemList = new List<TesteItens>();
        AddItem(new TesteItens {  itemType = TesteItens.ItemType.Flor, amount = 1 });
        AddItem(new TesteItens { itemType = TesteItens.ItemType.Cogumelo, amount = 1 });
        AddItem(new TesteItens { itemType = TesteItens.ItemType.Mandragora, amount = 1 });
        AddItem(new TesteItens { itemType = TesteItens.ItemType.Lavanda, amount = 1 });
        AddItem(new TesteItens { itemType = TesteItens.ItemType.Carambola, amount = 1 });
        Debug.Log(itemList.Count);
    }

    public void AddItem(TesteItens item)
    {
        itemList.Add(item);
    }

    public List<TesteItens > GetItemList()
    {
        return itemList;
    }
}
