using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QDS.Utils;
using UnityEngine.UI;
using TDGame;

namespace Placement
{
    public partial class TDMasterGrid
    {
        public partial class LiveGrid
        {
            [SerializableAttribute]
            public class pGrid
            {
                public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
                public class OnGridValueChangedEventArgs : EventArgs
                { public int x; public int y; }


                private int width_x;
                private int height_y;
                private float cellSize;


                private Vector3 _OriginPosition;
                public Vector3 OriginPosition { get { return _OriginPosition; } }

                private pBlock[,] gridBlocks;

                private LiveGrid LGParent;

                private int _GridWidth;
                public int GetGridWidth() { return _GridWidth; }

                private int _GridHeight;
                public int GetGridHeight() { return _GridHeight; }

                private float _CellSize;
                public float GetCellSize() { return _CellSize; }
                

                public float GetWorldGridHeight() { return _GridHeight * _CellSize; }
                public float GetWorldGridWidth() { return _GridWidth * _CellSize; }


                //constructor/s
                public pGrid(int width, int height, float cellSize, Vector3 OriginPosition, LiveGrid parent)
                {
                    this.width_x = width;
                    this.height_y = height;
                    this.cellSize = cellSize;
                    this._OriginPosition = OriginPosition;
                    this.LGParent = parent;

                    gridBlocks = new pBlock[width, height];

                    bool showDebug = true;
                    if (showDebug)
                    {
                        TextMesh[,] debugTextArray = new TextMesh[width, height];

                        for (int x = 0; x < gridBlocks.GetLength(0); x++)
                        {
                            for (int y = 0; y < gridBlocks.GetLength(1); y++)
                            {
                                Debug.DrawLine(Get3DWorldPositionFrom2D(x, y), Get3DWorldPositionFrom2D(x, y + 1), Color.white, 100f);
                                Debug.DrawLine(Get3DWorldPositionFrom2D(x, y), Get3DWorldPositionFrom2D(x + 1, y), Color.white, 100f);


                            }
                        }
                        Debug.DrawLine(Get3DWorldPositionFrom2D(0, height), Get3DWorldPositionFrom2D(width, height), Color.white, 100f);
                        Debug.DrawLine(Get3DWorldPositionFrom2D(width, 0), Get3DWorldPositionFrom2D(width, height), Color.white, 100f);

                        OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
                        {
                            debugTextArray[eventArgs.x, eventArgs.y].text = gridBlocks[eventArgs.x, eventArgs.y].ToString();
                        };
                    }
                }



                public void InitializeGridBlocks()
                {
                    //initialization/static Variables will be set here

                    UpdateGridBlocks();
                }

                public void UpdateGridBlocks()
                {
                    if (this._GridWidth >= 0 && this._GridHeight >= 0)
                    {

                        for (int x = 0; x < _GridWidth; x++)
                        {
                            for (int y = 0; y < _GridHeight; y++)
                            {
                                int index = x * _GridHeight + y;
                                Vector3 baseSize = new Vector3(1, 0, 1) * this._CellSize;

                                Vector3 cellOrigin = this._OriginPosition + (new Vector3(1 * x, 0, 1 * y) * _CellSize);
                                Vector3 cellCenter = cellOrigin + (new Vector3(0.5f, 0, 0.5f) * _CellSize);

                                if (gridBlocks[x, y] == null)
                                { gridBlocks[x, y] = new pBlock(this, x, y, cellCenter, _CellSize); }
                                else
                                { gridBlocks[x, y].UpdateBlock(this, x, y, cellCenter, _CellSize); }
                            }
                        }
                    }
                    Debug.Log("test success");
                }




                public Vector3 GetXYWorldPosition(int x, int y)
                { return new Vector3(x, y) * _CellSize + _OriginPosition; }
                public Vector3 Get3DWorldPositionFrom2D(int x, int y)
                { return new Vector3(x, 0, y) * _CellSize + _OriginPosition; }
                private void GetXY(Vector3 worldPosition, out int x, out int y)
                {
                    x = Mathf.FloorToInt(Mathf.Clamp((worldPosition - _OriginPosition).x / _CellSize, 0, _CellSize * _GridWidth));
                    y = Mathf.FloorToInt(Mathf.Clamp((worldPosition - _OriginPosition).z / _CellSize, 0, _CellSize * _GridHeight));
                }
                public pBlock GetBlockFromXY(int x, int y) { return gridBlocks[x, y]; }
                public pBlock GetBlockAtWorldPosition(Vector3 worldPosition)
                {
                    int x;
                    int y;

                    GetXY(worldPosition, out x, out y);
                    //Debug.Log(x + " " + y);
                    return GetBlockFromXY(x, y);
                }
            }
        }
    }
}