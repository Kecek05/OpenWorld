using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    [Header("House")]
    public AudioClip[] footstepHouse;
    public AudioClip[] trash;
    public AudioClip[] potionInBag;
    public AudioClip[] potionRecipeDontExist;
    public AudioClip[] pestleSuccess;
    public AudioClip[] pestleFail;
    public AudioClip[] caulderonBubbles;
    public AudioClip[] changeLiquidContent;
    public AudioClip[] caulderonCleanLiquid;
    [Space]

    [Header("GreenHouse")]
    public AudioClip[] footstepOutSide;
    public AudioClip[] completeCollect;
    public AudioClip[] stashItem;
    [Space]

    [Header("Delivery")]
    public AudioClip[] knockDoorPerfect;
    public AudioClip[] knockDoorGood;

    public AudioClip[] goodDelivery;
    public AudioClip[] badDelivery;
    public AudioClip[] startMinigameRingBell;
    public AudioClip[] finishedMinigame;

    public AudioClip[] missedClick;
    public AudioClip[] badClick;
    public AudioClip[] fallWater;
    [Header("General")]
    public AudioClip[] interact;
    public AudioClip[] clockTicking;
}
