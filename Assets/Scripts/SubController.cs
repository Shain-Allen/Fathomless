using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubController : MonoBehaviour
{

    public float speed;
    public float minSpeed;
    public float maxSpeed;

    public float xRotSpeed = 1;

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
    public string AnimationName;
    public GameObject followPoint;
    public float followSpeed;
    public Animator followAnim;

    public GameObject endFollowPoint_1;
    public GameObject endFollowPoint_2;

    private float downRotSpeed;
    private float upRotSpeed;

    public float animTopFollowSpeed;
    public float animBottomSpeed;

    public bool resetSubRot;
    public HatchInteractable hatchScript;

    public AudioSource movementSound;

    public static SubController instance;
    public static SubController Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        instance = this;
    }

    void FixedUpdate()//this will use simple keycodes for now, but we can use this for the unity input system if we want. This is just to see the best way to control the sub
    {
        if (isSub)//checks to see if the player has pressed e on the control pannel
        {
            SubControl();
        }
        else
        {
            subRigi.isKinematic = false;
            speed = 0f;
        }

        if(follow == true)  //this is the part of the code that lets the sub follow the animated follow point
        {

            followAnim.SetBool(AnimationName, true);
            transform.position = Vector3.MoveTowards(transform.position, followPoint.transform.position, followSpeed);
            transform.LookAt(followPoint.transform);

            float distance = Vector3.Distance(transform.position, followPoint.transform.position);

            //these are the different distances that the sub will look out for to controll the speed of when it is following the animated follow point
            if (distance >= 100)
            {
                followSpeed = animTopFollowSpeed;
            }
            if (distance <= 75)
            {
                followSpeed = animTopFollowSpeed / 2;
            }
            if (distance <= 50)
            {
                followSpeed = animBottomSpeed / 2;
            }
            if (distance <= 25)
            {
                followSpeed = animBottomSpeed / 4;
            }

        }


        if(!verMove) //this checks to see if the sub is moving up or down, if not, the sub will slow to a stop
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


    }

    private void Update() 
    {
        verticalSubControl();

        if(resetSubRot) //checks to see if subs rotation is reset
        {
            ResetRotation();
        }
    }

    public void verticalSubControl()
    {
        //Moves sub up
        if (Input.GetKey(KeyCode.LeftShift))
        {
            verticalSpeed += 1f;
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }

        //Moves sub down
        if (Input.GetKey(KeyCode.LeftControl))
        {
            verticalSpeed -= 1f;
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }
    }

    private void OnSubElevate(InputValue inputValue)
    {
        /*//Moves sub up
        if (inputValue.Get<float>() == 1f)
        {
            verticalSpeed += 1f;
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }

        //Moves sub down
        if (inputValue.Get<float>() == -1f)
        {
            verticalSpeed -= 1f;
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }*/
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
        if (Input.GetKey(KeyCode.W))
        {
            speed += 3f;
            movementSound.Play();
        }

        //This will decrease the speed of sub
        if (Input.GetKey(KeyCode.S))
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
    }

    public void ResetRotation() //Will reset the rotaion of the sub if called
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), .05f);
        StartCoroutine(resetRotTimer());
    }

    public void SlowSub()
    {
        movementSound.Stop();
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

    IEnumerator resetRotTimer() //timer for how long the reset rotation will be called
    {
        yield return new WaitForSeconds(2f);
        resetSubRot = false;
    }

    private void OnLook(InputValue inputValue)
    {
        Vector2 mouse = inputValue.Get<Vector2>() * subMouseSensitivity * Time.deltaTime;
        
        xRotation -= mouse.y;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        yRotation = mouse.x;

        subCam.transform.localRotation = Quaternion.Euler(xRotation, mouse.y, 0);
    }
}
