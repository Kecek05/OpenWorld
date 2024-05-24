using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryTest : MonoBehaviour
{
    [SerializeField]
    private Text[] texts;

    

    
    void Update()
    {
        texts[(int)PlayerItens.ItensType.Bread].text = PlayerItens.main.GetBreadCount().ToString();
        texts[(int)PlayerItens.ItensType.MeatPatty].text = PlayerItens.main.GetMeatyPattyCount().ToString();
        texts[(int)PlayerItens.ItensType.Chesse].text = PlayerItens.main.GetCheeseCount().ToString();
        texts[(int)PlayerItens.ItensType.Cabbage].text = PlayerItens.main.GetCabbageCount().ToString();
        texts[(int)PlayerItens.ItensType.Carambola].text = PlayerItens.main.GetCarambolaCount().ToString();
        texts[(int)PlayerItens.ItensType.Tomato].text = PlayerItens.main.GetTomatoCount().ToString();
        texts[(int)PlayerItens.ItensType.Cogumelo].text = PlayerItens.main.GetCogumeloCount().ToString();
        texts[(int)PlayerItens.ItensType.Flor].text = PlayerItens.main.GetFlorCount().ToString();
        texts[(int)PlayerItens.ItensType.Lavanda].text = PlayerItens.main.GetLavandaCount().ToString();
        texts[(int)PlayerItens.ItensType.Mandragora].text = PlayerItens.main.GetMandragoraCount().ToString();
        texts[(int)PlayerItens.ItensType.Samambaia].text = PlayerItens.main.GetSamambaiaCount().ToString();
    }
}
