using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoRosto : MonoBehaviour
{

    public GameObject Rosto;
    public int Expressao;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float valor = Mathf.Clamp(Expressao, 0, 3);
        Rosto.GetComponent<Renderer>().materials[1].mainTextureOffset = new Vector2(0, 0.25f* valor);
        
    }
}
