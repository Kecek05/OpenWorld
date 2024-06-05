using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemOnGroundSO : ScriptableObject
{

    public string itemName;

    public Transform prefab;

    public int clicksToCollect;

    public PlayerItemsSO.ItensType itemType;

    public Sprite itemSprite;
}
