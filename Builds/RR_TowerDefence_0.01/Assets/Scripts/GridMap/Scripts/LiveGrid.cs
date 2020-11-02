using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QDS.Utils;
using UnityEditor;

public class LiveGrid : MonoBehaviour
{
    public bool DebugMouse = false;
    
    private Placement.Grid grid;
    private Mesh mesh;
    public int Height;
    public int Width;
    public float cellSize; 
    public Vector3 GridOrigin;

    public GameObject CellHighlighter;
    public GameObject SelectedStructure;

    public MenuManager menuManager;

    public Camera _camera;
    private CameraController _cameraController;


    private Placement.Grid.Block _SelectedBlock;


    void Start()
    {
        grid = new Placement.Grid(Width, Height, cellSize, GridOrigin, this);
        grid.InitializeGridBlocks();
        _cameraController = _camera.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit workingRayHit;
        Ray workingRay;
        Placement.Grid.Block workingBlock;

        workingRayHit = _cameraController.GetCursorRayHit();
        workingRay = _cameraController.GetCursorRay();
        workingBlock = grid.GetBlockAtWorldPosition(workingRayHit.point);

        if (DebugMouse)
        {
            _cameraController.DebugThis = true;
        }
        else
        {
            _cameraController.DebugThis = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //create a ray cast and set it to the mouses cursor position in game
            GameObject _structure = workingBlock.GetStructure();
            
            if (_structure == null)
            {
                _structure = workingBlock.SpawnStructure(SelectedStructure);
                Debug.Log(_structure.name + " has been Spawned at " + workingBlock.BlockKey());
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            workingBlock.DestroyStructure();
        }
    }

    public Placement.Grid.Block GetSelectedBlock()
    {
        if(_SelectedBlock != null) { return _SelectedBlock; }
        else { return null; throw new System.Exception("Selected Block now found."); }
    }
    private void SetSelectedBlock(Placement.Grid.Block block) { _SelectedBlock = block; }
    private void UpdateCellHighlight(Placement.Grid.Block destinationBlock)
    {
        //placeholder
    }
}
