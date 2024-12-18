using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChangeScene : MonoBehaviour
{
    public static ChangeScene Instance { get; private set; }

    [SerializeField] private float totalTime = 40f;
    public float currentTime;
    [SerializeField] private Image clockImage;
    [SerializeField] private Text timerText;

    [SerializeField] private Loader.Scene scene;

    private bool doSFX = false;

    private void Start()
    {
        currentTime = totalTime;
        StartCoroutine(Clock());
    }

    private IEnumerator Clock()
    {
        currentTime = totalTime;

        while (currentTime > 0) 
        {
            currentTime -= Time.deltaTime; 
            float fillAmount = (float)currentTime / totalTime; 
            clockImage.fillAmount = fillAmount;
            int seconds = Mathf.FloorToInt(currentTime);
            int milliseconds = Mathf.FloorToInt((currentTime - seconds) * 100);
            timerText.text = seconds.ToString("00") + ":" + milliseconds.ToString("00");
            if(currentTime <= 10f && !doSFX)
            {
                doSFX = true;
                SFXManager.Instance.PlayRandomSFXClip(SFXManager.Instance.GetAudioClipRefsSO().clockTicking, Camera.main.transform);
            }
            yield return null;
        }
        currentTime = 0;
        timerText.text = "00:00";
        StartCoroutine(DelayToChangeScene());
    }

    private IEnumerator DelayToChangeScene()
    {
        
        if(LevelFade.instance != null)
           LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        //Check if its in House
        if(SceneManager.GetActiveScene().name == Loader.Scene.House.ToString())
        {
            //its in house, check if have made any potions
            if(StoredPotions.Instance.CheckHaveMorePotionsToDelivery())
            {
                //Have potions to delivery, next scene
                if (WitchInputs.Instance != null)
                    WitchInputs.Instance.ChangeActiveMap(scene);
                Loader.Load(scene);
            } else
            {
                //Dont have made any potions, PAYMENT
                if (WitchInputs.Instance != null)
                    WitchInputs.Instance.ChangeActiveMap(Loader.Scene.GreenHouse);
                Loader.Load(Loader.Scene.Payment);
            }
        } else
        {
            //Other scene
            if (WitchInputs.Instance != null)
                WitchInputs.Instance.ChangeActiveMap(scene);
            Loader.Load(scene);
        }

        
    }


    
}
