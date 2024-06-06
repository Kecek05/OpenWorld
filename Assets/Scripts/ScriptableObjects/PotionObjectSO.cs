using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PotionObjectSO : ScriptableObject
{
    public string PotionName;
    [Space]
    public Sprite potionSprite;
    [Space]
    public List<KitchenObjectSO> ingredientsSOList;
    [Space]
    public GameObject PotionShape;
    [Space]
    public int potionMoneyRecieve;
}
