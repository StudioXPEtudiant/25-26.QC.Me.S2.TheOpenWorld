using UnityEngine;

public class Mouvement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpCooldown = 0.5f;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float crouchSpeed = 5f;
    [SerializeField] private float crouchCooldown = 0.3f;

    private Rigidbody rb;
    private Vector3 originalScale;
    private Vector3 crouchedScale;
    private bool isCrouching;
    private bool canJump = true;
    private bool canCrouch = true;

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
        HandleJump();
        HandleCrouch();
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = (transform.forward * moveZ + transform.right * moveX).normalized;
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward * moveZ + transform.right * moveX);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && canCrouch)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothCrouch(true));
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && isCrouching && canCrouch)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothCrouch(false));
        }
    }

    private System.Collections.IEnumerator SmoothCrouch(bool crouch)
    {
        canCrouch = false;
        Vector3 targetScale = crouch ? crouchedScale : originalScale;
        Vector3 startScale = transform.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * crouchSpeed;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        transform.localScale = targetScale;
        isCrouching = crouch;
        yield return new WaitForSeconds(crouchCooldown);
        canCrouch = true;
    }

    private void ResetJump()
    {
        canJump = true;
    }
}
