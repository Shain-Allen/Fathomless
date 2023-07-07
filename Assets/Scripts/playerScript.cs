using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public GameObject sub;
    public Rigidbody playerBody;
    public float tereSpeed, aquaSpeed, gravity, jumpHeight, groundDistance;
    public bool Frozen, inSub, isGrounded;
    public LayerMask playerMask;
    public LayerMask subMask;
    public Transform groundCheck;
    public GameObject cam;
    Vector2 direction;
    private float jump;
    float playerHeightOffset;

    private Vector2 mouseInput;
    public float mouseSensitivity = 100f;
    float yRotation = 0f;

    private Transform parentTransform;
    GameObject playerContainer;
    Vector3 initialPos;
    public float spaceRadiusX;
    public float spaceRadiusZ;
    CapsuleCollider playerCollider;
    public GameObject FlashLight;

    public SubController controller;

    public static PlayerScript instance;
    public static PlayerScript Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = sub.GetComponent<SubController>();
        playerContainer = controller.playerContainer;
        playerCollider = GetComponent<CapsuleCollider>();
        playerHeightOffset = playerContainer.transform.localPosition.y;
        if (spaceRadiusX < 1)
        {
            spaceRadiusX = 1;
        }
        if (spaceRadiusZ < 1)
        {
            spaceRadiusZ = 1;
        }
        parentTransform = sub.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = new (direction.x, jump, direction.y);
        
        initialPos = playerContainer.transform.localPosition;
        if (!Frozen) // if Frozen is false
        {
            if (!inSub)
            {
                if (!FlashLight.activeSelf)
                    FlashLight.SetActive(true);
                OutsideMovement(moveVector);
            }
            else
            {
                if (FlashLight.activeSelf)
                    FlashLight.SetActive(false);
                InsideMovement(moveVector);
            }

            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        yRotation -= mouseInput.y;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseInput.x);
    }

    //movement logic for when the player is not in the water
    private void InsideMovement(Vector3 moveVector)
    {
        //sets the freeze position constrains, since we're moving with transform. needed for parenting to work.
        playerBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        //setting players collider to be a trigger so it doesn't influence sub movement
        playerCollider.isTrigger = true;

        transform.SetParent(sub.transform);

        moveVector *= Time.deltaTime * (tereSpeed / 5);

        //establishes ellipse which represents player movement space
        Vector3 newPos = transform.localPosition + moveVector;
        Vector3 offset = newPos - initialPos;
        offset.x /= spaceRadiusX;
        offset.z /= spaceRadiusZ;
        //compare distance of player to origin of ellipse to see if player would be out of bounds. if so, then skip adding movement.
        if (offset.magnitude < 1.0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, playerHeightOffset, transform.localPosition.z);
            transform.position += transform.TransformDirection(moveVector);
        }
        else if (offset.magnitude > 1.1f)
        {
            transform.position = playerContainer.transform.position;
        }
    }

    //movement logic for when the player is in the water
    private void OutsideMovement(Vector3 moveVector)
    {
        //sets the freeze position constrains, since we're moving with transform. needed for rigidbody to work.
        playerBody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
        transform.SetParent(null);

        //setting players collider to not be a trigger so that we can use it for physics
        playerCollider.isTrigger = false;

        int combinedMask = playerMask | subMask;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~combinedMask); //checks to see if the ground check is contacting anything except the player mask

        moveVector.Normalize();

        moveVector.x *= Time.deltaTime * aquaSpeed;
        moveVector.z *= Time.deltaTime * aquaSpeed;

        moveVector.y -= gravity * Time.deltaTime;

        if (isGrounded && Input.GetButtonDown("Jump")) //if you press space while grounded
        {
            moveVector.y += jumpHeight;
        }

        playerBody.AddForce(transform.TransformDirection(moveVector), ForceMode.Impulse);
    }

    //water sound
    public float maxSpeed = 10f;

    private void FixedUpdate()
    {
        float speedRatio = playerBody.velocity.magnitude / maxSpeed;
        float volume = Mathf.Clamp01(speedRatio);

        GlobalSoundsManager.instance.audiosources[10].volume = volume + 0.2f;
    }


    //Draws the ellipse to the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = parentTransform != null ? parentTransform.localToWorldMatrix : Matrix4x4.identity;
        Gizmos.DrawWireMesh(CreateEllipseMesh(), initialPos);
        Gizmos.matrix = Matrix4x4.identity;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(new Vector3(direction.x, 0, direction.y))  * 50);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 25);
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

    //expression body statement to get input for player movement
    private void OnMove(InputValue inputValue) => direction = inputValue.Get<Vector2>();

    //expression body statement to get input for when the player Jumps
    private void OnJump(InputValue inputValue) => jump = inputValue.Get<float>();

    //expression body statement to get input for when the player moves their mouse
    private void OnLook(InputValue inputValue) => mouseInput = inputValue.Get<Vector2>() * mouseSensitivity * Time.deltaTime;
}
