using UnityEngine;

public class Mouvement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float crouchSpeed = 5f;

    private Rigidbody rb;
    private Vector3 originalScale;
    private Vector3 crouchedScale;
    private bool isCrouching;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        crouchedScale = new Vector3(originalScale.x, originalScale.y * crouchHeight, originalScale.z);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Update()
    {
        Jump();
        Crouch();
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0f, moveZ).normalized;
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!isCrouching)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, crouchedScale, Time.deltaTime * crouchSpeed);
                isCrouching = true;
            }
        }
        else
        {
            if (isCrouching)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * crouchSpeed);
                if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
                    transform.localScale = originalScale;
                isCrouching = false;
            }
        }
    }
}
