using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //Raw input data
    private Vector2 direction;
    private Vector2 mouseInput;
    private float jumpInput;

    //Player States
    [Header("Player States")]
    public bool frozen;
    public bool inSub;


    //Player movement Vars
    [Header("Player Adjustments")]
    [Tooltip("Doesn't do anything yet")]
    [Min(0)][SerializeField] private float maxSpeed = 20f;
    private float currentSpeed;
    [Tooltip("This is the Acceleration of the player while not in the water")]
    [Min(0)][SerializeField] private float insideAcceleration = 1f;
    [Tooltip("This is the Acceleration of the player while in the water")]
    [Min(0)][SerializeField] private float outsideAcceleration = 2f;
    private Vector3 moveVector;
    [Tooltip("This is the jump height of the player while out of the water")]
    [Min(0)][SerializeField] private float insideJumpHeight = 1f;
    [Tooltip("This is the Time to apex of the jump while out of the water")]
    [Min(0)][SerializeField] private float insideJumpTime = 0.5f;
    [Tooltip("This is the jump height of the player while in the water")]
    [Min(0)][SerializeField] private float outsideJumpHeight = 1f;
    [Tooltip("This is the Time to apex of the jump while in the water")]
    [Min(0)][SerializeField] private float outsideJumpTime = 0.5f;
    private float outsideInitialJumpVelocity;
    private float insideInitialJumpVelocity;


    //Other Player Vars
    [Header("Other Player Adjustments")]
    [Min(0)][SerializeField] private float mouseSensitivity = 100f;
    private float yRotation;
    [Min(0)][SerializeField] private float reloadSpeed;


    //Physics Vars
    [Header("Physics adjustments")]
    [Min(0)][SerializeField] private float groundedGravity = -0.05f;
    private float outsideGravity = -9.8f;
    private float insideGravity = -9.8f;
    [Min(0)][SerializeField] private float insideDrag = 2f;
    [Min(0)][SerializeField] private float outsideDrag = 6f;


    //Player references
    [Header("Player Component References")]
    [SerializeField] private CharacterController characterController;
    public GameObject cam;
    public GameObject flashLight;
    public GameObject HarpoonGun;
    public static PlayerScript instance;
    public static PlayerScript Instance => instance;


    //Sub References
    [Header("Sub Component References")]
    public GameObject sub;
    [HideInInspector] public SubController subController;
    public GameObject playerContainer;
    public float spaceRadiusX;
    public float spaceRadiusZ;


    //Other References
    [Header("Other References")]
    public GameObject MinimapCamera;


    //expression body statement to get input for player movement
    public void OnMove(InputValue inputValue)
    {
        if (!frozen) direction = inputValue.Get<Vector2>();
    }

    //expression body statement to get input for when the player Jumps
    private void OnJump(InputValue inputValue)
    {
        if (!frozen) jumpInput = inputValue.Get<float>();
    }

    //expression body statement to get input for when the player moves their mouse
    private void OnLook(InputValue inputValue)
    {
        if (!frozen) mouseInput = inputValue.Get<Vector2>() * mouseSensitivity * Time.deltaTime;
    }

    //handles the firing of the handheld harpoon gun (if that gets added)
    private void OnFire(InputValue inputValue)
    {
        if (GameManager.Instance.playerReloadPercentage >= 100f)
        {
            GameManager.Instance.playerReloadPercentage = 0;
        }
    }

    private void Awake()
    {
        instance = this;
        SetupJumpVariables();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        subController = sub.GetComponent<SubController>();
        playerContainer = subController.playerContainer;
        transform.position = playerContainer.transform.position;
        characterController = GetComponent<CharacterController>();

        if (spaceRadiusX < 1)
        {
            spaceRadiusX = 1;
        }
        if (spaceRadiusZ < 1)
        {
            spaceRadiusZ = 1;
        }

        transform.SetParent(sub.transform);
    }

    private void SetupJumpVariables()
    {
        float timeToApex = outsideJumpTime / 2;
        outsideGravity = (2 * outsideJumpHeight) / Mathf.Pow(timeToApex, 2);
        outsideInitialJumpVelocity = (2 * outsideJumpHeight) / timeToApex;

        timeToApex = insideJumpTime / 2;
        insideGravity = (2 * insideJumpHeight) / Mathf.Pow(timeToApex, 2);
        insideInitialJumpVelocity = (2 * insideJumpHeight) / timeToApex;
    }

    private void FixedUpdate()
    {
        float speedRatio = characterController.velocity.magnitude / maxSpeed;
        float volume = Mathf.Clamp01(speedRatio);

        GlobalSoundsManager.instance.audiosources[10].volume = volume + 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen) // if frozen is false
        {
            if (!inSub)
            {
                characterController.enabled = true;
                moveVector += new Vector3(direction.x, 0, direction.y) * outsideAcceleration;
                if (!flashLight.activeSelf)
                    flashLight.SetActive(true);
                characterController.Move(transform.TransformDirection(moveVector) * Time.deltaTime);
                HandleGravity(outsideGravity);
                HandleDrag(outsideDrag);
                HandleJump(outsideInitialJumpVelocity);
            }
            else
            {
                characterController.enabled = false;
                if (flashLight.activeSelf)
                    flashLight.SetActive(false);
                InsideMovement(new Vector3(direction.x, 0, direction.y));
            }

            MoveCamera();

            GunLogic();
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            transform.position = SubController.instance.playerContainer.transform.position;
        }
    }
    
    private void InsideMovement(Vector3 moveVector)
    {
        transform.SetParent(sub.transform);

        moveVector *= Time.deltaTime * (insideAcceleration / 5);

        //establishes ellipse which represents player movement space
        Vector3 newPos = transform.localPosition + moveVector;
        Vector3 offset = newPos - playerContainer.transform.localPosition;
        offset.x /= spaceRadiusX;
        offset.z /= spaceRadiusZ;
        //compare distance of player to origin of ellipse to see if player would be out of bounds. if so, then skip adding movement.
        if (offset.magnitude < 1.0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, playerContainer.transform.localPosition.y, transform.localPosition.z);
            transform.position += transform.TransformDirection(moveVector);
        }
        else if (offset.magnitude > 1f)
        {
            transform.position = playerContainer.transform.position;
        }
    }

    private void HandleJump(float initialJumpVelocity)
    {
        if (jumpInput > 0f && characterController.isGrounded)
        {
            moveVector.y = initialJumpVelocity;
        }
    }

    private void HandleGravity(float gravity)
    {
        //apply a proper outsideGravity if the player is grounded or not
        if (characterController.isGrounded)
        {
            moveVector.y = -groundedGravity;
        }
        else
        {
            moveVector.y -= gravity * Time.deltaTime;
        }
    }

    private void HandleDrag(float drag)
    {
        moveVector.x = Mathf.Lerp(moveVector.x, direction.x, Time.deltaTime * drag);
        moveVector.z = Mathf.Lerp(moveVector.z, direction.y, Time.deltaTime * drag);
    }

    private void GunLogic()
    {
        if (GameManager.Instance.playerReloadPercentage < 100)
            GameManager.Instance.playerReloadPercentage += reloadSpeed * Time.deltaTime;
        else if (GameManager.Instance.playerReloadPercentage > 100)
            GameManager.Instance.playerReloadPercentage = 100;
    }

    private void MoveCamera()
    {
        yRotation -= mouseInput.y;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        CameraManager.instance.MoveCam(yRotation);
        transform.Rotate(Vector3.up * mouseInput.x);
    }

    //Draws the ellipse to the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.parent != null ? transform.parent.localToWorldMatrix : Matrix4x4.identity;
        Gizmos.DrawWireMesh(CreateEllipseMesh(), playerContainer.transform.localPosition);
        Gizmos.matrix = Matrix4x4.identity;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(new Vector3(direction.x, 0, direction.y)) * 50);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10);
    }
    
    private Mesh CreateEllipseMesh()
    {
        int numSegments = 32;
        float angleStep = 360.0f / numSegments;

        Vector3[] vertices = new Vector3[numSegments + 1];
        Vector3[] normals = new Vector3[numSegments + 1];
        int[] indices = new int[numSegments * 2];

        vertices[0] = Vector3.zero;
        normals[0] = Vector3.up;
        for (int i = 0; i < numSegments; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = Mathf.Sin(angle * Mathf.Deg2Rad);
            vertices[i + 1] = new Vector3(x * spaceRadiusX, 0.0f, z * spaceRadiusZ);
            normals[i + 1] = Vector3.up;

            if (i < numSegments - 1)
            {
                indices[i * 2] = i;
                indices[i * 2 + 1] = i + 1;
            }
            else
            {
                indices[i * 2] = i;
                indices[i * 2 + 1] = 0;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.SetIndices(indices, MeshTopology.Lines, 0);

        return mesh;
    }

    public void ResetMoveVector()
    {
        moveVector = Vector3.zero;
        StartCoroutine(freezePlayer());
    }

    // temporarily freezes the player movement for the specified time 
    public IEnumerator freezePlayer(float time = 0.05f)
    {
        frozen = true;
        yield return new WaitForSeconds(time);
        frozen = false;
    }

    public bool Vector3Compare(Vector3 firstVector3, Vector3 secondVector3, float threshold = 0.1f)
    {
        // check to see how close the vectors are to equal
        float sqrDistance = (firstVector3 - secondVector3).sqrMagnitude;

        float sqrThres = threshold * threshold;

        if (sqrDistance > 1f)
        {
            Debug.Log("BigNumber");
        }
        
        Debug.Log(sqrDistance);

        // if they are equal enough (via threshold) then return true
        return sqrDistance < sqrThres;
    }
}
