using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript2 : MonoBehaviour
{
    public Rigidbody playerBody;
    public float tereSpeed, aquaSpeed, gravity, jumpHeight, groundDistance;
    public bool Frozen, inSub, isGrounded;
    public LayerMask playerMask;
    public Transform groundCheck;
    public GameObject cam;
    Vector3 direction;
    public float playerHeightOffset;

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
                //sets the freeze position constrains, since we're moving with transform. needed for rigidbody to work.
                playerBody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
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
                direction *= Time.deltaTime * aquaSpeed;                          
                              
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
                //sets the freeze position constrains, since we're moving with transform. needed for parenting to work.
                playerBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

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
                
                direction *= Time.deltaTime * (tereSpeed / 5);
                print(direction);
                transform.localPosition = new Vector3(transform.localPosition.x, playerHeightOffset, transform.localPosition.z);
                transform.position += new Vector3(direction.x, transform.localPosition.y, direction.z); //applies impulse force to all movements
            }
            
        }
        else //if Frozen is true
        {

        }
    }
}
