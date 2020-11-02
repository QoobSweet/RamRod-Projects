using UnityEngine;
using System.Collections;



namespace PlacementSystem
{
    public class PlacementBlock
    {
        private Point Mid_Point;
        private static float BlockScale = PlacementGrid.BlockScale;

        private class Point
        {
            public UnityEngine.Vector3 position = new UnityEngine.Vector3(0, PlacementGrid.BaseElevation, 0);
            public bool Connected = false;
            public GameObject[] Linked = new GameObject[4];
            public bool X_Block = false;
            public bool Y_Block = false;
        }
        public class Space
        {
            private bool occupied = false;
            private GameObject _occupant;

            public GameObject Occupant()
            {
                if (occupied && _occupant)
                {
                    return _occupant;
                }
                else
                {
                    return null;
                }
            }
            public bool SetOccupant(GameObject _o)
            {
                if (_o)
                {
                    _occupant = _o;
                    occupied = true;
                    return true;
                }
                else
                {
                    Debug.Log("Occupant not specified! Invalid use of Method!");
                    return false;
                }
            }
            public bool RemoveOccupant()
            {
                _occupant = null;
                occupied = false;
                return true;
            }
        }



        public static PlacementBlock CreatePlacementBlock(UnityEngine.Vector3 centerpoint, float Scale)
        {
            PlacementBlock _r = new PlacementBlock();
            _r.Mid_Point = CreatePoint(centerpoint);
            return _r;

        }
        public UnityEngine.Vector3 CornerPoint(string cornerAbrev)
        {
            UnityEngine.Vector3 _return = new UnityEngine.Vector3();

            switch (cornerAbrev)
            {
                case "TL":
                    _return.x = Mid_Point.position.x - PlacementGrid.Offset;
                    _return.z = Mid_Point.position.z + PlacementGrid.Offset;
                    return _return;
                case "TR":
                    _return.x = Mid_Point.position.x + PlacementGrid.Offset;
                    _return.z = Mid_Point.position.z + PlacementGrid.Offset;
                    return _return;
                case "BL":
                    _return.x = Mid_Point.position.x + PlacementGrid.Offset;
                    _return.z = Mid_Point.position.z - PlacementGrid.Offset;
                    return _return;
                case "BR":
                    _return.x = Mid_Point.position.x + PlacementGrid.Offset;
                    _return.z = Mid_Point.position.z - PlacementGrid.Offset;
                    return _return;
            }

            throw new System.Exception("Currect Corner Abreviation not met.");
        }


        private static Point CreatePoint(UnityEngine.Vector3 _position)
        {
            Point _point = new Point();
            _point.position = _position;
            return _point;
        }
        
    }
}