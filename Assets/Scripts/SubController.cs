using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubController : MonoBehaviour
{

    public float speed;
    public float minBackwardsSpeed;
    public float maxForwardSpeed;
    public float subAcceleration = 3f;
    public float subBreakForce = 5f;

    public float xRotSpeed = 1;

    public float verticalSpeed;
    public float minVertSpeed;
    public float maxVertSpeed;

    private bool verMove;
    private bool moveUp;
    private bool moveDown;
    private float rawVertInput;
    private Vector2 rawWASDInput;

    public bool isSub;

    public Rigidbody subRigi;

    public GameObject subCam;
    public float subMouseSensitivity = 100f;
    private float xRotation;

    public float delayTime;
    private float delayTimer;

    bool isMoving;

    public float dampening;
    public float velocityThreshold;

    public GameObject Player;
    public GameObject playerContainer;
    public GameObject chasePoint;
    public bool follow;
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

    public AudioSource movementSound;

    public static SubController instance;
    public static SubController Instance => instance;

    public Fathomless fathomlessInputActions;

    private void Awake()
    {
        instance = this;

        fathomlessInputActions = new Fathomless();
        fathomlessInputActions.Player_AMap.Enable();
    }

    private void OnEnable()
    {
        fathomlessInputActions.Player_AMap.Move.performed += OnMove;
        fathomlessInputActions.Player_AMap.Move.canceled += OnMove;
        fathomlessInputActions.Player_AMap.SubElevate.performed += OnSubElevate;
        fathomlessInputActions.Player_AMap.SubElevate.canceled += OnSubElevate;
        fathomlessInputActions.Player_AMap.Look.performed += OnLook;
        fathomlessInputActions.Player_AMap.Look.canceled += OnLook;
        fathomlessInputActions.Player_AMap.LeavePost.performed += OnLeavePost;

        GetComponentInChildren<TurretSystem>().TurretSystemEnable();
    }

    private void OnDisable()
    {
        fathomlessInputActions.Player_AMap.Move.performed -= OnMove;
        fathomlessInputActions.Player_AMap.Move.canceled -= OnMove;
        fathomlessInputActions.Player_AMap.SubElevate.performed -= OnSubElevate;
        fathomlessInputActions.Player_AMap.SubElevate.canceled -= OnSubElevate;
        fathomlessInputActions.Player_AMap.Look.performed -= OnLook;
        fathomlessInputActions.Player_AMap.Look.canceled -= OnLook;
        fathomlessInputActions.Player_AMap.LeavePost.performed -= OnLeavePost;
        GetComponentInChildren<TurretSystem>().TurretSystemDisabled();
    }
    
    Vector2 mouse;
    
    private void OnLook(InputAction.CallbackContext context)
    {
        if (!isSub) return;
        
        
        mouse = context.ReadValue<Vector2>() * subMouseSensitivity;
    }
    
    private void OnMove(InputAction.CallbackContext context)
    {
        if (!isSub) return;
        
        rawWASDInput = context.ReadValue<Vector2>();
    }
    
    private void OnSubElevate(InputAction.CallbackContext context)
    {
        if (!isSub) return;

        rawVertInput = context.ReadValue<float>();
        
        //Moves sub up
        moveUp = rawVertInput >= 0.1f;

        if (moveUp)
        {
            verMove = true;
        }
        else
        {
            verMove = false;
        }


        if(rawVertInput == 0)
        {
            verticalSpeed = rawVertInput;
            verMove = false;
        }

        //Moves sub down
        moveDown = rawVertInput <= -0.1f;

        if (moveDown)
        {
            verMove = true;
        }
        else
        {
            verMove = false;
        }
    }

    private void OnLeavePost(InputAction.CallbackContext context)
    {
        TurretInteractable.Instance.OnLeavePost();
        PilotPanelInteractable.Instance.OnLeavePost();
        verticalSpeed = 0f;
        subRigi.velocity = Vector3.zero;
    }

    void FixedUpdate()//this will use simple keycodes for now, but we can use this for the unity input system if we want. This is just to see the best way to control the sub
    {

        verticalSpeed += rawVertInput;

        if (isSub)//checks to see if the player has pressed e on the control panel
        {
            SubControl();
        }
        else
        {
            subRigi.isKinematic = false;
            speed = 0f;
            verticalSpeed = 0f;
            SlowSub();
        }

        if(follow)  //this is the part of the code that lets the sub follow the animated follow point
        {

            //followAnim.SetBool(AnimationName, true);
            //transform.position = Vector3.MoveTowards(transform.position, followPoint.transform.position, followSpeed);
            transform.position = Vector3.Lerp(transform.position, followPoint.transform.position, 0.5f);
            transform.LookAt(followPoint.transform);

            float distance = Vector3.Distance(transform.position, followPoint.transform.position);
        }


        if (!verMove) //this checks to see if the sub is moving up or down, if not, the sub will slow to a stop
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
        if(resetSubRot) //checks to see if subs rotation is reset
        {
            ResetRotation();
        }
    }
    
     private void SubControl()
    {
        subRigi.isKinematic = false;
        //this will ensure that the sub will always move forward. 0 speed will stop the throttle of the speed.
        subRigi.AddForce(transform.forward * speed);

        subRigi.AddForce(transform.up * verticalSpeed);
        
        //Handle Sub rotation left right
        float rotationY = mouse.x * xRotSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
        subRigi.MoveRotation(subRigi.rotation * rotation);
        
        // Handle the up down rot of player cam in sub seat
        xRotation -= mouse.y * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
        subCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //This will increase the speed of sub
        
        switch (rawWASDInput.y)
        {
            case > 0:
                speed += subAcceleration * rawWASDInput.y;
                speedControl(maxForwardSpeed);
                movementSound.Play();
                break;
            //This will decrease the speed of sub
            case 0:
                speed = 0;
                movementSound.Stop();
                break;
            case < 0:
                speed += subAcceleration * rawWASDInput.y;
                speedControl(minBackwardsSpeed);
                movementSound.Play();
                break;
        }

        /*//This will set the sub speed to zero when the thrust is turned off
        if (Mathf.Abs(speed) >= minSpeed)
        {

            //right now the sub will stop immediately, but we can probably find a way to make it gradually stop.
            speed = minSpeed * Mathf.Sign(rawWASDInput.y);

            //SlowSub();
        }*/

        verticalSpeed = Mathf.Clamp(verticalSpeed, minVertSpeed, maxVertSpeed);
    }

    public void ResetRotation() //Will reset the rotation of the sub if called
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), .05f);
        StartCoroutine(resetRotTimer());
    }

    private void SlowSub()
    {
        movementSound.Stop();
        //lerps the velocity so that the sub will slow down.
        subRigi.velocity = Vector3.Lerp(subRigi.velocity, Vector3.zero, dampening * Time.deltaTime);

        //gets the speed of the sub and makes sure it will come to a slow stop. Also checks to see if sub is moving.
        if (subRigi.velocity.magnitude < velocityThreshold && isMoving == true)
        {

            delayTimer += Time.deltaTime;//this is the delay timer to check is the delay will stop it (needs work)

            if (delayTimer >= delayTime) //will stop sub after delay timer
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
    
    public void SetSubPosAndRot(Vector3 pos, Quaternion rot)
    {
        Debug.Log("setting sub pos");
        subRigi.velocity = Vector3.zero;
        subRigi.angularVelocity = Vector3.zero;
        transform.position = pos;
        transform.rotation = rot;
    }

    public void speedControl(float speedCap)
    {
        //This will set the sub speed to zero when the thrust is turned off
        if (Mathf.Abs(speed) >= speedCap)
        {

            //right now the sub will stop immediately, but we can probably find a way to make it gradually stop.
            speed = speedCap * Mathf.Sign(rawWASDInput.y);

            //SlowSub();
        }
    }
}
