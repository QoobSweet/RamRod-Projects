using Placement;
using System.Threading;
using TDGame;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public bool DebugThis = false;

    private Camera cam;
    public LiveGrid MapGrid;
    public float panSpeed = 200f;
    public float PanBorder;
    public Vector2 PanStepLimit = new Vector2(0, 0);


    public float RayDistance = 1000f;
    public float scrollSpeed = 2;
    public float minimumY = 40f;
    public float maximumY = 500f;

    private Vector3 _WorldMousePosition = new Vector3();



    void Start()
    {
        cam = Camera.main;
        PanStepLimit.y = MapGrid.Grid.WorldGridHeight;
        PanStepLimit.x = MapGrid.Grid.WorldGridWidth;
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

        if (Input.mousePosition.x <= 0 || Input.mousePosition.y <= 0 || Input.mousePosition.x >= Screen.width || Input.mousePosition.y >= Screen.height)
        { 
        
        }
        else
        {
            Vector3 posAdjust = new Vector3(0, 0, 0);

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - PanBorder)
            {
                posAdjust.z += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= PanBorder)
            {
                posAdjust.z -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= PanBorder)
            {
                posAdjust.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - PanBorder)
            {
                posAdjust.x += panSpeed * Time.deltaTime;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            posAdjust.y -= scroll * scrollSpeed * 100f * Time.deltaTime;


            posAdjust += transform.position;

            posAdjust.x = Mathf.Clamp(posAdjust.x, -PanStepLimit.x, PanStepLimit.x);
            posAdjust.y = Mathf.Clamp(posAdjust.y, minimumY, maximumY);
            posAdjust.z = Mathf.Clamp(posAdjust.z, -PanStepLimit.y, PanStepLimit.y);


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

    public Ray GetCursorRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    public RaycastHit GetCursorRayHit()
    {
        RaycastHit hit;
        Ray ray = GetCursorRay();
        Physics.Raycast(ray, out hit);

        if (DebugThis)
        {
            DebugRayCastLine(ray, hit);
        }
        
        return hit;
    }

    public Vector3 GetCursorWorldPosition()
    {
        RaycastHit hit = GetCursorRayHit();
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

