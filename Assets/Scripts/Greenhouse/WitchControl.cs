using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WitchControl : MonoBehaviour
{
    private CharacterController witchController;
    private Animator anim; 

    [SerializeField] private float speed; // velocidade de movimentação
    [SerializeField] private float rotSpeed; // velocidade de rotação
    [SerializeField] private float gravity; // gravidade de movimentação

    private float rotation; // rotação do personagem
    private Vector3 direction; // direção do personagem


    private void Awake() // Utilizei o awake por ser mais eficiente, real ou barça?
    {
        witchController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
    }


    void Movement()
    {
        
            if(Input.GetKey(KeyCode.W))
            {
                direction = Vector3.forward * speed;
                anim.SetInteger("transition", 1);
            }

            if(Input.GetKeyUp(KeyCode.W)) 
            {
                direction = Vector3.zero;
                anim.SetInteger("transition", 0);
            }

            rotation += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime; // posição que apertamos para esquerda e direita
            transform.eulerAngles = new Vector3(0, rotation, 0); // manter a rotação apenas no y
            direction.y -= gravity * Time.deltaTime; // forçando o jogador a ficar no chão
            direction = transform.TransformDirection(direction); // pega a posição do jogador e atualiza para a rotação global
            witchController.Move(direction * Time.deltaTime);
    }


}
