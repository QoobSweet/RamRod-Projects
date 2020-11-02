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

    private int width_x;
    private int height_y;
    private float cellSize;

    private Vector3 originPosition;
    private Block[,] gridBlocks;

    public class Block
    {
        private Vertices PerimiterVertices;


        public class Vertices
        
        
        {
            private Vector3 center;
            private float OffSet;
            private Vector3 vBL;
            private Vector3 vTL;
            private Vector3 vTR;
            private Vector3 vBR;

            public Vector3 GetCenter() { return center; }
            public Vector3 GetBLVert() { return vBL; }
            public Vector3 GetTLVert() { return vTL; }
            public Vector3 GetTRVert() { return vTR; }
            public Vector3 GetBRVert() { return vBR; }


            public Vertices(Vector3 Center, float cellSize)
            {
                this.center = Center;
                this.OffSet = cellSize / 2;
                this.vBL = new Vector3(Center.x - OffSet, 0, Center.y - OffSet);
                this.vTL = new Vector3(Center.x - OffSet, 0, Center.y + OffSet);
                this.vTR = new Vector3(Center.x + OffSet, 0, Center.y + OffSet);
                this.vBR = new Vector3(Center.x + OffSet, 0, Center.y - OffSet);
            }
        }

        public Block(Vector2 CenterPosition, float cellSize)
        {
            this.PerimiterVertices = new Vertices(CenterPosition, cellSize);
        }
        public Block(Vector3 CenterPosition, float cellSize)
        {
            this.PerimiterVertices = new Vertices(CenterPosition, cellSize);
        }

        public void UpdateBlock(Vector3 CenterPosition, float cellSize)
        {
            this.PerimiterVertices = new Vertices(CenterPosition, cellSize);
        }
    }

    public Grid(int width, int length, int height, float cellSize, Vector3 originPosition)
    {
        this.width_x = width;
        this.height_y = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        //declare what cells should contain


    }

    public int GetGridWidth()           { return width_x;   }
    public int GetGridHeight()          { return height_y;  }
    public float GetCellSize()          { return cellSize;  }
    public Vector3 GetOriginPosition()  { return originPosition; }


    public void UpdateGridBlocks()
    {
        if(this.width_x >= 0 && this.height_y >= 0)
        {
            if (gridBlocks != null)
            {
                for (int x = 0; x < GetGridWidth(); x++)
                {
                    for (int y = 0; y<GetGridHeight(); y++)
                    {
                        int index = x * GetGridHeight() + y;
                        Vector3 baseSize = new Vector3(1, 0, 1) * this.GetCellSize();

                        Vector3 cellOrigin = this.originPosition + (new Vector3(1, 0, 1) * cellSize);
                        Vector3 cellCenter = cellOrigin + (new Vector3(0.5f, 0, 0.5f) * cellSize);

                        if (gridBlocks[x, y] == null) 
                        { gridBlocks[x, y] = new Block(cellCenter, cellSize); }
                        else 
                        { gridBlocks[x, y].UpdateBlock(cellCenter, cellSize); }


                    }
                }
            }
            else
            {
                //update positions etc
            }
        }

    }




    public Vector3 GetXYWorldPosition(int x, int y)
        { return new Vector3(x, y) * cellSize + originPosition; }
    public Vector3 Get3DWorldPositionFrom2D(int x, int y)
        { return new Vector3(x, 0, y) * cellSize + originPosition; }

}
