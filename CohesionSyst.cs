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
            sigmaPos+=atomPos;
            // if(cnt<neighborhood){
            //     sigmaPos+=atomPos;
            // }

            
            

            // foreach (TransformAspect transvect in SystemAPI.Query<TransformAspect>()){
            //     float3 atomPos = transvect.Position;
            //     float diff= math.distance(selfPos,atomPos);
            //     Debug.Log(diff);
            //     if(diff<radar){
            //         sigmaPos+=atomPos;
            //         radarCount+=1;
            //     }
            //}
        }
        
        

        foreach ((TransformAspect transpect, RefRW<AtomFields> atom) in SystemAPI.Query<TransformAspect, RefRW<AtomFields>>())
        {
            //[+]-- Get position and stuff -----//
            float3 centroid= (sigmaPos/atomCount);
            float3 avgVel= (sigmaVel/atomCount);

            float speed= atom.ValueRW.speed;
            float speedLimit= atom.ValueRW.speedLimit;
            float3 selfPos = transpect.Position;
            float3 centroidGrav= atom.ValueRW.centroidGrav;
            float socialDistance= atom.ValueRW.socialDistance;
            float3 velocity= atom.ValueRW.velocity;
            float radar = atom.ValueRW.radar;
            

            float radius=  (math.distance(selfPos, centroid));
            float displacement= math.distance(selfPos, float3.zero);


            velocity=  centroid*(radius/radar);

            

            // if (math.abs(math.length(velocity))<=speedLimit){
                // if(radius>=socialDistance){
                //     transpect.Position+= velocity*centroidGrav*deltaTime;
                //     Debug.Log("within limit");
                // }else{
                //     transpect.Position-= velocity*centroidGrav*deltaTime*10;
                //     Debug.Log("beyond limit");
                // }
            // }
            
            if(radius>=socialDistance){
                transpect.Position+= velocity*centroidGrav*deltaTime;
                Debug.Log("within limit");
            }else{
                transpect.Position-= velocity*centroidGrav*deltaTime;
                Debug.Log("beyond limit");
            }
            Debug.Log(radius);



        }



        cnt= (cnt<=1000)? cnt+1 : 1;

    }
}
