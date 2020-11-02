using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Camera _camera;
    public GameObject Cursor;
    public GameObject SelectionHologram;
    private UnityEngine.Vector3 PlacementPosition;

    public enum AvailableTurrets
    {
        BeamTurret
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit);
        PlacementPosition = hit.point;
    }

    public void ClearCursorSelection()
    {

    }
    public bool TurretSelected(AvailableTurrets TurretType)
    {
        return false;
    }
}
