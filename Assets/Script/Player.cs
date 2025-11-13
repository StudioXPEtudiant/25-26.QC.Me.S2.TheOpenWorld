using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    private Rigidbody rb;

    void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0f, moveZ).normalized;

        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);

        if (movement != Vector3.zero)
        {

            Quaternion targetRotation = Quaternion.LookRotation(movement);

            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        }

    }

}
