using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDGame;
using UnityEngine;

namespace Placement
{
    public partial class TDMasterGrid : MonoBehaviour
    {
        private TDMap _TDMap;
        public TDMap GetTDMap() { return _TDMap; }
        private Vector3 DefaultOrigin = new Vector3(0, 0, 0);
        
        private LiveGrid MapGrid;
        public LiveGrid GetMapGrid() { return MapGrid; }


        private Dictionary<int, LiveGrid> _PlayerSpawns;
        public LiveGrid GetPlayerGrid(int PlayerNumber) { if (PlayerNumber <= _TDMap.GetMaxPlayers()) { return _PlayerSpawns[PlayerNumber]; } else { throw new Exception("Game exceeds max players!"); } }
        



        public LiveGrid OfflineGrid;

        private CameraController[] PlayerCameras;



        public float CellSize { get { return MapGrid.GetCellSize(); } }
        public Vector3 OriginPosition { get { return MapGrid.GetOriginPosition(); } }

        public class Block
        {
            private TDMasterGrid _MasterGrid;
            public TDMasterGrid GetMasterGrid() { return _MasterGrid; }

            private Vector2 _MapGridKey;
            public Vector2 GetMapGridKey() { return _MapGridKey; }

            private LiveGrid[] _LiveGridContainers;
            public LiveGrid[] LiveGridContainers() { return _LiveGridContainers; }

            private Vector2[] _PlayerGridKeys;
            public Vector2[] GetPlayerGridKeys() { return _PlayerGridKeys; }


            public bool HasLiveGridContainer(LiveGrid liveGrid) { return Array.Exists(_LiveGridContainers, element => element == liveGrid); }
            public bool AddLiveGridContainer(int playerNumber, LiveGrid liveGrid, Vector2 LocalGridKeys) 
            {
                _LiveGridContainers[playerNumber] = liveGrid;
                this._PlayerGridKeys[playerNumber] = LocalGridKeys;
                return true;
            }
            public void RemoveLiveGridContainer(int playerNumber) { _LiveGridContainers[playerNumber] = null; RemoveLGCKey(playerNumber); }
            public void RemoveLGCKey(int playerNumber) { _PlayerGridKeys[playerNumber] = new Vector2(0,0); }
        }


        public bool Init(int PlayerCount)
        {
            //Load Map
            _TDMap = new TDMap("");
            _TDMap.Init(this);

            MapGrid = new LiveGrid(DefaultOrigin, _TDMap.Width, _TDMap.Height, _TDMap.GetGridCellSize(), _TDMap);
            


            Debug.Log(MapGrid);
            InitializePlayerGrids();

            return true;
        }

        //Initialize Player Grids
        private void InitializePlayerGrids()
        {
            for (int i = 0; i < _TDMap.GetMaxPlayers(); i++)
            {
                Vector2 offset = _TDMap.GetPlayerGridOffsets()[i];
                Vector3 pStartPosition = new Vector3(offset.x, 0, offset.y);
                Vector2 pGridSize = _TDMap.GetPlayerGridSizes()[i];

                _PlayerSpawns[i] = new TDMasterGrid.LiveGrid(pStartPosition, Mathf.RoundToInt(pGridSize.x), Mathf.RoundToInt(pGridSize.y), _TDMap.GetGridCellSize(), _TDMap);
            }
        }
        public void UpdateGrids()
        {
            
        }
    }
}
