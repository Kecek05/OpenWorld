using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class changeScene : MonoBehaviour
{

    [SerializeField] private float totalTime = 40f;
    private float currentTime;
    [SerializeField] private Image clockImage;
    [SerializeField] private Text timerText;

    [SerializeField] private Loader.Scene scene;

    private void Start()
    {
        currentTime = totalTime;
        StartCoroutine(ChangeScene());
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
            yield return null;
        }
        currentTime = 0;
        timerText.text = "00:00";
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(totalTime);
        Loader.Load(scene);
    }

    
}
