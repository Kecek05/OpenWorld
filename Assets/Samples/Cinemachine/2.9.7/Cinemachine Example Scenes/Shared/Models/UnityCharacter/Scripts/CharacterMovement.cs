﻿using UnityEngine;

namespace Cinemachine.Examples
{

    [AddComponentMenu("")] // Don't display in add component menu
    public class CharacterMovement : MonoBehaviour
    {



        public bool useCharacterForward = false;
        public bool lockToCameraForward = false;
        public float turnSpeed = 10f;
        public KeyCode sprintJoystick = KeyCode.JoystickButton2;
        public KeyCode sprintKeyboard = KeyCode.Space;

        private float turnSpeedMultiplier;
        private float speed = 2f;
        private float direction = 0f;
        private bool isSprinting = false;
        private Animator anim;
        private Vector3 targetDirection;
        private Vector2 input;
        private Quaternion freeRotation;
        private Camera mainCamera;
        private float velocity;

        // variáveis para congelar o jogador
        private bool isFrozen = false;
        private float freezeTimer = 0f;
        private float freezeDuration = 3f;


        //private float reducedSpeed = 0.5f; // velocidade reduzida
        //private float normalSpeed = 1f; // velocidade normal
        //private bool isSpeedReduced = false; // ta falando se ta ou nao reduzida
        //private float speedReductionDuration = 3f; // duração da redução de velocidade reduzida
        // Use this for initialization



        void Start()
        {
            anim = GetComponent<Animator>();
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            if (!isFrozen)
            {
                input.x = Input.GetAxis("Horizontal");
                input.y = Input.GetAxis("Vertical");

                // set speed to both vertical and horizontal inputs
                if (useCharacterForward)
                    speed = Mathf.Abs(input.x) + input.y;
                else
                    speed = Mathf.Abs(input.x) + Mathf.Abs(input.y);

                speed = Mathf.Clamp(speed, 0f, 1f);
                speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
                anim.SetFloat("Speed", speed);

                if (input.y < 0f && useCharacterForward)
                    direction = input.y;
                else
                    direction = 0f;

                anim.SetFloat("Direction", direction);

                // set sprinting
                isSprinting = ((Input.GetKey(sprintJoystick) || Input.GetKey(sprintKeyboard)) && input != Vector2.zero && direction >= 0f);
                anim.SetBool("isSprinting", isSprinting);

                // Update target direction relative to the camera view (or not if the Keep Direction option is checked)
                UpdateTargetDirection();
                if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
                {
                    Vector3 lookDirection = targetDirection.normalized;
                    freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
                    var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
                    var eulerY = transform.eulerAngles.y;

                    if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
                    var euler = new Vector3(0, eulerY, 0);

                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
                }
            }
            else
            {
                // fazendo com q se estiver congelado não se movimente
                speed = 0f;
                anim.SetFloat("Speed", speed);
            }

            // quanto tempo de congelamento
            if (isFrozen)
            {
                freezeTimer -= Time.deltaTime;
                if (freezeTimer <= 0)
                {
                    isFrozen = false;
                }
            }

            //if (isSpeedReduced)
            //{
            //    speed = reducedSpeed;
            //    speedReductionDuration -= Time.deltaTime;
            //    if (speedReductionDuration <= 0)
            //    {
            //        speed = normalSpeed;
            //        isSpeedReduced = false;
            //        speedReductionDuration = 3f; // reinicia a duração da redução de velocidade
            //    }
            //}
#else
        InputSystemHelper.EnableBackendsWarningMessage();
#endif
        }

        public virtual void UpdateTargetDirection()
        {
            if (!useCharacterForward)
            {
                turnSpeedMultiplier = 1f;
                //var forward = mainCamera.transform.TransformDirection(Vector3.forward);
                //forward.y = 0;
//
                //get the right-facing direction of the referenceTransform
             //   var right = mainCamera.transform.TransformDirection(Vector3.right);

                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
               // targetDirection = input.x * right + input.y * forward;
            }
            else
            {
                turnSpeedMultiplier = 0.2f;
                var forward = transform.TransformDirection(Vector3.forward);
                forward.y = 0;

                //get the right-facing direction of the referenceTransform
                var right = transform.TransformDirection(Vector3.right);
                targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
            }
        }

        // congelando o jogador
        public void FreezePlayer()
        {
            isFrozen = true;
            freezeTimer = freezeDuration;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                FreezePlayer(); // levando pra variavel de congelar o jogador
            }

            if (collision.gameObject.CompareTag("Caranguejo"))
             {
            // Reduz a velocidade do jogador
             speed -= 2f;

            // Garante que a velocidade não seja menor que zero
            speed = Mathf.Max(speed, 0f);
             }
        }

       
    }

}
