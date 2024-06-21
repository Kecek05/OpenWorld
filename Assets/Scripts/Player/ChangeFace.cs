using UnityEngine;

public class ChangeFace : MonoBehaviour
{

    public GameObject face;

    public enum Faces
    {
        Open = 0,
        Close = 3,
        Purple = 2,
        Screaming = 1,
    }

    public void ChangeRosto(Faces _faceEnum)
    {
        int faceEnum = (int)_faceEnum;
        int valor = Mathf.Clamp(faceEnum, 0, 3);
        face.GetComponent<Renderer>().materials[1].mainTextureOffset = new Vector2(0, 0.25f * valor);
    }
}
