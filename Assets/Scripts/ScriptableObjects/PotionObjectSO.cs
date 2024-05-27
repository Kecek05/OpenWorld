using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PotionObjectSO : ScriptableObject
{
    public string PotionName;

    public List<KitchenObjectSO> ingredientsSOList;

}
