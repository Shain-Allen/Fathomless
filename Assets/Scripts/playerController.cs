using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController controller;

    public float Speed = 12f;
    public float Gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask, seaMask;

    Vector3 velocity;
    public bool isGrounded, seaGrounded;
    public bool Frozen;

    public GameObject Sub;
    public Rigidbody subRb;
    Vector3 totalMovementDirection;
    Vector3 previousSubmarineRotation;

    private void Start()
    {
        Frozen = false;
        subRb = Sub.GetComponent<Rigidbody>();
        previousSubmarineRotation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Frozen)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            seaGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, seaMask); //related to the check

            if (isGrounded && velocity.y < 0)
            {
                Gravity = -27f;
                velocity.y = -2f;
            }

            if (seaGrounded && velocity.y < 0) //related to the check
            {
                Gravity = -10f;
                velocity.y = -1.6f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * Speed * Time.deltaTime);

            if (isGrounded) //related to the check
            {
                Speed = 5f;
            }
            else
            {
                if (seaGrounded)
                {
                    Speed = 2.5f;
                }
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * Gravity);
            }
            else //related to the check
            {
                if (Input.GetButtonDown("Jump") && seaGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * Gravity) * 6 / 5;
                }
            }

            velocity.y += Gravity * Time.deltaTime;

            if (isGrounded)
            {
                totalMovementDirection =  controller.velocity + subRb.velocity;
                float submarineHorizontalRotChange = subRb.rotation.eulerAngles.y - previousSubmarineRotation.y;
                transform.rotation *= Quaternion.Euler(0, submarineHorizontalRotChange, 0);
                previousSubmarineRotation = subRb.rotation.eulerAngles;

            }
            else
            {
                totalMovementDirection = controller.velocity;
            }
            controller.Move((totalMovementDirection + velocity) * Time.deltaTime);
        }
        else
        {

        }
    }
}
