using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float verticalRotationSpeed = 8f; // Increased rotation speed for vertical movement
    public float cameraDistance = 5f;
    public float cameraVerticalDistance = 12f; // Increased camera height
    public float cameraUpperLimit = 60f; // Upper vertical look limit
    public float cameraLowerLimit = -60f; // Lower vertical look limit

    [SerializeField] private Rigidbody rb;
    [SerializeField] public Transform cameraTransform;

    public float cameraRotationX = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // Set the initial camera position
        cameraTransform.position = transform.position + new Vector3(0f, cameraVerticalDistance, 0f) - cameraTransform.forward * cameraDistance;
    }

    private void Update()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Rotate the player with the mouse
        //float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        //transform.Rotate(Vector3.up, mouseX);

        // Look up and down with the mouse
        //float mouseY = Input.GetAxis("Mouse Y") * verticalRotationSpeed; // Use verticalRotationSpeed for vertical movement
        //cameraRotationX -= mouseY;
        //cameraRotationX = Mathf.Clamp(cameraRotationX, cameraLowerLimit, cameraUpperLimit);

        // Calculate the camera position based on the spherical coordinates
        Vector3 cameraPosition = transform.position + Quaternion.Euler(cameraRotationX, transform.eulerAngles.y, 0f) * Vector3.back * cameraDistance;
        // Increase camera height
        cameraTransform.position = cameraPosition + new Vector3(0f, cameraVerticalDistance, 0f); 

        // Get the forward direction of the camera without vertical component
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // Get the right direction relative to the camera
        Vector3 cameraRight = cameraTransform.right;

        // Check if the forward key is pressed (e.g., W or Up arrow)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Move the player forward relative to the camera
            Vector3 moveDirection = cameraForward * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + moveDirection);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // Move the player backward relative to the camera
            Vector3 moveDirection = -cameraForward * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + moveDirection);
        }

        // Check if the strafe keys are pressed (e.g., A or D)
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Move the player to the left relative to the camera
            Vector3 moveDirection = -cameraRight * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + moveDirection);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Move the player to the right relative to the camera
            Vector3 moveDirection = cameraRight * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + moveDirection);
        }
    }
}
