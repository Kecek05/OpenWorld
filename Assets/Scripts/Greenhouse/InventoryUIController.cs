using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private Text[] texts;

    [SerializeField] private Transform itemOnHandTemplate;
    [SerializeField] private Transform container;

    [SerializeField] private WitchInventorySO witchInventorySO;


    private void Awake()
    {
       itemOnHandTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        witchInventorySO.OnDepositeItems += WitchInventory_OnDepositeItems;
        witchInventorySO.OnItemGrab += WitchInventory_OnItemGrab;
    }

    private void WitchInventory_OnItemGrab(object sender, WitchInventorySO.OnItemGrabEventArgs e)
    {
        OnItemGrabedUI(e.itemSprite);
    }

    private void WitchInventory_OnDepositeItems(object sender, System.EventArgs e)
    {
        OnItemStore();
        
    }

   
   

    private void OnItemGrabedUI(Sprite _itemSprite)
    {
        Transform itemOnHandTransform = Instantiate(itemOnHandTemplate, container);
        itemOnHandTransform.gameObject.SetActive(true);
        itemOnHandTransform.GetComponent<ItemOnHandSingle>().SetItemSprite(_itemSprite);
    }

    private void OnItemStore()
    {
        foreach (Transform child in container)
        {
            if (child == itemOnHandTemplate) continue;
            Destroy(child.gameObject);
        }
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].text = PlayerItens.Instance.GetItemsCollectedArray()[i].ToString();
        }
        //texts[(int)PlayerItens.ItensType.Carambola].text = PlayerItens.Instance.GetCarambolaCount().ToString();
        //texts[(int)PlayerItens.ItensType.Cogumelo].text = PlayerItens.Instance.GetCogumeloCount().ToString();
        //texts[(int)PlayerItens.ItensType.Flor].text = PlayerItens.Instance.GetFlorCount().ToString();
        //texts[(int)PlayerItens.ItensType.Lavanda].text = PlayerItens.Instance.GetLavandaCount().ToString();
        //texts[(int)PlayerItens.ItensType.Mandragora].text = PlayerItens.Instance.GetMandragoraCount().ToString();
        //texts[(int)PlayerItens.ItensType.Samambaia].text = PlayerItens.Instance.GetSamambaiaCount().ToString();
    }
}
