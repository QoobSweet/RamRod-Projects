using System.Threading;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public bool DebugThis = false;

    private Camera cam;
    public float panSpeed = 20f;
    public float panBorder = 20f;
    public Vector2 panLimit;


    public float RayDistance = 1000f;
    public float scrollSpeed = 2;
    public float minimumY = 40f;
    public float maximumY = 500f;

    private Vector3 _WorldMousePosition = new Vector3();

    void Start()
    {
        cam = Camera.main;
    }

    void OnGUI()
    {
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + GetCursorWorldPosition());
        GUILayout.EndArea();
    }
    void Update()
    {

        if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 || Input.mousePosition.x > Screen.width - 1 || Input.mousePosition.y > Screen.height - 1)
        { 
        
        }
        else
        {
            Vector3 posAdjust = new Vector3(0, 0, 0);

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorder)
            {
                posAdjust.z += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorder)
            {
                posAdjust.z -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= panBorder)
            {
                posAdjust.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorder)
            {
                posAdjust.x += panSpeed * Time.deltaTime;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            posAdjust.y -= scroll * scrollSpeed * 100f * Time.deltaTime;


            posAdjust += transform.position;

            posAdjust.x = Mathf.Clamp(posAdjust.x, -panLimit.x, panLimit.x);
            posAdjust.y = Mathf.Clamp(posAdjust.y, minimumY, maximumY);
            posAdjust.z = Mathf.Clamp(posAdjust.z, -panLimit.y, panLimit.y);


            transform.position = posAdjust;
        }
    }

    public Vector3 WorldMousePosition
    {
        get
        {
            return _WorldMousePosition;
        }
    }


    public Vector3 GetCursorWorldPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        if (DebugThis)
        {
            DebugRayCastLine(ray, hit);
        }

        return hit.point;
    }

    public void DebugRayCastLine(Ray ray, RaycastHit hit)
    {
        //draw invisible ray cast/vector
        Debug.DrawLine(ray.origin, hit.point);
        //log hit area to the console
        _WorldMousePosition = hit.point;
        //Debug.Log(_WorldMousePosition);
    }

}

