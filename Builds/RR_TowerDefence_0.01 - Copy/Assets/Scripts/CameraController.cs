using System.Threading;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    private Camera cam;
    public float panSpeed = 20f;
    public float panBorder = 20f;
    public Vector2 panLimit;

    public float scrollSpeed = 2;
    public float minimumY = 20f;
    public float maximumY = 120f;


    void Start()
    {
        cam = Camera.main;
    }

    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
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
}