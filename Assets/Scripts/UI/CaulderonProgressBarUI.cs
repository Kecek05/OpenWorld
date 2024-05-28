using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CaulderonProgressBarUI : MonoBehaviour
{

    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;



    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();


        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        barImage.fillAmount = 0;

        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
    {
        switch(e.progressCount)
        {
            case 0:
                barImage.fillAmount = (float)Math.Round(e.progressNormalized / 3, 3);
                break;
            case 1:
                barImage.fillAmount = (float)Math.Round(e.progressNormalized / 3 + 0.3f, 3);
                break;
            case 2:
                barImage.fillAmount = (float)Math.Round(e.progressNormalized / 3 + 0.6f, 3);
                break;
        }
        Debug.Log(barImage.fillAmount);

        Show();
        //if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        //{
        //    Hide();
        //}
        //else
        //{
        //    Show();
        //}
    }



    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
