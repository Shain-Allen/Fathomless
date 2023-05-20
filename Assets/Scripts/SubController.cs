using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{

    public float speed;
    public float minSpeed;
    public float maxSpeed;

    private float xRotSpeed = 1;
    private float yRotSpeed = 1;

    public float verticalSpeed;
    public float minVertSpeed;
    public float maxVertSpeed;

    private bool verMove;
    private bool moveUp;
    private bool moveDown;

    public bool isSub;

    public Rigidbody subRigi;

    public GameObject subCam;
    public float subMouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;


    public float delayTime;
    private float delayTimer;

    bool isMoving;

    public float dampening;
    public float velocityThreshold;

    public GameObject Player;
    public GameObject playerContainer;

    public bool follow;
    public GameObject followPoint;
    public float followSpeed;
    public Animator followAnim;


    private void Start()
    {
        
    }

    void FixedUpdate()//this will use simple keycodes for now, but we can use this for the unity input system if we want. This is just to see the best way to control the sub
    {
        if (isSub)//checks to see if the player has pressed e on the control pannel
        {
            SubControl();
            SubCameraControl();
        }
        else
        {
            subRigi.isKinematic = false;
        }

        if(followAnim.GetBool("StartAnim") == true)
        {

            transform.position = Vector3.MoveTowards(transform.position, followPoint.transform.position, followSpeed);
            transform.LookAt(followPoint.transform);

            float distance = Vector3.Distance(transform.position, followPoint.transform.position);
            Debug.Log(distance);

            if(distance >= 100)
            {
                followSpeed = 2;
            }
            if (distance <= 100)
            {
                followSpeed = 1;
            }

        }

        if(!verMove)
        {
            SlowSub();

            if (moveUp == false && moveDown != true)
            {
                verticalSpeed -= .3f;
                if (verticalSpeed <= 0)
                {
                    verticalSpeed = 0;
                }
            }

            if (moveDown == false && moveUp != true)
            {
                verticalSpeed += .3f;
                if (verticalSpeed >= 0)
                {
                    verticalSpeed = 0;
                }
            }
        }


        //Debug.Log(subRigi.velocity.magnitude);
    }

    private void Update()
    {
        verticalSubControl();
    }

    public void SubCameraControl()
    {

        Cursor.visible = false;

        float mouseX = Input.GetAxis("Mouse X") * subMouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * subMouseSensitivity * Time.deltaTime;

        yRotation -= mouseY;
        xRotation -= mouseX;

        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //yRotation = Mathf.Clamp(-90, yRotation, 50f);

        //yRotation = mouseX;


        subCam.transform.localRotation = Quaternion.Euler(yRotation, -xRotation, 0);
    }


    public void verticalSubControl()
    {
        //Moves sub up
        if (Input.GetKey(KeyCode.W))
        {
            verticalSpeed += 1f;
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            verticalSpeed -= 1f;
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }

    }

     public void SubControl()
    {
        subRigi.isKinematic = false;
        //this will ensure that the sub will allways move forward. 0 speed will stop the throttle of the speed.
        subRigi.AddForce(transform.forward * speed);

        subRigi.AddForce(transform.up * verticalSpeed);

        //float rotationX = Input.GetAxis("Vertical") * yRotSpeed;
        float rotationY = Input.GetAxis("Horizontal") * xRotSpeed;
        Quaternion rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
        subRigi.MoveRotation(subRigi.rotation * rotation);


        //This will increase the speed of sub
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed += 3f;
        }

        //This will decrease the speed of sub
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed -= 5f;
        }


        //this will cap the speed to the set max speed
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }

        //This will set the sub speed to zero when the thrust is tunred off
        if (speed <= minSpeed)
        {

            //right now the sub will stop immediatly, but we can probably find a way to make it gradually stop.
            speed = minSpeed;

            SlowSub();


        }

        //this will cap the decent speed to the set max decent speed
        if (verticalSpeed >= maxVertSpeed)
        {
            verticalSpeed = maxVertSpeed;
        }

        if (verticalSpeed >= minVertSpeed)
        {
            verticalSpeed = minVertSpeed;
        }
        /*//This will set the sub decent speed to zero when the thrust is tunred off
        if (verticalSpeed <= minVertSpeed)
        {

            //right now the sub will stop immediatly, but we can probably find a way to make it gradually stop.
            verticalSpeed = minVertSpeed;
            subRigi.velocity = Vector3.zero;
            subRigi.angularVelocity = Vector3.zero;
        }*/
    }

   public void SlowSub()
    {
        //lerps the velocity so that the sub will slow down.
        subRigi.velocity = Vector3.Lerp(subRigi.velocity, Vector3.zero, dampening * Time.deltaTime);

        //gets the speed of the sub and makes sure it will come to a slow stop. Also checks to see if sub is moving.
        if (subRigi.velocity.magnitude < velocityThreshold && isMoving == true)
        {

            delayTimer += Time.deltaTime;//this is the delay timer to check is the delay will stop it (needs work)

            if (delayTimer >= delayTime) //will stop sub after delat timer
            {
                subRigi.velocity = Vector3.zero;
                subRigi.angularVelocity = Vector3.zero;
            }

        }
        else
        {
            delayTimer = 0f;
        }
    }
}
