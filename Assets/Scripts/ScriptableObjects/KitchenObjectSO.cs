using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{

    public Transform prefab;
    public Sprite sprite;
    public string objectName;

    public float timeInCaulderon;

    public Material particleMaterial;

    [Tooltip("SFX For Chop or Pestle")]
    public AudioClip[] interactSFX;
}
