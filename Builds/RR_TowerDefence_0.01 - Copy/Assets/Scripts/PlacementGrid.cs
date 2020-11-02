using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEditorInternal;
using UnityEngine;


namespace PlacementSystem
{
    public class PlacementGrid : MonoBehaviour
    {
        private static UnityEngine.Vector3 _MapSize = new UnityEngine.Vector3(0, 0, 0);
        private UnityEngine.Vector3 CachedMapSize = new UnityEngine.Vector3(0, 0, 0);
        public static float BaseElevation = 0;
        public static float BlockScale = 1;
        public static float Offset = BlockScale / 2;
        private float cachedBlockScale = 1;

        List<PlacementBlock> GridBlocks;

        private void Start()
        {

        }

        private void Update()
        {
            if (CachedMapSize != _MapSize)
            {

            }
        }

        public bool InitializeMapGrid(UnityEngine.Vector3 MapSize)
        {
            _MapSize = MapSize;
            int xRepeat = Mathf.FloorToInt(MapSize.x / BlockScale);
            int zRepeat = Mathf.FloorToInt(MapSize.z / BlockScale);

            int xCounter = 0;
            int zCounter = 0;
            UnityEngine.Vector3 CursorPosition = new UnityEngine.Vector3(Offset, BaseElevation, Offset);
            UnityEngine.Vector3 xCursorIncrement = new UnityEngine.Vector3(BlockScale, 0, 0);
            UnityEngine.Vector3 zCursorIncrement = new UnityEngine.Vector3(0, 0, BlockScale);

            GridBlocks = new List<PlacementBlock>();

            while (zCounter < zRepeat)
            {
                while(xCounter < xRepeat)
                {
                    PlacementBlock _pb = PlacementBlock.CreatePlacementBlock(CursorPosition, BlockScale);
                    GridBlocks.Add(_pb);

                    CursorPosition += xCursorIncrement;
                    xCounter += 1;
                }
                CursorPosition += zCursorIncrement;
                zCounter += 1;
            }

            return false;
        }

        public int GetGridCellCount()
        {
            return Mathf.FloorToInt(_MapSize.x * _MapSize.z / BlockScale);
        }
    }
}

