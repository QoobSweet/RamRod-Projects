using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Placement
{
    
    public partial class TDMasterGrid
    {
        
        public partial class LiveGrid
        {
            [SerializableAttribute]
            public class pBlock
            {
                private pGrid parentGrid;

                private int xKey;
                private int yKey;


                //Structure Properties - Encapsulate later
                public GameObject Structure { get { return Structure; } private set { Structure = value; } }



                private Verticies _verticies;
                public Verticies verticies { get { return _verticies; } }

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


                public pBlock(pGrid ParentGrid, int xKey, int yKey, Vector3 CenterPosition, float cellSize)
                {
                    this.UpdateBlock(ParentGrid, xKey, yKey, CenterPosition, cellSize);
                }

                public void UpdateBlock(pGrid ParentGrid, int xKey, int yKey, Vector3 CenterPosition, float cellSize)
                {
                    this.parentGrid = ParentGrid;
                    this._verticies = new Verticies(CenterPosition, cellSize);
                    this.xKey = xKey;
                    this.yKey = yKey;
                }


                //Spawn if available - no overwrite
                public GameObject SpawnStructure(GameObject prefab) { return SpawnStructure(prefab, false); }
                //Spawn and overwrite existing
                public GameObject SpawnStructure(GameObject prefab, bool ReplaceExisting)
                {
                    if (ReplaceExisting)
                    {
                        if (Structure)
                        {
                            DestroyStructure();
                        }
                    }
                    if (!Structure)
                    {
                        Structure = TDMasterGrid.Instantiate(prefab, this.verticies.Center, new Quaternion(0, 0, 0, 0));
                    }
                    return Structure;
                }
                public GameObject GetStructure() { return Structure; }
                public Vector2 BlockKey()
                {
                    Vector2 _r = new Vector2(xKey, yKey);
                    return _r;
                }
                public void DestroyStructure()
                {
                    TDMasterGrid.Destroy(Structure);
                }
                public bool HasStructure()
                {
                    if (Structure) { return true; }
                    else { return false; }
                }
            }
        }
    }
}