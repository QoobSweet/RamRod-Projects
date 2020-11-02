using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QDS.Utils;
using UnityEditor;
using UnityEngine.UI;

namespace Placement
{
    [SerializableAttribute]
    public partial class TDMasterGrid
    {
        [SerializableAttribute]
        public partial class LiveGrid
        {

            //State Controllers
            public bool DebugMouse = true;
            private bool Initialized = false;

            //Public Dependencies
            private pGrid _Grid;
            public pGrid GetGrid() { return _Grid; }

            private int _Height;
            public int GetHeight() { return _Height; }

            private int _Width;
            public int GetWidth() { return _Width; }

            private float _CellSize;
            public float GetCellSize() { return _CellSize; }

            private Vector3 _OriginPosition;
            public Vector3 GetOriginPosition() { return _OriginPosition; }

            private pBlock _SelectedBlock;
            public pBlock GetSelectedBlock() { return _SelectedBlock; }
            //public MenuManager MenuManager { get { return MenuManager; } private set { MenuManager = value; } }

            //__public Set
            public GameObject SelectedStructure { get { return SelectedStructure; } set { SelectedStructure = value; } }


            public Camera _camera;
            private CameraController _cameraController;

            private TDMap _Map;
            public TDMap GetMap() { return _Map; }


            public LiveGrid(Vector3 OriginPosition, int Width, int Height, float cellSize, TDMap map)
            {
                this._Width = Width;
                this._Height = Height;
                this._CellSize = cellSize;
                this._OriginPosition = OriginPosition;
                //this.MapBackground = MapBackground;
                this._Map = map;

                _Grid = new pGrid(Width, Height, cellSize, OriginPosition, this);
                _Grid.InitializeGridBlocks();
                //_cameraController = _camera.GetComponent<CameraController>();
                Initialized = true;
            }

            // Update is called once per frame
            public void UpdatePlayerGrid(CameraController playerCam)
            {
                if (Initialized)
                {
                    RaycastHit workingRayHit = playerCam.GetCursorRayHit();
                    Ray workingRay = playerCam.GetCursorRay();
                    pBlock workingBlock = _Grid.GetBlockAtWorldPosition(workingRayHit.point);


                    if (DebugMouse) { playerCam.DebugThis = true; }
                    else { playerCam.DebugThis = false; }


                    if (Input.GetMouseButtonDown(0))
                    {
                        //create a ray cast and set it to the mouses cursor position in game
                        GameObject Structure = workingBlock.GetStructure();

                        if (Structure == null)
                        {
                            Structure = workingBlock.SpawnStructure(SelectedStructure);
                            Debug.Log(Structure.name + " has been Spawned at " + workingBlock.BlockKey());
                        }
                    }

                    if (Input.GetMouseButtonDown(1)) { workingBlock.DestroyStructure(); }
                }
            }

            
            private pBlock BlockAtPointer()
            {
                if (_SelectedBlock != null) { return _SelectedBlock; }
                else { return null; throw new System.Exception("Selected Block now found."); }
            }
            private void SetSelectedBlock(pBlock block) { _SelectedBlock = block; }
            private void UpdateCellHighlight(pBlock destinationBlock)
            {
                //placeholder
            }
        }
    }
}