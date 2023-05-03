using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript2 : MonoBehaviour
{
    public Rigidbody playerBody;
    public float speed, gravity, jumpHeight, groundDistance;
    public bool Frozen, inSub, isGrounded;
    public LayerMask playerMask;
    public Transform groundCheck;
    public GameObject cam;
    Vector3 velocity;

    public GameObject sub;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Frozen) // if Frozen is false
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~playerMask); //checks to see if the ground check is contacting anything except the player mask

            float x = Input.GetAxis("Horizontal"); //sets up the x axis (A and D)
            float z = Input.GetAxis("Vertical"); //sets up the Z axis (W and S)

            if (x > 0) //pressed D
            {
                velocity += playerBody.transform.right;
            }
            if (x < 0) //pressed A
            {
                velocity += -playerBody.transform.right;
            }
            if (z > 0) //pressed W
            {
                velocity += playerBody.transform.forward;
            }
            if (z < 0) //pressed S
            {
                velocity += -playerBody.transform.forward;
            }
            
            velocity.Normalize();
            velocity *= Time.deltaTime * speed;
            
            if (inSub) //gravity if the player is in the sub, and set player to be child of sub
            {
                transform.SetParent(sub.transform);
                velocity.y -= gravity * Time.deltaTime;
            }
            else //gravity otherwise, and remove parent
            {
                transform.SetParent(null);
                velocity.y -= gravity * Time.deltaTime / 2;
            }

            if (isGrounded && Input.GetButtonDown("Jump")) //if you press space while grounded
            {
                velocity.y += jumpHeight;
            }
            print(velocity);
            playerBody.AddForce(velocity, ForceMode.Impulse); //applies impulse force to all movements
        }
        else //if Frozen is true
        {

        }
    }
}
