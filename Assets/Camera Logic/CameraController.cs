using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float zoomSpeed = 50f;
    public float minY = 10f;
    public float maxY = 40f;
    public Transform target; // center map

    public Vector2 panLimitX = new Vector2(0, 100);
    public Vector2 panLimitZ = new Vector2(0, 100);

    private Vector3 lastPanPosition;
    private bool isPanning;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
      //  Debug.Log("Scroll Value: " + scroll);
        HandleMousePan();
        HandleMouseZoom();
    }

    void HandleMousePan()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPanning = true;
            lastPanPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPanning = false;
        }

        if (isPanning)
        {
            Vector3 delta = Input.mousePosition - lastPanPosition;
            Vector3 move = new Vector3(-delta.x, 0, -delta.y) * panSpeed * Time.deltaTime;

            transform.Translate(move, Space.World);

            // Clamp posisi kamera
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, panLimitX.x, panLimitX.y);
            pos.z = Mathf.Clamp(pos.z, panLimitZ.x, panLimitZ.y);
            transform.position = pos;

            lastPanPosition = Input.mousePosition;
        }
    }

   void HandleMouseZoom()
{
    float scroll = Input.GetAxis("Mouse ScrollWheel");
    if (Mathf.Approximately(scroll, 0f)) return;

    Vector3 direction = (transform.position - target.position).normalized;
    float distance = Vector3.Distance(transform.position, target.position);

    float zoomAmount = scroll * zoomSpeed * Time.deltaTime;
    Vector3 newPosition = transform.position - direction * zoomAmount;

    float newDistance = Vector3.Distance(newPosition, target.position);

    if (newDistance >= minY && newDistance <= maxY)
    {
        transform.position = newPosition;
    }
}





 
}
