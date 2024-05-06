using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float followRange = 5f; // Raio de alcance para seguir o jogador
    public float floatSpeed = 1f; // Velocidade de flutuação
    public float floatHeight = 0.5f; // Altura de flutuação
    public GameObject itemToDrop; // Objeto de item a ser dropado
    private Transform player;
    private bool isFrozen = false;
    private float freezeTimer = 0f;
    private float freezeDuration = 3f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isFrozen)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Verifica se o jogador está dentro do raio de alcance
            if (distanceToPlayer <= followRange)
            {
                // Calcula a direção em que o inimigo deve se mover (direção do jogador - direção do inimigo)
                Vector3 direction = (player.position - transform.position).normalized;

                // Move o inimigo na direção do jogador
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            // Contagem regressiva do tempo de congelamento
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                isFrozen = false;
            }
        }

        // Calcula a posição de flutuação ao longo do eixo Y usando uma função senoidal
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Atualiza a posição do inimigo mantendo a mesma posição ao longo dos eixos X e Z
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruir o inimigo
            FreezePlayer();
        }
    }

    private void FreezePlayer()
    {
        isFrozen = true;
        freezeTimer = freezeDuration;
        Debug.Log("Player Congelado!");

        // Adicione aqui qualquer lógica adicional que você queira executar quando o jogador é congelado.
    }

    private void OnDestroy()
    {
        // Instanciar o item dropado quando o inimigo for destruído
        if (itemToDrop != null)
        {
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }
}