using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace Entities
{
    public class Entity : MonoBehaviour
    {
        private int _ID = 0;
        private string _name;

        public UnityEngine.Vector3 position()
        {
            return this.transform.position;
        }
        
        public bool SetName(string _n)
        {
            _name = _n;
            return true;
        }
        public int GenerateEntityID()
        {
            EntitiesManager.AddEntity(this);
            //unfinished
            
            return _ID;
        }


        
        

        public bool TryAction()
        {
            return false;
        }
        public bool DoAction()
        {
            return false;
        }

        public int GetID()
        {
            return _ID;
        }
        public string GetName()
        {
            return _name;
        }

    }

    public class Structure : Entity
    {
        public GameObject StructureBase;
        public Vector2 AreaCoverage = new Vector2(1,1);
        public float DetectionRange = 0;

        
        public bool Online = false;

        public void TurnOn()
        {
            Online = true;
        }
        public void TurnOff()
        {
            Online = false;
        }



        public bool CheckInRange(GameObject Origin, GameObject Target)
        {
            return CheckInRange(Origin.transform, Target.transform);
        }//chained to next Method
        public bool CheckInRange(Transform Origin, Transform Target)
        {
            return (UnityEngine.Vector3.Distance(Origin.position, Target.position) < DetectionRange);
        }

    }

    public class Being : Entity
    {
        //Not Used Yet
    }
}