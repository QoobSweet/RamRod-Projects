using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;
using UnityEngine.UI;

namespace Placement
{
    public class TDMap : MonoBehaviour
    {
        private string _MapName;
        public string MapName               { get { return MapName; }           private set { MapName = value; } }
        public Vector2 MapSize              { get { return MapSize; }           private set { MapSize = value; } }
        public int MapGridCellSize          { get { return MapGridCellSize; }   private set { MapGridCellSize = value; } }
        public int MaxPlayers               { get { return MaxPlayers; }        private set { MaxPlayers = value; } }
        public LiveGrid[] PlayerGrids       { get { return PlayerGrids; }       private set { PlayerGrids = value; } }
        public Vector2[] PlayerGridOffsets  { get { return PlayerGridOffsets; } private set { PlayerGridOffsets = value; } }
        public Vector2[] PlayerGridSizes    { get { return PlayerGridSizes; }   private set { PlayerGridSizes = value; } }

        public TDMap(string MapPath)
        {
            StreamReader reader = new StreamReader(MapPath);
            string RawMap = reader.ReadToEnd();
            //Parse RawMap info to variables....
            string[] vars = RawMap.Split(' ');

            string sampleMap = 
                "Name: TestMap;" +
                "MapSize: 100,100;" +
                "MapGridCellsize: 10;" +
                "MaxPlayers: 4;" +

                "PlayersPositionOffsets:" +
                "{" +
                    "20,20" +
                    "40,20" +
                    "60,20" +
                    "80,20" +
                "};" +

                "PlayerGridSizes:" +
                "{" +
                    "-0,0" +
                    "-0,0" +
                    "-0,0" +
                    "-0,0" +
                "}; ";

            //Remove Whitespace
            sampleMap = sampleMap.Trim();
            sampleMap = sampleMap.Replace(" ", String.Empty);

            //id and values are seperated by ";"
            string[] inputs = sampleMap.Split(';');

            
            foreach(var _V in inputs)
            {
                string[] _v = _V.Split(':', ' ');

                if (_v[0] == "Name")                    { MapName = _v[1]; }
                if (_v[0] == "MapSize")                 { MapSize = ParseV2(_v[1]); }
                if (_v[0] == "MapGridCellSize")         { MapGridCellSize = Int32.Parse(_v[1]); }
                if (_v[0] == "MaxPlayers")              { UpdateMaxPlayers(Int32.Parse(_v[1])); }
                if (_v[0] == "PlayersPositionOffsets")  { PlayerGridOffsets = ParseEncapsulatedV2Array(_v[1]); }
                if (_v[0] == "PlayersGridSizes")        { PlayerGridSizes = ParseEncapsulatedV2Array(_v[1]); }
            }

            
        }
        

        // Update is called once per frame
        public void MapUpdate()
        {
            
        }

        public void Init()
        {
            for (int i = 0; i < MaxPlayers; i++)
            {
                Vector2 offset = PlayerGridOffsets[i];
                Vector3 pStartPosition = new Vector3(offset.x, 0, offset.y);
                Vector2 pGridSize = PlayerGridSizes[i];
                PlayerGrids[i] = new LiveGrid(pStartPosition, pGridSize.x, pGridSize.y, MapGridCellSize, );
                
            
            }
        }

        private void UpdateMapName(string value) { MapName = value; }
        private void UpdateMaxPlayers(int value)
        {
            MaxPlayers = value;
            PlayerGrids = new LiveGrid[MaxPlayers];
            PlayerGridOffsets = new Vector2[MaxPlayers];
            PlayerGridSizes = new Vector2[MaxPlayers];

        }

        private Vector2[] ParseEncapsulatedV2Array(string input)
        {
            string[] _array = input.Split('{', '}', '-');

            return ParseV2Array(_array);
        }
        private Vector2[] ParseV2Array(string[] input)
        {
            Vector2[] _return = new Vector2[MaxPlayers];
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
            string[] values = input.Split('{', '}', '-');
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
