using UnityEngine;

public class playerScript2 : MonoBehaviour
{
    public GameObject sub;
    public Rigidbody playerBody;
    public float tereSpeed, aquaSpeed, gravity, jumpHeight, groundDistance;
    public bool Frozen, inSub, isGrounded;
    public LayerMask playerMask;
    public LayerMask subMask;
    public Transform groundCheck;
    public GameObject cam;
    Vector3 direction;
    float playerHeightOffset;

    private Transform parentTransform;
    GameObject playerContainer;
    Vector3 initialPos;
    public float spaceRadiusX;
    public float spaceRadiusZ;
    CapsuleCollider collider;
    public GameObject FlashLight;

    public SubController controller;

    public static playerScript2 instance;
    public static playerScript2 Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = sub.GetComponent<SubController>();
        playerContainer = controller.playerContainer;
        collider = GetComponent<CapsuleCollider>();
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
        initialPos = playerContainer.transform.localPosition;
        if (!Frozen) // if Frozen is false
        {
            if (!inSub)
            {
                if (!FlashLight.activeSelf)
                    FlashLight.SetActive(true);
                //sets the freeze position constrains, since we're moving with transform. needed for rigidbody to work.
                playerBody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                transform.SetParent(null);

                //setting players collider to not be a trigger so that we can use it for physics
                collider.isTrigger = false;

                int combinedMask = playerMask | subMask;
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~combinedMask); //checks to see if the ground check is contacting anything except the player mask

                direction = Vector3.zero;

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

                direction.x *= Time.deltaTime * aquaSpeed;
                direction.z *= Time.deltaTime * aquaSpeed;

                direction.y -= gravity * Time.deltaTime;
                
                if (isGrounded && Input.GetButtonDown("Jump")) //if you press space while grounded
                {
                    direction.y += jumpHeight;
                }
                playerBody.AddForce(direction, ForceMode.Impulse); //applies impulse force to all movements
            }
            else
            {
                if (FlashLight.activeSelf)
                    FlashLight.SetActive(false);
                //sets the freeze position constrains, since we're moving with transform. needed for parenting to work.
                playerBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

                //setting players collider to be a trigger so it doesn't influence sub movement
                collider.isTrigger = true;

                transform.SetParent(sub.transform);
                if (Input.GetKey(KeyCode.D)) //pressed D
                {
                    direction += transform.right;
                }
                if (Input.GetKey(KeyCode.A)) //pressed A
                {
                    direction += -transform.right;
                }
                if (Input.GetKey(KeyCode.W)) //pressed W
                {
                    direction += transform.forward;
                }
                if (Input.GetKey(KeyCode.S)) //pressed S
                {
                    direction += -transform.forward;
                }
                direction *= Time.deltaTime * (tereSpeed / 5);

                

                //establishes ellipse which represents player movement space
                Vector3 newPos = transform.localPosition + direction;
                Vector3 offset = newPos - initialPos;
                offset.x /= spaceRadiusX;
                offset.z /= spaceRadiusZ;
                //compare distance of player to origin of ellipse to see if player would be out of bounds. if so, then skip adding movement.
                if (offset.magnitude < 1.0)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, playerHeightOffset, transform.localPosition.z);
                    transform.position += new Vector3(direction.x, direction.y, direction.z);
                    //Quaternion rotation = Quaternion.Euler( new Vector3(playerContainer.transform.rotation.x, playerContainer.transform.rotation.y, 80));
                    //transform.rotation = rotation;
                }
                else if (offset.magnitude > 1.1f)
                {
                    transform.position = playerContainer.transform.position;
                    //Quaternion rotation = Quaternion.Euler(new Vector3(playerContainer.transform.rotation.x, playerContainer.transform.rotation.y, playerBody.transform.rotation.z));
                    //transform.rotation = rotation;
                }
            }

        }
        else //if Frozen is true
        {

        }
        
    }


    //Draws the ellipse to the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = parentTransform != null ? parentTransform.localToWorldMatrix : Matrix4x4.identity;
        Gizmos.DrawWireMesh(CreateEllipseMesh(), initialPos);
        Gizmos.matrix = Matrix4x4.identity;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction * 50);
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
}
