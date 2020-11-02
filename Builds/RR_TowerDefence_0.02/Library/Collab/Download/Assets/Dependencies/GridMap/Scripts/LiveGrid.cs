using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QDS.Utils;
using UnityEditor;
using UnityEngine.UI;

namespace Placement
{
    public class LiveGrid
    {
        public bool DebugMouse = true;
        private bool Initialized = false;

        private Mesh mesh;

        public Image MapBackground;

        public int Height;
        public int Width;

        public float cellSize;

        public Vector3 GridOrigin;

        public GameObject CellHighlighter;
        public GameObject SelectedStructure;

        public MenuManager menuManager;

        public Camera _camera;
        private CameraController _cameraController;

        private Placement.pGrid grid;
        private Placement.pGrid.Block _SelectedBlock;
        private TDMap map;
        public TDMap Map { get { return map; } }


        public LiveGrid(int Width, int Height, float cellSize, Vector3 GridOrigin, Image MapBackground, TDMap map)
        {
            this.Width = Width;
            this.Height = Height;
            this.cellSize = cellSize;
            this.GridOrigin = GridOrigin;
            this.MapBackground = MapBackground;
            this.map = map;

            grid = new Placement.pGrid(Width, Height, cellSize, GridOrigin, MapBackground, this);
            grid.InitializeGridBlocks();
            _cameraController = _camera.GetComponent<CameraController>();
            Initialized = true;
        }

        // Update is called once per frame
        public void UpdateGrid()
        {
            if (Initialized)
            {
                RaycastHit workingRayHit;
                Ray workingRay;
                Placement.pGrid.Block workingBlock;

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
        }

        public Placement.pGrid Grid { get { return grid; } }
        public Placement.pGrid.Block BlockAtPointer()
        {
            if (_SelectedBlock != null) { return _SelectedBlock; }
            else { return null; throw new System.Exception("Selected Block now found."); }
        }
        private void SetSelectedBlock(Placement.pGrid.Block block) { _SelectedBlock = block; }
        private void UpdateCellHighlight(Placement.pGrid.Block destinationBlock)
        {
            //placeholder
        }
    }
}