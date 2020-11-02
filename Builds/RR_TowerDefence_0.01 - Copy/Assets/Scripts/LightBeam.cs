using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
/**
namespace Beams
{
    public class LightBeam : MonoBehaviour
    {
        //Line Pathing Reqs
        private UnityEngine.Vector3 OriginPoint;
        private GameObject Target;
        private UnityEngine.Vector3 TargetPoint;
        private GameObject BeamSource;
        private GameObject ObjBeam;
        private LineRenderer BeamActual;

        private bool RecievingEnergy = false;
        private float EnergyFlowRate = 0;
        private static int MaxBeamsAcceptable = 5;

        private List<LightBeam> IncomingBeams = new List<LightBeam>();
        private LightBeam IncomingBeam;


        private float EnergyRate = 0; //Visibility & Energy Per Second Transfer rate
        private float MaxDistance = 0; //Tie to Current EnergyRate and loss over distance
        private float EnergyLossMultiplier = 0.10f; //Energy loss per meter
        private float Intensity = 0; //Thickness

        //color controls
        private static float RedLevel = 0;
        private static float GreenLevel = 0;
        private static float BlueLevel = 0;
        private static Color WhiteColor = new Color(0, 0, 0);
        private Color CurrentColor = WhiteColor;
        private float MaxColorIntensity = 255;



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CurrentColor = CalculateBeamColor();
            UpdateTargetPosition();
            UpdateBeam();
        }

        //Basic Beam Constructor
        public static LightBeam CreateBeam(GameObject Source, UnityEngine.Vector3 Origin, GameObject Target)
        {
            LightBeam _LB = Source.AddComponent<LightBeam>();
            _LB.BeamActual = Source.AddComponent<LineRenderer>();
            _LB.BeamSource = Source;
            _LB.OriginPoint = Origin;
            _LB.BeamActual.SetPosition(0, Origin);
            _LB.Target = Target;
            _LB.TargetPoint = Target.transform.position;
            _LB.BeamActual.SetPosition(1, Target.transform.position);

            return _LB;
        }
        public bool SetOrigin(UnityEngine.Vector3 Origin)
        {
            OriginPoint = Origin;
            BeamActual.SetPosition(0, Origin);
            return true;
        }
        public float GetEnergyLevelAtPoint(UnityEngine.Vector3 TargetPoint)
        {
            float dist = UnityEngine.Vector3.Distance(OriginPoint, TargetPoint);
            float _energy = EnergyRate - (dist * EnergyLossMultiplier);
            return _energy;
        }
        public bool AddIncomingBeam(LightBeam input)
        {
            if (IncomingBeams.Count < MaxBeamsAcceptable)
            {
                IncomingBeams.Add(input);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveIncomingBeam(LightBeam input)
        {
            if (IncomingBeams.Contains(input))
            {
                IncomingBeams.Remove(input);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateOriginPosition(UnityEngine.Vector3 OriginPosition)
        {
            OriginPoint = OriginPosition;
            return true;
        }
        public bool UpdateTargetPosition()
        {
            TargetPoint = Target.transform.position;
            return true;
        }
        public bool UpdateBeam()
        {
            BeamActual.SetPosition(0, BeamSource.transform.position);
            BeamActual.SetPosition(1, Target.transform.position);
            return true;
        }
        private Color CalculateBeamColor()
        {
            if (IncomingBeams.Count > 1)
            {
                float _Red = 0;
                float _Green = 0;
                float _Blue = 0;

                int BeamCount = 1; //includes the starting beam

                foreach (LightBeam _LB in IncomingBeams)
                {
                    BeamCount += 1;
                    _Red += _LB.CurrentColor.r;
                    _Green += _LB.CurrentColor.g;
                    _Blue += _LB.CurrentColor.b;
                }

                _Red = _Red / BeamCount;
                _Green = _Green / BeamCount;
                _Blue = _Blue / BeamCount;

                return new Color(_Red, _Green, _Blue);
            }
            else
            {
                return new Color(0, 0, 0); //White Beam
            }
        }
        public void Destroy()
        {
            Destroy(BeamActual);
            Destroy(this);
        }

    }
}
**/