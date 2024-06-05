using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private Text[] texts;

    [SerializeField] private GameObject itemOnHandTemplate;
    [SerializeField] private GameObject container;


    private void Awake()
    {
       itemOnHandTemplate.SetActive(false);
    }

    void Update()
    {
        texts[(int)PlayerItens.ItensType.Carambola].text = PlayerItens.Instance.GetCarambolaCount().ToString();
        texts[(int)PlayerItens.ItensType.Cogumelo].text = PlayerItens.Instance.GetCogumeloCount().ToString();
        texts[(int)PlayerItens.ItensType.Flor].text = PlayerItens.Instance.GetFlorCount().ToString();
        texts[(int)PlayerItens.ItensType.Lavanda].text = PlayerItens.Instance.GetLavandaCount().ToString();
        texts[(int)PlayerItens.ItensType.Mandragora].text = PlayerItens.Instance.GetMandragoraCount().ToString();
        texts[(int)PlayerItens.ItensType.Samambaia].text = PlayerItens.Instance.GetSamambaiaCount().ToString();
    }

    private void OnItemGrabedUI()
    {

    }

}
