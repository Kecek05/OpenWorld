using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WitchControl : MonoBehaviour
{
    private CharacterController witchController;
    private Animator anim; 

    [SerializeField] private float speed; // velocidade de movimenta��o
    [SerializeField] private float rotSpeed; // velocidade de rota��o
    [SerializeField] private float gravity; // gravidade de movimenta��o

    private float rotation; // rota��o do personagem
    private Vector3 direction; // dire��o do personagem


    private void Awake() // Utilizei o awake por ser mais eficiente, real ou bar�a?
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

            rotation += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime; // posi��o que apertamos para esquerda e direita
            transform.eulerAngles = new Vector3(0, rotation, 0); // manter a rota��o apenas no y
            direction.y -= gravity * Time.deltaTime; // for�ando o jogador a ficar no ch�o
            direction = transform.TransformDirection(direction); // pega a posi��o do jogador e atualiza para a rota��o global
            witchController.Move(direction * Time.deltaTime);
    }


}
