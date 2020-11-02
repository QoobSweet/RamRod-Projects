using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamRotation : MonoBehaviour
{
    public GameObject InnerBeam;
    public float StepAmount;


    // Start is called before the first frame update
    void Start()
    {
        if(InnerBeam == null)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, -StepAmount, 0);
        InnerBeam.transform.Rotate(0, StepAmount * 2, 0);
    }
}
