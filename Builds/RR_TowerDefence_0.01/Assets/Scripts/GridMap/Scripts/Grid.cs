using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QDS.Utils;


namespace Placement
{
    public class Grid
    {

        public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
        public class OnGridValueChangedEventArgs : EventArgs
        {
            public int x;
            public int y;
        }

        private int width_x;
        private int height_y;
        private float cellSize;

        private Vector3 originPosition;
        private Block[,] gridBlocks;

        private LiveGrid LGParent;

        public class Block
        {
            private Grid parentGrid;

            private int xKey;
            private int yKey;

            private GameObject _structure;

            private Verticies _verticies;
            public class Verticies
            {
                private Vector3 center;
                private float OffSet;
                private Vector3 vBL;
                private Vector3 vTL;
                private Vector3 vTR;
                private Vector3 vBR;

                public Vector3 Center { get { return center; } }
                public Vector3 BottomLeft { get { return vBL; } }
                public Vector3 TopLeft { get { return vTL; } }
                public Vector3 TopRight { get { return vTR; } }
                public Vector3 BottomRight { get { return vBR; } }


                public Verticies(Vector3 Center, float cellSize)
                {
                    this.center = Center;
                    this.OffSet = cellSize / 2;

                    this.vBL = new Vector3(Center.x - OffSet, 0, Center.z - OffSet);
                    this.vTL = new Vector3(Center.x - OffSet, 0, Center.z + OffSet);
                    this.vTR = new Vector3(Center.x + OffSet, 0, Center.z + OffSet);
                    this.vBR = new Vector3(Center.x + OffSet, 0, Center.z - OffSet);
                }
            }

            public Verticies verticies
            {
                get
                {
                    return _verticies;
                }
            }


            public Block(Grid ParentGrid, int xKey, int yKey, Vector3 CenterPosition, float cellSize)
            {
                this.UpdateBlock(ParentGrid, xKey, yKey, CenterPosition, cellSize);
            }

            public void UpdateBlock(Grid ParentGrid, int xKey, int yKey, Vector3 CenterPosition, float cellSize)
            {
                this.parentGrid = ParentGrid;
                this._verticies = new Verticies(CenterPosition, cellSize);
                this.xKey = xKey;
                this.yKey = yKey;
            }
            public GameObject structure { get { return _structure; } }

            public GameObject SpawnStructure(GameObject prefab)
            {
                return SpawnStructure(prefab, false);
            }
            public GameObject SpawnStructure(GameObject prefab, bool ReplaceExisting)
            {
                if (ReplaceExisting)
                {
                    if (_structure)
                    {
                        DestroyStructure();
                    }
                }
                if (!_structure) 
                {
                    _structure = LiveGrid.Instantiate(prefab, this.verticies.Center, new Quaternion(0, 0, 0, 0), parentGrid.LGParent.transform);
                }
                return _structure;
            }
            public GameObject GetStructure() { return _structure; }
            public Vector2 BlockKey()
            {
                Vector2 _r = new Vector2(xKey, yKey);
                return _r;
            }
            public void DestroyStructure()
            {
                LiveGrid.Destroy(_structure);
            }
            public bool HasStructure() { 
                if (_structure) { return true; } 
                else { return false; } }
        }

        public Grid(int width, int height, float cellSize, Vector3 originPosition, LiveGrid parent)
        {
            this.width_x = width;
            this.height_y = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.LGParent = parent;

            gridBlocks = new Block[width, height];

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

                OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => {
                    debugTextArray[eventArgs.x, eventArgs.y].text = gridBlocks[eventArgs.x, eventArgs.y].ToString();
                };
            }
        }

        public int GetGridWidth() { return width_x; }
        public int GetGridHeight() { return height_y; }
        public float GetCellSize() { return cellSize; }
        public Vector3 GetOriginPosition() { return originPosition; }


        public void InitializeGridBlocks()
        {
            //initialization/static Variables will be set here

            UpdateGridBlocks();
        }

        public void UpdateGridBlocks()
        {

            if (this.width_x >= 0 && this.height_y >= 0)
            {

                for (int x = 0; x < GetGridWidth(); x++)
                {
                    for (int y = 0; y < GetGridHeight(); y++)
                    {
                        int index = x * GetGridHeight() + y;
                        Vector3 baseSize = new Vector3(1, 0, 1) * this.GetCellSize();

                        Vector3 cellOrigin = this.originPosition + (new Vector3(1 * x, 0, 1 * y) * cellSize);
                        Vector3 cellCenter = cellOrigin + (new Vector3(0.5f, 0, 0.5f) * cellSize);

                        if (gridBlocks[x, y] == null)
                        { gridBlocks[x, y] = new Block(this, x, y, cellCenter, cellSize); }
                        else
                        { gridBlocks[x, y].UpdateBlock(this, x, y, cellCenter, cellSize); }
                    }
                }
            }
            Debug.Log("test success");

        }




        public Vector3 GetXYWorldPosition(int x, int y)
        { return new Vector3(x, y) * cellSize + originPosition; }
        public Vector3 Get3DWorldPositionFrom2D(int x, int y)
        { return new Vector3(x, 0, y) * cellSize + originPosition; }
        private void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt(Mathf.Clamp((worldPosition - originPosition).x / cellSize, 0, cellSize * width_x));
            y = Mathf.FloorToInt(Mathf.Clamp((worldPosition - originPosition).z / cellSize, 0, cellSize * height_y));
        }
        public Block GetBlockFromXY(int x, int y)
        {
            return gridBlocks[x, y];
        }
        public Block GetBlockAtWorldPosition(Vector3 worldPosition)
        {
            int x;
            int y;

            GetXY(worldPosition, out x, out y);
            //Debug.Log(x + " " + y);
            return GetBlockFromXY(x, y);
        }
    }
}