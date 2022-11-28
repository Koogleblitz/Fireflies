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
    public float cohesionWeight;
    public float separationWeight;
    public float alignmentWeight;
    public float originGravity;
    public float boundary;
    UnityEngine.Vector3 origin= UnityEngine.Vector3.zero;
    
    
    void Start()
    {
        //assign initial velocity as the speed limit
        objVel= this.transform.forward*objSpeedLim;
        UnityEngine.Debug.Log(objVel);  
        atom= GetComponent<CuboidObj>();    
    }

    void Update()
    {
        var deltaTime= Time.deltaTime;
        var atoms= FindObjectsOfType<CuboidObj>();
        var centroid= origin;
        var avgVel= origin;
        int sampling= 0;

        foreach(var atom in atoms){
            var selfPos= this.transform.position;
            var atomPos= atom.transform.position;
            var directionVector= atomPos-selfPos;

            var dist= directionVector.magnitude;
            if(dist<radar){
                centroid+= directionVector;
                avgVel+= atom.objVel;
                sampling+= 1;
            }
        }   
        centroid= centroid/sampling;
        avgVel= avgVel/sampling;
        var displacement= (origin - this.transform.position).magnitude;
        var position= this.transform.position;
        var direction= position.normalized;

        objVel+= (UnityEngine.Vector3.Lerp(origin, centroid, (centroid.magnitude/radar)))*cohesionWeight;
        objVel-= (UnityEngine.Vector3.Lerp(origin, centroid, (radar/centroid.magnitude)))*separationWeight;
        objVel+= (UnityEngine.Vector3.Lerp(this.objVel, avgVel, deltaTime))*alignmentWeight;
        

        if(displacement > boundary){
            atom.objVel-= (direction* displacement) * originGravity;
        }
        if(objVel.magnitude>objSpeedLim){
            objVel= objVel.normalized*objSpeedLim;
        }


        this.transform.position+= objVel*deltaTime;
        this.transform.rotation= UnityEngine.Quaternion.LookRotation(objVel);
    }
}
