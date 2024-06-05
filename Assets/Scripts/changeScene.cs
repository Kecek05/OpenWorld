using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(40f);
        SceneManager.LoadScene("House");
    }
}
