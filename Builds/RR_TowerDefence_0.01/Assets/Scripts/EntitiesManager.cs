using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Entities
{
    public class EntitiesManager : MonoBehaviour
    {
        public static Dictionary<int, GameObject> EntityPrefabs = new Dictionary<int, GameObject>();
        public static int MaxEntities = 100;
        private static Dictionary<int, Entity> Entities = new Dictionary<int, Entity>();

        private enum EntityTypes
        {
            Being,
            Structure,
            Item
        }


        public static Entity GetEntityByID(int ID)
        {
            if (Entities[ID])
            {
                return Entities[ID];
            }
            else
            {
                throw new Exception("Enitity with ID: " + ID + " does not exist");
            }
        }
        public static int AddEntity(Entity _E)
        {
            if (!CheckIfAtMaxEntities()) //checks for room in Dictionary
            {
                int _newID = 0;

                while (!Entities[_newID])
                {
                    _newID += 1;
                }


                Entities.Add(_newID, _E);
                return _newID;
            }
            else
            {
                throw new Exception("Max Entity Count Reached.");
            }
        }
        private static bool CheckIfAtMaxEntities()
        {
            if (Entities.Count >= MaxEntities)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}