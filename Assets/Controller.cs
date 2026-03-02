using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;
    public float horizontalSpeed = 15f;
    public float horizontalLimit = 4f;
    public float jumpForce = 5f;

    [Header("Jump Settings")]
    private bool isGrounded = false; 
    private Rigidbody rb;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.blue;
        rb = GetComponent<Rigidbody>();

        if (rb == null)
            UnityEngine.Debug.LogError("Rigidbody not found on Player!");
    }

    void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            horizontalInput = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            horizontalInput = 1f;

        float moveX = horizontalInput * horizontalSpeed * Time.deltaTime;
        float newX = Mathf.Clamp(transform.position.x + moveX, -horizontalLimit, horizontalLimit);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

       
        UnityEngine.Debug.Log($"Is Grounded: {isGrounded}");
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        isGrounded = false; 
        UnityEngine.Debug.Log("Jump!");
    }

    void OnCollisionStay(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}