using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SceneManagement;
using UnityEditor;
using Placement;

namespace TDGame
{
    public partial class GameManager
    {

        private bool _Loaded = false;
        public bool  Loaded     { get { return _Loaded; } }


        private GameObject _MapObject;
        private TDMasterGrid _LoadedMap;

        public TDMasterGrid GetLoadedMap() { return _LoadedMap; }
        public CameraController LocalCameraController;

        private int PlayerCountTemp = 1;


        private void UpdateInGame()
        {
            if (_Loaded)
            {

            }
            else
            {
                LoadLevel();
            }
        }

        public bool LoadLevel()
        {
            _Loaded = true;
            _MapObject = GameManager.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
            _LoadedMap = _MapObject.AddComponent<TDMasterGrid>();
            LocalCameraController = LocalPlayerCam.gameObject.AddComponent<CameraController>();
            TDMasterGrid.TDMap TDMap = _LoadedMap.GetTDMap();
            LocalCameraController.SetCameraPosition(new Vector3(TDMap.MapWidth / 2, 0, TDMap.MapHeight / 2));
            return _LoadedMap.Init(PlayerCountTemp);
        }
    }
}
