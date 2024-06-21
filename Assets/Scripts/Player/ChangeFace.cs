using UnityEngine;

public class ChangeFace : MonoBehaviour
{

    public GameObject face;

    public void ChangeRosto(int expresao)
    {
        //float valor = Mathf.Clamp(expresao, 0, 3);
       // face.GetComponent<Renderer>().materials[1].mainTextureOffset = new Vector2(0, 0.25f * valor);
        Debug.Log("MUDOU");
    }
}
