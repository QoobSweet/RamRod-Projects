using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private Block[,] gridBlockArray;

    public class Block
    {
        private Grid parent;
        private Vector3 centerpoint;
        private float width;
        private float height;

        public Vector3[] verts() 
        {
            Vector3[] _verts = 
            {
            new Vector3( parent.cellSize/2, 0,  parent.cellSize/2) + centerpoint,
            new Vector3( parent.cellSize/2, 0, -parent.cellSize/2) + centerpoint,
            new Vector3(-parent.cellSize/2, 0, -parent.cellSize/2) + centerpoint,
            new Vector3(-parent.cellSize/2, 0,  parent.cellSize/2) + centerpoint
            };
            return _verts;
        }
        public Vector3 vert(int vertKey)
        {
            return verts()[vertKey];
        }

        public static Block CreateBlock(Grid parent, Vector3 centerpoint, float width, float height)
        {
            Block _block = new Block();
            _block.width = width;
            _block.height = height;
            _block.centerpoint = centerpoint;
            _block.parent = parent;
            return _block;
        }
    }
    public void GenerateBlocks()
    {
        Vector3[] vertices;
        Vector2[] uv;
        int[] triangles;
        Vector3 OffSet = new Vector3(cellSize/2,0,cellSize/2);

        MeshUtils.CreateEmptyMeshArrays(GetWidth() * GetHeight(), out vertices, out uv, out triangles);

        for (int x = 0; x < GetWidth(); x++)
        {
            for (int y = 0; y < GetHeight(); y++)
            {
                int index = x * GetHeight() + y;
                Vector3 baseSize = new Vector3(1, 1) * this.GetCellSize();

                int gridValue = this.GetValue(x, y);
                int maxGridValue = 100;
                float gridValueNormalized = Mathf.Clamp01((float)gridValue / maxGridValue);
                Vector2 gridCellUV = new Vector2(gridValueNormalized, 0f);

                //create new block for cell
                Block newBlock = Block.CreateBlock(this, Get3DWorldPositionFrom2D(x, y) + OffSet, cellSize, cellSize);
                SetValue(x, y, newBlock);
                Debug.Log(newBlock.vert(0) + " | " + newBlock.vert(1) + " | " + newBlock.vert(2) + " | " + newBlock.vert(3));



                Grid.Block block = GetBlock(x, y);
                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, Get3DWorldPositionFrom2D(x, y) + baseSize * .5f, 0f, baseSize, gridCellUV, gridCellUV);
            }
        }
        Debug.Log(gridBlockArray);
    }
    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        //declare what cells should contain
        gridArray = new int[width, height]; //each cell has an int writeable int value "GetValue(x,y)"
        gridBlockArray = new Block[width, height];
    }

    public int GetWidth() {return width; }
    public int GetHeight() {return height; }
    public float GetCellSize() {return cellSize; }
    public Vector3 GetOriginPosition() { return originPosition; }

    public Vector3 GetXYWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }
    public Vector3 Get3DWorldPositionFrom2D(int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.RoundToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.RoundToInt((worldPosition - originPosition).y / cellSize);
    }

    //for Setting Cell Block Containers
    public bool SetValue(int x, int y, Block block)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridBlockArray[x, y] = block;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
            return true;
        }
        return false;
    }
    public bool SetValue(Vector3 worldPosition, Block block)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return SetValue(x, y, block);
       
    }

    //for Setting Cell Int Values
    public bool SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
            return true;
        }
        return false;
    }
    public bool SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return SetValue(x, y, value);
    }



    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public Block GetBlock(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridBlockArray[x, y];
        }
        else
        {
            return null;
        }
    }
    public Block GetBlock(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetBlock(x, y);
    }
}
