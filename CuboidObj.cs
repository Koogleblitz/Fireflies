using System.Numerics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboidObj : MonoBehaviour
{
    public UnityEngine.Vector3 objVel;
    public float objSpeedLim;
    
    
    void Start()
    {
        objVel= this.transform.forward*objSpeedLim;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(objVel.magnitude>objSpeedLim){
            objVel= objVel.normalized*objSpeedLim;
        }
        
        this.transform.position+= objVel*Time.deltaTime;
        this.transform.rotation= UnityEngine.Quaternion.LookRotation(objVel);
    }
}
