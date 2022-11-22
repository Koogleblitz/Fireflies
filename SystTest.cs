using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;



public partial class SystTest : SystemBase
{
    
    uint cnt= 1;
    public uint clusterosity= 100;
    public int randFactor= 300;
    
    protected override void OnUpdate()
    {
        float deltaTime= SystemAPI.Time.DeltaTime;
        Unity.Mathematics.Random rand= new Unity.Mathematics.Random(cnt/clusterosity + 1);
        float3 sigmaPos= new float3(0,0,0);
        //float3 randPos= float3.zero;
        EntityQuery atoms= EntityManager.CreateEntityQuery(typeof(AtomFields));
        int atomCount= atoms.CalculateEntityCount();


        foreach ((TransformAspect transpect, RefRW<AtomFields> atom) in SystemAPI.Query<TransformAspect, RefRW<AtomFields>>()){
            float3 atomPos = transpect.Position;
            sigmaPos+=atomPos;
            
        }

        

        foreach ((TransformAspect transpect, RefRW<AtomFields> atom) in SystemAPI.Query<TransformAspect, RefRW<AtomFields>>())
        {
            //[+]-- Get position and stuff -----//
            float speed= atom.ValueRW.speed;
            float3 selfPos = transpect.Position;
            float displacement= math.distance(selfPos, float3.zero);
            float3 centroid= (sigmaPos/atomCount);
            float3 randRange= new float3(1,1,1)*randFactor;
            float radius=  (math.distance(centroid, selfPos)) + 0.1f;
            float3 randPos= rand.NextFloat3(selfPos-randRange, selfPos+randRange);



            float3 targetPos = (math.normalize(centroid)*radius) + randPos  ;
            targetPos+= math.normalize(-targetPos)*displacement;
            


            Debug.Log(centroid);


            

     
            
            // Get direction and step size
            float3 distVector= (targetPos- selfPos);
            float3 dir = math.normalize(distVector);
            float3 deltaPos= dir * speed * deltaTime;
           
   
  
            //Move one step, adjust orientation to the direction of of step
            transpect.Position += deltaPos;

           
            // if(math.distance(selfPos, targetPos)>5){
            //     transpect.LookAt(transpect.Position+distVector );
            // }
            transpect.LookAt(selfPos+distVector );
        }



        cnt= (cnt<=1000)? cnt+1 : 1;

    }
}
