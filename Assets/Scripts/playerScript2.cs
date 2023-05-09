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
    Vector3 direction;

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
            if (!inSub)
            {
                transform.SetParent(null);
                
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~playerMask); //checks to see if the ground check is contacting anything except the player mask

                if (Input.GetKey(KeyCode.D)) //pressed D
                {
                    direction += playerBody.transform.right;
                }
                if (Input.GetKey(KeyCode.A)) //pressed A
                {
                    direction += -playerBody.transform.right;
                }
                if (Input.GetKey(KeyCode.W)) //pressed W
                {
                    direction += playerBody.transform.forward;
                }
                if (Input.GetKey(KeyCode.S)) //pressed S
                {
                    direction += -playerBody.transform.forward;
                }

                direction.Normalize();
                direction *= Time.deltaTime * speed;                          
                              
                direction.y -= gravity * Time.deltaTime / (10 / 3);
                
                if (isGrounded && Input.GetButtonDown("Jump")) //if you press space while grounded
                {
                    direction.y += jumpHeight;
                }
                //print(velocity);
                playerBody.AddForce(direction, ForceMode.Impulse); //applies impulse force to all movements
            }
            else
            {
                transform.SetParent(sub.transform);

                if (Input.GetKey(KeyCode.D)) //pressed D
                {
                    direction += playerBody.transform.right;
                }
                if (Input.GetKey(KeyCode.A)) //pressed A
                {
                    direction += -playerBody.transform.right;
                }
                if (Input.GetKey(KeyCode.W)) //pressed W
                {
                    direction += playerBody.transform.forward;
                }
                if (Input.GetKey(KeyCode.S)) //pressed S
                {
                    direction += -playerBody.transform.forward;
                }
                
                direction *= Time.deltaTime * (speed / 5);
                transform.localPosition += new Vector3(direction.x, direction.y, direction.z); //applies impulse force to all movements
            }
            
        }
        else //if Frozen is true
        {

        }
    }
}
