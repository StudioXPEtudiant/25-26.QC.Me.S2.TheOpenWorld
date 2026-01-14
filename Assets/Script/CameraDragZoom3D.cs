using UnityEngine;

public class CameraDragZoom3D : MonoBehaviour
{
    [Header("Drag")]
    public float dragSensitivity = 0.1f;

    [Header("Zoom")]
    public float zoomSpeed = 20f;
    public float minZoomHeight = 5f;
    public float maxZoomHeight = 50f;

    private Vector3 lastMousePos;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        HandleDrag();
        HandleZoom();
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            transform.Rotate(Vector3.up, delta.x * dragSensitivity, Space.World);
            transform.Rotate(transform.right, delta.y * -dragSensitivity, Space.Self);
            lastMousePos = Input.mousePosition;
        }
    }

    void LateUpdate()
    {
        // Block Z rotation
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector3 zoomDirection = transform.forward;
            Camera.main.transform.position += zoomDirection * scroll * zoomSpeed;

            // Clamp zoom by height
            Vector3 pos = Camera.main.transform.position;
            pos.y = Mathf.Clamp(pos.y, minZoomHeight, maxZoomHeight);
            Camera.main.transform.position = pos;
        }
    }
}
