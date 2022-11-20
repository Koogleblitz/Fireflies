using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Mathematics;


public partial class SystTest : SystemBase
{
    //[x]-- Move atom WITHOUT referencing component field
    /*    protected override void OnUpdate()
        {
            foreach (TransformAspect transformAspect in SystemAPI.Query<TransformAspect>())
            {
                transformAspect.Position += new float3(SystemAPI.Time.DeltaTime, 0, 0);
            }
        }*/




    //[x]-- Move atom WITH referencing component field
    /*  protected override void OnUpdate()
      {
          foreach ((TransformAspect transformAspect, RefRO<SpeedTest> speed) in SystemAPI.Query<TransformAspect, RefRO<SpeedTest>>())
          {
              transformAspect.Position += new float3(SystemAPI.Time.DeltaTime*speed.ValueRO.value, 0, 0);
          }
      }*/



    //[x]--  Move atom referincing 2 fields from a multi-field component AtomField with two AddComponents in the same Baker
    //[x]--  Able to manipulate direction and rotation by some basic logic
    int cnt= 0;
    protected override void OnUpdate()
    {
        float3 sigmaPos= new float3(0,0,0);
        EntityQuery atoms= EntityManager.CreateEntityQuery(typeof(AtomFields));
        int atomCount= atoms.CalculateEntityCount();


        foreach ((TransformAspect transpect, RefRW<AtomFields> atom) in SystemAPI.Query<TransformAspect, RefRW<AtomFields>>()){
            
            float3 atomPos = transpect.Position;
            sigmaPos+=atomPos;
        }
        float3 centroid= (sigmaPos/atomCount);



        foreach ((TransformAspect transpect, RefRW<AtomFields> atom) in SystemAPI.Query<TransformAspect, RefRW<AtomFields>>())
        {
            //[+]-- Get position and stuff -----//
            float speed= atom.ValueRW.speed;
            float3 selfPos = transpect.Position;
            float3 dist=  centroid - selfPos;

            float3 targetPos= atom.ValueRW.targetPos;
            //float3 targetPos = centroid + atom.ValueRW.targetPos;

            // if(dist[0]>0 ){
            //     targetPos= centroid;
            // }else{
            //     //targetPos = atom.ValueRW.targetPos;
            //     targetPos = -centroid;
            // }
            
            
            float3 dir = math.normalize(targetPos - selfPos);
            float3 deltaPos= dir * speed * SystemAPI.Time.DeltaTime;

            //Move one step, adjust orientation to the direction of of step
            transpect.Position += deltaPos;
            transpect.LookAt(transpect.Position+deltaPos);
            
        }


        foreach ((TransformAspect transpect, RefRW<QueenFields> queen) in SystemAPI.Query<TransformAspect, RefRW<QueenFields>>())
        {


            float speed= queen.ValueRW.speed;
            float3 targetPos = queen.ValueRW.targetPos;


            float3 selfPos = transpect.Position;
            float3 dir = math.normalize(targetPos - selfPos);

            transpect.Position += dir * speed * SystemAPI.Time.DeltaTime;
            transpect.LookAt(targetPos);
            
        }
        if(cnt<10000){
            cnt= cnt+1;
        }else{
            cnt= 0;
        }

    }
}
