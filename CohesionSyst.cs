using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;



public partial class CohesionSyst : SystemBase
{
    
    uint cnt= 1;

    
    protected override void OnUpdate()
    {
        float deltaTime= SystemAPI.Time.DeltaTime;
        float3 sigmaPos= float3.zero;
        float3 sigmaVel= float3.zero;
        EntityQuery atoms= EntityManager.CreateEntityQuery(typeof(AtomFields));
        int atomCount= atoms.CalculateEntityCount();
        int radarCount= 0;
        int neighborhood= 20;


        foreach ((TransformAspect transpect, RefRW<AtomFields> atom) in SystemAPI.Query<TransformAspect, RefRW<AtomFields>>()){
            float3 atomPos = transpect.Position;
            float3 atomVel= atom.ValueRW.velocity;
            sigmaPos+=atomPos;
            sigmaVel+=atomVel;
 
        }
        
        

        foreach ((TransformAspect transpect, RefRW<AtomFields> atom) in SystemAPI.Query<TransformAspect, RefRW<AtomFields>>())
        {
            //[+]-- Get position and stuff -----//
            float3 centroid= (sigmaPos/atomCount);
            float3 avgVel= (sigmaVel/atomCount);
            

            float speed= atom.ValueRW.speed;
            float speedLimit= atom.ValueRW.speedLimit;
            float3 selfPos = transpect.Position;
            float centroidGrav= atom.ValueRW.centroidGrav;
            float boundary= atom.ValueRO.boundary;
            float originGrav= atom.ValueRO.originGrav;
            float socialDistance= atom.ValueRW.socialDistance;
            float separationWeight= atom.ValueRW.separationWeight;
            float alignmentWeight= atom.ValueRO.alignmentWeight;
            float3 velocity= atom.ValueRW.velocity;
            float3 targetVel= avgVel-velocity;
            float radar = atom.ValueRW.radar;
            

            float radius=  (math.distance(selfPos, centroid))+ 0.000001f;
            float displacement= math.distance(selfPos, float3.zero);
            float3 direction= math.normalize(selfPos);


            atom.ValueRW.velocity+= centroid * (radius/radar) * centroidGrav;
            atom.ValueRW.velocity-= centroid * (radar/radius) * separationWeight;
            atom.ValueRW.velocity+= targetVel * (deltaTime) * alignmentWeight;

            if(displacement>boundary){
                atom.ValueRW.velocity-= (direction* displacement) * originGrav;
            }
            
            UnityEngine.Debug.Log(displacement);


            if(math.length(atom.ValueRW.velocity)>speedLimit){
                atom.ValueRW.velocity= math.normalize(atom.ValueRW.velocity)* speedLimit;
            }

            transpect.Position+= atom.ValueRW.velocity*deltaTime;
            transpect.LookAt(atom.ValueRW.velocity);

        }



        cnt= (cnt<=1000)? cnt+1 : 1;

    }
}
