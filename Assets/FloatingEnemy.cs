using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemy : MonoBehaviour
{
    public float floatSpeed = 1f; // Velocidade de flutuação
    public float floatHeight = 0.5f; // Altura de flutuação

    private void Update()
    {
        // Calcula a posição de flutuação ao longo do eixo Y usando uma função senoidal
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Atualiza a posição do inimigo mantendo a mesma posição ao longo dos eixos X e Z
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
