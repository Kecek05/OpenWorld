
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
            texts[i].text = PlayerItems.Instance.GetItemsCollectedArray()[i].ToString();
        }
    }
}
