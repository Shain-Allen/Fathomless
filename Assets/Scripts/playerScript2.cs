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
    float playerHeightOffset;

    public GameObject sub;

    public GameObject playerContainer;
    Vector3 initialPos;
    public float spaceRadiusX;
    public float spaceRadiusZ;

    // Start is called before the first frame update
    void Start()
    {
         playerHeightOffset = playerContainer.transform.localPosition.y;
        if (spaceRadiusX < 1)
        {
            spaceRadiusX = 1;
        }
        if (spaceRadiusZ < 1)
        {
            spaceRadiusZ = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        initialPos = playerContainer.transform.position;
        if (!Frozen) // if Frozen is false
        {
            if (!inSub)
            {
                //sets the freeze position constrains, since we're moving with transform. needed for rigidbody to work.
                playerBody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                transform.SetParent(null);

                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~playerMask); //checks to see if the ground check is contacting anything except the player mask

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

                
                direction.x *= Time.deltaTime * aquaSpeed;
                direction.z *= Time.deltaTime * aquaSpeed;

                direction.y -= gravity * Time.deltaTime;
                direction.Normalize();
                if (isGrounded && Input.GetButtonDown("Jump")) //if you press space while grounded
                {
                    direction.y += jumpHeight;
                }
                print(direction);
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

                //establishes ellipse which represents player movement space
                Vector3 newPos = transform.position + direction;
                Vector3 offset = newPos - initialPos;
                offset.x /= spaceRadiusX;
                offset.z /= spaceRadiusZ;
                //compare distance of player to origin of ellipse to see if player would be out of bounds. if so, then skip adding movement.
                if (offset.magnitude < 1.0)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, playerHeightOffset, transform.localPosition.z);
                    transform.position += new Vector3(direction.x, 0f, direction.z);
                }
                else if(offset.magnitude > 1.1f)
                {
                    transform.position = playerContainer.transform.position;
                }
            }

        }
        else //if Frozen is true
        {

        }
    }

    //Draws the ellipse to the scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireMesh(CreateEllipseMesh(), initialPos);
        Gizmos.matrix = Matrix4x4.identity;
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
