using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;



public partial class RandSyst : SystemBase
{
    
    uint cnt= 500;
    public uint clusterosity= 100;
    public int randFactor= 200;
    
    protected override void OnUpdate()
    {
        
        float deltaTime= SystemAPI.Time.DeltaTime;
        Unity.Mathematics.Random rand= new Unity.Mathematics.Random(cnt/clusterosity + 1);
        EntityQuery atoms= EntityManager.CreateEntityQuery(typeof(AtomFields));
        int atomCount= atoms.CalculateEntityCount();

        if(cnt>0){
            foreach ((TransformAspect transpect, RefRW<AtomFields> atom) in SystemAPI.Query<TransformAspect, RefRW<AtomFields>>())
            {
                //[+]-- Get position and stuff -----//
                float speed= atom.ValueRW.speed;
                float randomness= atom.ValueRW.randomness;
                float3 selfPos = transpect.Position;
                float3 randRange= new float3(1,1,1)*randFactor;
                float3 randPos= rand.NextFloat3(selfPos-randRange, selfPos+randRange);
            
                // Get direction and step size
                float3 distVector= (randPos- selfPos);
                float3 dir = math.normalize(distVector);
                float3 deltaPos= dir * speed * deltaTime * randomness;

    
    
                //Move one step, adjust orientation to the direction of of step
                transpect.Position += deltaPos;
                transpect.LookAt(selfPos+distVector );


            }
        }

        cnt= (cnt>0)? cnt-1 : 0;
        UnityEngine.Debug.Log(cnt);

    }
}
