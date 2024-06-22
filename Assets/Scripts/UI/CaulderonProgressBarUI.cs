using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CaulderonProgressBarUI : MonoBehaviour
{

    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;
    [SerializeField] private CaulderonCounter caulderonCounter;
    [SerializeField] private GameObject completeImg;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        caulderonCounter.OnPotionDone += CaulderonCounter_OnPotionDone;
        caulderonCounter.OnPotionCollected += CaulderonCounter_OnPotionCollected;

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        barImage.fillAmount = 0;

        Hide();
    }

    private void CaulderonCounter_OnPotionCollected(object sender, EventArgs e)
    {
        Debug.Log("Collected");
        Hide();
    }

    private void CaulderonCounter_OnPotionDone(object sender, EventArgs e)
    {
        completeImg.SetActive(true);
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

        Show();

    }



    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        completeImg.SetActive(false);
        gameObject.SetActive(false);
    }
}
