using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;
using UnityEngine.UI;
using TDGame;

namespace Placement
{
    public partial class TDMasterGrid
    {
        public class TDMap
        {
            private string _MapName= "";
            public string GetMapName() { return _MapName; }

            private GameObject MapObject;
            public bool SetMapObject(GameObject MapObject) { this.MapObject = MapObject; return true; }           
            public Transform MapTransform { get { return MapObject.transform; } }

            private MeshRenderer _MapRenderer;
            public MeshRenderer GetMapRenderer() { return _MapRenderer; }

            private Material _MapMaterial;
            public Material GetMapMaterial() { return _MapMaterial; }

            private Shader shader;
            public Texture m_MainTexture;

            private Vector2 _MapSize = new Vector2();
            public Vector2 GetMapSize() { return _MapSize; }
            private int _GridCellSize = 1;
            public int GetGridCellSize() { return _GridCellSize; }
            public int Width { get { return Mathf.FloorToInt(_MapSize.x); } }
            public int Height { get { return Mathf.FloorToInt(_MapSize.y); } }
            public int MapWidth { get { return Width * _GridCellSize; } }
            public int MapHeight { get { return Height * _GridCellSize; } }

            private int _MaxPlayers = 1;
            public int GetMaxPlayers() { return _MaxPlayers; }

            private TDMasterGrid.LiveGrid[] _PlayerGrids;
            public TDMasterGrid.LiveGrid[] GetPlayerGrids() { return _PlayerGrids; }

            private Vector2[] _PlayerGridOffsets;
            public Vector2[] GetPlayerGridOffsets() { return _PlayerGridOffsets; }

            private Vector2[] _PlayerGridSizes;
            public Vector2[] GetPlayerGridSizes() { return _PlayerGridSizes; }
            public TDMasterGrid.LiveGrid GetPlayerGrid(int playerNumber) { return _PlayerGrids[playerNumber]; }
            public Vector2 GetPlayerGridOffset(int playerNumber) { return _PlayerGridOffsets[playerNumber]; }
            public int PlayerGridWidth(int playerNumber) { return Mathf.FloorToInt(GetPlayerGrid(playerNumber).GetWidth()); }
            public int PlayerGridHeight(int playerNumber) { return Mathf.FloorToInt(GetPlayerGrid(playerNumber).GetHeight()); }

            private Dictionary<int, string[]> SplitRawValues;

            public TDMap(string MapPath)
            {
                //StreamReader reader = new StreamReader(MapPath);
                //string RawMap = reader.ReadToEnd();
                //Parse RawMap info to variables....
                //string[] vars = RawMap.Split(' ');

                string sampleMap =
                    "Name: TestMap;" +
                    "MapSize: 50,50;" +
                    "GridCellSize: 20;" +
                    "MaxPlayers: 4;" +

                    "PlayersPositionOffsets:" +
                    "{" +
                        "'2,2'" +
                        "'2,51'" +
                        "'51,51'" +
                        "'51,2'" +
                    "};" +

                    "PlayerGridSizes:" +
                    "{" +
                        "'47,47'" +
                        "'47,47'" +
                        "'47,47'" +
                        "'47,47'" +
                    "};";

                Debug.Log(sampleMap);

                //Remove Whitespace
                sampleMap = sampleMap.Trim(' ');
                sampleMap = sampleMap.Replace(" ", String.Empty);

                Debug.Log(sampleMap);

                //id and values are seperated by ";"
                string[] inputs = sampleMap.Split(';');
                SplitRawValues = new Dictionary<int, string[]>();

                int i = 0;
                foreach (string var in inputs)
                {
                    if (var != "")
                    {
                        SplitRawValues[i] = (var.Split(':'));
                        i++;
                    }
                }
                
                foreach(var _v in SplitRawValues)
                {
                    Debug.Log("Successful Parse Output: " + _v.Value[0] + " = " + _v.Value[1]);
                }

                ParseRawMap(SplitRawValues);
               
                
            }
            public bool Init(TDMasterGrid masterGrid)
            {
                masterGrid.transform.localScale = new Vector3(Width * GetGridCellSize(), .1f, Height * GetGridCellSize());
                masterGrid.transform.position = new Vector3(Width * GetGridCellSize() / 2, 0, Height * GetGridCellSize() / 2);
                masterGrid.GetComponent<MeshRenderer>().material = GameManager.gameManager.TempMapMaterial;
                return true;

            }

            private void UpdateMaxPlayers(int value)
            {
                _MaxPlayers = value;
                _PlayerGrids = new TDMasterGrid.LiveGrid[_MaxPlayers];
                _PlayerGridOffsets = new Vector2[_MaxPlayers];
                _PlayerGridSizes = new Vector2[_MaxPlayers];
            }

            private void ParseRawMap(Dictionary<int, string[]> SplitRawValues)
            {
                for (int i = 0; i < SplitRawValues.Count; i++)
                {
                    //assign line to parse
                    string[] _v = SplitRawValues[i];
                    string _v1 = _v[0];
                    string _v2 = _v[1];
                    Debug.Log("Trying to assign: " + _v1 + " value of: " + _v2);

                    //check against variable name
                    if (_v1 == "Name")
                    {
                        _MapName = _v2;
                    }

                    if (_v1 == "MapSize")
                    {
                        _MapSize = ParseV2(_v2);
                    }

                    if (_v1 == "GridCellSize")
                    {
                        _GridCellSize = Int32.Parse(_v2);
                    }

                    if (_v1 == "MaxPlayers")
                    {
                        UpdateMaxPlayers(Int32.Parse(_v2));
                    }

                    if (_v1 == "PlayersPositionOffsets")
                    {
                        _PlayerGridOffsets = ParseEncapsulatedV2Array(_v2);
                    }

                    if (_v1 == "PlayersGridSizes")
                    {
                        _PlayerGridSizes = ParseEncapsulatedV2Array(_v2);
                    }
                }
            }
            private Vector2[] ParseEncapsulatedV2Array(string input)
            {
                string[] _array = input.Split(new[] { '{', '}', '\'' }, StringSplitOptions.RemoveEmptyEntries);

                return ParseV2Array(_array);
            }
            private Vector2[] ParseV2Array(string[] input)
            {
                Vector2[] _return = new Vector2[_MaxPlayers];
                int key = 0;
                foreach (var v2 in input)
                {
                    _return[key] = ParseV2(v2);
                    key += 1;
                }
                return _return;
            }
            private Vector2 ParseV2(string input)
            {
                Vector2 _return = new Vector2();
                string[] values = input.Split('{', '}', '\'');
                int key = 0;
                foreach (var v2 in values)
                {
                    string[] rawValues = v2.Split(',');
                    _return.x = Int32.Parse(rawValues[0]);
                    _return.y = Int32.Parse(rawValues[1]);
                    key += 1;
                }
                return _return;
            }
        }
    }
}