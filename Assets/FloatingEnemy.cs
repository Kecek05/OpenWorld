using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemy : MonoBehaviour
{
    public float floatSpeed = 1f; // Velocidade de flutua��o
    public float floatHeight = 0.5f; // Altura de flutua��o

    private void Update()
    {
        // Calcula a posi��o de flutua��o ao longo do eixo Y usando uma fun��o senoidal
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Atualiza a posi��o do inimigo mantendo a mesma posi��o ao longo dos eixos X e Z
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
