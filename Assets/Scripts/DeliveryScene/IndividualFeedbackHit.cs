using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndividualFeedbackHit : MonoBehaviour
{
    [SerializeField] private IndividualHit individualHit;
    [Space]
    [SerializeField] private TextMeshProUGUI feedbackTxt;
    [SerializeField] private Animator feedbackTxtAnim;

    private IEnumerator turnOffFeedbackCoroutine;

    private void Start()
    {
        individualHit.OnTextFeedback += IndividualHit_OnTextFeedback; ;
        DeliveryMinigame.Instance.OnFinishedMinigame += DeliveryMinigame_OnFinishedMinigame;
    }

    private void IndividualHit_OnTextFeedback(IndividualHit.HitType obj)
    {
        switch (obj)
        {
            case IndividualHit.HitType.Perfect:
                feedbackTxt.text = "Perfect";
                feedbackTxt.color = Color.green;
                break;
            case IndividualHit.HitType.Good:
                feedbackTxt.text = "Good";
                feedbackTxt.color = Color.white;
                break;
            case IndividualHit.HitType.Bad:
                feedbackTxt.text = "Bad";
                feedbackTxt.color = Color.red;
                break;
        }
        feedbackTxt.gameObject.SetActive(true);
    }

        private void DeliveryMinigame_OnFinishedMinigame()
    {
        turnOffFeedbackCoroutine = TurnOffFeedback();
        StartCoroutine(turnOffFeedbackCoroutine);
    }



    private IEnumerator TurnOffFeedback()
    {
        feedbackTxtAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(0.2f);
        feedbackTxt.gameObject.SetActive(false);
        turnOffFeedbackCoroutine = null;
    }
}
