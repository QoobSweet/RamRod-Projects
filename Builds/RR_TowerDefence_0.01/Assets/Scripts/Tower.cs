using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

/**
public class Tower : MonoBehaviour
{
    public GameObject Emitter;
    public GameObject Turret;

    public enum TowerTypes 
    {
        BeamLaser
    }

    public TowerTypes TowerType = TowerTypes.BeamLaser;


    public bool Online = false;

    public float Speed;
    public float MaxRange = 2;
    public float RotationSpeed = 1;
    public float Delay;
    private float _Tick;

    public GameObject Target;

    private List<GameObject> _Projectiles = new List<GameObject>();
    private List<GameObject> _TrashCan = new List<GameObject>();

    private bool Trigger = false;

    // Start is called before the first frame update
    void Start()
    {

        Trigger = true;
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RemoveBeamOut()
    {
        if (BeamOut)
        {
            BeamOut.Destroy();
        }
    }
    public static bool CheckInRange(GameObject Origin, GameObject Target, float MaxRange)
    {
        return CheckInRange(Origin.transform, Target.transform, MaxRange);
    }
    public static bool CheckInRange(Transform Origin, Transform Target, float MaxRange)
    {
        return (UnityEngine.Vector3.Distance(Origin.position, Target.position) < MaxRange);
    }
    public static void RotateTurretTowardsTarget(GameObject Turret, Transform _target)
    {
        Vector3 relativePos = _target.position - Turret.transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        rotation.x = 0;
        rotation.z = 0;
        Turret.transform.rotation = rotation;
        
    }


    class TDProjectile
    {
        private GameObject projectile;


    }
}
    **/