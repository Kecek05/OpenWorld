
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoredPotions : MonoBehaviour
{
    public static StoredPotions Instance { get; private set; }


    public List<PotionObjectSO> potionsMade;

    [SerializeField] private int[] recipeSavedCountArray;

    [SerializeField] PotionObjectSO[] potionsDebug;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        recipeSavedCountArray = new int[RandomizeRecipeController.Instance.GetSelectedPotionsSOList().Count];
        //debug only
        //for (int i = 0; i < potionsDebug.Length; i++)
        //{
        //    potionsMade.Add(potionsDebug[i]);
        //}

    }
    //debug only
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
        {
            for (int i = 0; i < potionsDebug.Length; i++)
            {
                potionsMade.Add(potionsDebug[i]);
            }
        }
    }

    public void ResetPotionsMade()
    {
        potionsMade.Clear();
    }

    public void StorePotion(PotionObjectSO _potionObjectSO)
    {
        for (int i = 0; i < recipeSavedCountArray.Length; i++)
        {
            //Runs for all the recipes
            if (_potionObjectSO == RandomizeRecipeController.Instance.GetSelectedPotionsSOList()[i])
            {
                //stored potion matches with the recipe
                recipeSavedCountArray[i]++; //add the potion
                potionsMade.Add(_potionObjectSO);
                break;
            }
        }
    }

    public void DeliveryPotion(PotionObjectSO _potionObjectSO)
    {
        for (int i = 0; i < recipeSavedCountArray.Length; i++)
        {
            //Runs for all the recipes
            if (_potionObjectSO == RandomizeRecipeController.Instance.GetSelectedPotionsSOList()[i])
            {
                //decrease the ammount of that potion
                recipeSavedCountArray[i]--;
                if(!CheckHaveMorePotionsToDelivery())
                {
                    //Change scene
                    StartCoroutine(ChangeScene());
                }
                
                break;
            }
        }
    }
    public bool CheckHaveMorePotionsToDelivery()
    {
        int count = 2;
        for (int i = 0; i < recipeSavedCountArray.Length; i++)
        {
            if (recipeSavedCountArray[i] <= 0)
            {
                //Delivered all potions in this recipe
                count--;
            }
        }
        if(count <= 0)
        {
            //Delivered all potions, change scene (wait for 10 secconds) 
            //Do fade out
            return false;
        }
        return true;
    }


    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(17f); // wait to finish the minigame
        if(LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        if (WitchInputs.Instance != null)
            WitchInputs.Instance.ChangeActiveMap(Loader.Scene.GreenHouse);
        Loader.Load(Loader.Scene.Payment);
    }

    public int[] GetRecipeSavedCountArray() { return recipeSavedCountArray; }

}
