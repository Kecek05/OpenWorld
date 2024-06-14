using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    [Header("House")]
    public AudioClip[] footstepHouse;
    public AudioClip[] dash;
    public AudioClip[] kitchenObjPickup;
    public AudioClip[] kitchenObjDrop;
    public AudioClip[] trash;
    public AudioClip[] potionSuccess;
    public AudioClip[] potionWrong;
    public AudioClip[] pestleSuccess;
    public AudioClip[] pestleFail;
    public AudioClip[] doneInteract; //finish cut and pestle (Similar to the same particle)
    public AudioClip[] caulderonMixing;
    public AudioClip[] caulderonLiquidToPotion;
    public AudioClip[] caulderonCleanLiquid;
    public AudioClip[] boxInteract;
    [Space]

    [Header("GreenHouse")]
    public AudioClip[] footstepOutSide;
    public AudioClip[] jump;
    public AudioClip[] run;
    public AudioClip[] clickCollect;
    public AudioClip[] completeCollect;
    public AudioClip[] stashItemInBox;
    [Space]

    [Header("Delivery")]
    public AudioClip[] knockDoor1;
    public AudioClip[] knockDoor2;
    public AudioClip[] knockDoor3;
    public AudioClip[] knockDoor4;

    public AudioClip[] goodDelivery;
    public AudioClip[] badDelivery;
    public AudioClip[] startMinigame;



}
