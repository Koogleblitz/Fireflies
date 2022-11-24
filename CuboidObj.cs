using System.Diagnostics;
using System;
using System.Numerics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CuboidObj))]
public class CuboidObj : MonoBehaviour
{
    private CuboidObj atom;
    public UnityEngine.Vector3 objVel;
    public float objSpeedLim;
    public float radar;
    UnityEngine.Vector3 origin= UnityEngine.Vector3.zero;
    
    
    void Start()
    {
        objVel= this.transform.forward*objSpeedLim;
        UnityEngine.Debug.Log(objVel);  
        atom= GetComponent<CuboidObj>();    
    }

    void Update()
    {
        var atoms= FindObjectsOfType<CuboidObj>();
        var centroid= origin;
        int sampling= 0;

        foreach(var atom in atoms){
            var selfPos= this.transform.position;
            var atomPos= atom.transform.position;
            var directionVector= atomPos-selfPos;

            var dist= directionVector.magnitude;
            if(dist<radar){
                centroid+= directionVector;
                sampling+= 1;
            }
        }
        centroid= centroid/sampling;

        atom.objVel+= UnityEngine.Vector3.Lerp(origin, centroid, (centroid.magnitude/radar));






        if(objVel.magnitude>objSpeedLim){
            objVel= objVel.normalized*objSpeedLim;
        }
        this.transform.position+= objVel*Time.deltaTime;
        this.transform.rotation= UnityEngine.Quaternion.LookRotation(objVel);
    }
}
