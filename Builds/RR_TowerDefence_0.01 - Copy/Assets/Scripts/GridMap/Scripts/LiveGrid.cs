using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class LiveGrid : MonoBehaviour
{
    private Grid grid;
    public int Height;
    public int Width;
    public Vector3 GridOrigin;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(Width, Height, 10f, GridOrigin);
        grid.GenerateBlocks();
        Debug.Log(grid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Experiment()
    {
        Vector3[] vertices;
        Vector2[] uv;
        int[] triangles;

        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out vertices, out uv, out triangles);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                int index = x * grid.GetHeight() + y;
                Vector3 baseSize = new Vector3(1, 1) * grid.GetCellSize();
                Grid.Block block = grid.GetBlock(x, y);

                int gridValue = grid.GetValue(x, y);
                int maxGridValue = 100;
                float gridValueNormalized = Mathf.Clamp01((float)gridValue / maxGridValue);
                Vector2 gridCellUV = new Vector2(gridValueNormalized, 0f);

                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.Get3DWorldPositionFrom2D(x, y) + baseSize * .5f, 0f, baseSize, gridCellUV, gridCellUV);
            }
        }
    }
}
